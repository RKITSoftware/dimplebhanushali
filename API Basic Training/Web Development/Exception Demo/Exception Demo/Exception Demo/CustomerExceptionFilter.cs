using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Exception_Demo
{
    public class CustomerExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            ShowError(actionExecutedContext.Exception);
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("Some Occured !!!"),
                ReasonPhrase = "Internal Server Error !!!"
            };
        }

        public void ShowError(Exception exception)
        {
            try
            {
                Console.WriteLine("Exception Handled");
            }

            catch (Exception ex)
            {
                Console.WriteLine(" Error Occured => " + ex.Message);
            }
        }

    }
}