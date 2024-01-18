using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Versoning_with_Query_String.Custom
{
    /// <summary>
    /// Custom controller selector for handling API versioning based on a query string parameter.
    /// </summary>
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomControllerSelector"/> class.
        /// </summary>
        /// <param name="configuration">The HTTP configuration.</param>
        public CustomControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            _config = configuration;
        }

        /// <summary>
        /// Selects the controller descriptor based on the modified controller name with version.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <returns>The selected controller descriptor or null if not found.</returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            // Get the controller mapping and route data
            var controllerMapping = GetControllerMapping();
            var routeData = request.GetRouteData();

            // Get the original controller name from the route data
            var controllerName = routeData.Values["controller"].ToString();

            // Default version is "1"
            string version = "1";

            //// Extract the version from the query string
            //var versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);
            //if (versionQueryString["v"] != null)
            //{
            //    version = versionQueryString["v"];
            //}


            // Custom Headers
            // Extract Version from the custom headers 'customerService-Version'
            string customHeader = "customerService-Version";
            if (request.Headers.Contains(customHeader))
            {
                version = request.Headers.GetValues(customHeader).FirstOrDefault();
                if (version.Contains(','))
                {
                    version = version.Substring(0,version.IndexOf(','));
                }
            }


            // Append 'V1' or 'V2' to the controller name based on the version number
            var modifiedControllerName = version == "1" ? controllerName + "V1" : controllerName + "V2";

            // Try to get the controller descriptor based on the modified controller name
            HttpControllerDescriptor controllerDescriptor;
            if (controllerMapping.TryGetValue(modifiedControllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }

            // If no descriptor is found, return null
            return null;
        }
    }
}
