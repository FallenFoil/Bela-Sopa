using BelaSopa.Models.Assistente;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace App.Shared
{
    public class RecursosEmbutidos
    {
        public static IEnumerable<Receita> CarregarReceitasDeExemplo()
        {
            var nomesRecursos =
                Assembly
                .GetEntryAssembly()
                .GetManifestResourceNames()
                .Where(name => name.StartsWith("App.Data.Receitas."));

            foreach (var nome in nomesRecursos)
            {
                using (var stream = Assembly.GetEntryAssembly().GetManifestResourceStream(nome))
                    yield return CarregarReceita(stream);
            }
        }

        public static IEnumerable<Ingrediente> CarregarIngredientesDeExemplo()
        {
            var nomesRecursos =
                Assembly
                .GetEntryAssembly()
                .GetManifestResourceNames()
                .Where(name => name.StartsWith("App.Data.Ingredientes."));

            foreach (var nome in nomesRecursos)
            {
                using (var stream = Assembly.GetEntryAssembly().GetManifestResourceStream(nome))
                    yield return CarregarIngrediente(stream);
            }
        }

        private static Receita CarregarReceita(Stream stream)
        {

        }

        private static Ingrediente CarregarIngrediente(Stream stream)
        {

        }
    }
}
