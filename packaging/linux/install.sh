#!/usr/bin/env bash
set -euo pipefail

SRC="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
DATA="${XDG_DATA_HOME:-$HOME/.local/share}"
APP_DIR="$HOME/.local/opt/TempConverter"
ICON_DIR="$DATA/icons/hicolor/256x256/apps"
DESKTOP_DIR="$DATA/applications"

mkdir -p "$APP_DIR" "$ICON_DIR" "$DESKTOP_DIR"

install -m 755 "$SRC/TempConverter" "$APP_DIR/TempConverter"
install -m 644 "$SRC/tempconverter.png" "$ICON_DIR/tempconverter.png"

sed -e "s|@EXEC@|$APP_DIR/TempConverter|" \
    -e "s|@ICON@|$ICON_DIR/tempconverter.png|" \
    "$SRC/tempconverter.desktop.in" > "$DESKTOP_DIR/tempconverter.desktop"

update-desktop-database "$DESKTOP_DIR" 2>/dev/null || true

echo "TempConverter installed. Look for it in your applications menu."
