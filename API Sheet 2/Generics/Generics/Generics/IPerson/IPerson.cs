using Generics.Models;
using System.Collections.Generic;

namespace Generics.IPerson
{
    /// <summary>
    /// Generic interface for CRUD operations on a person entity.
    /// </summary>
    /// <typeparam name="T">Type of the person entity.</typeparam>
    public interface IPerson<T>
    {
        /// <summary>
        /// Gets a person by ID.
        /// </summary>
        /// <param name="id">The ID of the person.</param>
        /// <returns>The person entity with the specified ID.</returns>
        T GetById(int id);

        /// <summary>
        /// Gets all persons.
        /// </summary>
        /// <returns>A list of all person entities.</returns>
        List<T> GetAll();

        /// <summary>
        /// Adds a new person entity.
        /// </summary>
        /// <param name="entity">The person entity to add.</param>
        List<T> Add(T entity);

        /// <summary>
        /// Updates an existing person entity.
        /// </summary>
        /// <param name="id">The ID of the person to update.</param>
        /// <param name="entity">The updated person entity.</param>
        List<T> Update(int id, T entity);

        /// <summary>
        /// Deletes a person by ID.
        /// </summary>
        /// <param name="id">The ID of the person to delete.</param>
        List<T> Delete(int id);
    }
}
