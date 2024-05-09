using iTextSharp.tool.xml.css;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resume_Builder.BL.Services;
using Resume_Builder.Models.DTO;
using System;
using System.IO;
using System.Text;

namespace Resume_Builder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Bulk : ControllerBase
    {
        private readonly BulkResumeGenerationService _resumeGenerationService;
        private readonly CSVToJSON _csvToJSON;
        private readonly HttpClient _httpClient;

        public Bulk(BulkResumeGenerationService resumeGenerationService, CSVToJSON csvToJSON, HttpClient httpClient)
        {
            _resumeGenerationService = resumeGenerationService;
            _csvToJSON = csvToJSON;
            _httpClient = httpClient;
        }

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
