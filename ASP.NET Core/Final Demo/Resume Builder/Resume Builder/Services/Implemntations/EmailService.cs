﻿using Resume_Builder.DL.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Resume_Builder.DL.Implemntations
{
    /// <summary>
    /// Email Service which implements ISender
    /// </summary>
    public class EmailService : IEmailService
    {
        #region Private Members

        private readonly IConfiguration _config;
        private readonly ILogging _logging;

        #endregion

        #region Constructor

        public EmailService(IConfiguration config, ILogging logging)
        {
            _logging = logging;
            _config = config;
        }

        #endregion

        #region Public Methods

        public void Send(string email, string body, byte[] attachmentBytes)
        {
            try
            {
                string senderEmail = _config["EmailSettings:SenderEmail"];
                string senderPassword = _config["EmailSettings:SenderPassword"];

                MailMessage mail = new MailMessage(senderEmail, email);
                mail.Subject = "Resume generated By Certificate Generator";
                mail.Body = body;

                if (attachmentBytes != null)
                {
                    // Attach resume as PDF
                    mail.Attachments.Add(new Attachment(new MemoryStream(attachmentBytes), "resume.pdf", "application/pdf"));
                }
                else
                {
                    mail.Attachments.Clear();
                }

                SmtpClient smtpClient = new SmtpClient(_config["EmailSettings:SmtpClient"]);
                smtpClient.Port = Convert.ToInt32(_config["EmailSettings:Port"]);
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);

                _logging.LogTrace("Email sent successfully to: " + email);
            }
            catch (Exception ex)
            {
                _logging.LogException(ex, "Failed to send email to: " + email);
            }
        }

        #endregion
    }
}