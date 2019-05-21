using BelaSopa.Models.Assistente;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace App.Shared
{
    public class RecursosEmbutidos
    {
        public static IEnumerable<Receita> CarregarReceitasDeExemplo()
        {
            return CarregarRecursos("App.Data.Receitas.", stream =>
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var yamlStream = new YamlStream();
                    yamlStream.Load(reader);

                    var root = yamlStream.Documents[0].RootNode;

                    return new Receita();
                }
            });
        }

        public static IEnumerable<Ingrediente> CarregarIngredientesDeExemplo()
        {
            return CarregarRecursos("App.Data.Ingredientes.", stream =>
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var yamlStream = new YamlStream();
                    yamlStream.Load(reader);

                    var root = yamlStream.Documents[0].RootNode;

                    return new Ingrediente();
                }
            });
        }

        private static IEnumerable<T> CarregarRecursos<T>(string prefixoNome, Func<Stream, T> carregador)
        {
            var nomesRecursos =
                Assembly
                .GetEntryAssembly()
                .GetManifestResourceNames()
                .Where(name => name.StartsWith(prefixoNome));

            foreach (var nome in nomesRecursos)
            {
                using (var stream = Assembly.GetEntryAssembly().GetManifestResourceStream(nome))
                    yield return carregador(stream);
            }
        }
    }
}
