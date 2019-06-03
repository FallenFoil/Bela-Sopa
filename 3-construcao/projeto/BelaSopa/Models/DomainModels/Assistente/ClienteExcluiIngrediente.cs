using BelaSopa.Models.DomainModels.Utilizadores;
using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ClienteExcluiIngrediente
    {
        [Key]
        public int ClienteId { set; get; }

        [Key]
        public int IngredienteId { set; get; }

        public virtual Cliente Cliente { get; set; }

        public virtual Ingrediente Ingrediente { get; set; }
    }
}
