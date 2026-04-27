using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

/// <summary>
/// High-level AI chat helpers for CdpClient.
/// Encapsulates complex multi-step JS operations for AI web interfaces
/// (ChatGPT, Gemini, Claude.ai).
/// </summary>
public sealed partial class CdpClient
{
    public sealed class PromptStreamContext
    {
        public string Provider { get; init; } = "";
        public string? GameId { get; init; }
        public string? QuestionId { get; init; }
        public string? RunId { get; init; }
        public string? TargetId { get; init; }
        public string? PageKey { get; init; }
        public string? EditorSelector { get; init; }
    }

    public sealed class PromptStreamEvent
    {
        public string Provider { get; init; } = "";
        public string? GameId { get; init; }
        public string? QuestionId { get; init; }
        public string? RunId { get; init; }
        public string? TargetId { get; init; }
        public string? PageKey { get; init; }
        public string? EditorSelector { get; init; }
        public string Status { get; init; } = "RUNNING";
        public long ChunkSeq { get; init; }
        public string ChunkText { get; init; } = "";
        public string FullTextSoFar { get; init; } = "";
        public string DeltaText { get; init; } = "";
        public string? OperationContext { get; init; }
        public bool IsFinal { get; init; }
    }

    public sealed class PromptResponseState
    {
        public string Status { get; init; } = "UNKNOWN";
        public int ResponseCount { get; init; }
        public int TextLength { get; init; }
        public string Text { get; init; } = "";
        public bool Streaming { get; init; }
        public bool StopVisible { get; init; }
        public string LastSendResult { get; init; } = "";
        public long LastSendAtMs { get; init; }
        public string SendMethod { get; init; } = "";
        public string SendKeyChord { get; init; } = "";
        public int SendAttempt { get; init; }

        public static PromptResponseState FromJson(string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return new PromptResponseState();
            try
            {
                var node = JsonSerializer.Deserialize<JsonNode>(json);
                return new PromptResponseState
                {
                    Status = node?["status"]?.GetValue<string>() ?? "UNKNOWN",
                    ResponseCount = node?["responseCount"]?.GetValue<int>() ?? 0,
                    TextLength = node?["textLength"]?.GetValue<int>() ?? 0,
                    Text = node?["text"]?.GetValue<string>() ?? "",
                    Streaming = node?["streaming"]?.GetValue<bool>() ?? false,
                    StopVisible = node?["stopVisible"]?.GetValue<bool>() ?? false,
                    LastSendResult = node?["lastSendResult"]?.GetValue<string>() ?? "",
                    LastSendAtMs = node?["lastSendAtMs"]?.GetValue<long>() ?? 0,
                    SendMethod = node?["sendMethod"]?.GetValue<string>() ?? "",
                    SendKeyChord = node?["sendKeyChord"]?.GetValue<string>() ?? "",
                    SendAttempt = node?["sendAttempt"]?.GetValue<int>() ?? 0,
                };
            }
            catch
            {
                return new PromptResponseState();
            }
        }
    }

    public PromptStreamContext? StreamingChunkContext { get; set; }
    public Action<PromptStreamEvent>? OnStreamingChunkEvent { get; set; }

    public void SetStreamingChunkContext(
        string provider,
        string? gameId = null,
        string? questionId = null,
        string? runId = null,
        string? editorSelector = null,
        string? pageKey = null)
    {
        var targetId = string.IsNullOrWhiteSpace(TargetId) ? null : TargetId;
        if (string.IsNullOrWhiteSpace(pageKey))
        {
            var pageSel = string.IsNullOrWhiteSpace(editorSelector) ? "*" : editorSelector;
            var pageTid = string.IsNullOrWhiteSpace(targetId) ? "no-target" : targetId;
            pageKey = $"{provider}::{pageTid}::{pageSel}";
        }

        StreamingChunkContext = new PromptStreamContext
        {
            Provider = provider,
            GameId = gameId,
            QuestionId = questionId,
            RunId = runId,
            TargetId = targetId,
            PageKey = pageKey,
            EditorSelector = editorSelector,
        };
    }

    // ─ Editor Operations ─

