using System.Globalization;

namespace BelaSopa.Shared
{
    public static class Util
    {
        public static bool FuzzyContains(string textoOrigem, string textoContido)
        {
            return CultureInfo.CurrentCulture.CompareInfo.IndexOf(
                textoOrigem,
                textoContido,
                CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols
                ) >= 0;
        }
    }
}
