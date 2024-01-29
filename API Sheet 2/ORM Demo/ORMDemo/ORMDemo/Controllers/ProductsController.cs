using ORMDemo.BL;
using ORMDemo.Models;
using System;
using System.Web.Http;

namespace ORMDemo.Controllers
{
    /// <summary>
    /// API Controller for managing product-related operations.
    /// </summary>
    [RoutePrefix("api")]
    public class ProductsController : ApiController
    {
        private BLProduct _products = new BLProduct();

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>List of products.</returns>
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(_products.GetAllProducts());
        }

        /// <summary>
        /// Retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">Identifier of the product.</param>
        /// <returns>The product with the specified identifier.</returns>
        [HttpGet]
        [Route("Product/{id}")]
        public IHttpActionResult GetProduct(int id)
        {
            if (id == null)
            {
                throw new InvalidOperationException("Invalid ID");
            }
            return Ok(BLProduct.GetProduct(id));
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="objProduct">Product to be added.</param>
        /// <returns>The added product.</returns>
        [HttpPost]
        [Route("AddProduct")]
        public IHttpActionResult AddProduct(prdct01 objProduct)
        {
            _products.AddProduct(objProduct);
            return Ok(objProduct);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">Identifier of the product to be updated.</param>
        /// <param name="objProduct">Updated product data.</param>
        /// <returns>The updated product.</returns>
        [HttpPut]
        [Route("EditProduct/{id}")]
        public IHttpActionResult EditProduct(int id, prdct01 objProduct)
        {
            if (id == null)
            {
                throw new InvalidOperationException("Invalid ID");
            }
            _products.EditProduct(id, objProduct);
            return Ok(objProduct);
        }

        /// <summary>
        /// Removes a product by its identifier.
        /// </summary>
        /// <param name="id">Identifier of the product to be removed.</param>
        /// <returns>The removed product.</returns>
        [HttpDelete]
        [Route("EditProduct/{id}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            if (id == null)
            {
                throw new InvalidOperationException("Invalid ID");
            }
            return Ok(_products.RemoveProduct(id));
        }
    }
}