    /// <summary>
    /// Insert text into a contenteditable element (3-tier: innerHTML ??execCommand ??Input.insertText).
    /// Returns true if text was inserted successfully.
    /// </summary>
    public async Task<bool> InsertContentEditableAsync(string selector, string text)
    {
        var jsStr = JsonSerializer.Serialize(text);

        var esc = Esc(selector);

        // Tier 1: Focusless innerHTML/textContent (works even when iconic)
        var result = await EvalAsync(
            "(()=>{var el=document.querySelector('" + esc + "');if(!el)return 'NOT_FOUND';" +
            "var t=" + jsStr + ";var p=el.querySelector('p');" +
            "if(p)p.textContent=t;else el.innerHTML='<p>'+t.replace(/</g,'&lt;')+'</p>';" +
            "el.dispatchEvent(new InputEvent('input',{bubbles:true,inputType:'insertText',data:t}));" +
            "return el.textContent.length>0?'OK':'EMPTY'})()");
        if (result == "OK") return true;

        // Tier 1 failed ??restore Chrome if iconic (Claude ProseMirror needs rendering)
        var chromeHwnd = GetChromeWindowHandle();
        if (chromeHwnd != IntPtr.Zero && IsIconic(chromeHwnd))
        {
            ShowWindowNative(chromeHwnd, 4); // SW_SHOWNOACTIVATE
            Thread.Sleep(100);
            try { await EmulateActiveTabAsync(); } catch { }
        }

        // Tier 2: execCommand (respects selection model)
        result = await EvalAsync(
            "(()=>{var el=document.querySelector('" + esc + "');if(!el)return 'NOT_FOUND';" +
            "el.focus();var sel=window.getSelection();var r=document.createRange();" +
            "r.selectNodeContents(el);r.collapse(false);sel.removeAllRanges();sel.addRange(r);" +
            "document.execCommand('insertText',false," + jsStr + ");" +
            "return el.textContent.length>0?'OK':'EMPTY'})()");
        if (result == "OK") return true;

        // Tier 3: Input.insertText (CDP protocol level)
        await FocusAsync(selector);
        await Task.Delay(50);
        await SendAsync("Input.insertText", new System.Text.Json.Nodes.JsonObject { ["text"] = text });
        var len = await GetTextLengthAsync(selector);
        return len > 0;
    }

    /// <summary>
    /// Append text to an editor without wiping prior content.
    /// Supports textarea/input as well as contenteditable editors.
    /// </summary>
    public async Task<bool> AppendContentEditableAsync(string selector, string text, string separator = "\n")
    {
        TickBotOverlay(); // pulse on each CDP append
        var jsText = JsonSerializer.Serialize(text);
        var jsSep = JsonSerializer.Serialize(separator);
        var esc = Esc(selector);

        var result = await EvalAsync(
            "(()=>{var el=document.querySelector('" + esc + "');if(!el)return 'NOT_FOUND';" +
            "var t=" + jsText + ";var sep=" + jsSep + ";" +
            "var isInput=(el.tagName==='TEXTAREA'||el.tagName==='INPUT');" +
            "var cur=isInput?(el.value||''):((el.innerText||el.textContent||''));" +
            "var suffix=(cur&&cur.length>0)?(sep+t):t;" +
            "if(isInput){" +
                "el.value=(el.value||'')+suffix;" +
                "el.dispatchEvent(new Event('input',{bubbles:true}));" +
                "el.dispatchEvent(new Event('change',{bubbles:true}));" +
                "return el.value.length>0?'OK':'EMPTY';" +
            "}" +
            "var p=el.querySelector('p:last-child');" +
            "if(p){p.textContent=(p.textContent||'')+suffix;}" +
            "else if(el.isContentEditable||el.getAttribute('contenteditable')==='true'){el.textContent=(el.textContent||'')+suffix;}" +
            "else return 'UNSUPPORTED';" +
            "el.dispatchEvent(new InputEvent('input',{bubbles:true,inputType:'insertText',data:suffix}));" +
            "el.dispatchEvent(new Event('change',{bubbles:true}));" +
            "return (el.innerText||el.textContent||'').length>0?'OK':'EMPTY';})()");
        if (result == "OK") return true;

        // Fallback: read existing text then rewrite as combined content.
        var existing = await GetEditorContentAsync(selector);
        var combined = string.IsNullOrWhiteSpace(existing) ? text : existing + separator + text;
        return await InsertContentEditableAsync(selector, combined);
    }

