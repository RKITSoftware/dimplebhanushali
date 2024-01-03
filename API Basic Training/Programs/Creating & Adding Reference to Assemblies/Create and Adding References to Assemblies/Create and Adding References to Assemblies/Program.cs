using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapping;

namespace Create_and_Adding_References_to_Assemblies
{
    class Program
    {
        static void Main(string[] args)
        {
            Scrape scrape = new Scrape();
            scrape.ScrapWebPage("https://www.rkitsoftware.com");
            Console.ReadKey();
        }
    }
}
