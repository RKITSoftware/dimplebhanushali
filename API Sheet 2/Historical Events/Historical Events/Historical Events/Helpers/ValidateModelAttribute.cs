using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Historical_Events.Helpers
{
    /// <summary>
    /// filter that validates model from request
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        #region Public Method

        /// <summary>
        /// validates model through data annotation 
        /// </summary>
        /// <param name="context"> http action context </param>
        public override void OnActionExecuting(HttpActionContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Response = context.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, context.ModelState);
            }
        }
        #endregion
    }
}