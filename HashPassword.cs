/*
 * Author: Satyam Khanna
 * Email: satyamkhanna66@gmail.com
 */

//Include Statements
using System;
using System.Text;
using System.Security.Cryptography;

namespace PasswordHasher
{
    public class HashPassword
    {
        private string _salt = "*1234567890!@#$%^&*()14344*";

        public HashPassword()
        {

        }
                
        internal string Crypto(string text)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(_salt);

            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(_salt));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider TripleDesProvider = new TripleDESCryptoServiceProvider();
            TripleDesProvider.Key = keyArray;
            TripleDesProvider.Mode = CipherMode.ECB;
            TripleDesProvider.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = TripleDesProvider.CreateEncryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        internal string Decrypto(string text)
        {
            try
            {

                var hashmd5 = new MD5CryptoServiceProvider();
                byte[] toEncryptArray = Convert.FromBase64String(text);

                byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(_salt));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider TripleDesProvider = new TripleDESCryptoServiceProvider();
                TripleDesProvider.Key = keyArray;
                TripleDesProvider.Mode = CipherMode.ECB;
                TripleDesProvider.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = TripleDesProvider.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                TripleDesProvider.Clear();

                return Encoding.UTF8.GetString(resultArray);
                //return Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