    /// <summary>
    /// Arm a page-local singleton prompt pump.
    /// The page keeps one timer per editor selector and tries to send after idleMs of silence.
    /// </summary>
    public async Task<bool> ArmPromptPumpAsync(string selector, int idleMs = 1000)
    {
        var esc = Esc(selector);
        var jsIdleMs = JsonSerializer.Serialize(idleMs);
        var armJs = "(()=>{" +
            "var sel='" + esc + "';" +
            "var idleMs=" + jsIdleMs + ";" +
            "var root=(window.__wkAskPump||(window.__wkAskPump={}));" +
            "if(!root.states)root.states={};" +
            "if(!root.trySend)root.trySend=function(selector){" +
                // BUG-2: isSending guard prevents double-send during CDP round-trip delay
                // Auto-expire after 5s so a crash/timeout doesn't permanently deadlock the pump
                "var st=root.states[selector];" +
                "if(st&&st.isSending&&(Date.now()-st.sendTimestamp)>5000){st.isSending=false;}" + // 5s safety timeout
                "if(st&&(st.locked||st.isSending))return st.isSending?'SENDING':'LOCKED';" +
                "var el=document.querySelector(selector);if(!el)return 'NO_EDITOR';" +
                "var txt=((el.value||el.innerText||el.textContent||'')).trim();if(!txt)return 'EMPTY';" +
                // BUG-1: ProseMirror empty state = only <br class="ProseMirror-trailingBreak"> in single <p>
                // IMPORTANT: check the childNode IS a br.ProseMirror-trailingBreak, not just any node (text node = real content)
                "var pmBr=el.querySelector('br.ProseMirror-trailingBreak');" +
                "if(pmBr){var pmPs=el.querySelectorAll('p');if(pmPs.length===1){var cn=pmPs[0].childNodes;if(cn.length===0||(cn.length===1&&cn[0].nodeName==='BR'))return 'EMPTY';}}" +
                "try{el.focus();}catch(_e){}" +
                "var form=el.closest('form');if(form&&typeof form.requestSubmit==='function'){" +
                    "try{form.requestSubmit();return 'FORM_SUBMIT';}catch(_e){}" +
                "}" +
                $"var send=document.querySelector(\"button[data-testid=''send-button''],button[aria-label*=''Send''],button[aria-label*=''send''],button[aria-label*=''{AskLocaleKeys.KoSend}''],button.send-button,button[type=''submit'']\");" +
                "if(send&&!send.disabled&&send.getAttribute('aria-disabled')!=='true'){" +
                    "try{send.click();return 'BTN_CLICK';}catch(_e){}" +
                "}" +
                "try{" +
                    "var kd=new KeyboardEvent('keydown',{key:'Enter',code:'Enter',keyCode:13,which:13,bubbles:true});" +
                    "var ku=new KeyboardEvent('keyup',{key:'Enter',code:'Enter',keyCode:13,which:13,bubbles:true});" +
                    "el.dispatchEvent(kd);el.dispatchEvent(ku);return 'KEY_ENTER';" +
                "}catch(_e){}" +
                // ProseMirror and similar editors ignore JS KeyboardEvent -- signal C# to fire CDP Enter
                // BUG-2: set isSending=true to block re-entry until C# confirms send (CheckPumpReadyAsync clears it)
                "st.isSending=true;st.sendTimestamp=Date.now();st.readyToSend=true;return 'CDP_SIGNAL';" +
            "};" +
            "var st=root.states[sel]||(root.states[sel]={interval:0,lastResult:'',locked:false,ticks:0,lastHash:null,isSending:false,sendTimestamp:0,readyToSend:false});" +
            // BUG-3: Always reset stability counters on re-arm so stale ticks don't cause premature trySend
            // Use null (not 0) as sentinel so hash 0 isn't accidentally treated as stable
            "st.ticks=0;st.lastHash=null;" +
            // Simple hash function for content change detection
            "if(!root.hash)root.hash=function(s){var h=0;for(var i=0;i<s.length;i++)h=((h<<5)-h+s.charCodeAt(i))|0;return h;};" +
            // Start periodic tick if not already running (single setInterval per editor)
            "if(!st.interval){st.interval=setInterval(function(){" +
                "var cur=root.states[sel];if(!cur||cur.locked||cur.isSending)return;" +
                "var el=document.querySelector(sel);if(!el)return;" +
                "var txt=((el.value||el.innerText||el.textContent||'')).trim();" +
                "if(!txt){cur.ticks=0;cur.lastHash=null;return;}" + // empty -> reset
                "var h=root.hash(txt);" +
                "if(h!==cur.lastHash){cur.ticks=0;cur.lastHash=h;" +
                    "console.debug('[PUMP] content changed len='+txt.length+' sel='+sel);" +
                    "return;}" + // content changed -> reset ticks
                "cur.ticks++;" + // content stable -> count up
                "console.debug('[PUMP] stable tick='+cur.ticks+' len='+txt.length+' sel='+sel);" +
                "if(cur.ticks>=2){" + // 2 ticks × 500ms = 1s of STABLE content
                    "cur.lastResult=root.trySend(sel);" +
                    "console.debug('[PUMP] trySend result='+cur.lastResult+' sel='+sel);" +
                    "cur.ticks=0;cur.lastHash=null;" +
                "}" +
            "}, 500);}" + // 500ms tick
            "return 'ARMED';" +
            "})()";
        var result = await EvalAsync(armJs, timeoutMs: 20000); // heavy JS: allow 20s -- Chrome may be throttled during streaming
        if (result == "ARMED")
        {
            TrackPromptPump(selector, idleMs); // enable self-heal re-arm on Chrome restart
            return true;
        }
        return false;
    }

