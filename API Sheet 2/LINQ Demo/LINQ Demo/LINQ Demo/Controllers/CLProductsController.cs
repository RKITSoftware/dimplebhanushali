using LINQ_Demo.BL;
using LINQ_Demo.Models;
using System.Data;
using System.Web.Http;

namespace LINQ_Demo.Controllers
{
    /// <summary>
    /// Controller for managing products using LINQ_Demo.
    /// </summary>
    public class CLProductsController : ApiController
    {
        /// <summary>
        /// Instance of BLProduct with Data Table.
        /// </summary>
        private BLProducts _objBlProd;

        /// <summary>
        /// Constructor for Initialising new Instance of BL Class with Data Table.
        /// </summary>
        public CLProductsController()
        {
            _objBlProd = new BLProducts();
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        [HttpGet]
        [Route("api/products")]
        public IHttpActionResult GetAllProducts()
        {
            DataTable allProducts = _objBlProd.GetProducts();
            return Ok(allProducts);
        }

        /// <summary>
        /// Gets sorted products based on the specified column.
        /// </summary>
        [HttpGet]
        [Route("api/products/sort")]
        public IHttpActionResult GetSortedProducts(string columnName, bool ascending = true)
        {

            DataTable sortedProducts = _objBlProd.GetSortedProducts(columnName, ascending);
            return Ok(sortedProducts);

        }

        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        [HttpGet]
        [Route("api/products/{id}")]
        public IHttpActionResult GetProductById(int id)
        {
            DataRow product = _objBlProd.GetProductById(id);
            return Ok(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        [HttpPost]
        [Route("api/products")]
        public IHttpActionResult CreateProduct([FromBody] Product newProduct)
        {
            _objBlProd.AddProduct(newProduct);
            return Created(Request.RequestUri + "/" + newProduct.Id, newProduct);

        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        [HttpPut]
        [Route("api/products/{id}")]
        public IHttpActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {

            _objBlProd.UpdateProduct(id, updatedProduct);
            return Ok(updatedProduct);

        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        [HttpDelete]
        [Route("api/products/{id}")]
        public IHttpActionResult DeleteProduct(int id)
        {

            _objBlProd.DeleteProduct(id);
            return Ok($"Product with ID {id} has been deleted.");

        }

        [HttpGet]
        [Route("GetProductOrders")]
        public IHttpActionResult GetProductOrders()
        {
            return Ok(_objBlProd.GetProductOrders());
        }
    }
}
