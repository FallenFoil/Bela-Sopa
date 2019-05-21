using System.Security.Cryptography;
using System.Text;

namespace App.Shared
{
    public static class Util
    {
        public static byte[] HashPalavraChave(string palavraChave)
        {
            using (var hash = SHA256.Create())
                return hash.ComputeHash(Encoding.UTF8.GetBytes(palavraChave));
        }
    }
}
