using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace BelaSopa.Models.DomainModels.Utilizadores
{
    public abstract class Utilizador
    {
        public int Id { get; set; }

        [Required, StringLength(32, MinimumLength = 4)]
        public string NomeDeUtilizador { get; set; }

        [Required, MinLength(32), MaxLength(32)]
        public byte[] HashPalavraPasse { get; set; }

        public static byte[] ComputarHashPalavraPasse(string palavraPasse)
        {
            using (var hash = SHA256.Create())
                return hash.ComputeHash(Encoding.UTF8.GetBytes(palavraPasse));
        }
    }
}
