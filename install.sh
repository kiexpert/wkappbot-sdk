#!/usr/bin/env sh
# Install the wkappbot portable CLI on Linux or macOS.
#
#   curl -fsSL https://raw.githubusercontent.com/kiexpert/wkappbot-sdk/main/install.sh | sh
#
# Picks the release asset for `uname -s` / `uname -m`, verifies it against the
# release's SHA256SUMS, and installs it as $WKAPPBOT_INSTALL_DIR/wkappbot.
#
# Environment overrides:
#   WKAPPBOT_VERSION       release tag (e.g. v7.12.0-sdk); default: latest release
#   WKAPPBOT_INSTALL_DIR   install directory; default: ~/.local/bin
#   WKAPPBOT_BASE_URL      directory holding the assets + SHA256SUMS (http(s):// or file://);
#                          overrides WKAPPBOT_VERSION. Used by CI to test unreleased builds.
set -eu

REPO="kiexpert/wkappbot-sdk"
INSTALL_DIR="${WKAPPBOT_INSTALL_DIR:-$HOME/.local/bin}"

die() { echo "install.sh: $*" >&2; exit 1; }

os=$(uname -s)
arch=$(uname -m)
case "$os" in
  Linux)  os_id=linux ;;
  Darwin) os_id=osx ;;
  *) die "unsupported OS '$os' (Linux and macOS only; on Windows download the release zip)" ;;
esac
case "$arch" in
  x86_64|amd64)  arch_id=x64 ;;
  arm64|aarch64) arch_id=arm64 ;;
  *) die "unsupported CPU '$arch'" ;;
esac
asset="wkappbot-$os_id-$arch_id"
case "$asset" in
  wkappbot-linux-x64|wkappbot-osx-arm64|wkappbot-osx-x64) ;;
  *) die "no prebuilt binary for $os/$arch ($asset)" ;;
esac

if [ -n "${WKAPPBOT_BASE_URL:-}" ]; then
  base="${WKAPPBOT_BASE_URL%/}"
elif [ -n "${WKAPPBOT_VERSION:-}" ]; then
  base="https://github.com/$REPO/releases/download/$WKAPPBOT_VERSION"
else
  base="https://github.com/$REPO/releases/latest/download"
fi

if command -v curl >/dev/null 2>&1; then
  fetch() { curl -fsSL "$1" -o "$2"; }
elif command -v wget >/dev/null 2>&1; then
  fetch() { wget -q "$1" -O "$2"; }
else
  die "need curl or wget"
fi

if command -v sha256sum >/dev/null 2>&1; then
  sha256() { sha256sum "$1" | awk '{print $1}'; }
elif command -v shasum >/dev/null 2>&1; then
  sha256() { shasum -a 256 "$1" | awk '{print $1}'; }
else
  die "need sha256sum or shasum"
fi

tmp=$(mktemp -d)
trap 'rm -rf "$tmp"' EXIT INT TERM

echo "Downloading $asset from $base"
fetch "$base/$asset" "$tmp/$asset" || die "download failed: $base/$asset"
fetch "$base/SHA256SUMS" "$tmp/SHA256SUMS" || die "download failed: $base/SHA256SUMS"

expected=$(awk -v f="$asset" '{ n = $2; sub(/^\*/, "", n); if (n == f) print $1 }' "$tmp/SHA256SUMS")
[ -n "$expected" ] || die "$asset is not listed in SHA256SUMS"
actual=$(sha256 "$tmp/$asset")
[ "$expected" = "$actual" ] || die "checksum mismatch for $asset (expected $expected, got $actual)"
echo "Checksum OK ($actual)"

mkdir -p "$INSTALL_DIR"
chmod +x "$tmp/$asset"
mv -f "$tmp/$asset" "$INSTALL_DIR/wkappbot"
echo "Installed $INSTALL_DIR/wkappbot"

case ":$PATH:" in
  *":$INSTALL_DIR:"*) ;;
  *) echo "Note: $INSTALL_DIR is not on PATH. Add it, e.g.: export PATH=\"$INSTALL_DIR:\$PATH\"" ;;
esac

"$INSTALL_DIR/wkappbot" --version || die "installed binary failed to run --version"
