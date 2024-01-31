using Exception_Demo.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Exception_Demo.Controllers
{
    /// <summary>
    /// Custom Controller for Exception
    /// </summary>
    public class CustomerController : ApiController
    {
        List<Customer> lstCustomer = new List<Customer>
        {
            new Customer { Id = 1, Name="Dimple", City="Rajkot", PhoneNumber=9624863508 },
            new Customer { Id = 2, Name="Pankaj", City="Mumbai", PhoneNumber=9988776655 },
            new Customer { Id = 3, Name="Ankit", City="London", PhoneNumber=9685741230 },
            new Customer { Id = 4, Name="Abc", City="America", PhoneNumber=9966330022 },
            new Customer { Id = 5, Name="Xyz", City="Ahmedabad", PhoneNumber=7788996655 },
        };

        /// <summary>
        /// Throws an InvalidOperationException for testing exception handling.
        /// </summary>
        [HttpGet]
        [Route("api/Error")]
        public IHttpActionResult ThrowError()
        {
            throw new InvalidOperationException("This is a Thrown Exception.");
        }
    }
}
