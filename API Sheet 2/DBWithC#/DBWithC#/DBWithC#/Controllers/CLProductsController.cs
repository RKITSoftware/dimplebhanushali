using DBWithC_.BL;
using DBWithC_.Models;
using System.Runtime.InteropServices;
using System.Web.Http;

namespace DBWithC_.Controllers
{
    [RoutePrefix("api")]
    public class CLProductsController : ApiController
    {
        private static BLProduct _products;
        static CLProductsController()
        {
            _products = new BLProduct();
        }

        [HttpGet,Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(_products.GetAll());
        }

        [HttpGet,Route("GetProduct/{id}")]
        public IHttpActionResult GetProduct([FromUri] int id)
        {
            if (id == null)
            {
                return BadRequest($"Product With Id => {id} Not Found");
            }
            return Ok(_products.GetProduct(id));
        }

        [HttpPost,Route("AddProduct")]
        public IHttpActionResult AddProduct(prdct01 objProduct)
        {
            if (objProduct == null)
            {
                return BadRequest($"Invalid Details");
            }
            return Ok(_products.AddProduct(objProduct));
        }

        [HttpPut,Route("EditProduct/{id}")]
        public IHttpActionResult EditProduct([FromUri] int id, prdct01 objProduct)
        {
            if (id == null)
            {
                return BadRequest($"Invalid Id");
            }
            return Ok(_products.EditProduct(id,objProduct));
        }

        [HttpDelete,Route("DeleteProduct/{id}")]
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
