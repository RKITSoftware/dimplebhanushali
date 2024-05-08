using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Services;
using Resume_Builder.Helpers;

namespace Resume_Builder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AB : ControllerBase
    {
        private readonly ResumeGenerationService _resumeGenerationService;

        public AB(ResumeGenerationService resumeGenerationService)
        {
            _resumeGenerationService = resumeGenerationService;
        }

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
