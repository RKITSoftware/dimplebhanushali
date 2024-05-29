using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Resume_Builder.DL.Interfaces;
using Resume_Builder.Models.DTO;
using System.IO.Compression;
using System.Net;

namespace Resume_Builder.BL.Services
{
    /// <summary>
    /// This Class Handles BUlk Resume Generations.
    /// </summary>
    public class BLBulkResumeGenerationService
    {
        #region Private Members

        /// <summary>
        /// Http Context Accessor Instance.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Email Send Service.
        /// </summary>
        private readonly IEmailService _sender;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for initialising httpContextAccessor and EmailService Instance.
        /// </summary>
        /// <param name="httpContextAccessor">Http Context Accessor</param>
        /// <param name="sender">IEmailService</param>
        public BLBulkResumeGenerationService(IHttpContextAccessor httpContextAccessor,
            IEmailService sender)
        {
            _httpContextAccessor = httpContextAccessor;
            _sender = sender;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Generates resumes in PDF format from JSON data.
        /// </summary>
        /// <param name="json">JSON string containing resume data.</param>
        /// <returns>A ZIP archive containing PDF resumes as byte arrays.</returns>
        public byte[] GenerateResumesFromJson(string json)
        {
            try
            {
                var resumesData = JsonConvert.DeserializeObject<List<RES02>>(json);
                List<byte[]> resumeBytesList = new List<byte[]>();

                // Generate PDF resumes for each user data
                foreach (var userData in resumesData)
                {
                    byte[] resumeBytes = GenerateResumePDF(userData);
                    resumeBytesList.Add(resumeBytes);
                }

                // Create a ZIP archive containing all generated resumes
                return CreateZip(resumeBytesList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while generating resumes: " + ex.Message);
                return null;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates a PDF resume for a user.
        /// </summary>
        /// <param name="userData">User data to include in the resume.</param>
        /// <returns>Byte array representing the PDF resume.</returns>
        private byte[] GenerateResumePDF(RES02 userData)
        {
            // Create a new PDF document
            Document document = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            // Add user details to the PDF
            AddUserDetails(document, userData);

            // Close the document
            document.Close();

            // Get the generated resume as byte array
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Adds user details to a PDF document.
        /// </summary>
        /// <param name="document">The PDF document to which user details are added.</param>
        /// <param name="userData">User data containing information to be added.</param>
        private void AddUserDetails(Document document, RES02 userData)
        {
            PdfPTable table = new PdfPTable(1); // 1 column for user information
            table.WidthPercentage = 100;
            table.DefaultCell.Border = PdfPCell.NO_BORDER;

            PdfPCell infoCell = new PdfPCell();
            infoCell.Border = PdfPCell.NO_BORDER;

            // Add basic user details, objective section, education, work experience, certificates, projects, languages, and skills
            AddBasicUserDetails(infoCell, userData.UserDetails);
            infoCell.AddElement(Chunk.NEWLINE);
            AddObjectiveSection(infoCell);
            bool isTitle = true;
            foreach (var education in userData.Education)
            {
                AddDetailsSection(infoCell, "EDUCATION", isTitle, education.Institute, education.Degree, education.FieldOfStudy, education.EducationYear.ToString());
                isTitle = false;
            }
            isTitle = true;
            foreach (var experience in userData.WorkExperience)
            {
                AddDetailsSection(infoCell, "WORK EXPERIENCE", isTitle, experience.Company, experience.Position, experience.StartDate.ToString(), experience.EndDate.ToString());
                isTitle = false;
            }
            isTitle = true;
            foreach (var certificate in userData.Certificates)
            {
                AddDetailsSection(infoCell, "CERTIFICATES", isTitle, certificate.CertificateName, certificate.Organization);
                isTitle = false;
            }
            infoCell.AddElement(Chunk.NEWLINE);
            isTitle = true;
            foreach (var project in userData.Projects)
            {
                AddDetailsSection(infoCell, "PROJECTS", isTitle, project.ProjectName, project.Description, project.StartDate.ToString(), project.EndDate.ToString());
                isTitle = false;
            }
            infoCell.AddElement(Chunk.NEWLINE);
            isTitle = true;
            AddLanguagesSection(infoCell, "LANGUAGES", userData.KnownLanguages, isTitle);
            infoCell.AddElement(Chunk.NEWLINE);
            AddSkillsSection(infoCell, "SKILLS", userData.Skills, isTitle);

            table.AddCell(infoCell);
            document.Add(table);
        }

        /// <summary>
        /// Adds basic user details to a PDF document.
        /// </summary>
        /// <param name="cell">The cell to which user details are added.</param>
        /// <param name="user">User data containing basic information.</param>
        private void AddBasicUserDetails(PdfPCell cell, User user)
        {
            // Create a nested table with user image and details
            PdfPTable nestedTable = new PdfPTable(2);
            nestedTable.DefaultCell.Border = PdfPCell.NO_BORDER;

            // Add user image on the left side
            PdfPCell imageCell = new PdfPCell();
            imageCell.Border = PdfPCell.NO_BORDER;
            Image photo = Image.GetInstance(new WebClient().DownloadData("https://static.vecteezy.com/system/resources/previews/029/376/511/original/shinchan-cute-face-free-vector.jpg"));
            photo.ScaleAbsolute(100f, 100f);
            imageCell.AddElement(photo);
            nestedTable.AddCell(imageCell);

            // Add user details on the right side
            PdfPCell detailsCell = new PdfPCell();
            detailsCell.Border = PdfPCell.NO_BORDER;
            string fullName = $"{user.FirstName} {user.LastName}";
            Paragraph name = new Paragraph(fullName, FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 14));
            Paragraph title = new Paragraph(user.ProfessionalTitle, FontFactory.GetFont(FontFactory.TIMES_BOLD, 11));
            detailsCell.AddElement(name);
            detailsCell.AddElement(title);
            // Add contact information
            Paragraph contactInfo = new Paragraph();
            contactInfo.Add(new Chunk("Email: ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 10)));
            contactInfo.Add(new Chunk($"{user.Email}\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
            contactInfo.Add(new Chunk("Phone: ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 10)));
            contactInfo.Add(new Chunk($"{user.Mobile}", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
            detailsCell.AddElement(contactInfo);

            nestedTable.AddCell(detailsCell);
            cell.AddElement(nestedTable);
        }

        /// <summary>
        /// Creates a ZIP archive containing PDF resumes.
        /// </summary>
        /// <param name="files">List of byte arrays representing PDF resumes.</param>
        /// <returns>Byte array representing the ZIP archive.</returns>
        private byte[] CreateZip(List<byte[]> files)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        var entry = zipArchive.CreateEntry($"resume_{i + 1}.pdf");
                        using (var entryStream = entry.Open())
                        {
                            entryStream.Write(files[i], 0, files[i].Length);
                        }
                    }
                }
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Adds a section with details to a PDF cell.
        /// </summary>
        /// <param name="cell">The PDF cell to which the section is added.</param>
        /// <param name="sectionTitle">Title of the section.</param>
        /// <param name="includeTitle">Specifies whether to include the section title.</param>
        /// <param name="fields">Fields to include in the section.</param>
        private void AddDetailsSection(PdfPCell cell, string sectionTitle, bool includeTitle, params string[] fields)
        {
            PdfPTable table = new PdfPTable(fields.Length);
            table.WidthPercentage = 100;

            foreach (string field in fields)
            {
                PdfPCell cell1 = new PdfPCell();
                cell1.Border = PdfPCell.NO_BORDER;
                cell1.Phrase = new Phrase(field, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)); // Regular font for field value
                table.AddCell(cell1);
            }

            if (includeTitle)
            {
                cell.AddElement(new Paragraph());
                Chunk titleChunk = new Chunk(sectionTitle.ToUpper(), FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 11)); // Bold and italic font for section title
                cell.AddElement(titleChunk);
                Paragraph line = new Paragraph(string.Format("{0}", new string('_', 100)), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
                cell.AddElement(line);
            }

            cell.AddElement(table);
            cell.AddElement(Chunk.NEWLINE);
        }

        /// <summary>
        /// Adds a section for listing languages to a PDF cell.
        /// </summary>
        /// <param name="cell">The PDF cell to which the section is added.</param>
        /// <param name="title">Title of the section.</param>
        /// <param name="languages">List of languages to include.</param>
        /// <param name="includeTitle">Specifies whether to include the section title.</param>
        private void AddLanguagesSection(PdfPCell cell, string title, List<string> languages, bool includeTitle)
        {
            if (includeTitle)
            {
                cell.AddElement(new Paragraph());
                Chunk titleChunk = new Chunk(title.ToUpper(), FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 11));
                cell.AddElement(titleChunk);
                Paragraph line = new Paragraph(string.Format("{0}", new string('_', 100)), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
                cell.AddElement(line);
            }

            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;

            foreach (var language in languages)
            {
                PdfPCell languageCell = new PdfPCell(new Phrase(language, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11)));
                languageCell.Border = PdfPCell.NO_BORDER;
                table.AddCell(languageCell);
            }

            cell.AddElement(table);
            cell.AddElement(Chunk.NEWLINE);
        }

        /// <summary>
        /// Adds a section for listing skills to a PDF cell.
        /// </summary>
        /// <param name="cell">The PDF cell to which the section is added.</param>
        /// <param name="title">Title of the section.</param>
        /// <param name="skills">List of skills to include.</param>
        /// <param name="includeTitle">Specifies whether to include the section title.</param>
        private void AddSkillsSection(PdfPCell cell, string title, List<string> skills, bool includeTitle)
        {
            if (includeTitle)
            {
                cell.AddElement(new Paragraph());
                Chunk titleChunk = new Chunk(title.ToUpper(), FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 11));
                cell.AddElement(titleChunk);
                Paragraph line = new Paragraph(string.Format("{0}", new string('_', 100)), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
                cell.AddElement(line);
            }

            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;

            foreach (var skill in skills)
            {
                PdfPCell skillCell = new PdfPCell(new Phrase(skill, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11)));
                skillCell.Border = PdfPCell.NO_BORDER;
                table.AddCell(skillCell);
            }

            cell.AddElement(table);
            cell.AddElement(Chunk.NEWLINE);
        }

        /// <summary>
        /// Adds an objective section to a PDF cell.
        /// </summary>
        /// <param name="cell">The PDF cell to which the objective section is added.</param>
        private void AddObjectiveSection(PdfPCell cell)
        {
            cell.AddElement(new Paragraph());
            Chunk titleChunk = new Chunk("OBJECTIVE".ToUpper(), FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 11));
            cell.AddElement(titleChunk);
            cell.AddElement(new Paragraph(string.Format("{0}", new string('_', 100)), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));

            string objectiveText = "Highly motivated and detail-oriented individual, I am seeking a position where I can utilize my skills and experience to contribute effectively to the organization.";
            Paragraph objective = new Paragraph(objectiveText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
            cell.AddElement(objective);
            cell.AddElement(Chunk.NEWLINE);
        }

        #endregion
    }
}
