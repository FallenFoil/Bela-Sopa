using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace BelaSopa.Shared
{
    public class RecursosEmbutidos
    {
        public static void CarregarReceitasDeExemplo(BelaSopaContext context)
        {
            CarregarRecursosYaml<YamlReceita>("BelaSopa.Data.Receitas.", yamlReceita =>
            {
                Dificuldade dificuldade;

                switch (yamlReceita.Dificuldade.ToLower())
                {
                    case "fácil": dificuldade = Dificuldade.Facil; break;
                    case "média": dificuldade = Dificuldade.Media; break;
                    case "difícil": dificuldade = Dificuldade.Dificil; break;
                    default: throw new Exception();
                }

                var receita = new Receita
                {
                    Nome = yamlReceita.Nome,
                    Descricao = yamlReceita.Descricao,
                    Dificuldade = dificuldade,
                    MinutosPreparacao = yamlReceita.MinutosPreparacao,
                    NumeroDoses = yamlReceita.NumeroDoses,
                    Imagem = yamlReceita.Imagem,
                    UtilizacoesIngredientes = yamlReceita.Ingredientes,
                    ValoresNutricionais = yamlReceita.ValoresNutricionais,
                    Processos = yamlReceita.Passos.Select((passos, i) => new Processo
                    {
                        Indice = i,
                        Tarefas = passos.Select((passo, j) => new Tarefa
                        {
                            Indice = j,
                            Texto = new List<TextoTarefa> { new TextoTarefa { Indice = 0, Texto = passo } }
                        }).ToList()
                    }).ToList()
                };

                context.AdicionarReceita(receita, yamlReceita.Etiquetas);
            });
        }

        public static void CarregarIngredientesDeExemplo(BelaSopaContext context)
        {
            CarregarRecursosYaml<YamlItu>("BelaSopa.Data.Ingredientes.", yamlIngrediente =>
            {
                var ingrediente = new Ingrediente
                {
                    Nome = yamlIngrediente.Nome,
                    Descricao = yamlIngrediente.Descricao,
                    Texto = yamlIngrediente.Texto,
                    Imagem = yamlIngrediente.Imagem,
                    NomesAlternativos = yamlIngrediente.NomesAlternativos.Select(
                        n => new NomeAlternativoIngrediente { Nome = n }
                        ).ToList()
                };

                context.AdicionarIngrediente(ingrediente);
            });
        }

        public static void CarregarTecnicasDeExemplo(BelaSopaContext context)
        {
            CarregarRecursosYaml<YamlItu>("BelaSopa.Data.Tecnicas.", yamlTecnica =>
            {
                var tecnica = new Tecnica
                {
                    Nome = yamlTecnica.Nome,
                    Descricao = yamlTecnica.Descricao,
                    Texto = yamlTecnica.Texto,
                    Imagem = yamlTecnica.Imagem,
                    NomesAlternativos = yamlTecnica.NomesAlternativos.Select(
                        n => new NomeAlternativoTecnica { Nome = n }
                        ).ToList()
                };

                context.AdicionarTecnica(tecnica);
            });
        }

        public static void CarregarUtensiliosDeExemplo(BelaSopaContext context)
        {
            CarregarRecursosYaml<YamlItu>("BelaSopa.Data.Utensilios.", yamlUtensilio =>
            {
                var utensilio = new Utensilio
                {
                    Nome = yamlUtensilio.Nome,
                    Descricao = yamlUtensilio.Descricao,
                    Texto = yamlUtensilio.Texto,
                    Imagem = yamlUtensilio.Imagem,
                    NomesAlternativos = yamlUtensilio.NomesAlternativos.Select(
                        n => new NomeAlternativoUtensilio { Nome = n }
                        ).ToList()
                };

                context.AdicionarUtensilio(utensilio);
            });
        }

        private static void CarregarRecursosYaml<T>(string prefixoNome, Action<T> carregador)
        {
            var nomesRecursos =
                Assembly
                .GetEntryAssembly()
                .GetManifestResourceNames()
                .Where(name => name.StartsWith(prefixoNome));

            foreach (var nome in nomesRecursos)
            {
                using (var stream = Assembly.GetEntryAssembly().GetManifestResourceStream(nome))
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        var deserializer =
                            new DeserializerBuilder()
                            .WithTagMapping("tag:yaml.org,2002:binary", typeof(byte[]))
                            .WithTypeConverter(new ByteArrayConverter())
                            .Build();

                        var recurso = deserializer.Deserialize<T>(reader);

                        carregador(recurso);
                    }
                }
            }
        }

        public static void CarregarDataRefeicao(BelaSopaContext context)
        {
            bool almoco = false;
            for (int i = 0; i < 2; i++)
            {
                almoco = !almoco;
                for (int j = 0; j < 7; j++)
                {
                    DataRefeicao dr = new DataRefeicao(j, almoco);
                    context.DataRefeicao.Add(dr);
                }
            }
        }

        private class YamlReceita
        {
            [YamlMember(Alias = "nome")]
            public string Nome { get; set; }

            [YamlMember(Alias = "descrição")]
            public string Descricao { get; set; }

            [YamlMember(Alias = "dificuldade")]
            public string Dificuldade { get; set; }

            [YamlMember(Alias = "minutos-de-preparação")]
            public int MinutosPreparacao { get; set; }

            [YamlMember(Alias = "número-de-doses")]
            public int NumeroDoses { get; set; }

            [YamlMember(Alias = "etiquetas")]
            public List<string> Etiquetas { get; set; }

            [YamlMember(Alias = "ingredientes")]
            public List<UtilizacaoIngrediente> Ingredientes { get; set; }

            [YamlMember(Alias = "valores-nutricionais")]
            public List<ValorNutricional> ValoresNutricionais { get; set; }

            [YamlMember(Alias = "passos")]
            public List<List<string>> Passos { get; set; }

            [YamlMember(Alias = "imagem")]
            public byte[] Imagem { get; set; }
        }

        public class YamlItu
        {
            [YamlMember(Alias = "nome")]
            public string Nome { get; set; }

            [YamlMember(Alias = "nomes-alternativos")]
            public List<string> NomesAlternativos { get; set; }

            [YamlMember(Alias = "descrição")]
            public string Descricao { get; set; }

            [YamlMember(Alias = "texto")]
            public string Texto { get; set; }

            [YamlMember(Alias = "imagem")]
            public byte[] Imagem { get; set; }
        }

        private class ByteArrayConverter : IYamlTypeConverter
        {
            public bool Accepts(Type type)
            {
                return type == typeof(byte[]);
            }

            public object ReadYaml(IParser parser, Type type)
            {
                var bytes = Convert.FromBase64String((parser.Current as Scalar).Value);
                parser.MoveNext();
                return bytes;
            }

            public void WriteYaml(IEmitter emitter, object value, Type type)
            {
                emitter.Emit(new Scalar(
                    null,
                    "tag:yaml.org,2002:binary",
                    Convert.ToBase64String(value as byte[]),
                    ScalarStyle.Plain,
                    false,
                    false
                ));
            }
        }
    }
}
