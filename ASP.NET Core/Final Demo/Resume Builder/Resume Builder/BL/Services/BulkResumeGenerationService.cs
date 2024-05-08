//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using Newtonsoft.Json;
//using Resume_Builder.DL.Interfaces;
//using Resume_Builder.Models.POCO;
//using System.IO.Compression;
//using System.Net;

//namespace Resume_Builder.BL.Services
//{
//    public class BulkResumeGenerationService
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly IEmailService _sender;

//        public BulkResumeGenerationService(IHttpContextAccessor httpContextAccessor,
//            IEmailService sender)
//        {
//            _httpContextAccessor = httpContextAccessor;
//            _sender = sender;
//        }

//        public byte[] GenerateResumesFromJson(string json)
//        {
//            try
//            {
//                var jsonData = ParseJson(json); // Call ParseJson method to parse the JSON data
//                List<byte[]> resumes = new List<byte[]>();

//                foreach (var record in jsonData)
//                {
//                    // Convert the record to a Dictionary<string, string>
//                    var stringRecord = record.ToDictionary(kvp => kvp.Key, kvp => string.Join(",", kvp.Value));

//                    byte[] resumeBytes = GenerateResumeFromRecord(stringRecord);
//                    resumes.Add(resumeBytes);
//                    // SendEmailWithResume(record, resumeBytes); 
//                }

//                // Create a zip file containing all the generated resumes
//                return CreateZip(resumes);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("An error occurred while generating resumes: " + ex.Message);
//                return null;
//            }
//        }

//        private byte[] GenerateResumeFromRecord(Dictionary<string, string> record)
//        {
//            // Create a new PDF document
//            Document document = new Document();
//            MemoryStream memoryStream = new MemoryStream();
//            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
//            document.Open();

//            // Add user details to the PDF
//            AddUserDetails(document, record);

//            // Close the document
//            document.Close();

//            // Get the generated resume as byte array
//            return memoryStream.ToArray();
//        }

//        private void SendEmailWithResume(Dictionary<string, string> record, byte[] resume)
//        {
//            try
//            {
//                // Send email to the recipient with the resume attached
//                string email = record.ContainsKey("Email") ? record["Email"] : "";
//                _sender.Send(email, "Please find attached your resume.", resume);
//            }
//            catch (Exception ex)
//            {
//                // Handle any exceptions
//                Console.WriteLine("An error occurred while sending an email: " + ex.Message);
//            }
//        }

//        public List<Dictionary<string, string[]>> ParseJson(string jsonData)
//        {
//            List<Dictionary<string, string[]>> jsonDataList = new List<Dictionary<string, string[]>>();

//            // Deserialize the JSON string into a list of dictionaries
//            var records = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);

//            // Iterate over each record in the JSON data
//            foreach (var record in records)
//            {
//                Dictionary<string, string[]> parsedRecord = new Dictionary<string, string[]>();

//                // Extract the key-value pairs from the record
//                foreach (var kvp in record)
//                {
//                    // Split keys and values by comma
//                    string[] keys = kvp.Key.Split(',');
//                    string[] values = kvp.Value.Split(',');

//                    // Add key-value pairs to the parsed record
//                    for (int i = 0; i < keys.Length; i++)
//                    {
//                        // Trim quotes from keys and values if present
//                        string key = keys[i].Trim('"');
//                        string value = values[i].Trim('"');

//                        // Check if the key already exists in the dictionary
//                        if (parsedRecord.ContainsKey(key))
//                        {
//                            // If the key exists, append the value to its array
//                            string[] existingValues = parsedRecord[key];
//                            Array.Resize(ref existingValues, existingValues.Length + 1);
//                            existingValues[existingValues.Length - 1] = value;
//                            parsedRecord[key] = existingValues;
//                        }
//                        else
//                        {
//                            // If the key doesn't exist, create a new array with the value
//                            parsedRecord.Add(key, new string[] { value });
//                        }
//                    }
//                }

//                // Add the parsed record to the list
//                jsonDataList.Add(parsedRecord);
//            }

//            return jsonDataList;
//        }

//        public List<Dictionary<string, string>> ParseCsv(Stream csvStream)
//        {
//            List<Dictionary<string, string>> csvData = new List<Dictionary<string, string>>();

//            csvStream.Position = 0;

