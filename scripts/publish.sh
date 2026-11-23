#!/usr/bin/env bash
set -euo pipefail

VERSION="${1:-0.1.0}"
RIDS="${RIDS:-linux-x64 osx-arm64 osx-x64}"
PROJECT="src/TempConverter.App"
OUT="artifacts"

rm -rf "$OUT"
mkdir -p "$OUT"

for RID in $RIDS; do
  DIR="$OUT/publish/$RID"
  NAME="TempConverter-$VERSION-$RID"

  echo "==> Publishing $RID"
  dotnet publish "$PROJECT" -c Release -r "$RID" --self-contained \
    -p:PublishSingleFile=true \
    -p:IncludeNativeLibrariesForSelfExtract=true \
    -p:DebugType=None -p:DebugSymbols=false \
    -p:Version="${VERSION%%-*}" \
    -o "$DIR"

  if [[ "$RID" == linux-* ]]; then
    cp packaging/linux/install.sh packaging/linux/uninstall.sh \
       packaging/linux/tempconverter.desktop.in "$DIR/"
    cp "$PROJECT/Assets/app-icon.png" "$DIR/tempconverter.png"
    chmod +x "$DIR/install.sh" "$DIR/uninstall.sh"

    if command -v nfpm >/dev/null 2>&1; then
      echo "==> Building native Linux packages (.deb, .rpm, .pkg.tar.zst)"
      sed -e "s|@EXEC@|/usr/bin/tempconverter|" \
          -e "s|@ICON@|tempconverter|" \
          packaging/linux/tempconverter.desktop.in > "$DIR/tempconverter.desktop"

      VERSION="$VERSION" nfpm package --config packaging/linux/nfpm.yaml --packager deb --target "$OUT/$NAME.deb"
      VERSION="$VERSION" nfpm package --config packaging/linux/nfpm.yaml --packager rpm --target "$OUT/$NAME.rpm"
      VERSION="$VERSION" nfpm package --config packaging/linux/nfpm.yaml --packager archlinux --target "$OUT/$NAME.pkg.tar.zst"
      rm -f "$DIR/tempconverter.desktop"
    fi
  fi

  if [[ "$RID" == win-* ]]; then
    (cd "$DIR" && zip -qr "../../$NAME.zip" .)
  else
    tar -czf "$OUT/$NAME.tar.gz" -C "$DIR" .
  fi
done

echo "Done. Packages are in $OUT/"
