using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Utilizadores
{
    public abstract class Utilizador
    {
        [Key]
        public int UtilizadorId { get; set; }

        [Required, StringLength(32, MinimumLength = 4)]
        public string NomeDeUtilizador { get; set; }

        [Required, MinLength(32), MaxLength(32)]
        public byte[] HashPalavraPasse { get; set; }
    }
}
