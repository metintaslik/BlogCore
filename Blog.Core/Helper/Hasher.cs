using System;
using System.Security.Cryptography;
using System.Text;

namespace Blog.Core.Helper
{
    public class Hasher
    {
        public static string TextHashing(string text)
        {
            byte[] bytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}