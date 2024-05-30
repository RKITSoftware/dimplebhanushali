using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Services;
using Resume_Builder.Helpers;

namespace Resume_Builder.Controllers
{
    /// <summary>
    /// Controller responsible for handling resume generation requests.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CLResumeGeneration : ControllerBase
    {
        #region Private Member
        /// <summary>
        /// instance of ResumeGenrationService
        /// </summary>
        private readonly BLResumeGenerationService _resumeGenerationService;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CLResumeGeneration"/> class.
        /// </summary>
        /// <param name="resumeGenerationService">The service responsible for generating resumes.</param>
        public CLResumeGeneration(BLResumeGenerationService resumeGenerationService)
        {
            _resumeGenerationService = resumeGenerationService;
        }
        #endregion

        /// <summary>
        /// Generates a resume for the authenticated user.
        /// </summary>
        /// <returns>A file containing the generated resume in PDF format.</returns>
        [HttpPost("GenerateResume")]
        public IActionResult GenerateResume()
        {
            try
            {
                int userId = HttpContext.GetUserIdFromClaims();
                byte[] resumeBytes = _resumeGenerationService.GenerateResume(userId);
                return File(resumeBytes, "application/pdf", "resume.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
