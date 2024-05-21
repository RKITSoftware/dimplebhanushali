using Historical_Events.Basic_Authorisation;
using Historical_Events.BL;
using System.Web.Http;

namespace Historical_Events.Controllers
{
    /// <summary>
    /// Controller for importing CSV data into the database.
    /// </summary>
    [RoutePrefix("api/import")]
    [BasicAuthenticationFilter]
    [BasicAuthorisation(Roles = "A")]
    public class CLImportController : ApiController
    {
        /// <summary>
        /// Instance of BL File of CSV to DataBase Service.
        /// </summary>
        private readonly BLCsvToDatabaseService _csvToDatabaseService;

        /// <summary>
        /// Constructor to initialise _csvToDatabaseService.
        /// </summary>
        public CLImportController()
        {
            _csvToDatabaseService = new BLCsvToDatabaseService();
        }

        /// <summary>
        /// Imports CSV data into the database.
        /// </summary>
        /// <returns>HTTP response indicating success or failure.</returns>
        [HttpPost]
        [Route("csv")]
        public IHttpActionResult ImportCsv()
        {
            //// Get the file path of the CSV file
            //var csvFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/Data.csv");

            //// Check if the file path is valid and the file exists
            //if (string.IsNullOrEmpty(csvFilePath) || !File.Exists(csvFilePath))
            //{
            //    return BadRequest("CSV file path is invalid or file does not exist.");
            //}

            //// Import CSV data into the database
            //_csvToDatabaseService.ImportCsvToDatabase(csvFilePath);

            return Ok("CSV data imported successfully.");
        }
    }
}
