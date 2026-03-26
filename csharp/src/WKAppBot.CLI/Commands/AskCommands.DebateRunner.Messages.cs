namespace WKAppBot.CLI;

/// <summary>
/// All moderator message templates for the 정반합 debate system.
/// Separated from game flow logic for maintainability.
/// Every message sent to AI (DM/broadcast) or Slack is defined here.
/// </summary>
internal partial class Program
{
    static class DebateMsg
    {
        // ── Game Start ──────────────────────────────────────────────────────
        public const string R2R3Start = "═══ *Moderator: R2(Critique) → R3(Synthesis)* ═══\nR1 already completed. Proceeding to cross-critique.";

        /// <summary>Round-scoped rules: only inject relevant rules per round (token optimization)</summary>
        public static string GetRulesForRound(string round) => round switch
        {
            "R2" => "[MODERATOR] R2 RULES:\n" +
                "1. [DISPUTE]{target,reason}[/DISPUTE] — challenge peers (MANDATORY)\n" +
                "2. [CLAIM]{claim,confidence}[/CLAIM] — 2-5 claims\n" +
                "3. [STANCE N=? R=? C=? E=? D=?] (sum=9, D≥1)\n" +
                "4. 답변 99단어 이하. [합의]/[CONCLUSION_KR] 사용 금지 (R3 only)!",
            "R3" => "[MODERATOR] R3 RULES:\n" +
                "1. [CONCLUSION_KR] with [합의]/[미합의]/[셀프힐링]/[개인의견]\n" +
                "2. [합의] ATOMIC items with score (0-9). <7 = [미합의]\n" +
                "3. [셀프힐링]: admit what you revised from prior rounds\n" +
                "4. [STANCE N=? R=? C=? E=? D=?] (sum=9)",
            _ => GameRules // full rules for R0/other
        };

        public static string GameRules => """
            [MODERATOR] 🎮 정반합 게임 시작!
            RULES:
            1. Use [CLAIM]{"claim":"...","confidence":0.9,"key_assumptions":["..."]}[/CLAIM] for each point
            2. Use [DISPUTE]{"target_assumption":"...","reason":"..."}[/DISPUTE] to challenge peers
            3. Include [STANCE N=? R=? C=? E=? D=?] (sum=9) to show your position
            4. Korean conclusion: [CONCLUSION_KR] with [합의]/[미합의]/[개인의견]
            5. [합의] items must be ATOMIC (one proposition per line) with score (0-9). [개인의견] must be ≥20 words.
            6. ⚠️ CRITICAL: If another AI puts something in [합의] that you disagree with, you MUST list it in YOUR [미합의]. Do NOT silently accept.
            7. Read-only tools available (file read, grep, web search). No writes.
            8. [셀프힐링] REQUIRED in R3: honestly admit what you got wrong or revised from prior rounds.
            9. ⚠️ WORD LIMIT: 답변 1회당 99단어 이하 (백단어). 초과 시 답변 거부 + 재제출. Atomic items, not essays.
            10. 🔧 도구 사용법:
               검색: [APPBOT_TOOL_CALL_BEGIN]
               wkappbot file grep 'keyword' --path W:/GitHub/WKAppBot --type cs
               [APPBOT_TOOL_CALL_END]
               수정: [APPBOT_TOOL_CALL_BEGIN]
               wkappbot file edit "old text" "new text" W:/GitHub/WKAppBot/path/to/file.cs
               [APPBOT_TOOL_CALL_END]
               Claude Code Edit보다 강력! C-style escape(\n\t) + 4-stage 인코딩감지 + indent-block context.
               사회자가 실행 후 결과를 알려드립니다.
            11. 🔢 GAME ID: This game is [G:{threadTs}]. Include game ID in responses.
               게임ID 불일치 시 사회자가 경고합니다 (참여 확인용).
            12. ⚠️ ROUND SCOPE: Follow ONLY the current round's rules. 다른 라운드 룰 사용시 답변 인정 안함! (REJECTED + forced retry)
               R2 = critique only ([DISPUTE]+[CLAIM]+[STANCE]). [합의]/[CONCLUSION_KR] 사용 금지!
               R3 = synthesis only ([CONCLUSION_KR]+[합의]/[미합의]/[셀프힐링]/[개인의견]). [DISPUTE] 필수 아님.
            Respond to the moderator's prompts. Be intellectually honest.
            """;

        public const string R0Complete = "═══ *R0 자유 답변 완료! 정반합 게임 시작!* ═══\n📋 DEBATE_JSON + STANCE 포맷으로 응답해주세요.";
        public const string NoR1Context = "❌ No R1 context available for R2/R3.";
        public const string R3Header = "═══ *R3: Consensus Loop* ═══";
        public const string CrossPromptStart = "🔄 *Real-time cross-prompting started!";
        public const string DebateComplete = "═══ *정반합 토론 완료* ═══";
        public const string NoConsensusFound = "⚠️ *[Moderator]* No [합의] content found! AIs must state consensus.";

        // ── DM Templates ────────────────────────────────────────────────────
        public static string DmD0NoDispute(string ai) =>
            $"[MODERATOR DM] Your STANCE has D=0 (no dissent) and you filed no [DISPUTE] blocks. " +
            "This is the CRITIQUE round — your job is to find weaknesses in peer arguments. " +
            "Please add at least one [DISPUTE]{\"target_assumption\":\"...\",\"reason\":\"...\"}[/DISPUTE] " +
            "and set D >= 1 in your STANCE. Revise now.";

        public static string DmNudgeFinished(string doneAi, string roundName) =>
            $"[MODERATOR]: ⏰ {AiDisplayName(doneAi)} has finished {roundName}. You're still writing — please wrap up promptly. Other participants are waiting.";