//            using (var reader = new StreamReader(csvStream))
//            {
//                // Read the first line to get the headers
//                string headerLine = reader.ReadLine();
//                string[] headers = headerLine.Split(',');

//                // Use a dictionary to track the occurrence of each header
//                Dictionary<string, int> headerCount = new Dictionary<string, int>();

//                // Remove the enclosing double quotes from each header
//                for (int i = 0; i < headers.Length; i++)
//                {
//                    string header = headers[i].Trim('"');

//                    // Check if the header already exists in the dictionary
//                    if (headerCount.ContainsKey(header))
//                    {
//                        // Increment the count for this header
//                        headerCount[header]++;

//                        // Append an index to the header to make it unique
//                        header += "_" + headerCount[header];
//                    }
//                    else
//                    {
//                        // Add the header to the dictionary with count 1
//                        headerCount.Add(header, 1);
//                    }

//                    headers[i] = header;
//                }

//                // Read the rest of the lines to get the data
//                while (!reader.EndOfStream)
//                {
//                    string dataLine = reader.ReadLine();
//                    string[] values = dataLine.Split(',');

//                    // Create a dictionary to store the key-value pairs for this row
//                    Dictionary<string, string> record = new Dictionary<string, string>();

//                    // Remove the enclosing double quotes from each value and associate it with the corresponding header
//                    for (int i = 0; i < headers.Length && i < values.Length; i++)
//                    {
//                        string value = values[i].Trim('"');
//                        record.Add(headers[i], value);
//                    }

//                    // Add the record to the list
//                    csvData.Add(record);
//                }
//            }

//            return csvData;
//        }

//        private void AddUserDetails(Document document, Dictionary<string, string> record)
//        {
//            PdfPTable table = new PdfPTable(1); // 1 column for user information
//            table.WidthPercentage = 100;
//            table.DefaultCell.Border = PdfPCell.NO_BORDER;

//            PdfPCell infoCell = new PdfPCell();
//            infoCell.Border = PdfPCell.NO_BORDER;

//            // Add user name and title
//            string firstName = record.ContainsKey("First Name") ? record["First Name"] : "";
//            string lastName = record.ContainsKey("Last Name") ? record["Last Name"] : "";
//            string fullName = $"{firstName} {lastName}";
//            Paragraph name = new Paragraph(fullName, FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 14));
//            Paragraph title = new Paragraph("Professional Title", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12));

//            infoCell.AddElement(name);
//            infoCell.AddElement(title);
//            infoCell.AddElement(new Paragraph(string.Format("{0}", new string('_',100)), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));

//            // Add contact information
//            string email = record.ContainsKey("Email") ? record["Email"] : "";
//            string mobile = record.ContainsKey("Mobile") ? record["Mobile"] : "";
//            Paragraph contactInfo = new Paragraph($"Email: {email} \nPhone: {mobile}", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
//            infoCell.AddElement(contactInfo);

//            // Add education details
//            infoCell.AddElement(Chunk.NEWLINE);
//            AddDetailsSection(infoCell, "EDUCATION", record, "Institute", "Degree", "Field of Study", "Education Year");

//            // Add work experience details
//            infoCell.AddElement(Chunk.NEWLINE);
//            AddDetailsSection(infoCell, "WORK EXPERIENCE", record, "Company", "Position", "Start Date", "End Date");

//            // Add language details
//            infoCell.AddElement(Chunk.NEWLINE);
//            AddDetailsSection(infoCell, "LANGUAGES", record, "Known Language");

//            // Add project details
//            infoCell.AddElement(Chunk.NEWLINE);
//            AddDetailsSection(infoCell, "PROJECTS", record, "Project Name", "Description", "Start Date", "End Date");

//            table.AddCell(infoCell);

//            document.Add(table);
//        }

//        private void AddSkillsSection(PdfPCell document, string title, List<SKL01> skillsList)
//        {
//            document.AddElement(new Paragraph());
//            Chunk titleChunk = new Chunk(title, FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12));
//            document.AddElement(titleChunk);
//            Paragraph line = new Paragraph(string.Format("{0}", new string('_',100)), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
//            document.AddElement(line);

//            PdfPTable table = new PdfPTable(1);
//            table.WidthPercentage = 100;

//            foreach (var skill in skillsList)
//            {
//                PdfPCell cell = new PdfPCell(new Phrase(skill.L01F03, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)));
//                cell.Border = PdfPCell.NO_BORDER;
//                table.AddCell(cell);
//            }

