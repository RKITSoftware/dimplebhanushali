﻿namespace Resume_Builder.DL.Interfaces
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
        /// <param name="message"> message which we are sending to user </param>
        /// <param name="address"> email id on whiich we are sending message </param>
        void Send(string email, string body, byte[] attachmentBytes);

        #endregion

    }
}
