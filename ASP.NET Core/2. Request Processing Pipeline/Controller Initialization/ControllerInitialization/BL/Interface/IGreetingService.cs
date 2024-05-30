namespace ControllerInitialization.BL.Interface
{
    /// <summary>
    /// Defines a contract for a service that provides greeting messages.
    /// </summary>
    public interface IGreetingService
    {
        /// <summary>
        /// Returns a greeting message for the specified name.
        /// </summary>
        /// <param name="name">The name to greet.</param>
        /// <returns>A greeting message.</returns>
        string Greet(string name);
    }
}
