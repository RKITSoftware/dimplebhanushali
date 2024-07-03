using MySql.Data.MySqlClient;
using Resume_Builder.Models;
using System.Data;
using System.Reflection;

namespace Resume_Builder.Data.Services
{
    public class DbCommonContext
    {
        public Response GetData(string connectionString,int userId)
        {
            var response = new Response();

            try
            {

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var sql = @"
                        SELECT 
                            usr.R01F01 as UserId,
                            usr.R01F02 as FirstName,
                            usr.R01F03 as LastName,
                            usr.R01F04 as Email,
                            usr.R01F05 as Mobile,
                            usr.R01F06 as Age,
                            usr.R01F08 as CreatedDate,
                            usr.R01F09 as UpdatedDate,
                            cer.R01F03 as CertificateName,
                            cer.R01F04 as IssuingOrganization,
                            cer.R01F05 as IssueDate,
                            edu.U01F03 as Institution,
                            edu.U01F04 as Degree,
                            edu.U01F05 as FieldOfStudy,
                            edu.U01F06 as EducationYear,
                            exp.P01F03 as Company,
                            exp.P01F04 as Position,
                            exp.P01F05 as StartDate,
                            exp.P01F06 as EndDate,
                            exp.P01F07 as ExperienceDescription,
                            lan.N01F03 as Language,
                            lan.N01F04 as ProficiencyLevel,
                            pro.O01F03 as ProjectName,
                            pro.O01F04 as ProjectDescription,
                            pro.O01F05 as ProjectStartDate,
                            pro.O01F06 as ProjectEndDate,
                            skl.L01F03 as SkillName,
                            skl.L01F04 as SkillProficiency
                        FROM USR01 usr
                        LEFT JOIN CER01 cer ON usr.R01F01 = cer.UserId
                        LEFT JOIN EDU01 edu ON usr.R01F01 = edu.UserId
                        LEFT JOIN EXP01 exp ON usr.R01F01 = exp.UserId
                        LEFT JOIN LAN01 lan ON usr.R01F01 = lan.UserId
                        LEFT JOIN PRO01 pro ON usr.R01F01 = pro.UserId
                        LEFT JOIN SKL01 skl ON usr.R01F01 = skl.UserId
                        WHERE 
                            usr.R01F01 = @UserId";

                    var command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        response.Data = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Failed to retrieve data: {ex.Message}";
            }

            return response;
        }

        public Response GetById<T>(int id, string connectionString) where T : class
        {
            var response = new Response();

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string tableName = typeof(T).Name;
                    var sql = $"SELECT * FROM {tableName} WHERE UserId = @UserId";

                    var command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@UserId", id);

                    using (var reader = command.ExecuteReader())
                    {
                        var entities = new List<T>();
                        while (reader.Read())
                        {
                            var entity = Activator.CreateInstance<T>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var propertyName = reader.GetName(i);
                                var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                                if (property != null && !reader.IsDBNull(i))
                                {
                                    var value = reader.GetValue(i);
                                    property.SetValue(entity, value);
                                }
                            }

                            entities.Add(entity);
                        }

                        response.Data = entities;
                    }
                }
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Failed to retrieve data: {ex.Message}";
            }

            return response;
        }
    }
}