//            document.AddElement(table);
//            document.AddElement(Chunk.NEWLINE);
//        }

//        private void AddDetailsSection(PdfPCell cell, string sectionTitle, Dictionary<string, string> record, params string[] fields)
//        {
//            cell.AddElement(new Paragraph());
//            Chunk titleChunk = new Chunk(sectionTitle.ToUpper(), FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12));
//            cell.AddElement(titleChunk);
//            cell.AddElement(new Paragraph("________________________________", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));

//            PdfPTable table = new PdfPTable(fields.Length);
//            table.WidthPercentage = 100;

//            foreach (string field in fields)
//            {
//                if (field == "Known Language" || field == "Skills")
//                    continue;

//                string value = record.ContainsKey(field) ? record[field] : "";
//                PdfPCell cell1 = new PdfPCell(new Phrase(field, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
//                cell1.Border = PdfPCell.NO_BORDER;
//                table.AddCell(cell1);

//                PdfPCell cell2 = new PdfPCell(new Phrase(value, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
//                cell2.Border = PdfPCell.NO_BORDER;
//                table.AddCell(cell2);
//            }

//            cell.AddElement(table);
//            cell.AddElement(Chunk.NEWLINE);
//        }

//        private void SendEmailsWithResumes(List<string> emails, List<byte[]> resumes)
//        {
//            try
//            {
//                for (int i = 0; i < emails.Count; i++)
//                {
//                    string email = emails[i];
//                    byte[] resume = resumes[i];

//                    // Send email to the recipient with the resume attached
//                    _sender.Send(email, "Please find attached your resume.", resume);
//                }
//            }
//            catch (Exception ex)
//            {
//                // Handle any exceptions
//                Console.WriteLine("An error occurred while sending emails: " + ex.Message);
//            }
//        }

//        private byte[] CreateZip(List<byte[]> files)
//        {
//            using (var memoryStream = new MemoryStream())
//            {
//                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
//                {
//                    for (int i = 0; i < files.Count; i++)
//                    {
//                        var entry = zipArchive.CreateEntry($"resume_{i + 1}.pdf");
//                        using (var entryStream = entry.Open())
//                        {
//                            entryStream.Write(files[i], 0, files[i].Length);
//                        }
//                    }
//                }
//                return memoryStream.ToArray();
//            }
//        }
//    }
//}

using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Resume_Builder.DL.Interfaces;
using Resume_Builder.Models.POCO;
using System.IO.Compression;
using System.Net;

namespace Resume_Builder.BL.Services
{
    public class BulkResumeGenerationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _sender;

        public BulkResumeGenerationService(IHttpContextAccessor httpContextAccessor,
            IEmailService sender)
        {
            _httpContextAccessor = httpContextAccessor;
            _sender = sender;
        }

