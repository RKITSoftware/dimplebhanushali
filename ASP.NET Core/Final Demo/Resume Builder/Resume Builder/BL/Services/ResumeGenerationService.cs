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
    /// <summary>
    /// Service for generating resumes in PDF format.
    /// </summary>
    public class ResumeGenerationService
    {
        #region Private Members

        /// <summary>
        /// Instance of HttpContextAccessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// DbFactory Instance.
        /// </summary>
        private readonly DbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// IEmailService Instance.
        /// </summary>
        private readonly IEmailService _sender;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ResumeGenerationService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="sender">The email service used for sending resumes.</param>
        public ResumeGenerationService(IHttpContextAccessor httpContextAccessor,
                                       DbConnectionFactory dbConnectionFactory,
                                       IEmailService sender)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbConnectionFactory = dbConnectionFactory;
            _sender = sender;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generates a resume for the specified user ID and sends it via email.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the resume is generated.</param>
        /// <returns>The generated resume as a byte array.</returns>
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

            //// Send the resume to user's email
            //_sender.Send(user.R01F04, "Please find attached your resume.", resumeBytes);

            // Return the generated PDF as a byte array
            return resumeBytes;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Adds user details including photo, contact information, skills, education, work experience, certifications, and projects to a PDF document.
        /// </summary>
        /// <param name="document">The PDF document to which user details are added.</param>
        /// <param name="user">The user whose details are added to the document.</param>
        private void AddUserDetails(Document document, USR01 user)
        {
            PdfPTable table = new PdfPTable(1); // Single column for all information
            table.WidthPercentage = 100;
            table.DefaultCell.Border = PdfPCell.NO_BORDER;

            // Add photo, contact information, skills, language, projects, etc. to the PDF
            PdfPCell cell = new PdfPCell();
            cell.Border = PdfPCell.NO_BORDER;

            AddBasicUserDetails(cell,user);

            AddObjectiveSection(cell);

            cell.AddElement(Chunk.NEWLINE);

            // Add projects details to the PDF
            List<PRO01> projectsList = FetchDetailsForUser<PRO01>(_httpContextAccessor.HttpContext.GetUserIdFromClaims());
            AddDetailsSection(cell, "Projects", projectsList, new Dictionary<string, Func<PRO01, object>>
                {
                    { "Project Title", proj => proj.O01F03 },
                    { "Description", proj => proj.O01F04 }
                });
            //table.AddCell(cell);

            //cell.AddElement(contactInfo); // Add contact information
            List<EDU01> educationList = FetchDetailsForUser<EDU01>(_httpContextAccessor.HttpContext.GetUserIdFromClaims());
            cell.AddElement(Chunk.NEWLINE);
            AddDetailsSection(cell, "EDUCATION", educationList, new Dictionary<string, Func<EDU01, object>>
                {
                    { "University", edu => edu.U01F03 },
                    { "Degree", edu => edu.U01F04 },
                    { "Field", edu => edu.U01F05 },
                    { "Passing Year", edu => edu.U01F06 }
                });
            cell.AddElement(Chunk.NEWLINE);
            List<EXP01> expnList = FetchDetailsForUser<EXP01>(_httpContextAccessor.HttpContext.GetUserIdFromClaims());
            AddDetailsSection(cell, "WORK EXPERIENCE", expnList, new Dictionary<string, Func<EXP01, object>>
                {
                    { "Company", exp => exp.P01F03 },
                    { "Position", exp => exp.P01F04 },
                    { "Start Date", exp => exp.P01F05 },
                    { "End Date", exp => exp.P01F06 },
                    { "Responsibilities", exp => exp.P01F07 }
                });

            cell.AddElement(Chunk.NEWLINE);
            List<CER01> certificationsList = FetchDetailsForUser<CER01>(_httpContextAccessor.HttpContext.GetUserIdFromClaims());
            AddDetailsSection(cell, "Certifications", certificationsList, new Dictionary<string, Func<CER01, object>>
                {
                    { "Certificate", cer => cer.R01F03 },
                    { "Organization", cer => cer.R01F04 },
                    { "Date", cer => cer.R01F05 }
                });

            cell.AddElement(Chunk.NEWLINE);

            List<SKL01> skillsList = FetchDetailsForUser<SKL01>(_httpContextAccessor.HttpContext.GetUserIdFromClaims());
            AddSkillsSection(cell, "SKILLS", skillsList);

            List<LAN01> languageList = FetchDetailsForUser<LAN01>(_httpContextAccessor.HttpContext.GetUserIdFromClaims());
            AddLanguageSection(cell, "LANGUAGE", languageList);

            cell.AddElement(Chunk.NEWLINE);

            table.AddCell(cell);

            // Add the table to the document
            document.Add(table);
        }

        /// <summary>
        /// Adds basic user details to a PDF document.
        /// </summary>
        /// <param name="cell">The cell to which user details are added.</param>
        /// <param name="user">User data containing basic information.</param>
        private void AddBasicUserDetails(PdfPCell cell, USR01 user)
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
            string fullName = $"{user.R01F02} {user.R01F03}";
            Paragraph name = new Paragraph(fullName, FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 14));
            Paragraph title = new Paragraph("Fresher", FontFactory.GetFont(FontFactory.TIMES_BOLD, 11));
            detailsCell.AddElement(name);
            detailsCell.AddElement(title);
            // Add contact information
            Paragraph contactInfo = new Paragraph();
            contactInfo.Add(new Chunk("Email: ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 10)));
            contactInfo.Add(new Chunk($"{user.R01F04}\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
            contactInfo.Add(new Chunk("Phone: ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 10)));
            contactInfo.Add(new Chunk($"{user.R01F05}", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
            detailsCell.AddElement(contactInfo);

            nestedTable.AddCell(detailsCell);
            cell.AddElement(nestedTable);
        }

        /// <summary>
        /// Adds contact information to a PDF cell.
        /// </summary>
        /// <param name="cell">The PDF cell to which contact information is added.</param>
        /// <param name="email">The email address.</param>
        /// <param name="phone">The phone number.</param>
        private void AddDetailsSection<T>(PdfPCell cell, string title, List<T> dataList, Dictionary<string, Func<T, object>> propertyMap)
        {
            PdfPTable table = new PdfPTable(propertyMap.Count); 
            table.WidthPercentage = 100;

            // Add title row
            PdfPCell titleCell = new PdfPCell(new Phrase("\n" + title.ToUpper() + "\n\n" , FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12)));
            titleCell.Colspan = propertyMap.Count; // Span all columns
            titleCell.Border = PdfPCell.BOTTOM_BORDER;
            table.AddCell(titleCell);

            // Add header row
            foreach (var propertyEntry in propertyMap)
            {
                string propertyName = propertyEntry.Key;
                PdfPCell headerCell = new PdfPCell(new Phrase(propertyName.ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_BOLD, 10)));
                headerCell.Border = PdfPCell.NO_BORDER;
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

                    PdfPCell valueCell = new PdfPCell(new Phrase(cellValue, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
                    valueCell.Border = PdfPCell.NO_BORDER;
                    table.AddCell(valueCell);
                }
            }

            cell.AddElement(table);
        }

        /// <summary>
        /// Adds the name and professional title to a PDF cell.
        /// </summary>
        /// <param name="cell">The PDF cell to which the name and title are added.</param>
        /// <param name="firstName">The first name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        private void AddSkillsSection(PdfPCell document, string title, List<SKL01> skillsList)
        {
            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;

            Paragraph objective = new Paragraph();
            objective.Add(new Phrase(title.ToUpper(), FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12)));
            objective.Add(new Phrase("\n\n", FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 8)));

            PdfPCell objectiveCell = new PdfPCell(objective);
            objectiveCell.Border = Rectangle.BOTTOM_BORDER;
            table.AddCell(objectiveCell);

            document.AddElement(table);

            PdfPTable taskTable = new PdfPTable(1);
            taskTable.WidthPercentage = 100;

            foreach (var skill in skillsList)
            {
                PdfPCell cell = new PdfPCell(new Phrase(skill.L01F03, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
                cell.Border = PdfPCell.NO_BORDER;
                taskTable.AddCell(cell);
            }

            document.AddElement(taskTable);
            document.AddElement(Chunk.NEWLINE);
        }

        /// <summary>
        /// Adds a section for displaying languages to a PDF cell.
        /// </summary>
        /// <param name="document">The PDF cell to which the language section is added.</param>
        /// <param name="title">The title of the language section.</param>
        /// <param name="languageList">The list of languages to be displayed.</param>
        private void AddLanguageSection(PdfPCell document, string title, List<LAN01> languageList)
        {
            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;

            Paragraph objective = new Paragraph();
            objective.Add(new Phrase(title.ToUpper(), FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12)));
            objective.Add(new Phrase("\n\n", FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 8)));

            PdfPCell objectiveCell = new PdfPCell(objective);
            objectiveCell.Border = Rectangle.BOTTOM_BORDER;
            table.AddCell(objectiveCell);

            document.AddElement(table);

            PdfPTable taskTable = new PdfPTable(1);
            taskTable.WidthPercentage = 100;


            foreach (var language in languageList)
            {
                PdfPCell cell = new PdfPCell(new Phrase(language.N01F03, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
                cell.Border = PdfPCell.NO_BORDER;
                taskTable.AddCell(cell);
            }

            document.AddElement(taskTable);
            //document.AddElement(Chunk.NEWLINE);
        }

        /// <summary>
        /// Adds the objective section to a PDF cell.
        /// </summary>
        /// <param name="cell">The PDF cell to which the objective section is added.</param>
        private void AddObjectiveSection(PdfPCell cell)
        {

            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;

            Paragraph objective = new Paragraph();
            objective.Add(new Phrase("OBJECTIVE", FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12)));
            objective.Add(new Phrase("\n\n", FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 8)));

            PdfPCell objectiveCell = new PdfPCell(objective);
            objectiveCell.Border = Rectangle.BOTTOM_BORDER;
            table.AddCell(objectiveCell);

            PdfPCell obj = new PdfPCell(new Phrase("Highly motivated and detail-oriented individual, I am seeking a position where I can utilize my skills and experience to contribute effectively to the organization.", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
            obj.Border = PdfPCell.NO_BORDER;

            table.AddCell(obj);

            cell.AddElement(table);
        }

        /// <summary>
        /// Fetches user details from the database based on the provided user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The user details.</returns>
        private USR01 FetchUserDetails(int userId)
        {
            using (var db = _dbConnectionFactory.CreateConnection())
            {
                return db.SingleById<USR01>(userId);
            }
        }

        /// <summary>
        /// Fetches details of a specific type for the given user from the database.
        /// </summary>
        /// <typeparam name="T">The type of details to fetch.</typeparam>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The list of details.</returns>
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
