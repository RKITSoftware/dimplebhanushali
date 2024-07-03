using iTextSharp.text;
using iTextSharp.text.pdf;
using Resume_Builder.Models;
using System.Net.Http.Headers;
using System.Text;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;
using Rectangle = iTextSharp.text.Rectangle;

namespace Resume_Builder.BL.Services
{
    /// <summary>
    /// Service for generating AI-based certificates.
    /// </summary>
    public class BLAICertificate
    {
        #region Private Members
        /// <summary>
        /// HttpClient
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialize dependencies.
        /// </summary>
        /// <param name="httpClient">HttpClient instance for making API requests.</param>
        /// <param name="configuration">Configuration instance to access app settings.</param>
        public BLAICertificate(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Generates and saves a certificate PDF asynchronously.
        /// </summary>
        /// <param name="request">Certificate request details.</param>
        /// <returns>The file path of the generated certificate.</returns>
        public async Task<string> GenerateAndSaveCertificateAsync(DTOCER02 request)
        {
            // Generate the image based on the theme
            byte[] backgroundImage = await GenerateImageFromPromptWithRetry(request.Award);

            // Generate the PDF certificate with the background image
            byte[] pdfBytes = GenerateCertificate(request, backgroundImage);

            // Save the PDF to a file
            string certificateDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Certificates");
            if (!Directory.Exists(certificateDirectory))
            {
                Directory.CreateDirectory(certificateDirectory);
            }

            string fileName = $"{request.ParticipantName}_{request.CertificateType}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = Path.Combine(certificateDirectory, fileName);
            await File.WriteAllBytesAsync(filePath, pdfBytes);

            // Return the file path
            return filePath;
        }

        /// <summary>
        /// Generates an image from the given prompt asynchronously.
        /// </summary>
        /// <param name="prompt">The prompt for generating the image.</param>
        /// <returns>The byte array of the generated image.</returns>
        public async Task<byte[]> GenerateImageFromPromptWithRetry(string prompt)
        {
            string apiUrl, apiKey, requestBody;

            apiUrl = "https://api-inference.huggingface.co/models/stabilityai/stable-diffusion-2";
            apiKey = _configuration["HuggingFace:ApiKey"];
            requestBody = $"{{\"inputs\":\"{prompt} in landscape mode\"}}";
            
            StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            int maxRetries, delaySeconds;

            maxRetries = 5;
            delaySeconds = 10;

            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(ms))
                    {
                        //if (image.Width < image.Height)
                        //{
                        //    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            using (MemoryStream rotatedMs = new MemoryStream())
                            {
                                image.Save(rotatedMs, System.Drawing.Imaging.ImageFormat.Jpeg);
                                return rotatedMs.ToArray();
                            }
                        //}
                    }

                    return imageBytes;
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    if (errorContent.Contains("Model stabilityai/stable-diffusion-2 is currently loading"))
                    {
                        await Task.Delay(delaySeconds * (int)Math.Pow(2, attempt) * 1000);
                    }
                    else
                    {
                        throw new HttpRequestException($"Error response from API: {errorContent}", null, response.StatusCode);
                    }
                }
            }

            throw new HttpRequestException("Exceeded maximum retry attempts.");
        }

        #endregion

        #region Private Method
        /// <summary>
        /// Generates the certificate PDF based on the request and background image.
        /// </summary>
        /// <param name="request">The certificate request details.</param>
        /// <param name="backgroundImageBytes">The byte array of the background image.</param>
        /// <returns>The byte array of the generated certificate PDF.</returns>
        private byte[] GenerateCertificate(DTOCER02 request, byte[] backgroundImageBytes)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4.Rotate()); // Landscape mode
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                // Add background image with reduced opacity
                Image backgroundImage = Image.GetInstance(backgroundImageBytes);
                backgroundImage.ScaleAbsolute(PageSize.A4.Rotate());
                backgroundImage.SetAbsolutePosition(0, 0);
                document.Add(backgroundImage);

                // Create a rectangle to hold certificate content
                Rectangle rect = new Rectangle(100, 100, document.PageSize.Width - 100, document.PageSize.Height - 100); // Adjust rectangle size and position
                rect.BackgroundColor = new BaseColor(255, 255, 255, 100); // Set background color with reduced opacity
                rect.Border = Rectangle.NO_BORDER;
                rect.BorderColor = BaseColor.BLACK;
                rect.BorderWidth = 2;
                PdfContentByte canvas = writer.DirectContent;
                canvas.Rectangle(rect);

                // Create a table to hold certificate content
                PdfPTable table = new PdfPTable(1);
                table.TotalWidth = rect.Width - 40; // Adjust width to leave some margin
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                // Certificate Title
                var titleFont = FontFactory.GetFont("Times-Roman", 36, Font.BOLD, BaseColor.BLACK);
                PdfPCell titleCell = new PdfPCell(new Phrase($"Certificate of {request.CertificateType}", titleFont));
                titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                titleCell.Border = Rectangle.NO_BORDER;
                titleCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                titleCell.PaddingTop = -5; // Adjust top padding
                table.AddCell(titleCell);

                // Participant Name
                var bodyFont = FontFactory.GetFont("Times-Roman", 18, Font.NORMAL, BaseColor.BLACK);
                PdfPCell nameCell = new PdfPCell(new Phrase($"This is to certify that {request.ParticipantName} has been awarded", bodyFont));
                nameCell.HorizontalAlignment = Element.ALIGN_CENTER;
                nameCell.Border = Rectangle.NO_BORDER;
                nameCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(nameCell);

                // Award
                PdfPCell awardCell = new PdfPCell(new Phrase(request.Award, titleFont));
                awardCell.HorizontalAlignment = Element.ALIGN_CENTER;
                awardCell.Border = Rectangle.NO_BORDER;
                awardCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(awardCell);

                // Description
                PdfPCell descriptionCell = new PdfPCell(new Phrase(request.Description, bodyFont));
                descriptionCell.HorizontalAlignment = Element.ALIGN_CENTER;
                descriptionCell.Border = Rectangle.NO_BORDER;
                descriptionCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(descriptionCell);

                // Add blank cell for spacing
                PdfPCell blankCell = new PdfPCell(new Phrase(""));
                blankCell.Border = Rectangle.NO_BORDER;
                table.AddCell(blankCell);

                // Add blank cell for spacing
                PdfPCell blankCell2 = new PdfPCell(new Phrase(""));
                blankCell2.Border = Rectangle.NO_BORDER;
                table.AddCell(blankCell2);

                // Date and Issuer
                var infoFont = FontFactory.GetFont("Times-Roman", 12, Font.NORMAL, BaseColor.BLACK);
                PdfPCell dateIssuerCell = new PdfPCell(new Phrase($"Date: {request.Date.ToString("yyyy-MM-dd")}\nIssuer: {request.IssuerName}", infoFont));
                dateIssuerCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                dateIssuerCell.Border = Rectangle.NO_BORDER;
                dateIssuerCell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(dateIssuerCell);

                float tableHeight = table.TotalHeight;
                float tableWidth = table.TotalWidth;
                table.WriteSelectedRows(0, -1, (rect.Left + rect.Right - tableWidth) / 2, rect.Top - 20 - tableHeight, canvas); // Center table horizontally and position it slightly below the top

                document.Close();
                return ms.ToArray();
            }
        }
        #endregion
    }
}
