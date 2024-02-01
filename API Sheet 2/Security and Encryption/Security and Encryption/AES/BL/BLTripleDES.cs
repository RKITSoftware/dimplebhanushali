using System;
using System.Security.Cryptography;
using System.Text;

namespace AES.BL
{
    /// <summary>
    /// Implementation of TripleDES Encryption/Decryption
    /// </summary>
    public class BLTripleDES
    {
        private static TripleDESCryptoServiceProvider _objDes = new TripleDESCryptoServiceProvider();

        /// <summary>
        /// Encrypts the specified plain text using TripleDES.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <returns>The encrypted text.</returns>
        public static string Encrypt(string plainText)
        {
            // Convert the plain text to bytes
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // Create an encryptor using TripleDES
            using (ICryptoTransform encryptor = _objDes.CreateEncryptor())
            {
                // Encrypt the bytes using the encryptor
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                // Convert the encrypted bytes to base64 string
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        /// <summary>
        /// Decrypts the specified encrypted text using TripleDES.
        /// </summary>
        /// <param name="encryptedText">The encrypted text to decrypt.</param>
        /// <returns>The decrypted plain text.</returns>
        public static string Decrypt(string encryptedText)
        {
            // Convert the base64 string to encrypted bytes
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);

            // Create a decryptor using TripleDES
            using (ICryptoTransform decryptor = _objDes.CreateDecryptor())
            {
                // Decrypt the bytes using the decryptor
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedTextBytes, 0, encryptedTextBytes.Length);

                // Convert the decrypted bytes to UTF-8 string
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
