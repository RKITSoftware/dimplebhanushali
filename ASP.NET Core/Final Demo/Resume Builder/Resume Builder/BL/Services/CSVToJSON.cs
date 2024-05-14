using Newtonsoft.Json;

namespace Resume_Builder.BL.Services
{
    /// <summary>
    /// Service to convert CSV file data to JSON format.
    /// </summary>
    public class CSVToJSON
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSVToJSON"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public CSVToJSON(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Converts the content of a CSV file to JSON format.
        /// </summary>
        /// <param name="file">The CSV file to be converted.</param>
        /// <returns>A string containing the JSON representation of the CSV data.</returns>
        public async Task<string> Convert(IFormFile file)
        {
            using (var streamReader = new StreamReader(file.OpenReadStream()))
            {
                var jsonObject = new List<Dictionary<string, string>>();

                // Read the first line to get column headers
                var headers = await streamReader.ReadLineAsync();
                var columns = headers.Split('\t');

                while (!streamReader.EndOfStream)
                {
                    var line = await streamReader.ReadLineAsync();
                    var values = line.Split('\t');

                    var rowObject = new Dictionary<string, string>();
                    for (int i = 0; i < columns.Length; i++)
                    {
                        rowObject[columns[i]] = values[i];
                    }

                    jsonObject.Add(rowObject);
                }

                // Convert the list of dictionaries to JSON
                return JsonConvert.SerializeObject(jsonObject);
            }
        }
    }
}
