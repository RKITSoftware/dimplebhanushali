using System;
using System.Drawing.Printing;
using System.Security.Cryptography;
using System.Text;

namespace AES.BL
{
    /// <summary>
    /// Implementation of RSA Algorithm
    /// </summary>
    public class BLRSA
    {
        private static RSACryptoServiceProvider _objRsa = new RSACryptoServiceProvider();    

        public static string Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] encryptedBytes = _objRsa.Encrypt(plainBytes,true);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string encryptedText) 
        {
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);
            byte[] decryptedBytes = _objRsa.Decrypt(encryptedTextBytes,true);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}