    public async Task<bool> MarkPromptDispatchAsync(string selector, string provider, string sendResult)
    {
        var esc = Esc(selector);
        var jsProvider = JsonSerializer.Serialize(provider);
        var jsSendResult = JsonSerializer.Serialize(sendResult);
        var result = await EvalAsync(
            "(()=>{" +
            "var sel='" + esc + "';" +
            "var provider=" + jsProvider + ";" +
            "var sendResult=" + jsSendResult + ";" +
            "var root=(window.__wkAskPump||(window.__wkAskPump={}));" +
            "if(!root.states)root.states={};" +
            "var st=root.states[sel]||(root.states[sel]={gen:0,timer:0,lastResult:'',locked:false});" +
            "var sendParts=(sendResult||'').split(':');" +
            "var sendMethod=(sendParts.length>1?sendParts[1]:sendParts[0]||'');" +
            "var sendKeyChord=/CTRL\\+ENTER/i.test(sendMethod)?'Ctrl+Enter':(/SHIFT\\+ENTER/i.test(sendMethod)?'Shift+Enter':(/ENTER/i.test(sendMethod)?'Enter':''));" +
            "st.sendAttempt=((st.sendAttempt||0)+1);" +
            "st.provider=provider;" +
            "st.lastSendAt=Date.now();" +
            "st.lastSendResult=sendResult;" +
            "st.sendMethod=sendMethod;" +
            "st.sendKeyChord=sendKeyChord;" +
            "st.responseStatus='QUEUED';" +
            "st.lastProbeAt=0;" +
            "return 'OK';" +
            "})()");
        return result == "OK";
    }

