using iTextSharp.tool.xml.css;
using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Services;
using System;
using System.IO;

namespace Resume_Builder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Bulk : ControllerBase
    {
        private readonly BulkResumeGenerationService _resumeGenerationService;
        private readonly CSVToJSON _csvToJSON;

        public Bulk(BulkResumeGenerationService resumeGenerationService, CSVToJSON csvToJSON)
        {
            _resumeGenerationService = resumeGenerationService;
            _csvToJSON = csvToJSON;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateResumes( IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var json = await _csvToJSON.Convert(file);
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
