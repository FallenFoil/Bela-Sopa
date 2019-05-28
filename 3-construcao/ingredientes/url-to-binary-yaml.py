#!/usr/bin/env python3
# ---------------------------------------------------------------------------- #

import sys
import urllib.request
import yaml

# ---------------------------------------------------------------------------- #
# main

if __name__ == '__main__':

    with urllib.request.urlopen(sys.argv[1]) as data_file:
        data = data_file.read()

    with open('data.yaml', 'w', encoding='utf-8') as yaml_file:

        yaml.dump(
            { "data": data },
            stream=yaml_file,
            allow_unicode=True,
            width=float("inf")
            )

# ---------------------------------------------------------------------------- #
