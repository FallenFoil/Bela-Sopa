#!/usr/bin/env python3
# ---------------------------------------------------------------------------- #

import bs4
import re
import sys
import typing
import urllib.request
import yaml

# ---------------------------------------------------------------------------- #
# scrape recipe list

def scrape_recipe_list(list_path: str) -> typing.AbstractSet[str]:

    # read html

    with open(list_path, 'r+b') as f:
        html = f.read()

    # scrape recipe list

    soup = bs4.BeautifulSoup(html, features='html.parser')

    return frozenset(
        tag.get('href')
        for tag in soup.find('div', class_='js-search-results')
                       .find_all('a', class_='pd-card recipe')
        )

# ---------------------------------------------------------------------------- #
# scrape recipe

def scrape_recipe(url: str) -> None:

    # read html

    with urllib.request.urlopen(url) as html_file:
        html = html_file.read()

    # parse recipe

    soup = bs4.BeautifulSoup(html, features='html.parser')

    # parse recipe - nome

    nome_receita = soup.find('h1', class_='main-slide-title').string.strip()

    # parse recipe - descrição

    descricao_receita = (
        soup
        .find('li', class_='detail-slide')
        .find('div', class_='description')
        .string
        )

    if not descricao_receita:

        # sem valores nutritionais

        print('    SKIPPING - no description')
        return

    descricao_receita = descricao_receita.strip().strip('"\'').strip()

    descricao_receita = re.sub(
        r'[\s\n]+',
        r' ',
        descricao_receita
        )

    # parse recipe - dificuldade

    tag_dificuldade = soup.find('label', class_='dificulty')

    dificuldade = {
        ''       : 'fácil',
        'Fácil'  : 'fácil',
        'Média'  : 'média',
        'Difícil': 'difícil',
        }['' if tag_dificuldade is None else tag_dificuldade.string]

    # parse recipe - tempo de preparação

    duracao_minutos = int(
        re.sub(
            r'(?i)^\s*(\d+)\s+min\.?\s*$',
            r'\1',
            soup.find('label', class_='preptime').string
            )
        )

    # parse recipe - número de doses

    num_pessoas = int(
        re.sub(
            r'(?i)^\s*(\d+)\s+(pessoas|doses|porções)?\s*$',
            r'\1',
            soup.find('label', class_='nr_persons').string
            )
        )

    # parse recipe - etiquetas

    etiquetas = list(
        tag.string.replace('|', '').strip().capitalize()
        for tag in soup.find_all('span', class_='recipetype')
        )

    # parse recipe - valores nutricionais

    valores_nutricionais = [] # type: typing.List[typing.Dict[str, str]]

    if soup.find('table', class_='nutritable') is not None:

        # formato em tabela completa (maioria das receitas)

        trs = soup.find('table', class_='nutritable').tbody.find_all('tr')

        prev_categoria = ''

        for tr in trs:

            nome = tr.find('td', class_='').string.strip()

            if tr.get('class') == ['inside']:
                nome = prev_categoria + ' | ' + nome
            else:
                prev_categoria = nome

            (dose, percentagem_vdr, *_) = (
                td.string or ''
                for td in tr.find_all('td', class_='down-border')
                if td.get('class') != ['down-border', 'hidden']
                )

            percentagem_vdr = percentagem_vdr.replace('%', '').strip()

            if percentagem_vdr:
                percentagem_vdr = int(percentagem_vdr)
            else:
                percentagem_vdr = None

            valores_nutricionais.append({
                'nome': nome,
                'dose': dose.strip(),
                'percentagem-do-vdr-adulto': percentagem_vdr
                })

    elif soup.find('div', class_='nutrition-table') is not None:

        # formato abreviado (algumas receitas)

        lis = soup.find('div', class_='nutrition-table').ul.find_all('li')

        for li in lis:

            nome = li.find('div', class_='title').string.strip()

            if nome == 'Calorias':
                nome = 'Energia'
            elif nome == 'Açucar':
                nome = 'Açúcar'
            elif nome == 'Saturada':
                nome = 'Gordura | das quais saturadas'
            elif nome not in ('Gordura', 'Sal', 'Fibra', 'Proteínas', 'Açúcar'):
                raise KeyError(nome)

            detail = li.find('div', class_='detail')

            dose = detail.find('div', class_='value').string.strip()

            try:
                float(dose)
            except:
                pass
            else:
                dose += ' Kcal'

            percentagem_vdr = (
                detail
                .find('div', class_='percent')
                .string
                .replace('%', '')
                .strip()
                )

            if percentagem_vdr:
                percentagem_vdr = int(percentagem_vdr)
            else:
                percentagem_vdr = None

            valores_nutricionais.append({
                'nome': nome,
                'dose': dose,
                'percentagem-do-vdr-adulto': percentagem_vdr
                })

    else:

        # sem valores nutritionais

        print('    SKIPPING - no nutrition table')
        return

    # parse recipe - ingredientes

    ingredientes = list(
        {
            'nome': ' '.join(
                list(
                    tag.find('span', class_='ingredient-product')
                    .stripped_strings
                    )[0].split()
                ).strip().capitalize(),
            'quantidade': '{} {}'.format(
                tag.find('span', class_='ingredient-quantity').string or '',
                tag.find('span', class_='ingredient-unit').string or ''
                ).strip()
        }
        for tag in soup.find_all('li', class_='ingredient-wrapper')
        if list(tag.find('span', class_='ingredient-product').stripped_strings)
        )

    # parse recipe - passos

    passos = [
        list(
            str(tag.string)
            for tag in soup.find_all('span', class_='instruction-body')
            )
        ]

    # parse recipe - imagem

    url_imagem = str(soup.find('div', id='imagemReceitaHeader').img.get('src'))

    with urllib.request.urlopen(url_imagem) as imagem_file:
        imagem = imagem_file.read()

    # output yaml

    receita = [
        ('nome'                 , nome_receita),
        ('descrição'            , descricao_receita),
        ('dificuldade'          , dificuldade),
        ('minutos-de-preparação', duracao_minutos),
        ('número-de-doses'      , num_pessoas),
        ('etiquetas'            , etiquetas),
        ('ingredientes'         , ingredientes),
        ('valores-nutricionais' , valores_nutricionais),
        ('passos'               , passos),
        ('imagem'               , imagem),
    ]

    path_yaml = re.sub(
        r'https://www\.pingodoce\.pt/receitas/([\w-]+)/',
        r'\1.yaml',
        url
        )

    with open(path_yaml, 'w', encoding='utf-8') as yaml_file:

        for (key, value) in receita:
            yaml_file.write('\n')
            yaml.dump(
                { key: value },
                stream=yaml_file,
                allow_unicode=True,
                width=float("inf")
                )

# ---------------------------------------------------------------------------- #
# main

if __name__ == '__main__':

    recipe_urls = scrape_recipe_list(sys.argv[1])

    for (i, url) in enumerate(recipe_urls):

        print(
            'Scraping recipe {} of {}... ({})'
            .format(i + 1, len(recipe_urls), url)
            )

        scrape_recipe(url)

    print('Done!')

# ---------------------------------------------------------------------------- #
