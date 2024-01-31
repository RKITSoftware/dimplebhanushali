using File_System.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace File_System.BL
{
    /// <summary>
    /// Business Logic class for file operations.
    /// </summary>
    public class BLFile
    {
        private const string UploadsFolder = "~/uploads/";

        /// <summary>
        /// Reads the content of a file.
        /// </summary>
        /// <param name="fileName">The name of the file to read.</param>
        /// <returns>The content of the file or an error message if the file is not found or an exception occurs.</returns>
        public string ReadFile(string fileName)
        {
            string filePath = HttpContext.Current.Server.MapPath($"{UploadsFolder}{fileName}");

            try
            {
                string content = File.ReadAllText(filePath);
                return content;
            }
            catch (FileNotFoundException)
            {
                return "File Not Found";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Uploads a file to the server.
        /// </summary>
        /// <param name="files">The collection of files to upload.</param>
        /// <returns>A success message or an error message if no files are received or an exception occurs.</returns>
        public string UploadFile(HttpFileCollection files)
        {
            try
            {
                if (files.Count > 0)
                {
                    foreach (string fileName in files)
                    {
                        var file = files[fileName];

                        if (file != null && file.ContentLength > 0)
                        {
                            var uploadPath = HttpContext.Current.Server.MapPath("~/uploads/");
                            var filePath = Path.Combine(uploadPath, file.FileName);

                            file.SaveAs(filePath);

                            return $"File '{file.FileName}' uploaded successfully.";
                        }
                    }
                }

                return "No files were received in the request.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Downloads a file from the server.
        /// </summary>
        /// <param name="fileName">The name of the file to download.</param>
        /// <returns>The file content as a byte array or null if the file is not found.</returns>
        public byte[] DownloadFile(string fileName)
        {
            string filePath = HttpContext.Current.Server.MapPath($"{UploadsFolder}{fileName}");

            if (File.Exists(filePath))
            {
                return File.ReadAllBytes(filePath);
            }

            return null;
        }

        /// <summary>
        /// Creates a new file and writes content to it.
        /// </summary>
        /// <param name="contentModel">The model containing file name and content.</param>
        /// <returns>A success message or an error message if an exception occurs.</returns>
        public string CreateAndWriteFile(FileContent contentModel)
        {
            string filePath = HttpContext.Current.Server.MapPath($"{UploadsFolder}{contentModel.FileName}");

            try
            {
                File.WriteAllText(filePath, contentModel.Content);
                return "File created and written successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Lists all files in the uploads folder.
        /// </summary>
        /// <returns>An array of file names or throws an exception if an error occurs.</returns>
        public string[] ListFiles()
        {
            try
            {
                var uploadPath = HttpContext.Current.Server.MapPath("~/uploads/");
                return Directory.GetFiles(uploadPath).Select(Path.GetFileName).ToArray();
            }
            catch (Exception ex)
            {
                // You may choose to handle or log this exception based on your requirements
                throw;
            }
        }

        /// <summary>
        /// Deletes a file from the server.
        /// </summary>
        /// <param name="fileName">The name of the file to delete.</param>
        /// <returns>A success message or an error message if the file is not found or an exception occurs.</returns>
        public string DeleteFile(string fileName)
        {
            try
            {
                string filePath = HttpContext.Current.Server.MapPath($"{UploadsFolder}{fileName}");

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return $"File '{fileName}' deleted successfully.";
                }
                else
                {
                    // You may choose to handle or propagate this as needed
                    return $"File '{fileName}' not found.";
                }
            }
            catch (Exception ex)
            {
                // You may choose to handle or log this exception based on your requirements
                throw;
            }
        }

        /// <summary>
        /// Gets information about a file.
        /// </summary>
        /// <param name="fileName">The name of the file to get information about.</param>
        /// <returns>FileInfo object containing information about the file or null if the file is not found.</returns>
        public FileInfo GetFileInfo(string fileName)
        {
            string filePath = HttpContext.Current.Server.MapPath($"{UploadsFolder}{fileName}");

            if (File.Exists(filePath))
            {
                return new FileInfo(filePath);
            }
            return null;
        }

        /// <summary>
        /// Gets information about the uploads directory.
        /// </summary>
        /// <returns>DirectoryInfo object containing information about the uploads directory or null if the directory is not found.</returns>
        public DirectoryInfo GetUploadsDirectoryInfo()
        {
            string uploadPath = HttpContext.Current.Server.MapPath("~/uploads/");

            if (Directory.Exists(uploadPath))
            {
                return new DirectoryInfo(uploadPath);
            }

            return null;
        }
    }
}
