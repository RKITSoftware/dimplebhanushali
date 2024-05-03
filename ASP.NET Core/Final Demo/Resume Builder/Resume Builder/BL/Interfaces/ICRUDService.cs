using Resume_Builder.Models;

namespace Resume_Builder.BL.Interfaces
{
    /// <summary>
    /// Interface defining CRUD (Create, Read, Update, Delete) operations and user details retrieval.
    /// </summary>
    /// <typeparam name="T">The type of the model class.</typeparam>
    public interface ICRUDService<T> where T : class
    {
        /// <summary>
        /// Retrieves all items.
        /// </summary>
        /// <returns>A response containing the result of the operation.</returns>
        Response Get();

        /// <summary>
        /// Retrieves an item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        /// <returns>A response containing the result of the operation.</returns>
        Response Get(int id);

        /// <summary>
        /// Deletes an item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <returns>A response containing the result of the operation.</returns>
        Response Delete(int id);

        /// <summary>
        /// Performs actions before saving an object.
        /// </summary>
        /// <param name="obj">The object to be saved.</param>
        void PreSave(object obj);

        /// <summary>
        /// Validates the object before saving.
        /// </summary>
        /// <returns>A response containing the validation result.</returns>
        Response Validate();

        /// <summary>
        /// Saves the object.
        /// </summary>
        /// <returns>A response containing the result of the save operation.</returns>
        Response Save();

        /// <summary>
        /// Validates before deleting an item.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <returns>A response containing the validation result.</returns>
        Response ValidateOnDelete(int id);

        /// <summary>
        /// Retrieves details of a user.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user details.</returns>
        object GetUserDetails(int id);
    }
}
