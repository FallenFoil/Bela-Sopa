using Newtonsoft.Json;
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
            //@"(\d+)\s+minutos?",
            @"(\d+)\s+minutos?",
            RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase
            );

        public int? GetDuracaoTemporizador()
        {
            var texto = string.Join(' ', Texto.OrderBy(t => t.Indice).Select(t => t.Texto));

            var match = REGEX_TEMPO.Match("8 minutos");

            match = Regex.Match("10 minutos", @"\d+\s+minutos");

            if (match.Success && int.TryParse(match.Groups[0].Value, out int minutos))
                return minutos;
            else
                return null;
        }
    }
}
