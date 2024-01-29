using System;
using System.Security.Cryptography;
using System.Text;

namespace AES.BL
{
    public class BLTripleDES
    {
        private static TripleDESCryptoServiceProvider _objDes = new TripleDESCryptoServiceProvider();

        public static string Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            using (ICryptoTransform encryptor = _objDes.CreateEncryptor())
            {
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes,0,plainBytes.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public static string Decrypt(string encryptedText) 
        {
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);

            using (ICryptoTransform decryptor = _objDes.CreateDecryptor())
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedTextBytes,0,encryptedTextBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
