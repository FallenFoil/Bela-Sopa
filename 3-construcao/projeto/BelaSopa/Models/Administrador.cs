using BelaSopa.Shared;
using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models
{
    public class Administrador
    {
        public static Administrador DeInfo(AdministradorInfo info)
        {
            return new Administrador
            {
                NomeDeUtilizador = info.NomeDeUtilizador,
                HashPalavraChave = Util.HashPalavraChave(info.PalavraChave)
            };
        }

        public int ID { get; set; }

        [Required]
        public string NomeDeUtilizador { get; set; }

        [Required, MinLength(32), MaxLength(32)]
        public byte[] HashPalavraChave { get; set; }
    }
}
