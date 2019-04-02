#!/bin/bash
# ---------------------------------------------------------------------------- #

set -e

pdftk "${1}" output "${1}.tmp" uncompress

sed -Ei \
    's|\(Visual Paradigm Standard\\\([^\\]*\\\(Universidade do Minho\\\)\\\)\)|()|g' \
    "${1}.tmp"

pdftk "${1}.tmp" output "${1}" compress

rm -f "${1}.tmp"

pdfcrop "${1}" "${1}"

# ---------------------------------------------------------------------------- #
