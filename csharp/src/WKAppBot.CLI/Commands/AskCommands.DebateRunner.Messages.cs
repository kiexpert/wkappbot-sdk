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
            "R2" => "⚔️ R2=CRITIQUE. [DISPUTE]+[CLAIM]+[STANCE D≥1]. ≤99w. No [합의]!",
            "R3" => "🤝 R3=SYNTHESIS. [CONCLUSION_KR] with [합의]/[미합의]/[셀프힐링]/[개인의견]. [STANCE].",
            _ => GameRules
        };

        public static string GameRules => """
            🎮 정반합! Answer the QUESTION. Stay on-topic or get benched.

            FORMAT (every response — start with [G:게임번호]):
            • [CLAIM]{claim,confidence,key_assumptions}[/CLAIM] — 2-5 per round
            • [DISPUTE]{target_assumption,reason}[/DISPUTE] — challenge peers (R2 mandatory)
            • [STANCE N=? R=? C=? E=? D=?] sum=9

            ROUND SCOPE — violate = REJECTED:
            • R2: critique only. [DISPUTE]+[CLAIM]+[STANCE]. No [합의]!
            • R3: synthesis. [CONCLUSION_KR] with [합의]/[미합의]/[셀프힐링]/[개인의견]
              [합의] = atomic items + score(0-9). Disagree? → [미합의].

            LIMITS: ≤99 words/response. English claims, Korean [CONCLUSION_KR].
            """;

        public const string R0Complete = "═══ *R0 자유 답변 완료! 정반합 게임 시작!* ═══\n📋 DEBATE_JSON + STANCE 포맷으로 응답해주세요.";
        public const string NoR1Context = "❌ No R1 context available for R2/R3.";
        public const string R3Header = "═══ *R3: Consensus Loop* ═══";
        public const string CrossPromptStart = "🔄 *Real-time cross-prompting started!";
        public const string DebateComplete = "═══ *정반합 토론 완료* ═══";
        public const string NoConsensusFound = "⚠️ *[Moderator]* No [합의] content found! AIs must state consensus.";

        // ── DM Templates ────────────────────────────────────────────────────
        public static string DmD0NoDispute(string ai) =>
            "[MODERATOR] D=0 + no [DISPUTE]? This is CRITIQUE round — challenge at least one peer. Add [DISPUTE] + set D≥1. Revise now.";

        public static string DmNudgeFinished(string doneAi, string roundName) =>
            $"[MODERATOR] ⏰ {AiDisplayName(doneAi)} done with {roundName}. Wrap up!";

        public static string DmR0Nudge(string doneAi) =>
            $"[MODERATOR] {AiDisplayName(doneAi)} done. Wrap up!";

        public const string DmR0FollowUp =
            "[MODERATOR] All in → 정반합 starts. Prepare [DEBATE_JSON] + STANCE.";

        // ── Format Violation ────────────────────────────────────────────────
        public static string FormatRetryR2(string retryReason) =>
            $"[MODERATOR] ⚠️ {retryReason}. Need: [CLAIM]x2 + [DISPUTE]x1 + [STANCE D≥1]. Revise.";

        public static string FormatRetryR3(string retryReason) =>
            $"[MODERATOR] ⚠️ {retryReason}. Need: [CONCLUSION_KR] with [합의]/[미합의]/[셀프힐링]/[개인의견] + [STANCE]. 한국어로.";

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
