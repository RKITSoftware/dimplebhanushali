using ActionMethod.BL.Services;
using ActionMethod.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActionMethod.Controllers
{
    /// <summary>
    /// CLProduct Controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Constructor for Initialising _productRepository.
        /// </summary>
        /// <param name="productRepository">IProductRepository</param>
        public CLProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<PRO01>> GetAll()
        {
            return Ok(_productRepository.GetAll());
        }

        /// <summary>
        /// Retrieves a product by its ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<PRO01> GetById(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        [HttpPost("Create")]
        public ActionResult<PRO01> Create([FromBody] PRO01 product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            _productRepository.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.O01F01 }, product);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product data.</param>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PRO01 product)
        {
            if (product == null || product.O01F01 != id)
            {
                return BadRequest();
            }
            var existingProduct = _productRepository.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            _productRepository.Update(product);
            return NoContent();
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingProduct = _productRepository.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            _productRepository.Delete(id);
            return NoContent();
        }

        /// <summary>
        /// Returns an empty response with no content.
        /// </summary>
        [HttpGet("NoContent")]
        public EmptyResult NoContent()
        {
            return new EmptyResult();
        }
        //// JS


        /// <summary>
        /// Returns a plain text response.
        /// </summary>
        [HttpGet("TextContent")]
        public ContentResult TextContent()
        {
            return Content("This is a plain text response.");
        }

        /// <summary>
        /// Returns the content of a file as a downloadable attachment.
        /// </summary>
        [HttpGet("FileContent")]
        public FileContentResult FileContent()
        {
            //// Excel return
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "file.txt");
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", "file.txt");
        }

        /// <summary>
        /// Returns JSON data.
        /// </summary>
        [HttpGet("jsondata")]
        public JsonResult JsonData()
        {
            var data = new { Name = "Sample", Value = 123 };
            return new JsonResult(data);
        }

        /// <summary>
        /// Redirects to a specified URL.
        /// </summary>
        [HttpGet("Redirect")]
        public RedirectResult Redirect()
        {
            return Redirect("https://www.google.com");
        }

        /// <summary>
        /// Redirects to a specified route.
        /// </summary>
        [HttpGet("RedirectToRoute")]
        public RedirectToRouteResult RedirectToRoute()
        {
            return RedirectToRoute(new { controller = "CLProduct", action = "GetAll" });
        }

        /// <summary>
        /// Returns an unauthorized response.
        /// </summary>
        [HttpGet("UnAuthorized")]
        public UnauthorizedResult Unauthorized()
        {
            //// Use
            return Unauthorized();
        }
    }
}
