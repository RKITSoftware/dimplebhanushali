//#define DEBUG_MODE
using System.Diagnostics;
using System.Web.Http;

namespace Debug_Points.Controllers
{
    /// <summary>
    /// Controller for handling default API requests.
    /// </summary>
    public class DefaultController : ApiController
    {

        /// <summary>
        /// Controller for handling default API requests.
        /// </summary>
        public IHttpActionResult Get()
        {
            int num = 11;  // Set breakpoint here

            // Insert a tracepoint here
            Trace.WriteLine($"Value of Number => {num}");

            //Temporary BreakPoint
            int sum = AddIntegers(num, 5);

            //Conditional BreakPoint
            for (int i = 0; i < 10; i++)
            
            {
                Trace.WriteLine(i);
            }

            //Dependent BreakPoint
            Trace.WriteLine(num);

            // Simulate some processing
            int result = ProcessData(num);

#if DEBUG_MODE
            // Code Included When DEBUG_MODE is Defined
            int debugVariable = 100;
            System.Diagnostics.Debug.WriteLine($"Debug variable: {debugVariable}");
#else
            // Code included when DEBUG_MODE is not defined
            int releaseVariable = 200;
            System.Diagnostics.Debug.WriteLine($"Release variable: {releaseVariable}");
#endif

            return Ok(result);
        }


        /// <summary>
        /// Adds two integers.
        /// </summary>
        /// <param name="num">The first integer.</param>
        /// <param name="num2">The second integer to be added.</param>
        /// <returns>The sum of the two integers.</returns>
        private int AddIntegers(int num, int num2)
        {
            return num + num2;
        }

        /// <summary>
        /// Processes an integer by multiplying it by 2.
        /// </summary>
        /// <param name="input">The input integer.</param>
        /// <returns>The result after processing.</returns>
        private int ProcessData(int input)
        {
            int output = input * 2;

            return output;
        }
    }
}
