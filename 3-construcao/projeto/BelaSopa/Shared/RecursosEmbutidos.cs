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
        public static IList<(Receita Receita, ISet<string> NomesEtiquetas, byte[] Imagem)> CarregarReceitasDeExemplo()
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

                return (
                    Receita: receita,
                    NomesEtiquetas: (ISet<string>)yamlReceita.Etiquetas,
                    Imagem: yamlReceita.Imagem
                    );
            });
        }

        public static IList<(Ingrediente Ingrediente, byte[] Imagem)> CarregarIngredientesDeExemplo()
        {
            return CarregarRecursosYaml("BelaSopa.Data.Ingredientes.", (YamlItu yamlIngrediente) =>
            {
                var ingrediente = new Ingrediente
                {
                    Nome = yamlIngrediente.Nome,
                    Descricao = yamlIngrediente.Descricao,
                    Texto = yamlIngrediente.Texto,
                    NomesAlternativos = yamlIngrediente.NomesAlternativos.Select(
                        n => new NomeAlternativoIngrediente { Nome = n }
                        ).ToList()
                };

                return (Ingrediente: ingrediente, Imagem: yamlIngrediente.Imagem);
            });
        }

        public static IList<(Tecnica Tecnica, byte[] Imagem)> CarregarTecnicasDeExemplo()
        {
            return CarregarRecursosYaml("BelaSopa.Data.Tecnicas.", (YamlItu yamlTecnica) =>
            {
                var tecnica = new Tecnica
                {
                    Nome = yamlTecnica.Nome,
                    Descricao = yamlTecnica.Descricao,
                    Texto = yamlTecnica.Texto,
                    NomesAlternativos = yamlTecnica.NomesAlternativos.Select(
                        n => new NomeAlternativoTecnica { Nome = n }
                        ).ToList()
                };

                return (Tecnica: tecnica, Imagem: yamlTecnica.Imagem);
            });
        }

        public static IList<(Utensilio Utensilio, byte[] Imagem)> CarregarUtensiliosDeExemplo()
        {
            return CarregarRecursosYaml("BelaSopa.Data.Utensilios.", (YamlItu yamlUtensilio) =>
            {
                var utensilio = new Utensilio
                {
                    Nome = yamlUtensilio.Nome,
                    Descricao = yamlUtensilio.Descricao,
                    Texto = yamlUtensilio.Texto,
                    NomesAlternativos = yamlUtensilio.NomesAlternativos.Select(
                        n => new NomeAlternativoUtensilio { Nome = n }
                        ).ToList()
                };

                return (Utensilio: utensilio, Imagem: yamlUtensilio.Imagem);
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
