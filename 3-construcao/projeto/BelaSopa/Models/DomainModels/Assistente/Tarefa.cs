using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Tarefa
    {
        [Key]
        public int TarefaId { get; set; }

        [Required]
        public int Indice { get; set; }

        public virtual ICollection<TextoTarefa> Texto { get; set; } = new List<TextoTarefa>();

        [Required]
        public int ProcessoId { get; set; }

        public virtual Processo Processo { get; set; }

        private static readonly Regex REGEX_TEMPO = new Regex(
            @"(\d+)\s+minutos?",
            RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase
            );

        public string GetDuracaoTemporizador()
        {
            var texto = string.Join(' ', Texto.OrderBy(t => t.Indice).Select(t => t.Texto));

            var matches = REGEX_TEMPO.Matches(texto);

            if (matches.Count == 1 && int.TryParse(matches[0].Groups[1].Value, out int minutos) && minutos > 0)
                return minutos.ToString();
            else
                return null;
        }

        public IEnumerator GetEnumerator() {
            return Texto.GetEnumerator();
        }
    }
}
