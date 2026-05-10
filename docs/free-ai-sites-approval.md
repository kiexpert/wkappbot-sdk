# Free AI Sites Approval

## Default Approval Policy

### Level 0 - Passive/Observational (always allowed)
Read DOM, screenshot, scroll, extract text

### Level 1 - Cosmetic/Nuisance (auto-dismiss, log required)
Close modal overlays, collapse banners, dismiss ads, close newsletter modals.
Cookie banners: ONLY reject/necessary-only/X-close actions are Level 1.

### Level 2 - Workflow Continuation (auto-allowed, log required)
Continue generating, retry after error, regenerate response, reopen conversation.

### Level 3 - State Mutation (USER APPROVAL REQUIRED)
Accept cookies / enable personalization / ad targeting / cross-site tracking.
Login / logout / connect account / enable sync or history / grant permissions.
Save persistent preferences.

### Level 4 - Economic/Quota Impact (USER APPROVAL REQUIRED)
Upgrades, subscription changes, consuming scarce quota (ChatGPT 5-hour limit,
Claude messages remaining, Perplexity Pro Search credits), using paid models.
Rule: if continuation costs money / scarce quota / irreversible allocation -> Level 4.

### Level 5 - High-Risk / Legal / Identity (ALWAYS EXPLICIT APPROVAL)
Age verification, CAPTCHA, identity verification, payment forms, OAuth,
export/delete account data.

## Cookie Banner Rule

Classify by the ACTION, not the UI element.
Auto-allowed (Level 1): Reject all, Necessary only, Close without consent, X dismiss.
Requires approval (Level 3): Accept all, Enable personalization, Ad targeting,
Cross-site tracking, Improve model training.
Rule: if action increases data sharing scope -> require approval.

## Rate Limit Rule

Safe soft limits (Level 0): response truncated, temporary overload, retry later.
Level 4 (quota-spending): ChatGPT limit resets in N hours, Claude messages remaining,
Perplexity Pro Search credits, switching from free to paid model pool.

## Runtime Enforcement

Every CDP action passes through `EvaluateAction()` returning `ALLOW/DENY/REQUIRE_APPROVAL/AUTO_REJECT`.
Schema: `{action, target, domain, risk: {privacy, economic, reversible}, decision}`

## CDP Monitor Rules

Treat nuisance blockers as Level 1 and workflow continuation blockers as Level 2.
Keep the nuisance order aligned so cosmetic dismissals stay Level 1 while continuation-only recoveries stay Level 2.

## Site Matrix

<!-- Existing Site Matrix table rows intentionally unchanged. -->
