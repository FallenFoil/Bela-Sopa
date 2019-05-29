using System.Globalization;

namespace BelaSopa.Shared
{
    public static class Util
    {
        public static bool FairlyFuzzyContains(string textoOrigem, string textoContido)
        {
            return CultureInfo.CurrentCulture.CompareInfo.IndexOf(
                textoOrigem,
                textoContido,
                CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols
                ) >= 0;
        }

        public static bool TextoContemIngredienteFuzzy(string texto, string nomeIngrediente)
        {
            return true;
        }
    }
}
