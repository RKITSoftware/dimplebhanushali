using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Services;
using Resume_Builder.Models;

namespace Resume_Builder.Controllers
{
    /// <summary>
    /// Controller to handle AI image and certificate generation requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CLAI : ControllerBase
    {
        #region Private Members
        /// <summary>
        /// SemaphoreSlim for Handling requests.
        /// </summary>
        private readonly SemaphoreSlim _requestSemaphore;

        /// <summary>
        /// Instance of AICertificate BL Class.
        /// </summary>
        private readonly BLAICertificate _aiCert;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialize dependencies.
        /// </summary>
        /// <param name="aiCert">Service for AI-based certificate generation.</param>
        public CLAI(BLAICertificate aiCert)
        {
            _requestSemaphore = new SemaphoreSlim(1, 1); // Allow only one request at a time
            _aiCert = aiCert;
        }
        #endregion

        /// <summary>
        /// Endpoint to generate a certificate PDF based on the provided request.
        /// </summary>
        /// <param name="request">Details for the certificate generation.</param>
        /// <returns>Returns the file path of the generated certificate.</returns>
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateCertificate([FromBody] DTOCER02 request)
        {

            // Acquire the semaphore before making the request
            await _requestSemaphore.WaitAsync();

            // Use the BL method to generate and save the certificate
            string filePath = await _aiCert.GenerateAndSaveCertificateAsync(request);

            // Return the file path as the response
            return Ok(new { FilePath = filePath });
        }

        /// <summary>
        /// Endpoint to generate an image based on the provided request and save it as a JPG file.
        /// </summary>
        /// <param name="request">Details for the image generation.</param>
        /// <returns>Returns the file path of the generated image.</returns>
        [HttpPost("generate-image")]
        public async Task<IActionResult> GenerateAndSaveImage([FromBody] string prompt)
        {
            await _requestSemaphore.WaitAsync();

            try
            {
                prompt += " Wallpaper";

                byte[] imageBytes = await _aiCert.GenerateImageFromPromptWithRetry(prompt);

                string imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                string fileName = $"{prompt}_{DateTime.Now:yyyyMMddHHmmss}.jpg";
                string filePath = Path.Combine(imageDirectory, fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                return Ok(new { FilePath = filePath });
            }
            finally
            {
                _requestSemaphore.Release();
            }
        }

    }
}
