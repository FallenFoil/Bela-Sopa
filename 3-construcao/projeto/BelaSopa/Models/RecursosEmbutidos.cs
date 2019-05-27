using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace BelaSopa.Models
{
    public class RecursosEmbutidos
    {
        public static void CarregarReceitasDeExemplo(BelaSopaContext context)
        {
            CarregarRecursos("BelaSopa.Data.Receitas.", stream =>
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var deserializer =
                        new DeserializerBuilder()
                        .WithTagMapping("tag:yaml.org,2002:binary", typeof(byte[]))
                        .WithTypeConverter(new ByteArrayConverter())
                        .Build();

                    var yamlReceita = deserializer.Deserialize<YamlReceita>(reader);
                }
            });
        }

        //public static IEnumerable<Ingrediente> CarregarIngredientesDeExemplo(BelaSopaContext context)
        //{
        //    return CarregarRecursos("BelaSopa.Data.Ingredientes.", stream =>
        //    {
        //        using (var reader = new StreamReader(stream, Encoding.UTF8))
        //        {
        //            var yamlStream = new YamlStream();
        //            yamlStream.Load(reader);

        //            var root = yamlStream.Documents[0].RootNode;

        //            return new Ingrediente();
        //        }
        //    });
        //}

        private static void CarregarRecursos(string prefixoNome, Action<Stream> carregador)
        {
            var nomesRecursos =
                Assembly
                .GetEntryAssembly()
                .GetManifestResourceNames()
                .Where(name => name.StartsWith(prefixoNome));

            foreach (var nome in nomesRecursos)
            {
                using (var stream = Assembly.GetEntryAssembly().GetManifestResourceStream(nome))
                    carregador(stream);
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
