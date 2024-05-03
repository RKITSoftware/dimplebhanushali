using Certificate_Generator.BL;
using Certificate_Generator.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Certificate_Generator.Controllers
{
    /// <summary>
    /// Certificate Controller For Generating Certificate
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        // Instance of BLCertificateGenerator
        private readonly BLCertificateGenerator _certificateGenerator;

        /// <summary>
        /// Constructor for Initialising BLCertificateGenerator
        /// </summary>
        /// <param name="certificateGenerator">Instance of BLCertificateGenerator</param>
        public CertificateController(BLCertificateGenerator certificateGenerator)
        {
            _certificateGenerator = certificateGenerator;
        }

        /// <summary>
        /// GenerateCertificate Method For generating Certificate
        /// </summary>
        /// <param name="generationData">DTOGEN01 Data For Certificate that would be Generated</param>
        /// <returns>Filepath of Downloaded Certificate</returns>
        [HttpPost]
        public IActionResult GenerateCertificate([FromBody] DTOGEN01 generationData)
        {
            // Call the GenerateCertificate method of BLCertificateGenerator
            var certificateContent = _certificateGenerator.GenerateCertificate(generationData);

            // Save the certificate to a file
            var filePath = _certificateGenerator.SaveCertificateToFile(generationData, certificateContent);

            // Return the file path for reference (you may modify this part based on your requirement)
            return Ok(new { FilePath = filePath });
        }

    }
}
