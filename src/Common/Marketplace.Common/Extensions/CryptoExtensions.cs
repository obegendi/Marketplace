using System.Security.Cryptography;
using System.Text;

namespace Marketplace.Common.Extensions
{
    public static class CryptoExtensions
    {
        public static string AsMd5(this string input)
        {
            using (var md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash. 
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes 
                // and create a string. 
                var sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string. 
                for (var i = 0; i < data.Length; i++)
                    sBuilder.Append(data[i].ToString("x2"));
                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
    }

}
