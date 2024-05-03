using Bogus;
using Certificate_Generator.Data;
using Certificate_Generator.Helpers;
using Certificate_Generator.Models;
using Certificate_Generator.Models.POCO;
using ServiceStack.OrmLite;
using System.Data;

namespace Certificate_Generator.BL
{
    public class BLCER01Handler
    {
        private readonly DbConnectionFactory _dbConnectionFactory;
        private Response response;
        private EnumMessage operation;

        public BLCER01Handler(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        // Create a new certificate template
        public Response CreateCertificateTemplate(CER01 template)
        {
            response = new Response();

            try
            {
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                db.Insert(template);
                response.Message = "Certificate template created successfully.";
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Failed to create certificate template: {ex.Message}";
            }

            return response;
        }

        // Retrieve all certificate templates
        public Response GetAllCertificateTemplates()
        {
            response = new Response();

            try
            {
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                List<CER01> templates = db.Select<CER01>();
                response.Data = templates;
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Failed to retrieve certificate templates: {ex.Message}";
            }

            return response;
        }

        // Retrieve a certificate template by ID
        public Response GetCertificateTemplateById(int id)
        {
            response = new Response();

            try
            {
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                CER01 template = db.SingleById<CER01>(id);
                if (template != null)
                {
                    response.Data = template;
                }
                else
                {
                    response.HasError = true;
                    response.Message = "Certificate template not found.";
                }
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Failed to retrieve certificate template: {ex.Message}";
            }

            return response;
        }

        // Update a certificate template
        public Response UpdateCertificateTemplate(CER01 template)
        {
            response = new Response();

            try
            {
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                db.Update(template);
                response.Message = "Certificate template updated successfully.";
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Failed to update certificate template: {ex.Message}";
            }

            return response;
        }

        // Delete a certificate template by ID
        public Response DeleteCertificateTemplate(int id)
        {
            response = new Response();

            try
            {
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                db.DeleteById<CER01>(id);
                response.Message = "Certificate template deleted successfully.";
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Failed to delete certificate template: {ex.Message}";
            }

            return response;
        }

        // Generate 10 certificate templates for testing
        public void GenerateTestCertificateTemplates()
        {
            // Example using Faker.NET to generate sample data
            var faker = new Faker<CER01>()
                .RuleFor(c => c.R01F02, f => f.Lorem.Word())
                .RuleFor(c => c.R01F03, f => f.PickRandom(new[] { "Type A", "Type B", "Type C" }))
                .RuleFor(c => c.R01F04, f => f.Lorem.Sentence())
                .RuleFor(c => c.R01F05, f => f.Date.Past())
                .RuleFor(c => c.R01F06, f => f.Date.Recent());

            List<CER01> sampleTemplates = faker.Generate(10);
            using IDbConnection db = _dbConnectionFactory.CreateConnection();
            db.InsertAll<CER01>(sampleTemplates);
        }
    }

}

