using System;

namespace Virtual_Diary.Exceptions
{
    /// <summary>
    /// Represents a custom exception in the Virtual Diary application.
    /// </summary>
    public class CustomException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public CustomException(string message) : base(message)
        {
        }
    }
}
