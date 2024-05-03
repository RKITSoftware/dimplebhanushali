using Certificate_Generator.Models.DTO;
using System.Text;

namespace Certificate_Generator.BL
{
    /// <summary>
    /// Business logic class for generating and saving certificates.
    /// </summary>
    public class BLCertificateGenerator
    {
        /// <summary>
        /// Generates a certificate in HTML format based on the provided generation data.
        /// </summary>
        /// <param name="generationData">The data used for generating the certificate.</param>
        /// <returns>A byte array representing the HTML content of the certificate.</returns>
        public byte[] GenerateCertificate(DTOGEN01 generationData)
        {
            var certificateContent = GenerateHtmlCertificate(generationData);
            return Encoding.UTF8.GetBytes(certificateContent);
        }

        /// <summary>
        /// Saves the certificate content to a file in the specified folder.
        /// </summary>
        /// <param name="generationData">The data used for generating the certificate.</param>
        /// <param name="certificateContent">The byte array representing the HTML content of the certificate.</param>
        /// <returns>The file path where the certificate is saved.</returns>
        public string SaveCertificateToFile(DTOGEN01 generationData, byte[] certificateContent)
        {
            string folderName = "Certificates";
            string fileName = $"{generationData.N01F02}_{generationData.N01F03}.html";

            // Combine the current directory with the folder name
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            // Create the folder if it doesn't exist
            Directory.CreateDirectory(folderPath);

            // Combine the folder path with the filename
            string filePath = Path.Combine(folderPath, fileName);

            // Write the certificate content to the file
            File.WriteAllBytes(filePath, certificateContent);

            return filePath;
        }

        /// <summary>
        /// Generates the HTML content of the certificate based on the provided generation data.
        /// </summary>
        /// <param name="generationData">The data used for generating the certificate.</param>
        /// <returns>An HTML string representing the content of the certificate.</returns>
        private string GenerateHtmlCertificate(DTOGEN01 generationData)
        {
            // Create an HTML string representing the certificate content
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<!DOCTYPE html>");
            htmlBuilder.Append("<html>");
            htmlBuilder.Append("<head>");
            htmlBuilder.Append("<title>Certificate</title>");
            htmlBuilder.Append("</head>");
            htmlBuilder.Append("<body>");
            htmlBuilder.Append($"<h1>Certificate Id: {generationData.N01F01}</h1>");
            htmlBuilder.Append($"<p>User Id: {generationData.N01F02}</p>");
            htmlBuilder.Append($"<p>Template Id: {generationData.N01F03}</p>");
            htmlBuilder.Append($"<p>Generation Date: {generationData.N01F04}</p>");
            htmlBuilder.Append("</body>");
            htmlBuilder.Append("</html>");

            return htmlBuilder.ToString();
        }
    }
}
