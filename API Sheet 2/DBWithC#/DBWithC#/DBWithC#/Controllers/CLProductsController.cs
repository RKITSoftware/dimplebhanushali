using DBWithC_.BL;
using DBWithC_.Models;
using System.Web.Http;

namespace DBWithC_.Controllers
{
    /// <summary>
    /// Controller for handling product-related API operations.
    /// </summary>
    [RoutePrefix("api")]
    public class CLProductsController : ApiController
    {
        private static BLProduct _products;

        /// <summary>
        /// Static constructor to initialize the business logic class.
        /// </summary>
        static CLProductsController()
        {
            _products = new BLProduct();
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>List of products.</returns>
        [HttpGet, Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(_products.GetAll());
        }

        /// <summary>
        /// Retrieves a specific product by its ID.
        /// </summary>
        /// <param name="id">ID of the product.</param>
        /// <returns>The product with the specified ID.</returns>
        [HttpGet, Route("GetProduct/{id}")]
        public IHttpActionResult GetProduct([FromUri] int id)
        {
            if (id == null)
            {
                return BadRequest($"Product With Id => {id} Not Found");
            }
            return Ok(_products.GetProduct(id));
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="objProduct">The product to be added.</param>
        /// <returns>The added product with updated information (e.g., ID).</returns>
        [HttpPost, Route("AddProduct")]
        public IHttpActionResult AddProduct(prdct01 objProduct)
        {
            if (objProduct == null)
            {
                return BadRequest($"Invalid Details");
            }
            return Ok(_products.AddProduct(objProduct));
        }

        /// <summary>
        /// Edits an existing product.
        /// </summary>
        /// <param name="id">ID of the product to be updated.</param>
        /// <param name="objProduct">Updated product information.</param>
        /// <returns>The updated product.</returns>
        [HttpPut, Route("EditProduct/{id}")]
        public IHttpActionResult EditProduct([FromUri] int id, prdct01 objProduct)
        {
            if (id == null)
            {
                return BadRequest($"Invalid Id");
            }
            return Ok(_products.EditProduct(id, objProduct));
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">ID of the product to be deleted.</param>
        /// <returns>A message indicating the success of the deletion operation.</returns>
        [HttpDelete, Route("DeleteProduct/{id}")]
        public IHttpActionResult DeleteProduct([FromUri] int id)
        {
            if (id == null)
            {
                return BadRequest($"Invalid Id");
            }
            return Ok(_products.DeleteProduct(id));
        }
    }
}
