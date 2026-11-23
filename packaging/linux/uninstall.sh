#!/usr/bin/env bash
set -euo pipefail

DATA="${XDG_DATA_HOME:-$HOME/.local/share}"

rm -rf "$HOME/.local/opt/TempConverter"
rm -f "$DATA/icons/hicolor/256x256/apps/tempconverter.png"
rm -f "$DATA/applications/tempconverter.desktop"
update-desktop-database "$DATA/applications" 2>/dev/null || true

echo "TempConverter removed."
