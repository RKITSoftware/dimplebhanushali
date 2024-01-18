using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exception_Handling
{
    /// <summary>
    /// This program demonstrates exception handling in C# when downloading content from a web page.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://www.google.com/";

            try
            {
                string content = DownloadWebPage(url);
                Console.WriteLine($"Web page content:\n{content}");
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("404"))
            {
                Console.WriteLine($"Handled HttpRequestException (404): {ex.Message}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Handled HttpRequestException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled Exception: {ex.Message}");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Downloads the content of a web page using HttpClient.
        /// </summary>
        /// <param name="url">The URL of the web page to download.</param>
        /// <returns>The content of the web page.</returns>
        static string DownloadWebPage(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                // Check if the request was successful
                response.EnsureSuccessStatusCode();

                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
