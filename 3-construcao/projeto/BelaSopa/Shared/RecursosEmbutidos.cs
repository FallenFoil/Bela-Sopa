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
                    Imagem = yamlReceita.Imagem
                };

                foreach (var yamlIngrediente in yamlReceita.Ingredientes)
                {
                    receita.ReceitaIngrediente.Add(new ReceitaIngrediente
                    {
                        Nome = yamlIngrediente.Nome,
                        Quantidade = yamlIngrediente.Quantidade
                    });
                }

                context.AdicionarReceita(receita, yamlReceita.Etiquetas);
            });
        }

        public static void CarregarIngredientesDeExemplo(BelaSopaContext context)
        {
            CarregarRecursosYaml<Ingrediente>("BelaSopa.Data.Ingredientes.", ingrediente =>
            {
                context.AdicionarIngrediente(ingrediente);
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
            public List<YamlReceitaIngrediente> Ingredientes { get; set; }

            [YamlMember(Alias = "valores-nutricionais")]
            public List<YamlReceitaValorNutricional> ValoresNutricionais { get; set; }

            [YamlMember(Alias = "passos")]
            public List<List<string>> Passos { get; set; }

            [YamlMember(Alias = "imagem")]
            public byte[] Imagem { get; set; }
        }

        private class YamlReceitaIngrediente
        {
            [YamlMember(Alias = "nome")]
            public string Nome { get; set; }

            [YamlMember(Alias = "quantidade")]
            public string Quantidade { get; set; }
        }

        private class YamlReceitaValorNutricional
        {
            [YamlMember(Alias = "nome")]
            public string Nome { get; set; }

            [YamlMember(Alias = "dose")]
            public string Dose { get; set; }

            [YamlMember(Alias = "percentagem-do-vdr-adulto")]
            public int? PercentagemVdrAdulto { get; set; }
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
