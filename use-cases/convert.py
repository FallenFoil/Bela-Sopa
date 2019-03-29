#!/usr/bin/env python3
# ---------------------------------------------------------------------------- #

import itertools as _it
import re        as _re
import sys       as _sys
import typing    as _t
import unidecode as _unidecode

# ---------------------------------------------------------------------------- #

Cells = _t.Dict[_t.Tuple[int, int], str]
Table = _t.Iterable[_t.Iterable[str]]

def _read_cells() -> Cells:

    cells = {} # type: _t.Dict[_t.Tuple[int, int], str]

    for (i, line) in enumerate(_sys.stdin.read().splitlines()):
        for (j, cell) in enumerate(line.split('\t')):

            if cell in ('Ator', 'Sistema'):
                cell = R'\textbf{' + cell + '}'

            cells[(i, j)] = cell

    return cells

def _find_origins(cells: Cells) -> _t.Iterable[_t.Tuple[int, int]]:

    return (
        (i, j)
        for ((i, j), cell) in cells.items()
        if _re.match(r'(?i)^\s*(use\s+case|nome):\s*$', cell)
        )

def _build_table(cells: Cells, oi: int, oj: int) -> Table:

    for i in _it.count(oi):

        if all(not cells.get((i, oj+j), '') for j in (0, 1, 2)):
            break

        line = []
        end_prev_is_cline = False

        for j in range(oj, oj+3):

            cell = cells.get((i, j), '')

            if j == oj:
                if cell:
                    for i2 in _it.count(i+1):
                        if cells.get((i2, j)) != '':
                            break
                    h = i2 - i
                    if h > 1:
                        line.append(
                            R'\multirow[t]{{{}}}{{=}}{{{}}}'
                            .format(h, cell)
                            )
                    else:
                        line.append(cell)
                else:
                    line.append(cell)
                    end_prev_is_cline = True
            else:
                line.append(cell)

        yield (
            (R'\cline{2-3} ' if end_prev_is_cline else R'\hline ') +
            ' & '.join(line) +
            R'\\'
            )

def _get_table_name(cells: Cells, oi: int, oj: int) -> str:
    return cells[(oi, oj+1)]

def _simplify_table_name(name: str) -> str:

    return (
        _unidecode.unidecode(name)
        .replace(' ', '-')
        .replace('/', '-')
        .lower()
        )

def _main() -> None:

    cells  = _read_cells()
    tables = (
        (_build_table(cells, i, j), _get_table_name(cells, i, j))
        for (i, j) in _find_origins(cells)
        )

    for (table, name) in tables:

        simple_name = _simplify_table_name(name)

        caption = (
            R'Especificação do \emph{use case} ``' +
            name.lower() +
            "''."
            )

        label = 'tab:uc-' + simple_name

        with open(simple_name + '.tex', 'w') as f:

            print('% ' + 76*'-' + ' %', file=f)
            print(file=f)
            print(R'\begin{table}[ht]', file=f)
            print(R'  \centering', file=f)
            print(R'  \begin{tabularx}{\textwidth}{|p{\usecaseleftcolwidth}|X|X|}', file=f)

            for line in table:
                print(line, file=f)

            print(R'  \hline', file=f)
            print(R'  \end{tabularx}', file=f)
            print(R'  \caption{{{}}}'.format(caption), file=f)
            print(R'  \label{{{}}}'.format(label), file=f)
            print(R'\end{table}', file=f)
            print(file=f)
            print('% ' + 76*'-' + ' %', file=f)

# ---------------------------------------------------------------------------- #

if __name__ == '__main__':
    _main()

# ---------------------------------------------------------------------------- #
