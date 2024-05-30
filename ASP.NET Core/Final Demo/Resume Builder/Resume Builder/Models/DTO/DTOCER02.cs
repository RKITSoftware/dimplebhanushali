namespace Resume_Builder.Models
{
    /// <summary>
    /// Data Transfer Object (DTO) for Certificate Request.
    /// </summary>
    public class DTOCER02
    {
        /// <summary>
        /// Participant Name
        /// </summary>
        /// <example>Dimple Mithiya</example>
        public string ParticipantName { get; set; }

        /// <summary>
        /// Theme
        /// </summary>
        /// <example>Artificial Intelligence</example>
        public string Theme { get; set; }

        /// <summary>
        /// Certificate Type
        /// </summary>
        /// <example>Participation</example>
        public string CertificateType { get; set; } = "Participation";

        /// <summary>
        /// Date of the certificate issuance
        /// </summary>
        /// <example>2023-05-29</example>
        public DateTime Date { get; set; } 

        /// <summary>
        /// The award being given
        /// </summary>
        /// <example>Excellence in AI</example>
        public string Award { get; set; }

        /// <summary>
        /// Name of the issuer
        /// </summary>
        /// <example>OpenAI</example>
        public string IssuerName { get; set; }

        /// <summary>
        /// Description or additional information for the certificate
        /// </summary>
        /// <example>For outstanding performance in AI development</example>
        public string Description { get; set; }
    }
}
