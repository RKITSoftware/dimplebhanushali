namespace Resume_Builder.Models.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for representing a Resume.
    /// </summary>
    public class DTORES02
    {
        /// <summary>
        /// Gets or sets the user details.
        /// </summary>
        public User UserDetails { get; set; }

        /// <summary>
        /// Gets or sets the list of education details.
        /// </summary>
        public List<Education> Education { get; set; }

        /// <summary>
        /// Gets or sets the list of work experience details.
        /// </summary>
        public List<WorkExperience> WorkExperience { get; set; }

        /// <summary>
        /// Gets or sets the list of project details.
        /// </summary>
        public List<Project> Projects { get; set; }

        /// <summary>
        /// Gets or sets the list of certificates.
        /// </summary>
        public List<Certificate>? Certificates { get; set; }

        /// <summary>
        /// Gets or sets the list of known languages.
        /// </summary>
        public List<string> KnownLanguages { get; set; }

        /// <summary>
        /// Gets or sets the list of skills.
        /// </summary>
        public List<string> Skills { get; set; }

    }

    /// <summary>
    /// Represents user details in a Resume.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the user.
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the age of the user.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the professional title of the user.
        /// </summary>
        public string ProfessionalTitle { get; set; }

        /// <summary>
        /// Gets or sets the profile image of the user.
        /// </summary>
        //public byte[] ProfileImage { get; set; }
    }

    /// <summary>
    /// Represents a certificate in a Resume.
    /// </summary>
    public class Certificate
    {
        /// <summary>
        /// Gets or sets the name of the certificate.
        /// </summary>
        public string CertificateName { get; set; }

        /// <summary>
        /// Gets or sets the issuing organization of the certificate.
        /// </summary>
        public string Organization { get; set; }
    }

    /// <summary>
    /// Represents education details in a Resume.
    /// </summary>
    public class Education
    {
        /// <summary>
        /// Gets or sets the institute name.
        /// </summary>
        public string Institute { get; set; }

        /// <summary>
        /// Gets or sets the degree obtained.
        /// </summary>
        public string Degree { get; set; }

        /// <summary>
        /// Gets or sets the field of study.
        /// </summary>
        public string FieldOfStudy { get; set; }

        /// <summary>
        /// Gets or sets the year of education.
        /// </summary>
        public int EducationYear { get; set; }
    }

    /// <summary>
    /// Represents work experience details in a Resume.
    /// </summary>
    public class WorkExperience
    {
        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the position held.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the start date of the work experience.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the work experience.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the responsibilities in the position.
        /// </summary>
        public string Responsibilities { get; set; }
    }

    /// <summary>
    /// Represents project details in a Resume.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the project description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the start date of the project.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the project.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
