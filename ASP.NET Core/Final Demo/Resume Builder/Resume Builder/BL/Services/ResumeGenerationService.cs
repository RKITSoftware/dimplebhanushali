using iTextSharp.text;
using iTextSharp.text.pdf;
using Resume_Builder.Data;
using Resume_Builder.Models.POCO;
using ServiceStack.OrmLite;

namespace Resume_Builder.BL.Services
{
    /// <summary>
    /// Service for generating resumes in PDF format.
    /// </summary>
    public class ResumeGenerationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ResumeGenerationService(IHttpContextAccessor httpContextAccessor, DbConnectionFactory dbConnectionFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Generates a resume in PDF format for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A byte array representing the generated PDF.</returns>
        public byte[] GenerateResume(int userId)
        {
            // Fetch user details based on the provided user ID
            USR01 user = FetchUserDetails(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Fetch other relevant details for the resume
            List<EDU01> educationList = FetchDetailsForUser<EDU01>(userId);
            List<EXP01> experienceList = FetchDetailsForUser<EXP01>(userId);
            List<SKL01> skillsList = FetchDetailsForUser<SKL01>(userId);
            List<CER01> certificationsList = FetchDetailsForUser<CER01>(userId);
            List<PRO01> projectsList = FetchDetailsForUser<PRO01>(userId);

            // Create a new PDF document
            Document document = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            // Add user details to the PDF
            AddUserDetails(document, user);

            // Add education details to the PDF
            AddDetailsSection(document, "Education", educationList, new Dictionary<string, Func<EDU01, object>>
            {
                { "University", edu => edu.U01F03 },
                { "Degree", edu => edu.U01F04 },
                { "Field", edu => edu.U01F05 },
                { "Year", edu => edu.U01F06 }
            });

            // Add experience details to the PDF
            AddDetailsSection(document, "Experience", experienceList, new Dictionary<string, Func<EXP01, object>>
            {
                { "Company", exp => exp.P01F03 },
                { "Position", exp => exp.P01F04 },
                { "Start Date", exp => exp.P01F05 },
                { "End Date", exp => exp.P01F06 },
                { "Responsibilities", exp => exp.P01F07 }
            });

            // Add skills details to the PDF
            AddSkillsSection(document, "Skills", skillsList);

            // Add certifications details to the PDF
            AddDetailsSection(document, "Certifications", certificationsList, new Dictionary<string, Func<CER01, object>>
            {
                { "Certificate", cer => cer.R01F03 },
                { "Organization", cer => cer.R01F04 },
                { "Date", cer => cer.R01F05 }
            });

            // Add projects details to the PDF
            AddDetailsSection(document, "Projects", projectsList, new Dictionary<string, Func<PRO01, object>>
            {
                { "Project", proj => proj.O01F03 },
                { "Description", proj => proj.O01F04 },
                { "Start Date", proj => proj.O01F05 },
                { "End Date", proj => proj.O01F06 }
            });

            // Close the document
            document.Close();

            // Return the generated PDF as a byte array
            return memoryStream.ToArray();
        }

        #region Private Methods

        // Add user details section to the PDF
        private void AddUserDetails(Document document, USR01 user)
        {
            Font userFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Paragraph userParagraph = new Paragraph($"{user.R01F02} {user.R01F03}", userFont);
            userParagraph.SpacingAfter = 10f;
            userParagraph.Alignment = Element.ALIGN_CENTER;
            document.Add(userParagraph);
            Paragraph contactParagraph = new Paragraph($"Email: {user.R01F04} \t Mobile: {user.R01F05}");
            contactParagraph.Alignment = Element.ALIGN_CENTER;
            document.Add(contactParagraph);
            document.Add(Chunk.NEWLINE);
        }

        // Add details section to the PDF
        private void AddDetailsSection<T>(Document document, string title, List<T> dataList, Dictionary<string, Func<T, object>> propertyMap)
        {
            document.Add(new Paragraph());
            Chunk titleChunk = new Chunk(title.ToUpper(), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, Font.UNDERLINE | Font.BOLD));
            document.Add(titleChunk);
            document.Add(Chunk.NEWLINE);

            PdfPTable table = new PdfPTable(propertyMap.Count);
            table.WidthPercentage = 100;

            // Set table header
            foreach (var propertyEntry in propertyMap)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(propertyEntry.Key, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE)));
                headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                headerCell.BorderColor = BaseColor.GRAY;
                table.AddCell(headerCell);
            }

            // Add data rows
            foreach (var item in dataList)
            {
                foreach (var propertyEntry in propertyMap)
                {
                    Func<T, object> propertySelector = propertyEntry.Value;
                    object propertyValue = propertySelector(item);
                    string cellValue = propertyValue != null ? propertyValue.ToString() : string.Empty;
                    PdfPCell dataCell = new PdfPCell(new Phrase(cellValue, FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                    dataCell.BorderColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(dataCell);
                }
            }

            // Add the table to the document
            document.Add(table);
            document.Add(Chunk.NEWLINE);
        }

        // Add skills section to the PDF
        private void AddSkillsSection(Document document, string title, List<SKL01> skillsList)
        {
            document.Add(new Paragraph());
            Chunk titleChunk = new Chunk(title + ":", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));
            document.Add(titleChunk);
            document.Add(Chunk.NEWLINE);

            List list = new List(List.UNORDERED);
            list.SetListSymbol("\u2022"); // Bullet point symbol

            foreach (var skill in skillsList)
            {
                ListItem listItem = new ListItem(skill.L01F03, FontFactory.GetFont(FontFactory.HELVETICA, 12));
                list.Add(listItem);
            }

            // Add the list to the document
            document.Add(list);
            document.Add(Chunk.NEWLINE);
        }

        // Fetch user details from the database
        private USR01 FetchUserDetails(int userId)
        {
            using (var db = _dbConnectionFactory.CreateConnection())
            {
                return db.SingleById<USR01>(userId);
            }
        }

        // Fetch details for a specific user based on POCO type
        private List<T> FetchDetailsForUser<T>(int userId)
        {
            using (var db = _dbConnectionFactory.CreateConnection())
            {
                var tableName = typeof(T).Name;
                var sql = $"SELECT * FROM {tableName} WHERE UserId = @UserId";
                return db.Select<T>(sql, new { UserId = userId });
            }
        }

        #endregion
    }
}
