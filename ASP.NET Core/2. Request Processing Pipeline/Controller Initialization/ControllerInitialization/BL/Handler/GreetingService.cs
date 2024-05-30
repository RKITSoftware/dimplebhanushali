using ControllerInitialization.BL.Interface;

namespace ControllerInitialization.BL.Handler
{
    /// <summary>
    /// Provides greeting services.
    /// </summary>
    public class GreetingService : IGreetingService
    {
        /// <summary>
        /// Returns a greeting message for the specified name.
        /// </summary>
        /// <param name="name">The name to greet.</param>
        /// <returns>A greeting message.</returns>
        public string Greet(string name)
        {
            return $"Hello, {name}!";
        }
    }
}
