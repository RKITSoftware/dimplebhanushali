using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Date_Time_Class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Constructors
            DateTime now = DateTime.Now;
            DateTime customDate = new DateTime(2023, 12, 12, 12, 0, 0);

            // Properties
            Console.WriteLine($"Current Date and Time: {now}");
            Console.WriteLine($"Year: {now.Year}, Month: {now.Month}, Day: {now.Day}");
            Console.WriteLine($"Hour: {now.Hour}, Minute: {now.Minute}, Second: {now.Second}");

            // Methods
            Console.WriteLine($"Short Date String: {now.ToShortDateString()}");
            Console.WriteLine($"Short Time String: {now.ToShortTimeString()}");
            Console.WriteLine($"Long Date String: {now.ToLongDateString()}");
            Console.WriteLine($"Long Time String: {now.ToLongTimeString()}");

            // Add and Subtract
            DateTime futureDate = now.AddDays(30);
            DateTime pastDate = now.AddMonths(-2);
            TimeSpan timeDifference = futureDate.Subtract(pastDate);

            Console.WriteLine($"Future Date (+30 days): {futureDate}");
            Console.WriteLine($"Past Date (-2 months): {pastDate}");
            Console.WriteLine($"Time Difference: {timeDifference.Days} days");

            // CompareTo and Equals
            DateTime sameDate = new DateTime(now.Year, now.Month, now.Day);
            int compareToResult = now.CompareTo(sameDate);

            Console.WriteLine($"CompareTo Result: {compareToResult}");
            Console.WriteLine($"Are they equal? {now.Equals(sameDate)}");

            // Parse
            string dateString = "2022-02-15";
            DateTime parsedDate;
            if (DateTime.TryParse(dateString, out parsedDate))
            {
                Console.WriteLine($"Parsed Date: {parsedDate}");
            }
            else
            {
                Console.WriteLine($"Failed to parse the date string: {dateString}");
            }

            // Static Methods
            string formattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"Formatted Date: {formattedDate}");

            // Time Zone Conversion
            DateTime utcNow = DateTime.UtcNow;
            DateTime localTime = utcNow.ToLocalTime();
            DateTime universalTime = now.ToUniversalTime();

            Console.WriteLine($"UTC Now: {utcNow}");
            Console.WriteLine($"Local Time: {localTime}");
            Console.WriteLine($"Universal Time: {universalTime}");

            Console.ReadKey();
        }
    }
}