        public byte[] GenerateResumesFromJson(string json)
        {
            try
            {
                var jsonData = ParseJson(json); // Call ParseJson method to parse the JSON data
                List<byte[]> resumes = new List<byte[]>();

                foreach (var record in jsonData)
                {
                    // Convert the record to a Dictionary<string, string>
                    var stringRecord = record.ToDictionary(kvp => kvp.Key, kvp => string.Join(",", kvp.Value));

                    // Parse the skills for this record
                    List<SKL01> skills = ParseSkills(record["Skills"]);
                    List<LAN01> languages = ParseLanguage(record["Known Language"]);

                    // Generate resume bytes and add to the list of resumes
                    byte[] resumeBytes = GenerateResumeFromRecord(stringRecord, skills,languages);
                    resumes.Add(resumeBytes);
                }

                // Create a zip file containing all the generated resumes
                return CreateZip(resumes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while generating resumes: " + ex.Message);
                return null;
            }
        }

        private List<SKL01> ParseSkills(string[] skillsData)
        {
            List<SKL01> skillsList = new List<SKL01>();

            foreach (var skillData in skillsData)
            {
                // Parse the skill data and create SKL01 objects
                SKL01 skill = new SKL01();
                skill.L01F03 = skillData; // Assuming L01F03 is the field for skill name
                                          // Add other properties as needed

                skillsList.Add(skill);
            }

            return skillsList;
        }

        private List<LAN01> ParseLanguage(string[] skillsData)
        {
            List<LAN01> skillsList = new List<LAN01>();

            foreach (var skillData in skillsData)
            {
                // Parse the skill data and create SKL01 objects
                LAN01 skill = new LAN01();
                skill.N01F03 = skillData; // Assuming L01F03 is the field for skill name
                                          // Add other properties as needed

                skillsList.Add(skill);
            }

            return skillsList;
        }

        private byte[] GenerateResumeFromRecord(Dictionary<string, string> record, List<SKL01> skillsList, List<LAN01> languageList)
        {
            // Create a new PDF document
            Document document = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            // Add user details to the PDF
            AddUserDetails(document, record, skillsList, languageList);

            // Close the document
            document.Close();

            // Get the generated resume as byte array
            return memoryStream.ToArray();
        }

        private void SendEmailWithResume(Dictionary<string, string> record, byte[] resume)
        {
            try
            {
                // Send email to the recipient with the resume attached
                string email = record.ContainsKey("Email") ? record["Email"] : "";
                _sender.Send(email, "Please find attached your resume.", resume);
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine("An error occurred while sending an email: " + ex.Message);
            }
        }

        public List<Dictionary<string, string[]>> ParseJson(string jsonData)
        {
            List<Dictionary<string, string[]>> jsonDataList = new List<Dictionary<string, string[]>>();

            // Deserialize the JSON string into a list of dictionaries
            var records = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);

            // Iterate over each record in the JSON data
            foreach (var record in records)
            {
                Dictionary<string, string[]> parsedRecord = new Dictionary<string, string[]>();

                // Extract the key-value pairs from the record
                foreach (var kvp in record)
                {
                    // Split keys and values by comma
                    string[] keys = kvp.Key.Split(',');
                    string[] values = kvp.Value.Split(',');

                    // Add key-value pairs to the parsed record
                    for (int i = 0; i < keys.Length; i++)
                    {
                        // Trim quotes from keys and values if present
                        string key = keys[i].Trim('"');
                        string value = values[i].Trim('"');

                        // Check if the key already exists in the dictionary
                        if (parsedRecord.ContainsKey(key))
                        {
                            // If the key exists, append the value to its array
                            string[] existingValues = parsedRecord[key];
                            Array.Resize(ref existingValues, existingValues.Length + 1);
                            existingValues[existingValues.Length - 1] = value;
                            parsedRecord[key] = existingValues;
                        }
                        else
                        {
                            // If the key doesn't exist, create a new array with the value
                            parsedRecord.Add(key, new string[] { value });
                        }
                    }
                }

                // Add the parsed record to the list
                jsonDataList.Add(parsedRecord);
            }

            return jsonDataList;
        }

