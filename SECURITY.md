# Security Policy

## Supported versions

| Version | Supported |
|---------|-----------|
| 7.11.x-sdk | Yes (current) |
| 7.10.x-sdk | Security fixes only |
| < 7.10 | No |

Versions follow the pattern in [CHANGELOG.md](CHANGELOG.md) and [VERSIONING.md](VERSIONING.md).

## Reporting a vulnerability

Please do not open a public issue for a security problem.

- Use GitHub's private vulnerability reporting: the **Security** tab of this repository, then **Report a vulnerability**.
- If that is not available to you, contact the maintainers through the channels listed in [SUPPORT.md](SUPPORT.md) and mark the message as a security report.

Include the affected version (`wkappbot --version`), a minimal reproduction and the impact you observed.
We aim to acknowledge a report within 3 business days.

## Scope

In scope: the launcher, the CLI and the scripts shipped in this repository.
Never include real credentials, tokens or customer data in a report; use placeholders.
