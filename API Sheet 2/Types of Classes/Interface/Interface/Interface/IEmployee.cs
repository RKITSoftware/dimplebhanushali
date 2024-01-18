using System.Collections.Generic;

namespace Interface.Interface
{
    /// <summary>
    /// Interface for managing employee data.
    /// </summary>
    /// <typeparam name="T">Type of the entity representing an employee.</typeparam>
    public interface IEmployee<T> where T : class
    {
        /// <summary>
        /// Gets an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        /// <returns>The employee entity.</returns>
        T GetById(int id);

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        /// <param name="entity">The entity representing the employee to be added.</param>
        void Add(T entity);

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="id">The ID of the employee to be updated.</param>
        /// <param name="entity">The updated entity representing the employee.</param>
        void Update(int id, T entity);

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to be deleted.</param>
        void Delete(int id);

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns>A collection of all employee entities.</returns>
        IEnumerable<T> GetAll();
    }
}
