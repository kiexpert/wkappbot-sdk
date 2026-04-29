#!/usr/bin/env bash
# Remove insight/wisdom skills from public SDK.
# Rule: free tier = memorization/reference only.
# Insight skills (hard-won design patterns, debugging breakthroughs, decision frameworks)
# belong in paid tier (TBD: skill registry mechanism).
set -e

cd D:/GitHub/wkappbot-sdk

PURGE=(
  # --- Focus / CDP design insights ---
  a11y-focus-steal-user-active-silent-yield
  cdp-focus-steal-guard-runtime-evaluate-in-isfocusstealingmethod-weminimized-500ms-wait
  ask-cdp-uia-focus-steal-coverage-via-sentinel
  ask-web-chrome-launch-focusless-guard-allowlist
  a11y-find-cdp-runtime-eval-focus-guard
  a11y-find-deprecated-eval-removal-guard
  a11y-find-verify-miss-empty-grap-guard
  cdp-evalasync-retry-policy
  cdp-runtime-enable-with-retry
  cdp-probe-port-9-guard-discard-low-port-values-to-prevent-httprequestexception
  guard-cdp-probe-ports-against-low-port-discard-values
  web-open-idle-keepalive

  # --- React / SPA design ---
  a11y-click-react-fiber-cdp-trusted-fallback
  react-spa-automation-fiber-props-click-controlled-input-fill
  naver-mail-compose-spa-read-fiber

  # --- ChatGPT / browser AI debugging insights ---
  chatgpt-blank-false-positive-guard
  chatgpt-stop-button-drain-before-send
  ask-gpt-editor-wait-tag-and-rescue-fallback
  ask-prompt-pump-watchdog-dismiss-resend-recovery
  claude-proxy-context-limit-auto-handoff-to-gemini-agent

  # --- A11y design / framework insights ---
  a11y-set-value-50ms-delay-read-back-warning-for-win11-richtextbox-and-chrome-stale-valuepattern
  a11y-close-focus-steal-guard
  a11y-find-performance-fastpath
  a11y-grap-hwnd-authoritative-no-and-reject-on-drifted-fields
  a11y-screenshot-focusless-window-capture
  a11y-callout
  a11y-grid-read-clipboard-bridge-for-owner-drawn-hts-grids
  a11y-hack-ocr-opt-in-vision-worker-sentinel-bridge
  a11y-kill-json5-grap-support
  a11y-type-force-hwnd-scope-bypass
  a11y-auto-find-grap-verify-skip-miss-report-for-empty-pattern-wpf-overlays
  a11y-find-mfc-deep-descendant-grandchildren-via-enumchildwindows
  a11y-playbook-writing

  # --- Multi-AI orchestration insights ---
  ask-triad-when-uncertain
  record-triad-insight-as-skill
  claude-token-optimization
  claude-code-agent-tier-routing
  delegation-policy-all-commands-use-mandatory-haiku-codex-opus-routing-in-claude-md
  agent-canonical-dispatch
  claude-cli-automation-patterns-wkappbot-integration
  claude-terminal-via-appbot-primary
  triad-ask-simultaneous-q-debate-tool-loop-agent-mode
  multi-tab-assistant-implementation-checklist
  multi-tab-assistant-interface-mvp
  prompt-send-self-prompting
  codex-cli-wkappbot-usage
  codex-friendly-skill-template

  # --- Screenshot / scan algorithms ---
  screenshot-grap-bbox
  screenshot-mask-transparency

  # --- Window dismiss / detection patterns ---
  spam-popup-dismiss-patterns
  win-click-dismiss-blocker-auto-dismiss-blocking-popup-dialogs
  win-click-coordinate-system-window-relative-vs-abs-screen-coords
  a11y-close-safety-ambiguity-focus-node-diagnostics

  # --- Competitor analyses (real insight) ---
  competitive-browser-cdp-mcp-tools
  competitive-fireship-claude-superpower
  competitive-habits-agent-builder
  competitive-oculos-desktop-mcp
  competitive-palmier-phone-bridge-eval
  competitive-ui-automata-desktop-agents
  competitor-analysis-desktop-ai-agents-2026
  compete-roadmap-2026q2
  ai-agent-desktop-automation-landscape-competitor-and-pattern-reference-2026-04
  competitive-analysis-css-selectors-rust-perf-scenario-expect-xp-sharing
  punk-pattern-remote-claude-code-control-via-lightweight-proxy
  kampala-pattern-reverse-engineer-apps-into-apis-for-automation
  mcp-standard-for-desktop-and-browser-automation-integration-patterns
  anthropic-computer-use-cross-platform
  claude-agent-team-no-code

  # --- Find / control deep insights ---
  find-any-window-element-your-way-grap-multi-modal-search
  control-ui-without-stealing-focus-3-tier-focusless-strategy
  stop-ai-from-clicking-the-wrong-button-6-layer-target-safety-net
  find-tree-grap-pitfalls
  grap-alias-system

  # --- Specific debugging insights ---
  electron-uia-deep-tree-traversal
  uia-getsupportedpatterns-forbidden
  per-ai-color-coding-for-cdp-bot-decoration
  bot-window-decoration-dwm-border-css-overlay
  remove-targetless-evals-no-implicit-dom-context
  new-chat-landing-has-no-turn-form
  proc-path-glob-matching
  cls-path-glob-matching
  why-your-wpf-overlay-hits-2gb-and-the-1-line-fix
  mdi-child-focusless-raise-swp-noactivate
  shell-is-a-scenario-step-action-not-a-cli-command
  playbook-actions-read-uia-text-shell-external-command
  cookbook-install-input-protection-on-a-new-a11y-action-beginner

  # --- Insightful misc ---
  ai-news-briefing
  ai-news-to-suggests
  ai-ecosystem-updates-april-2026-briefing-backlog-reference
  skill-audience-taxonomy
  skill-coverage-ranking-news
  hn-browser-control-and-computer-use-as-mcp-tools-works-with-claude-codex-cursor-ai-computer-use-pattern-reference
  hn-oculos-give-ai-agents-control-of-your-desktop-via-mcp-ai-computer-use-pattern-reference
  hn-self-healing-browser-harness-via-direct-cdp-ai-computer-use-pattern-reference
  hn-show-hn-oculos-give-ai-agents-control-of-your-desktop-via-mcp-ai-computer-use-pattern-reference
  hn-show-hn-self-healing-browser-harness-via-direct-cdp-ai-computer-use-pattern-reference
  hn-ui-automata-windows-desktop-automation-for-ai-agents-ai-computer-use-pattern-reference

  # --- Operational/architecture insight ---
  automating-legacy-mfc-apps-with-no-uia-hts-playbook
  always-on-ai-automation-daemon-appbot-eye-internals
  concurrent-ai-isolation-env-var-detection-beats-parent-chain
  session-continuity-persist-command-history-and-agent-state
  repo-maintenance-gc-cleanup-and-pre-release-git-audit
  public-repo-checklist-pre-release-audit-for-secrets-and-pii
  github-pat-fine-grained-permission-edit
  github-actions-budget-and-pat-recovery
  schedule-cmd-bash-windows
  wkappbot-run-process-launcher-with-integrated-tracking
  win-click-postmessage-mfc-nfrunlite
  a11y-find
  scenariorunner-process-key-no-dup-flag-for-existing-process-adoption

  # --- Slack/channel routing insights ---
  slack-send-reply-per-cwd-reply-context-no-misroute
  slack-bot-self-mention-no-socket
  suggest-resolve-evidence-conventions
  suggest-triage-playbook
  suggest-workflow

  # --- Build/CI insights ---
  wkappbot-build-verify-workflow
  wkappbot-windows-focus-flag-and-no-filter-relevance-mode

  # --- Codex auto-approval insights ---
  codex-auto-approval
  codex-auto-approval-safe
  codex-global-approval
  "codex-global-approval --exit-event wkappbot-exit-59736"

  # --- Quantbot user input protection (insight) ---
  quantbot-user-input-protection-guard

  # --- Skill review/audit ---
  skill-review-completed-codex-cli-wkappbot-usage-updated-to-v1-4
  chore-resolved-claude-md-delegation-rules-already-mandatory

  # --- Workflow insights ---
  ai-news-briefing
  ai-news-to-suggests

  # --- Data extraction patterns ---
  speak-timeout-dynamic-safety
  grap-target

  # --- News refs (operational) ---
  news-20260420-sudo-encoding
  nfrunlite-uninvited-modal-dismiss-pattern

  # --- Misc operational ---
  lm-wiki-personal-knowledge-base
  nlrc-evidence-webbot
)

removed=0
for id in "${PURGE[@]}"; do
  for f in skills/*/"$id".skill.json; do
    if [ -f "$f" ]; then
      rm "$f"
      removed=$((removed + 1))
    fi
  done
done

echo "Purged: $removed insight skills"
echo "Remaining: $(find skills -name '*.skill.json' | wc -l) skills"
echo ""
echo "Remaining categories:"
for cat in $(ls skills/); do
  if [ -d "skills/$cat" ]; then
    n=$(ls skills/$cat/*.skill.json 2>/dev/null | wc -l)
    [ "$n" -gt 0 ] && printf "  %-22s %3d\n" "$cat" "$n"
  fi
done
