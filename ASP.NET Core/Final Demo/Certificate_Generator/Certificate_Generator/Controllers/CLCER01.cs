using Certificate_Generator.BL;
using Certificate_Generator.Models.POCO;
using Microsoft.AspNetCore.Mvc;

namespace Certificate_Generator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLCER01Controller : ControllerBase
    {
        private readonly BLCER01Handler _certificateHandler;

        public CLCER01Controller(BLCER01Handler certificateHandler)
        {
            _certificateHandler = certificateHandler;
        }

        [HttpPost, Route("Create")]
        public IActionResult CreateCertificateTemplate([FromBody] CER01 template)
        {
            var response = _certificateHandler.CreateCertificateTemplate(template);
            return Ok(response);
        }

        [HttpPost, Route("Temp")]
        public IActionResult GenerateTestCertificateTemplates()
        {
            _certificateHandler.GenerateTestCertificateTemplates();
            return Ok("Fake Certificates Generated Successfully.");
        }

        [HttpGet, Route("GetAll")]
        public IActionResult GetAllCertificateTemplates()
        {
            var response = _certificateHandler.GetAllCertificateTemplates();
            return Ok(response);
        }

        [HttpGet, Route("Get/{id}")]
        public IActionResult GetCertificateTemplateById(int id)
        {
            var response = _certificateHandler.GetCertificateTemplateById(id);
            return Ok(response);
        }

        [HttpPut, Route("Update")]
        public IActionResult UpdateCertificateTemplate([FromBody] CER01 template)
        {
            var response = _certificateHandler.UpdateCertificateTemplate(template);
            return Ok(response);
        }

        [HttpDelete, Route("Delete/{id}")]
        public IActionResult DeleteCertificateTemplate(int id)
        {
            var response = _certificateHandler.DeleteCertificateTemplate(id);
            return Ok(response);
        }
    }
}
