using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BelaSopa.Models
{
    public abstract class Utilizador
    {
        public int Id { get; set; }

        [Required, StringLength(32, MinimumLength = 4)]
        public string NomeDeUtilizador { get; set; }

        [Required, MinLength(32), MaxLength(32)]
        public byte[] HashPalavraPasse { get; set; }

        public static Utilizador GetComNomeDeUtilizador(BelaSopaDbContext context, string nomeDeUtilizador)
        {
            return (Utilizador)
                context.Clientes.SingleOrDefault(c => c.NomeDeUtilizador == nomeDeUtilizador) ??
                context.Administradores.SingleOrDefault(a => a.NomeDeUtilizador == nomeDeUtilizador);
        }
    }
}
