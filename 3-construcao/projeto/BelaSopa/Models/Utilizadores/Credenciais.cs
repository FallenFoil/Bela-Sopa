using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace BelaSopa.Models.Utilizadores
{
    public class Credenciais
    {
        [Display(Name = "Nome de utilizador")]
        [Required(ErrorMessage = "O nome de utilizador é obrigatório.")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "O nome de utilizador deve ter entre 4 e 32 carateres.")]
        public string NomeDeUtilizador { get; set; }

        [Display(Name = "Palavra-passe")]
        [Required(ErrorMessage = "A palavra-passe é obrigatória.")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "A palavra-passe deve ter entre 4 e 32 carateres.")]
        [DataType(DataType.Password, ErrorMessage = "A palavra-passe é inválida.")]
        public string PalavraPasse { get; set; }

        public byte[] ComputarHashPalavraPasse()
        {
            return ComputarHashPalavraPasse(this.PalavraPasse);
        }

        public static byte[] ComputarHashPalavraPasse(string palavraPasse)
        {
            using (var hash = SHA256.Create())
                return hash.ComputeHash(Encoding.UTF8.GetBytes(palavraPasse));
        }
    }
}
