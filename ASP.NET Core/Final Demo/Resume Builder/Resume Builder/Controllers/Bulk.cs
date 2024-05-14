using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resume_Builder.BL.Services;
using Resume_Builder.Models.DTO;

namespace Resume_Builder.Controllers
{
    /// <summary>
    /// Controller responsible for bulk operations related to resume generation.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class Bulk : ControllerBase
    {
        private readonly BulkResumeGenerationService _resumeGenerationService;
        private readonly CSVToJSON _csvToJSON;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bulk"/> class.
        /// </summary>
        /// <param name="resumeGenerationService">The service responsible for generating bulk resumes.</param>
        /// <param name="csvToJSON">The service responsible for converting CSV files to JSON.</param>
        /// <param name="httpClient">The HTTP client for making HTTP requests.</param>
        public Bulk(BulkResumeGenerationService resumeGenerationService, CSVToJSON csvToJSON, HttpClient httpClient)
        {
            _resumeGenerationService = resumeGenerationService;
            _csvToJSON = csvToJSON;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Generates resumes in bulk from a list of RES02 objects.
        /// </summary>
        /// <param name="lstRes">The list of RES02 objects representing resume data.</param>
        /// <returns>A zip file containing the generated resumes.</returns>
        [HttpPost]
        public async Task<IActionResult> GenerateResumes([FromBody] List<RES02> lstRes)
        {
            try
            {
                string json = JsonConvert.SerializeObject(lstRes);
                byte[] zipFile = _resumeGenerationService.GenerateResumesFromJson(json);

                if (zipFile != null)
                {
                    return File(zipFile, "application/zip", "resumes.zip");
                }
                else
                {
                    return StatusCode(500, "Failed to generate resumes.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Converts an uploaded Excel file to JSON format.
        /// </summary>
        /// <param name="file">The Excel file to convert.</param>
        /// <returns>The converted JSON data.</returns>
        [HttpPost("upload")]
        public async Task<IActionResult> ConvertFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            try
            {
                var json = await _csvToJSON.Convert(file);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
