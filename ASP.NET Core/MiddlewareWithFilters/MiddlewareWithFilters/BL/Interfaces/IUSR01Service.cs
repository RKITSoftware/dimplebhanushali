using MiddlewareWithFilters.Helpers;
using MiddlewareWithFilters.Models;
using MiddlewareWithFilters.Models.DTO;

namespace MiddlewareWithFilters.BL.Interfaces
{
    /// <summary>
    /// Interface for user service operations.
    /// </summary>
    public interface IUSR01Service
    {
        /// <summary>
        /// Enum Operation.
        /// </summary>
        enmOperation Operation { get; set; }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A response containing the result of the operation.</returns>
        Response Get();

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A response containing the result of the operation.</returns>
        Response Get(int id);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <returns>A response containing the result of the operation.</returns>
        Response Delete();

        /// <summary>
        /// Prepares to save a user object.
        /// </summary>
        /// <param name="objUSR">The user object to save.</param>
        void PreSave(DTOUSR01 objUSR);

        /// <summary>
        /// Validates user data.
        /// </summary>
        /// <returns>A response containing the validation result.</returns>
        Response Validate();

        /// <summary>
        /// Saves user data.
        /// </summary>
        /// <returns>A response containing the result of the operation.</returns>
        Response Save();

        /// <summary>
        /// Validates user data before deletion.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A response containing the validation result.</returns>
        Response ValidateOnDelete(int id);
    }
}
