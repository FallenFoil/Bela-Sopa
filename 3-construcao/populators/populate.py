#!/usr/bin/env python3
# ---------------------------------------------------------------------------- #

import yaml
import pyodbc
import sys

# ---------------------------------------------------------------------------- #
# things

def insert_recipe(cursor, yaml_path: str) -> None:

    # load recipe

    with open(yaml_path, 'r+', encoding='utf-8') as f:
        recipe = yaml.safe_load(f)

    # TODO

# ---------------------------------------------------------------------------- #
# main

if __name__ == '__main__':

    with pyodbc.connect('DSN=ParaLI4') as conn:

        cursor = conn.cursor()

        for (i, yaml_path_recipe) in enumerate(sys.argv[1:]):

            print(
                'Inserting recipe {} of {}... ({})'
                .format(i + 1, len(sys.argv), yaml_path_recipe)
                )

            insert_recipe(cursor, yaml_path_recipe)

        print('Done!')

# ---------------------------------------------------------------------------- #