    public async Task<PromptResponseState> ProbePromptResponseStateAsync(string selector, string provider, int baseResponseCount = 0, int previewChars = 3000)
    {
        var esc = Esc(selector);
        var jsProvider = JsonSerializer.Serialize(provider);
        var jsBase = JsonSerializer.Serialize(baseResponseCount);
        var jsPreview = JsonSerializer.Serialize(previewChars);
        var result = await EvalAsync(
            "(()=>{" +
            "var sel='" + esc + "';" +
            "var provider=" + jsProvider + ";" +
            "var baseCount=" + jsBase + ";" +
            "var previewChars=" + jsPreview + ";" +
            "var root=(window.__wkAskPump||(window.__wkAskPump={}));" +
            "if(!root.states)root.states={};" +
            "var st=root.states[sel]||(root.states[sel]={gen:0,timer:0,lastResult:'',locked:false});" +
            "var el=document.querySelector(sel);" +
            "if(!el)return JSON.stringify({status:'NO_EDITOR',responseCount:0,textLength:0,text:'',streaming:false,stopVisible:false,lastSendResult:(st.lastSendResult||''),lastSendAtMs:(st.lastSendAt||0),sendMethod:(st.sendMethod||''),sendKeyChord:(st.sendKeyChord||''),sendAttempt:(st.sendAttempt||0)});" +
            "function qsa(s){try{return document.querySelectorAll(s);}catch(_e){return [];}}" +
            "function lastText(nodes){if(!nodes||!nodes.length)return '';var n=nodes[nodes.length-1];return ((n.innerText||n.textContent||'').trim());}" +
            "var responseCount=0,text='',streaming=false,stopVisible=false;" +
            "if(provider==='chatgpt'||provider==='gpt'){" +
                "var msgs=qsa('[data-message-author-role=\"assistant\"]');" +
                "responseCount=msgs.length;" +
                "if(!responseCount){var turns=qsa('[data-testid*=\"conversation-turn\"]');var articles=qsa('article');responseCount=turns.length?Math.floor(turns.length/2):(articles.length||0);}" +
                "text=lastText(msgs);" +
                "if(!text){var arts=qsa('article');text=lastText(arts);}" +
                "stopVisible=!!(document.querySelector(\"button[data-testid='stop-button']\")||document.querySelector(\"button[aria-label='Stop streaming']\"));" +
                "streaming=stopVisible;" +
            "} else if(provider==='claude'){" +
                "var streamMsgs=qsa('[data-is-streaming]');" +
                "var asstMsgs=qsa('[data-testid=\"assistant-message\"]');" +
                "responseCount=Math.max(streamMsgs.length, asstMsgs.length);" +
                "text=streamMsgs.length?lastText(streamMsgs):lastText(asstMsgs);" +
                "var last=streamMsgs.length?streamMsgs[streamMsgs.length-1]:null;" +
                "streaming=!!(last&&last.getAttribute('data-is-streaming')==='true');" +
                "stopVisible=streaming||!!document.querySelector(\"button[aria-label*='Stop']\");" +
            "} else if(provider==='gemini'){" +
                "var models=qsa('model-response');" +
                "var arts=qsa('[role=\"article\"]');" +
                "responseCount=models.length||arts.length||0;" +
                "text=models.length?lastText(models):lastText(arts);" +
                "var stopBtn=document.querySelector(\"button[aria-label*='Stop']\");" +
                "var stopIcon=document.querySelector('mat-icon[fonticon=\"stop_circle\"]');" +
                "stopVisible=!!stopBtn||!!stopIcon;" +
                "streaming=stopVisible;" +
            "}" +
            "var editorText=((el.value||el.innerText||el.textContent||'').trim());" +
            "var status='WAITING';" +
            "if(st.locked) status='LOCKED';" +
            "else if(responseCount>baseCount && (streaming||stopVisible)) status='RUNNING';" +
            "else if(responseCount>baseCount && text) status='DONE';" +
            "else if((st.lastSendAt||0)>0 && ((Date.now()-st.lastSendAt)<15000) && (st.lastSendResult||'') && editorText.length===0) status='QUEUED';" +
            "st.provider=provider;" +
            "st.responseStatus=status;" +
            "st.lastProbeAt=Date.now();" +
            "st.responseCount=responseCount;" +
            "st.textLength=text.length;" +
            "st.streaming=!!streaming;" +
            "st.stopVisible=!!stopVisible;" +
            "return JSON.stringify({status:status,responseCount:responseCount,textLength:text.length,text:text.substring(0,previewChars),streaming:!!streaming,stopVisible:!!stopVisible,lastSendResult:(st.lastSendResult||''),lastSendAtMs:(st.lastSendAt||0),sendMethod:(st.sendMethod||''),sendKeyChord:(st.sendKeyChord||''),sendAttempt:(st.sendAttempt||0)});" +
            "})()");
        return PromptResponseState.FromJson(result);
    }

    public async Task<bool> SetPromptPumpLockAsync(string selector, bool locked)
    {
        var esc = Esc(selector);
        var jsLocked = JsonSerializer.Serialize(locked);
        var result = await EvalAsync(
            "(()=>{" +
            "var sel='" + esc + "';" +
            "var locked=" + jsLocked + ";" +
            "var root=(window.__wkAskPump||(window.__wkAskPump={}));" +
            "if(!root.states)root.states={};" +
            "var st=root.states[sel]||(root.states[sel]={gen:0,timer:0,lastResult:'',locked:false});" +
            "st.locked=locked;" +
            "if(locked&&st.timer)try{clearTimeout(st.timer);}catch(_e){}" +
            "return 'OK';" +
            "})()");
        return result == "OK";
    }
}
