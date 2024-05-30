using ActionMethod.Models;

namespace ActionMethod.BL.Services
{
    /// <summary>
    /// Interface for a product repository.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        /// <returns>An enumerable collection of products.</returns>
        IEnumerable<PRO01> GetAll();

        /// <summary>
        /// Retrieves a product by its ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID, or null if not found.</returns>
        PRO01 GetById(int id);

        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="product">The product to add.</param>
        void Add(PRO01 product);

        /// <summary>
        /// Updates an existing product in the repository.
        /// </summary>
        /// <param name="product">The product to update.</param>
        void Update(PRO01 product);

        /// <summary>
        /// Deletes a product from the repository by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        void Delete(int id);
    }
}
