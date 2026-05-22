// yt-ad-skip.js -- YouTube Ad Skipper (auto-loaded via bookmarklet)
// Hosted: https://cdn.jsdelivr.net/gh/kiexp/wkappbot-sdk@main/scripts/yt-ad-skip.js
(function(){
var D='ytLastT_';
function gc(k){var m=document.cookie.match('(?:^|; )'+k+'=([^;]*)');return m?m[1]:null}
function sc(k,v){document.cookie=k+'='+v+';path=/;samesite=lax'}
function badge(txt,c){
  var b=document.getElementById('_adskip_b');
  if(!b){b=document.createElement('div');b.id='_adskip_b';b.style='position:fixed;bottom:8px;right:8px;font:bold 11px sans-serif;padding:3px 8px;border-radius:4px;z-index:999999;opacity:.8';document.body.appendChild(b);}
  b.style.background=c||'#f00';b.style.color='#fff';b.textContent=txt;
}
function arm(){
  var p=document.querySelector('#movie_player');
  var vid=new URLSearchParams(location.search).get('v');
  if(!p||!vid)return;
  var KEY=D+vid;
  if(window._tT)clearInterval(window._tT);
  window._reloading=false;
  window._tT=setInterval(function(){
    var ad=document.documentElement.classList.contains('ad-showing');
    var ov=!!document.querySelector('.ytp-ad-player-overlay-layout');
    if(!ad&&!ov&&p.getPlayerState&&p.getPlayerState()===1){
      var t=p.getCurrentTime();
      if(t>1){sessionStorage.setItem(KEY,t.toFixed(1));sc(KEY,t.toFixed(1));}
    }
    if((ad||ov)&&!window._reloading){
      window._reloading=true;
      var last=parseFloat(sessionStorage.getItem(KEY)||gc(KEY)||'1');
      badge('SKIP '+last.toFixed(0)+'s→','#e00');
      setTimeout(function(){location.replace('/watch?v='+vid+'&t='+(last+0.1).toFixed(1));},150);
    }
  },1000);
  badge('ON '+vid.slice(-4),'#080');
}
if(!window._adSkipNav){
  window._adSkipNav=true;
  document.addEventListener('yt-navigate-finish',function(){setTimeout(arm,800);});
}
arm();
console.log('[yt-ad-skip] v4 armed');
})();