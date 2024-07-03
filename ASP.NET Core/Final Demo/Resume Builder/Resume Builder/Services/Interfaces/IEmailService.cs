namespace Resume_Builder.DL.Interfaces
{
    /// <summary>
    /// Interface for any message sending 
    /// </summary>
    public interface IEmailService
    {
        #region Public Methods

        /// <summary>
        /// Sending message to user
        /// </summary>
        /// <param name="email"> email id of user</param>
        /// <param name="body"> body for sending message </param>
        /// <param name="attachmentBytes"> Resume Attachment </param>
        void Send(string email, string body, byte[] attachmentBytes);

        #endregion

    }
}
