#!/bin/bash
# ---------------------------------------------------------------------------- #

# convert to unix line endings

IFS=$'\n' \
    dos2unix \
    $(find . -type f -not -name '*.png' -not -name '*.jpg' -not -name '*.vpp')

# trim trailing whitespace

IFS=$'\n' \
    sed -i -E -e 's/[[:space:]]+$//g' \
    $(find . -type f -not -name '*.png' -not -name '*.jpg' -not -name '*.vpp')

# ---------------------------------------------------------------------------- #
