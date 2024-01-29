using System;
using System.Web.Http;

namespace Dynamic_Values.Controllers
{
    /// <summary>
    /// Controller for handling generic collections.
    /// </summary>
    public class GenericCollectionController : ApiController
    {
        /// <summary>
        /// Adds a value to the appropriate list based on the input type.
        /// </summary>
        /// <param name="userInput">The input value to be added to the list.</param>
        /// <returns>An IHttpActionResult indicating the result of the operation.</returns>
        [HttpPost]
        public IHttpActionResult Post([FromBody] dynamic userInput)
        {
            try
            {
                if (userInput != null)
                {
                    if (int.TryParse(userInput.ToString(), out int intValue))
                    {
                        ListStorage.NumList.Add(intValue);
                        return Ok("Value Added to Int List");
                    }
                    else if (bool.TryParse(userInput.ToString(), out bool boolValue))
                    {
                        ListStorage.BoolList.Add(boolValue);
                        return Ok("Value Added to Bool List");
                    }
                    else
                    {
                        // For non-integer and non-boolean values, add to the string List only
                        string stringValue = userInput.ToString();
                        ListStorage.StrList.Add(stringValue);
                        return Ok("Value Added to String List");
                    }
                }
                else
                {
                    return BadRequest("Invalid input. Please provide a non-null value.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Gets the list of integers.
        /// </summary>
        /// <returns>An IHttpActionResult containing the list of integers.</returns>
        [HttpGet]
        [Route("api/IntList")]
        public IHttpActionResult GetIntList()
        {
            return Ok(ListStorage.NumList);
        }

        /// <summary>
        /// Gets the list of strings.
        /// </summary>
        /// <returns>An IHttpActionResult containing the list of strings.</returns>
        [HttpGet]
        [Route("api/strList")]
        public IHttpActionResult GetstrList()
        {
            return Ok(ListStorage.StrList);
        }

        /// <summary>
        /// Gets the list of booleans.
        /// </summary>
        /// <returns>An IHttpActionResult containing the list of booleans.</returns>
        [HttpGet]
        [Route("api/boolList")]
        public IHttpActionResult GetboolList()
        {
            return Ok(ListStorage.BoolList);
        }
    }
}
