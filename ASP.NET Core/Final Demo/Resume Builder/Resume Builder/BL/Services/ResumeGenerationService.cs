using iTextSharp.text;
using iTextSharp.text.pdf;
using Resume_Builder.Data;
using Resume_Builder.DL.Interfaces;
using Resume_Builder.Helpers;
using Resume_Builder.Models.POCO;
using ServiceStack.OrmLite;
using System.Net;

namespace Resume_Builder.BL.Services
{
    public class ResumeGenerationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DbConnectionFactory _dbConnectionFactory;
        private readonly IEmailService _sender;

        public ResumeGenerationService(IHttpContextAccessor httpContextAccessor, 
                                       DbConnectionFactory dbConnectionFactory,
                                       IEmailService sender)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbConnectionFactory = dbConnectionFactory;
            _sender = sender;
        }

        public byte[] GenerateResume(int userId)
        {
            // Fetch user details based on the provided user ID
            USR01 user = FetchUserDetails(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Create a new PDF document
            Document document = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            // Add photo and user information to the PDF
            AddUserDetails(document, user);

            // Close the document
            document.Close();

            // Get the generated resume as byte array
            byte[] resumeBytes = memoryStream.ToArray();

            // Send the resume to user's email
            _sender.Send(user.R01F04, "Please find attached your resume.", resumeBytes);

            // Return the generated PDF as a byte array
            return resumeBytes;
        }

        private void AddUserDetails(Document document, USR01 user)
        {
            PdfPTable table = new PdfPTable(2); // 2 columns for photo and user information
            table.WidthPercentage = 100;
            table.DefaultCell.Border = PdfPCell.NO_BORDER;

            // Add photo to the left column
            PdfPCell photoCell = new PdfPCell();
            photoCell.Border = PdfPCell.NO_BORDER;
            Image photo = Image.GetInstance(new WebClient().DownloadData("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSlSUzObn1SwCdJsnu7rFnYNr3SFGIh76XkoA&s"));
            photo.ScaleAbsolute(80f, 80f);
            photoCell.AddElement(photo);

            // Create separate chunks for email and phone with bold font for headings
            Chunk emailHeadingChunk = new Chunk("Email: ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 10));
            Chunk phoneHeadingChunk = new Chunk("Phone: ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 10));

            // Create separate chunks for email and phone values with normal font
            Chunk emailValueChunk = new Chunk(user.R01F04, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
            Chunk phoneValueChunk = new Chunk(user.R01F05, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));

            // Create a paragraph and add the chunks to it
            Paragraph contactInfo = new Paragraph();
            contactInfo.Add(emailHeadingChunk);
            contactInfo.Add(emailValueChunk);
            contactInfo.Add(Chunk.NEWLINE); // Add a newline between email and phone
            contactInfo.Add(phoneHeadingChunk);
            contactInfo.Add(phoneValueChunk);

            // Add the contact info paragraph to the cell
            photoCell.AddElement(contactInfo);


            photoCell.AddElement(Chunk.NEWLINE);
            List<SKL01> skillsList = FetchDetailsForUser<SKL01>(_httpContextAccessor.HttpContext.GetUserIdFromClaims());
            AddSkillsSection(photoCell, "SKILLS", skillsList);
            List<LAN01> languageList = FetchDetailsForUser<LAN01>(_httpContextAccessor.HttpContext.GetUserIdFromClaims());
            AddLanguageSection(photoCell, "LANGUAGE", languageList);
            // Add projects details to the PDF
            List<PRO01> projectsList = FetchDetailsForUser<PRO01>(_httpContextAccessor.HttpContext.GetUserIdFromClaims());
            AddDetailsSection(photoCell, "Projects", projectsList, new Dictionary<string, Func<PRO01, object>>
                {
                    { "Project", proj => proj.O01F03 },
                    { "Description", proj => proj.O01F04 },
                    { "Start Date", proj => proj.O01F05 },
                    { "End Date", proj => proj.O01F06 }
                });
            table.AddCell(photoCell);

            // Add user information to the right column
            PdfPCell infoCell = new PdfPCell();
            infoCell.Border = PdfPCell.NO_BORDER;
            Paragraph name = new Paragraph($"{user.R01F02} {user.R01F03}", FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 14));
            Paragraph title = new Paragraph("Professional Title", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12));
            Paragraph line = new Paragraph("___________________________________", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
            //Paragraph objective = new Paragraph("Highly motivated and detail-oriented individual, I am seeking a position where I can utilize my skills and experience to contribute effectively to the organization.", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
            infoCell.AddElement(name);
            infoCell.AddElement(title);
            infoCell.AddElement(line); // Add a line break
            //infoCell.AddElement(objective);

            AddObjectiveSection(infoCell);

            //infoCell.AddElement(contactInfo); // Add contact information
            List<EDU01> educationList = FetchDetailsForUser<EDU01>(_httpContextAccessor.HttpContext.GetUserIdFromClaims());
            infoCell.AddElement(Chunk.NEWLINE);
            AddDetailsSection(infoCell, "EDUCATION", educationList, new Dictionary<string, Func<EDU01, object>>
                {
                    { "University", edu => edu.U01F03 },
                    { "Degree", edu => edu.U01F04 },
                    { "Field", edu => edu.U01F05 },
                    { "Year", edu => edu.U01F06 }
                });

            List<EXP01> expnList = FetchDetailsForUser<EXP01>(_httpContextAccessor.HttpContext.GetUserIdFromClaims());
            AddDetailsSection(infoCell, "WORK EXPERIENCE", expnList, new Dictionary<string, Func<EXP01, object>>
                {
                    { "Company", exp => exp.P01F03 },
                    { "Position", exp => exp.P01F04 },
                    { "Start Date", exp => exp.P01F05 },
                    { "End Date", exp => exp.P01F06 },
                    { "Responsibilities", exp => exp.P01F07 }
                });

            List<CER01> certificationsList = FetchDetailsForUser<CER01>(_httpContextAccessor.HttpContext.GetUserIdFromClaims());
            AddDetailsSection(infoCell, "Certifications", certificationsList, new Dictionary<string, Func<CER01, object>>
                {
                    { "Certificate", cer => cer.R01F03 },
                    { "Organization", cer => cer.R01F04 },
                    { "Date", cer => cer.R01F05 }
                });


            table.AddCell(infoCell);

            // Add the table to the document
            document.Add(table);
        }

        private void AddDetailsSection<T>(PdfPCell document, string title, List<T> dataList, Dictionary<string, Func<T, object>> propertyMap)
        {
            document.AddElement(new Paragraph());
            Chunk titleChunk = new Chunk(title.ToUpper(), FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12)); // Make the title uppercase and bold
            document.AddElement(titleChunk);
            Paragraph line = new Paragraph("________________________________", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
            document.AddElement(line);

            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;

            foreach (var item in dataList)
            {
                foreach (var propertyEntry in propertyMap)
                {
                    string propertyName = propertyEntry.Key;
                    Func<T, object> propertySelector = propertyEntry.Value;
                    object propertyValue = propertySelector(item);
                    string cellValue = propertyValue != null ? propertyValue.ToString() : string.Empty;

                    PdfPCell propertyNameCell = new PdfPCell(new Phrase(propertyName, FontFactory.GetFont(FontFactory.TIMES_BOLD, 10))); // Make the property name bold
                    propertyNameCell.Border = PdfPCell.NO_BORDER;
                    table.AddCell(propertyNameCell);

                    PdfPCell propertyValueCell = new PdfPCell(new Phrase(cellValue, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10))); // Make the property value bold
                    propertyValueCell.Border = PdfPCell.NO_BORDER;
                    table.AddCell(propertyValueCell);
                }
                // Add a blank line between each company's details
                document.AddElement(Chunk.NEWLINE);
            }

            document.AddElement(table);
            document.AddElement(Chunk.NEWLINE);
        }



        private void AddSkillsSection(PdfPCell document, string title, List<SKL01> skillsList)
        {
            document.AddElement(new Paragraph());
            Chunk titleChunk = new Chunk(title, FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12));
            document.AddElement(titleChunk);
            Paragraph line = new Paragraph("________________________________", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
            document.AddElement(line);

            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;

            foreach (var skill in skillsList)
            {
                PdfPCell cell = new PdfPCell(new Phrase(skill.L01F03, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)));
                cell.Border = PdfPCell.NO_BORDER;
                table.AddCell(cell);
            }

            document.AddElement(table);
            document.AddElement(Chunk.NEWLINE);
        }

        private void AddLanguageSection(PdfPCell document, string title, List<LAN01> languageList)
        {
            document.AddElement(new Paragraph());
            Chunk titleChunk = new Chunk(title, FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12));
            document.AddElement(titleChunk);
            Paragraph line = new Paragraph("________________________________", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
            document.AddElement(line);

            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;

            foreach (var language in languageList)
            {
                PdfPCell cell = new PdfPCell(new Phrase(language.N01F03, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)));
                cell.Border = PdfPCell.NO_BORDER;
                table.AddCell(cell);
            }

            document.AddElement(table);
            document.AddElement(Chunk.NEWLINE);
        }

        private void AddObjectiveSection(PdfPCell cell)
        {
            cell.AddElement(new Paragraph());
            Chunk titleChunk = new Chunk("OBJECTIVE".ToUpper(), FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12));
            cell.AddElement(titleChunk);
            cell.AddElement(new Paragraph(string.Format("{0}", new string('_', 40)), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
            string objectiveText = "Highly motivated and detail-oriented individual, I am seeking a position where I can utilize my skills and experience to contribute effectively to the organization.";
            Paragraph objective = new Paragraph(objectiveText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
            cell.AddElement(objective);
            cell.AddElement(Chunk.NEWLINE);
        }

        private USR01 FetchUserDetails(int userId)
        {
            using (var db = _dbConnectionFactory.CreateConnection())
            {
                return db.SingleById<USR01>(userId);
            }
        }

        private List<T> FetchDetailsForUser<T>(int userId)
        {
            using (var db = _dbConnectionFactory.CreateConnection())
            {
                var tableName = typeof(T).Name;
                var sql = $"SELECT * FROM {tableName} WHERE UserId = @UserId";
                return db.Select<T>(sql, new { UserId = userId });
            }
        }

    }
}
