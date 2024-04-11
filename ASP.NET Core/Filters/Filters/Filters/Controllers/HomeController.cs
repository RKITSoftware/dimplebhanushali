using Filters.Filters.Exception_Filter;
using Filters.Filters.Resource_FIlter;
using Filters.Filters.Result_FIlter;
using Filters.Filters.Synchronous_Filter;
using Filters.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Filters.Controllers
{
    //[ServiceFilter(typeof(MyAuthorizationFilter))] 
    [ServiceFilter(typeof(MyResourceFilter))] 
    [ServiceFilter(typeof(LogActionFilter))] 
    [ServiceFilter(typeof(MyExceptionFilter))]
    [ServiceFilter(typeof(MyResultFilter))] 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}