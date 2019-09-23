using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ReceitaEtiqueta
    {
        [Key]
        public int ReceitaId { get; set; }

        [Key]
        public int EtiquetaId { get; set; }

        public virtual Receita Receita { get; set; }

        public virtual Etiqueta Etiqueta { get; set; }
    }
}
