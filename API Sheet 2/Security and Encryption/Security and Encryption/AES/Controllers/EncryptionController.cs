using AES.BL;
using System.Web.Http;

namespace AES.Controllers
{
    [RoutePrefix("api")]
    public class EncryptionController : ApiController
    {
        /// <summary>
        /// Encryption by AES Algorithm
        /// </summary>
        /// <returns> Ciphertext </returns>
        [HttpGet,Route("AES/Encrypt/{plainText}")]
        public IHttpActionResult EncryptionAES([FromUri] string plainText)
        {
            return Ok(BLAesEncryptionService.Encrypt(plainText));
        }

        /// <summary>
        /// Decryption by AES Algorithm
        /// </summary>
        /// <param name="cipherText"> Generated ciphertext in previous method </param>
        /// <returns> Plain text </returns>
        [HttpGet,Route("AES/Decrypt")]
        public IHttpActionResult DecryptionAES(string cipherText)
        {
            return Ok(BLAesEncryptionService.Decrypt(cipherText));
        }

        /// <summary>
        /// Encryption by Triple DES Algorithm
        /// </summary>
        /// <returns> Ciphertext </returns>
        [HttpGet,Route("DES/Encrypt/{plainText}")]
        public IHttpActionResult EncryptionDES([FromUri] string plainText)
        {
            return Ok(BLTripleDES.Encrypt(plainText));
        }

        /// <summary>
        /// Decryption by Triple DES Algorithm
        /// </summary>
        /// <param name="cipherText"> Generated ciphertext in previous method </param>
        /// <returns> Plain text </returns>
        [HttpGet,Route("DES/Decrypt")]
        public IHttpActionResult DecryptionDES(string cipherText)
        {
            return Ok(BLTripleDES.Decrypt(cipherText));
        }


        /// <summary>
        /// Encryption by RSA Algorithm
        /// </summary>
        /// <returns> Ciphertext </returns>
        [HttpGet,Route("RSA/Encrypt/{plainText}")]
        public IHttpActionResult EncryptionRSA([FromUri] string plainText)
        {
            return Ok(BLRSA.Encrypt(plainText));
        }

        /// <summary>
        /// Decryption by RSA Algorithm
        /// </summary>
        /// <param name="cipherText"> Generated ciphertext in previous method </param>
        /// <returns> Plain text </returns>
        [HttpGet,Route("RSA/Decrypt")]
        public IHttpActionResult DecryptionRSA(string cipherText)
        {
            return Ok(BLRSA.Decrypt(cipherText));
        }
    }
}
