using Historical_Events.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace Historical_Events.BL
{
    public class BLCsvToDatabaseService
    {
        public void ImportCsvToDatabase(string csvFilePath)
        {
            List<HSTEVT01> records = ReadCsvFile(csvFilePath);
            InsertRecordsIntoDatabase(records);
        }

        private List<HSTEVT01> ReadCsvFile(string filePath)
        {
            List<HSTEVT01> records = new List<HSTEVT01>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    HSTEVT01 record = new HSTEVT01
                    {
                        t01f02 = int.Parse(values[0]),
                        t01f03 = values[1],
                        t01f04 = values[2],
                        t01f05 = 0 
                    };
                    records.Add(record);
                }
            }
            return records;
        }

        private void InsertRecordsIntoDatabase(List<HSTEVT01> records)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (HSTEVT01 record in records)
                {
                    string query = $@"INSERT INTO 
                                                    hstevt01 
                                                    (t01f02, t01f03, t01f04, t01f05) 
                                            VALUES 
                                                    ({record.t01f02}, '{record.t01f03}', '{record.t01f04}', {record.t01f05})";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
