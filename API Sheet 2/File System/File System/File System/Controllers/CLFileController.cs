using File_System.BL;
using File_System.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace File_System.Controllers
{
    /// <summary>
    /// Controller for handling file-related operations.
    /// </summary>
    [RoutePrefix("api")]
    public class CLFileController : ApiController
    {
        private readonly BLFile _fileLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLFileController"/> class.
        /// </summary>
        /// <param name="fileLogic">The business logic for file operations.</param>
        public CLFileController()
        {
            _fileLogic = new BLFile();
        }

        /// <summary>
        /// Gets the list of files.
        /// </summary>
        [HttpGet]
        [Route("ListFiles")]
        public IHttpActionResult ListFiles()
        {
            try
            {
                // Retrieve and return the list of files
                string[] files = _fileLogic.ListFiles();
                return Ok(files);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors and return an internal server error
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Reads the content of a file.
        /// </summary>
        /// <param name="fileName">The name of the file to read.</param>
        [HttpGet]
        [Route("Read/{FileName}")]
        public IHttpActionResult ReadFile(string fileName)
        {
            try
            {
                // Retrieve and return the content of the file
                string content = _fileLogic.ReadFile(fileName);
                return Ok(content);
            }
            catch (FileNotFoundException)
            {
                // Return a not found response if the file is not found
                return NotFound();
            }
            catch (Exception ex)
            {
                // Handle unexpected errors and return an internal server error
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Gets Information about File.
        /// </summary>
        /// <param name="filename">filename</param>
        /// <returns>File information</returns>
        [HttpGet, Route("GetFileInfo/{filename}")]
        public IHttpActionResult GetFileInfo([FromUri] string filename)
        {
            if (filename != null)
            {
                FileInfo fileInfo = _fileLogic.GetFileInfo(filename);

                if (fileInfo != null)
                {
                    return Ok(fileInfo);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Get Directory Information of File.
        /// </summary>
        /// <returns>Directory Info</returns>
        [HttpGet, Route("GetDirectoryInfo")]
        public IHttpActionResult GetDirectoryInfo()
        {
            return Ok(_fileLogic.GetUploadsDirectoryInfo());
        }

        /// <summary>
        /// Creates a new file and writes content to it.
        /// </summary>
        /// <param name="contentModel">The model containing file name and content.</param>
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateAndWriteFile(FileContent contentModel)
        {
            try
            {
                // Create and write content to the file
                string result = _fileLogic.CreateAndWriteFile(contentModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors and return an internal server error
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Uploads a file to the server.
        /// </summary>
        [HttpPost]
        [Route("UploadFile")]
        public IHttpActionResult UploadFile()
        {
            try
            {
                // Upload the file
                var files = HttpContext.Current.Request.Files;
                string result = _fileLogic.UploadFile(files);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors and return an internal server error
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Downloads a file from the server.
        /// </summary>
        /// <param name="fileName">The name of the file to download.</param>
        [HttpGet]
        [Route("Download/{FileName}")]
        public IHttpActionResult DownloadFile(string fileName)
        {
            try
            {
                // Download the file and return it as a response
                byte[] fileBytes = _fileLogic.DownloadFile(fileName);

                if (fileBytes != null)
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new ByteArrayContent(fileBytes);
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = fileName
                    };
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                    return ResponseMessage(response);
                }
                else
                {
                    // Return a not found response if the file is not found
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors and return an internal server error
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Deletes a file from the server.
        /// </summary>
        /// <param name="fileName">The name of the file to delete.</param>
        [HttpDelete]
        [Route("Delete/{FileName}")]
        public IHttpActionResult DeleteFile(string fileName)
        {
            try
            {
                // Delete the file
                string result = _fileLogic.DeleteFile(fileName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors and return an internal server error
                return InternalServerError(ex);
            }
        }
    }
}