        public static string DmR0Nudge(string doneAi) =>
            $"[MODERATOR]: {AiDisplayName(doneAi)} has finished. Please wrap up your answer promptly.";

        public const string DmR0FollowUp =
            "[MODERATOR]: Once all answers are in, we begin Round 1 of 정반합 debate. Prepare your [DEBATE_JSON] with STANCE points.";

        // ── Format Violation ────────────────────────────────────────────────
        public static string FormatRetryR2(string retryReason) =>
            $"[MODERATOR] ⚠️ Your response is missing required format: {retryReason}.\n" +
            "You MUST include:\n• At least 2 [CLAIM] blocks\n• At least 1 [DISPUTE] block\n• [STANCE N=? R=? C=? E=? D=?] with D >= 1\nPlease revise your answer now.";

        public static string FormatRetryR3(string retryReason) =>
            $"[MODERATOR] ⚠️ Your response is missing required format: {retryReason}.\n" +
            "You MUST include:\n• At least 2 [CLAIM] blocks\n• [CONCLUSION_KR] with [합의]/[미합의]/[셀프힐링]/[개인의견]\n" +
            "• [합의] must be at least 100 words IN KOREAN (한국어 100단어 이상!)\n" +
            "• [셀프힐링]: 이전 라운드에서 수정/인정한 내용 (없으면 \"수정 없음\")\n" +
            "• [STANCE N=? R=? C=? E=? D=?]\n한국어로 충분히 상세하게 다시 응답해주세요.";

        // ── Slack Notifications ─────────────────────────────────────────────
        public static string SlackRoundPrompt(string roundName, string prompt) =>
            $"🎙️ *[Moderator → {roundName}]*: {prompt}";

        public static string SlackRulesInjected(string rules) =>
            $"📋 *Debate Rules injected to all AIs:*\n```\n{rules}\n```";

        public static string SlackFormatViolation(string ai, string reason) =>
            $"🔄 *[Moderator→{ai}]* Format violation: {reason}. Requesting revision.";

        public static string SlackD0Warning(string ai) =>
            $"⚠️ *[Moderator→{ai}]* D=0 + no [DISPUTE] in critique round! You MUST challenge at least one peer assumption. Revise your STANCE.";

        public static string SlackAiFinished(string doneAi, string roundName, int remaining) =>
            $"⏰ *{AiDisplayName(doneAi)}* finished {roundName}! Moderator: wrap up, {remaining} AI(s) remaining.";

        public static string SlackAiNoResponse(string doneAi, string roundName, int remaining) =>
            $"⚠️ *{AiDisplayName(doneAi)}* returned no response for {roundName}. {remaining} AI(s) remaining.";

        public static string SlackR3Attempt(int attempt) =>
            $"🔄 *R3-{attempt}* Consensus attempt";

        public static string SlackCascading(string ai, int priorCount) =>
            $"🔗 *{AiDisplayName(ai)}* — 이전 {priorCount}개 합의 항목 포함하여 작성 중...";

        public static string SlackConvergence(string roundName, double conv) =>
            $"✅ *{roundName} Convergence* (Jaccard={conv:F2})";

        public static string SlackFullConsensus(int attempt) =>
            $"✅ *Full consensus!* (R3-{attempt})";

        public static string SlackUnresolvedRemaining(int count) =>
            $"⚠️ *{count} 미합의 남음* → 다음 라운드";

        public static string SlackHaBrief(string ai, int words) =>
            $"⚠️ *[Moderator→{ai}]* [합의] too brief ({words} words, need ≥100). 한국어 100단어 이상으로 다시!";

        public static string SlackOpinionBrief(string ai, int chars) =>
            $"⚠️ *[Moderator→{ai}]* Personal opinion too brief ({chars} chars). Express yourself fully!";

        public static string SlackDm(string targetAi, string content) =>
            $"📩 *[DM→{targetAi}]* {content}";

        public static string SlackBroadcast(string content) =>
            $"📢 *[Moderator→ALL]* {content}";

        public static string SlackStallNudge(string ai, double seconds, int nudgeCount) =>
            $"⏰ *[Moderator→{ai}]* {seconds:F0}초 무응답 — 재촉 #{nudgeCount}";

        public static string SlackConvergenceCheck(int aiCount, double conv) =>
            $"📊 *[Convergence]* {conv:F2} ({aiCount} AIs)";

        public static string SlackConvergenceReached(double conv) =>
            $"✅ *Convergence reached!* Jaccard={conv:F2} — debate complete";

        // ── Stall Nudge (CDP inject) ────────────────────────────────────────
        public static string StallNotStarted => "[MODERATOR] ⏰ You haven't started responding. Please begin your answer now.";
        public static string StallInProgress => "[MODERATOR] ⏰ Your response appears stalled. Please continue or wrap up.";

        // ── Cross-check ─────────────────────────────────────────────────────
        public static string CrossCheckDmHeader(string targetAi) =>
            $"[MODERATOR DM → {targetAi} only] 아래 항목들이 당신의 [합의]에 빠져있습니다:";

        public const string CrossCheckDmFooter =
            "각 항목에 대해: 동의하면 [합의]에 추가, 반대하면 [미합의]에 이유와 함께 넣어주세요.";

        public const string CrossCheckBroadcastHeader =
            "[MODERATOR] 합의 항목 교차검증 결과입니다. 모든 AI에 공개합니다.";

        public const string CrossCheckBroadcastFooter =
            "\n각 AI는 미확인 항목에 대해 동의/거부를 밝혀주세요.\n전체 [합의]/[미합의]/[개인의견] 양식을 다시 출력해주세요.";
    }
}
