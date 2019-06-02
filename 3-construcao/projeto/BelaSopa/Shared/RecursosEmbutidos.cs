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
        public static IList<(Receita Receita, ISet<string> NomesEtiquetas)> CarregarReceitasDeExemplo()
        {
            return CarregarRecursosYaml("BelaSopa.Data.Receitas.", (YamlReceita yamlReceita) =>
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

                return (Receita: receita, NomesEtiquetas: (ISet<string>)yamlReceita.Etiquetas);
            });
        }

        public static IList<Ingrediente> CarregarIngredientesDeExemplo()
        {
            return CarregarRecursosYaml("BelaSopa.Data.Ingredientes.", (YamlItu yamlIngrediente) =>
            {
                return new Ingrediente
                {
                    Nome = yamlIngrediente.Nome,
                    Descricao = yamlIngrediente.Descricao,
                    Texto = yamlIngrediente.Texto,
                    Imagem = yamlIngrediente.Imagem,
                    NomesAlternativos = yamlIngrediente.NomesAlternativos.Select(
                        n => new NomeAlternativoIngrediente { Nome = n }
                        ).ToList()
                };
            });
        }

        public static IList<Tecnica> CarregarTecnicasDeExemplo()
        {
            return CarregarRecursosYaml("BelaSopa.Data.Tecnicas.", (YamlItu yamlTecnica) =>
            {
                return new Tecnica
                {
                    Nome = yamlTecnica.Nome,
                    Descricao = yamlTecnica.Descricao,
                    Texto = yamlTecnica.Texto,
                    Imagem = yamlTecnica.Imagem,
                    NomesAlternativos = yamlTecnica.NomesAlternativos.Select(
                        n => new NomeAlternativoTecnica { Nome = n }
                        ).ToList()
                };
            });
        }

        public static IList<Utensilio> CarregarUtensiliosDeExemplo()
        {
            return CarregarRecursosYaml("BelaSopa.Data.Utensilios.", (YamlItu yamlUtensilio) =>
            {
                return new Utensilio
                {
                    Nome = yamlUtensilio.Nome,
                    Descricao = yamlUtensilio.Descricao,
                    Texto = yamlUtensilio.Texto,
                    Imagem = yamlUtensilio.Imagem,
                    NomesAlternativos = yamlUtensilio.NomesAlternativos.Select(
                        n => new NomeAlternativoUtensilio { Nome = n }
                        ).ToList()
                };
            });
        }

        private static IList<U> CarregarRecursosYaml<T, U>(string prefixoNome, Func<T, U> carregador)
        {
            var carregados = new List<U>();

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

                        carregados.Add(carregador(recurso));
                    }
                }
            }

            return carregados;
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
            public HashSet<string> Etiquetas { get; set; }

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
            public HashSet<string> NomesAlternativos { get; set; }

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
