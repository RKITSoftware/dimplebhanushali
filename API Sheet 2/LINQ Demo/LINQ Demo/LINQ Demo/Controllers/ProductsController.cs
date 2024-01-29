using LINQ_Demo.BL;
using System;
using System.Data;
using System.Linq;
using System.Web.Http;
using LINQ_Demo.Models;

namespace LINQ_Demo.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        [Route("api/products")]
        public IHttpActionResult GetAllProducts()
        {
            try
            {
                DataTable allProducts = BLProducts.GetProducts();
                return Ok(allProducts);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/products/sort")]
        public IHttpActionResult GetSortedProducts(string columnName, bool ascending = true)
        {
            try
            {
                DataTable sortedProducts = BLProducts.GetSortedProducts(columnName, ascending);
                return Ok(sortedProducts);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/products/{id}")]
        public IHttpActionResult GetProductById(int id)
        {
            try
            {
                DataRow product = BLProducts.GetProductById(id);

                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/products")]
        public IHttpActionResult CreateProduct([FromBody] Product newProduct)
        {
            try
            {
                BLProducts.AddProduct(newProduct);
                return Created(Request.RequestUri + "/" + newProduct.Id, newProduct);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("api/products/{id}")]
        public IHttpActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            try
            {
                BLProducts.UpdateProduct(id, updatedProduct);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("api/products/{id}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            try
            {
                BLProducts.DeleteProduct(id);
                return Ok($"Product with ID {id} has been deleted.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
