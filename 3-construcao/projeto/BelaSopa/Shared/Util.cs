using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BelaSopa.Shared
{
    public static class Util
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector)
        {
            return enumerable.GroupBy(keySelector).Select(grp => grp.First());
        }

        public static string BytesToBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes, Base64FormattingOptions.None);
        }

        public static bool FuzzyEquals(string texto1, string texto2)
        {
            return CultureInfo.CurrentCulture.CompareInfo.Compare(
                texto1,
                texto2,
                CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols
                ) == 0;
        }

        public static bool FuzzyContains(string textoOrigem, string textoContido)
        {
            return CultureInfo.CurrentCulture.CompareInfo.IndexOf(
                textoOrigem,
                textoContido,
                CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols
                ) >= 0;
        }

        public static List<(string Titulo, List<string> Paragrafos)> FormatarTextoComSeccoes(string texto)
        {
            var seccoes = new List<(string Titulo, List<string> Paragrafos)>();

            foreach (var parte in texto.Split('\n'))
            {
                var trimmed = parte.Trim();

                if (trimmed.Length == 0)
                    continue;

                if (trimmed.First() == '[' && trimmed.Last() == ']')
                {
                    // título da secção

                    seccoes.Add((trimmed.Substring(1, trimmed.Length - 2), new List<string>()));
                }
                else
                {
                    // parágrafo

                    if (seccoes.Count == 0)
                        seccoes.Add((null, new List<string>()));

                    seccoes.Last().Paragrafos.Add(trimmed);
                }
            }

            return seccoes;
        }
    }
}
