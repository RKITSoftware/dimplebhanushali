namespace Resume_Builder.Models.DTO
{
    public class RES02
    {
        public User UserDetails { get; set; }
        public List<Education> Education { get; set; }
        public List<WorkExperience> WorkExperience { get; set; }
        public List<Project> Projects { get; set; }
        public List<Certificate>? Certificates { get; set; }
        public List<string> KnownLanguages { get; set; }
        public List<string> Skills { get; set; }
    }

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public string ProfessionalTitle { get; set; }

    }

    public class Certificate
    {
        public string CertificateName { get; set; }
        public string Organization { get; set; }
    }

    public class Education
    {
        public string Institute { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public int EducationYear { get; set; }
    }

    public class WorkExperience
    {
        public string Company { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Responsibilities { get; set; }
    }

    public class Project
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
