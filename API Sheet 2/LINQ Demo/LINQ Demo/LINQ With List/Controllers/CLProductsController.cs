using LINQ_With_List.BL;
using LINQ_With_List.Models;
using System.Web.Http;

namespace LINQ_With_List.Controllers
{
    /// <summary>
    /// Controller for managing products using a List.
    /// </summary>
    public class CLProductsController : ApiController
    {
        /// <summary>
        /// Gets all products from the List.
        /// </summary>
        /// <returns>All products in the List.</returns>
        [HttpGet]
        [Route("api/productslist")]
        public IHttpActionResult GetAllProducts()
        {
            return Ok(BLProductList.GetProducts());
        }

        /// <summary>
        /// Gets sorted products from the List based on the specified column and order.
        /// </summary>
        /// <param name="columnName">Name of the property to sort by.</param>
        /// <param name="ascending">True for ascending order, False for descending order.</param>
        /// <returns>Sorted products from the List.</returns>
        [HttpGet]
        [Route("api/productslist/sort")]
        public IHttpActionResult GetSortedProducts(string columnName, bool ascending = true)
        {

            return Ok(BLProductList.GetSortedProducts(columnName, ascending));
        }

        /// <summary>
        /// Gets a product from the List by ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID or NotFound if not found.</returns>
        [HttpGet]
        [Route("api/productslist/{id}")]
        public IHttpActionResult GetProductById(int id)
        {
            Product product = BLProductList.GetProductById(id);

            if (product != null)
            {
                return Ok(product);
            }

            return NotFound();
        }

        /// <summary>
        /// Creates a new product in the List.
        /// </summary>
        /// <param name="newProduct">The new product to add.</param>
        /// <returns>The created product.</returns>
        [HttpPost]
        [Route("api/productslist")]
        public IHttpActionResult CreateProduct([FromBody] Product newProduct)
        {

            newProduct.Id = BLProductList.ProductsList.Count + 1;
            BLProductList.AddProduct(newProduct);
            return Created(Request.RequestUri + "/" + newProduct.Id, newProduct);

        }

        /// <summary>
        /// Updates an existing product in the List.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="updatedProduct">The updated product data.</param>
        /// <returns>The updated product.</returns>
        [HttpPut]
        [Route("api/productslist/{id}")]
        public IHttpActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {

            BLProductList.UpdateProduct(id, updatedProduct);
            return Ok(updatedProduct);

        }

        /// <summary>
        /// Deletes a product from the List by ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A message indicating the deletion status.</returns>
        [HttpDelete]
        [Route("api/productslist/{id}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            BLProductList.DeleteProduct(id);
            return Ok($"Product with ID {id} has been deleted.");

        }
    }
}