        private void AddUserDetails(Document document, Dictionary<string, string> record, List<SKL01> skillsList, List<LAN01> languageList)
        {
            PdfPTable table = new PdfPTable(1); // 1 column for user information
            table.WidthPercentage = 100;
            table.DefaultCell.Border = PdfPCell.NO_BORDER;

            PdfPCell infoCell = new PdfPCell();
            infoCell.Border = PdfPCell.NO_BORDER;

            Image photo = Image.GetInstance(new WebClient().DownloadData("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSlSUzObn1SwCdJsnu7rFnYNr3SFGIh76XkoA&s"));
            photo.ScaleAbsolute(100f, 100f);
            infoCell.AddElement(photo);

           
            // Add user name and title
            string firstName = record.ContainsKey("First Name") ? record["First Name"] : "";
            string lastName = record.ContainsKey("Last Name") ? record["Last Name"] : "";
            string fullName = $"{firstName} {lastName}";
            Paragraph name = new Paragraph(fullName, FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 14));
            Paragraph title = new Paragraph("Professional Title", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12));

            infoCell.AddElement(name);
            infoCell.AddElement(title);
            infoCell.AddElement(new Paragraph(string.Format("{0}", new string('_', 100)), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));

            // Add contact information
            string email = record.ContainsKey("Email") ? record["Email"] : "";
            string mobile = record.ContainsKey("Mobile") ? record["Mobile"] : "";
            Paragraph contactInfo = new Paragraph();
            contactInfo.Add(new Chunk("Email: ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 10))); // Bold font for "Email: "
            contactInfo.Add(new Chunk($"{email}\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
            contactInfo.Add(new Chunk("Phone: ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 10))); // Bold font for "Phone: "
            contactInfo.Add(new Chunk($"{mobile}", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
            infoCell.AddElement(contactInfo);

            //Add ObjectiveSection Objective Section
            infoCell.AddElement(Chunk.NEWLINE);
            AddObjectiveSection(infoCell);

            // Add education details
            infoCell.AddElement(Chunk.NEWLINE);
            AddDetailsSection(infoCell, "EDUCATION", record, "Institute", "Degree", "Field of Study", "Education Year");

            // Add work experience details
            infoCell.AddElement(Chunk.NEWLINE);
            AddDetailsSection(infoCell, "WORK EXPERIENCE", record, "Company", "Position", "Start Date", "End Date");

            // Add language details
            infoCell.AddElement(Chunk.NEWLINE);
            AddLanguagesSection(infoCell, "LANGUAGES", languageList);

            // Add skills details
            infoCell.AddElement(Chunk.NEWLINE);
            AddSkillsSection(infoCell, "SKILLS", skillsList);

            // Add project details
            infoCell.AddElement(Chunk.NEWLINE);
            AddDetailsSection(infoCell, "PROJECTS", record, "Project Name", "Description", "Start Date", "End Date");

            table.AddCell(infoCell);

            document.Add(table);
        }

        private void AddLanguagesSection(PdfPCell cell, string title, List<LAN01> skillsList)
        {
            cell.AddElement(new Paragraph());
            Chunk titleChunk = new Chunk(title, FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12));
            cell.AddElement(titleChunk);
            Paragraph line = new Paragraph(string.Format("{0}", new string('_', 100)), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
            cell.AddElement(line);

            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;

            foreach (var skill in skillsList)
            {
                PdfPCell skillCell = new PdfPCell(new Phrase(skill.N01F03, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)));
                skillCell.Border = PdfPCell.NO_BORDER;
                table.AddCell(skillCell);
            }

            cell.AddElement(table);
            cell.AddElement(Chunk.NEWLINE);
        }

        private void AddSkillsSection(PdfPCell cell, string title, List<SKL01> skillsList)
        {
            cell.AddElement(new Paragraph());
            Chunk titleChunk = new Chunk(title, FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12));
            cell.AddElement(titleChunk);
            Paragraph line = new Paragraph(string.Format("{0}", new string('_', 100)), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
            cell.AddElement(line);

            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;

            foreach (var skill in skillsList)
            {
                PdfPCell skillCell = new PdfPCell(new Phrase(skill.L01F03, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)));
                skillCell.Border = PdfPCell.NO_BORDER;
                table.AddCell(skillCell);
            }

            cell.AddElement(table);
            cell.AddElement(Chunk.NEWLINE);
        }

        private void AddDetailsSection(PdfPCell cell, string sectionTitle, Dictionary<string, string> record, params string[] fields)
        {
            cell.AddElement(new Paragraph());
            Chunk titleChunk = new Chunk(sectionTitle.ToUpper(), FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12)); // Bold and italic font for section title
            cell.AddElement(titleChunk);
            Paragraph line = new Paragraph(string.Format("{0}", new string('_', 100)), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
            cell.AddElement(line);

            PdfPTable table = new PdfPTable(fields.Length);
            table.WidthPercentage = 100;

            foreach (string field in fields)
            {
                string value = record.ContainsKey(field) ? record[field] : "";
                PdfPCell cell1 = new PdfPCell(new Phrase(field, FontFactory.GetFont(FontFactory.TIMES_BOLD, 10))); // Bold font for field label
                cell1.Border = PdfPCell.NO_BORDER;
                table.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell(new Phrase(value, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));
                cell2.Border = PdfPCell.NO_BORDER;
                table.AddCell(cell2);
            }

            cell.AddElement(table);
            cell.AddElement(Chunk.NEWLINE);
        }


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

        private void AddObjectiveSection(PdfPCell cell)
        {
            cell.AddElement(new Paragraph());
            Chunk titleChunk = new Chunk("OBJECTIVE".ToUpper(), FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 12));
            cell.AddElement(titleChunk);
            cell.AddElement(new Paragraph(string.Format("{0}", new string('_', 100)), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)));

            string objectiveText = "Highly motivated and detail-oriented individual, I am seeking a position where I can utilize my skills and experience to contribute effectively to the organization.";
            Paragraph objective = new Paragraph(objectiveText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
            cell.AddElement(objective);
            cell.AddElement(Chunk.NEWLINE);
        }

    }
}
