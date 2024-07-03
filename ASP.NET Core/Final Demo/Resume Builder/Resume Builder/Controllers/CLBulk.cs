﻿using Microsoft.AspNetCore.Authorization;
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
    public class CLBulk : ControllerBase
    {
        #region Private Member
        /// <summary>
        /// instance of BulkResumeGenerationService
        /// </summary>
        private readonly BLBulkResumeGenerationService _resumeGenerationService;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CLBulk"/> class.
        /// </summary>
        /// <param name="resumeGenerationService">The service responsible for generating bulk resumes.</param>
        /// <param name="csvToJSON">The service responsible for converting CSV files to JSON.</param>
        /// <param name="httpClient">The HTTP client for making HTTP requests.</param>
        public CLBulk(BLBulkResumeGenerationService resumeGenerationService)
        {
            _resumeGenerationService = resumeGenerationService;   
        }
        #endregion

        /// <summary>
        /// Generates resumes in bulk from a list of RES02 objects.
        /// </summary>
        /// <param name="lstRes">The list of RES02 objects representing resume data.</param>
        /// <returns>A zip file containing the generated resumes.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateResumes([FromBody] List<DTORES02> lstRES02)
        {
            try
            {
                byte[] zipFile = _resumeGenerationService.GenerateResumesFromJson(lstRES02);

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
    }
}
