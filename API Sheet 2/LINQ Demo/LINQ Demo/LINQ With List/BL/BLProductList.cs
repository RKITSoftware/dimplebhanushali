using LINQ_With_List.Models;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_With_List.BL
{
    public class BLProductList
    {
        #region Public Members
        /// <summary>
        /// List to store product data.
        /// </summary>
        public static List<Product> ProductsList;

        /// <summary>
        /// Static constructor to initialize List and add sample data.
        /// </summary>
        static BLProductList()
        {
            // Initialize List and add some sample data
            ProductsList = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 999.99m },
                new Product { Id = 2, Name = "Book", Category = "Books", Price = 19.99m },
                new Product { Id = 3, Name = "Smartphone", Category = "Electronics", Price = 599.99m },
                new Product { Id = 4, Name = "Chair", Category = "Furniture", Price = 49.99m }
            };
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Gets a copy of all products in List.
        /// </summary>
        /// <returns>A List containing all products.</returns>
        public List<Product> GetProducts()
        {
            return ProductsList;
        }

        /// <summary>
        /// Gets a sorted List based on the specified column and order.
        /// </summary>
        /// <param name="columnName">Name of the property to sort by.</param>
        /// <param name="ascending">True for ascending order, False for descending order.</param>
        /// <returns>A sorted List.</returns>
        public List<Product> GetSortedProducts(string columnName, bool ascending = true)
        {
            // Use LINQ to sort products based on the specified column and order
            var sortedProducts = ascending
                ? ProductsList.OrderBy(p => GetPropertyValue(p, columnName))
                : ProductsList.OrderByDescending(p => GetPropertyValue(p, columnName));

            return sortedProducts.ToList();
        }

        /// <summary>
        /// Gets a Product from the List with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>A Product representing the product or null if not found.</returns>
        public Product GetProductById(int id)
        {
            return ProductsList.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Adds a new product to the List.
        /// </summary>
        /// <param name="newProduct">The new product to add.</param>
        public void AddProduct(Product newProduct)
        {
            if (newProduct != null)
            {
                // Generate a new unique ID for the product
                newProduct.Id = ProductsList.Max(p => p.Id) + 1;

                // Add the new product to the List
                ProductsList.Add(newProduct);
            }
        }

        /// <summary>
        /// Updates an existing product in the List.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="updatedProduct">The updated product data.</param>
        public void UpdateProduct(int id, Product updatedProduct)
        {
            if (updatedProduct != null)
            {
                // Find the existing product
                Product existingProduct = ProductsList.FirstOrDefault(p => p.Id == id);

                if (existingProduct != null)
                {
                    // Update the existing product
                    existingProduct.Name = updatedProduct.Name;
                    existingProduct.Category = updatedProduct.Category;
                    existingProduct.Price = updatedProduct.Price;
                }
            }
        }

        /// <summary>
        /// Deletes a product from the List.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        public void DeleteProduct(int id)
        {
            // Remove the product
            ProductsList.RemoveAll(p => p.Id == id);
        }

        //// int list Except

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the value of a specified property from an object using reflection.
        /// </summary>
        /// <param name="obj">The object from which to retrieve the property value.</param>
        /// <param name="propertyName">The name of the property whose value is to be retrieved.</param>
        /// <returns>The value of the specified property, or null if the property is not found.</returns>
        private object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        }
        #endregion
    }
}