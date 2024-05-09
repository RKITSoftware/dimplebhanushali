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
        #region Private Member
        private static RSACryptoServiceProvider _objRsa = new RSACryptoServiceProvider();
        #endregion

        #region Public Methods
        /// <summary>
        /// Encrypts the specified plain text using RSA.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <returns>The encrypted text.</returns>
        public static string Encrypt(string plainText)
        {
            // Convert the plain text to bytes
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // Encrypt the bytes using RSA
            byte[] encryptedBytes = _objRsa.Encrypt(plainBytes, true);

            // Convert the encrypted bytes to base64 string
            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Decrypts the specified encrypted text using RSA.
        /// </summary>
        /// <param name="encryptedText">The encrypted text to decrypt.</param>
        /// <returns>The decrypted plain text.</returns>
        public static string Decrypt(string encryptedText)
        {
            // Convert the base64 string to encrypted bytes
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);

            // Decrypt the bytes using RSA
            byte[] decryptedBytes = _objRsa.Decrypt(encryptedTextBytes, true);

            // Convert the decrypted bytes to UTF-8 string
            return Encoding.UTF8.GetString(decryptedBytes);
        }
        #endregion
    }
}
