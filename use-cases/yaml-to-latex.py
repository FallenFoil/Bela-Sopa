#!/usr/bin/env python3
# ---------------------------------------------------------------------------- #

import pathlib   as _p
import itertools as _it
import re        as _re
import sys       as _sys
import typing    as _t

import yaml as _yaml

# ---------------------------------------------------------------------------- #

def _main() -> None:

    input_files = _sys.argv[1:]

    for file in input_files:
        _gen_latex(_p.Path(file))

def _gen_latex(yaml_path: _p.Path) -> None:

    simple_name = yaml_path.stem
    tex_path    = yaml_path.parent / (yaml_path.stem + '.tex')

    with yaml_path.open('r') as f:
        data = _yaml.load(f.read())

    with tex_path.open('w') as f:

        f.write(
R'''% ---------------------------------------------------------------------------- %

\begin{table}[ht]
  \centering
  \begin{tabularx}{\textwidth}{|p{\usecaseleftcolwidth}|X|X|}
    \hline
''')

        f.write(
            R'    \emph{Use case}: & \multicolumn{2}{l|}{' +
            data['nome'] +
            R'} \\ \hline' +
            '\n'
            )

        f.write(
            R'    Pré-condição: & \multicolumn{2}{l|}{' +
            data['pré-condição'] +
            R'} \\ \hline' +
            '\n'
            )

        f.write(
            R'    Pós-condição: & \multicolumn{2}{l|}{' +
            data['pós-condição'] +
            R'} \\ \hline' +
            '\n'
            )

        f.write(R'     & \textbf{Ator} & \textbf{Sistema} \\ \hline' '\n')

        _write_latex_parte(
            f, 'Comportamento Normal', '', '', data['comportamento-normal']
            )

        for (i, alt) in enumerate(data['comportamentos-alternativos']):
            _write_latex_parte(
                f, 'Comportamento Alternativo {}'.format(i+1), alt['condição'],
                alt['passo-origem'], alt['passos']
                )

        for (i, exc) in enumerate(data['exceções']):
            _write_latex_parte(
                f, 'Exceção {}'.format(i+1), exc['condição'],
                exc['passo-origem'], exc['passos']
                )

        f.write(
R'''\end{{tabularx}}
  \caption{{Especificação do \emph{{use case}} ``{}''.}}
  \label{{tab:uc-{}}}
\end{{table}}

% ---------------------------------------------------------------------------- %
'''.format(data['nome'].lower(), simple_name))

def _write_latex_parte(
    f: _t.TextIO,
    nome: str,
    condicao: str,
    passo_origem: str,
    passos: _t.List[str]
    ) -> None:

    for (i, passo) in enumerate(passos):

        if i == 0:

            parte = nome

            if condicao:
                parte += ' [{}]'.format(condicao)

            if passo_origem:
                parte += ' (passo {})'.format(passo_origem)

            col_parte = R'\multirow[t]{{{}}}{{=}}{{{}}}'.format(len(passos), parte)

        else:

            col_parte = ''

        if passo.startswith('(A)'):
            col_ator    = passo[3:].strip()
            col_sistema = ''
        elif passo.startswith('(S)'):
            col_ator    = ''
            col_sistema = passo[3:].strip()
        else:
            raise ValueError

        if i == len(passos) - 1:
            linha_fim = R'\hline'
        else:
            linha_fim = R'\cline{2-3}'

        f.write(
            R'    {} & {} & {} \\ {}'.format(
                col_parte, col_ator, col_sistema, linha_fim
                ) +
            '\n'
            )

# ---------------------------------------------------------------------------- #

if __name__ == '__main__':
    _main()

# ---------------------------------------------------------------------------- #
