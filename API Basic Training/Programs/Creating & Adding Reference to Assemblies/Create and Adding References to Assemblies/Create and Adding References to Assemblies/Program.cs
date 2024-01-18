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
            // Create an instance of the Scrape class
            Scrape scrape = new Scrape();

            // Perform web scraping on the specified URL
            scrape.ScrapWebPage("https://www.rkitsoftware.com");

            // Wait for a key press before closing the console window
            Console.ReadKey();
        }
    }
}
