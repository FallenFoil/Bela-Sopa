using System;
using System.Security.Cryptography;
using System.Text;

namespace BelaSopa.Shared
{
    public class UtilPalavrasChave
    {
        public static byte[] hash(string palavraChave)
        {
            using (var hash = SHA256.Create())
                return hash.ComputeHash(Encoding.UTF8.GetBytes(palavraChave));
        }
    }

    public class Encript
    {
        //retorna a string que equivale ao encriptamento
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }

        //função hash através de uma determinada password            
        public static String HashPassword(string password)
        {
            using (var md5Hash = MD5.Create())
                return GetMd5Hash(md5Hash, password);
        }

        //verifica se é a mesma string
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            return StringComparer.OrdinalIgnoreCase.Compare(
                GetMd5Hash(md5Hash, input), hash
                ) == 0;
        }
    }
}
