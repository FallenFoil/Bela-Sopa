using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace App.Shared
{

        public class Encript
        {

            //retorna a string que equivale ao encriptamento
            public static string GetMd5Hash(MD5 md5Hash, string input)
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }

            //função hash através de uma determinada password            
            public static String HashPassword(string password)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, password);
                    return hash;
                }
            }

            //verifica se é a mesma string
            public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
            {
                string hashOfInput = GetMd5Hash(md5Hash, input);
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
}
