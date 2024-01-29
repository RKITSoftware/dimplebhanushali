using LINQ_Demo.Models;
using System.Data;
using System.Linq;

namespace LINQ_Demo.BL
{
    /// <summary>
    /// Business Logic class for managing products using DataTable and LINQ.
    /// </summary>
    public class BLProducts
    {
        /// <summary>
        /// DataTable to store product data.
        /// </summary>
        public static DataTable productsTable;

        /// <summary>
        /// Static constructor to initialize DataTable and add sample data.
        /// </summary>
        static BLProducts()
        {
            // Initialize DataTable and add some sample data
            productsTable = new DataTable("Products");
            productsTable.Columns.Add("Id", typeof(int));
            productsTable.Columns.Add("Name", typeof(string));
            productsTable.Columns.Add("Category", typeof(string));
            productsTable.Columns.Add("Price", typeof(decimal));

            productsTable.Rows.Add(1, "Laptop", "Electronics", 999.99m);
            productsTable.Rows.Add(2, "Book", "Books", 19.99m);
            productsTable.Rows.Add(3, "Smartphone", "Electronics", 599.99m);
            productsTable.Rows.Add(4, "Chair", "Furniture", 49.99m);
        }

        /// <summary>
        /// Gets a copy of all products in DataTable.
        /// </summary>
        /// <returns>A DataTable containing all products.</returns>
        public static DataTable GetProducts()
        {
            return productsTable.Copy();
        }

        /// <summary>
        /// Gets a sorted DataTable based on the specified column and order.
        /// </summary>
        /// <param name="columnName">Name of the column to sort by.</param>
        /// <param name="ascending">True for ascending order, False for descending order.</param>
        /// <returns>A sorted DataTable.</returns>
        public static DataTable GetSortedProducts(string columnName, bool ascending = true)
        {
            // Use LINQ to sort products based on the specified column and order
            var sortedProducts = ascending
                ? productsTable.AsEnumerable().OrderBy(row => row[columnName])
                : productsTable.AsEnumerable().OrderByDescending(row => row[columnName]);

            // Copy the sorted results to a new DataTable
            DataTable sortedTable = productsTable.Clone();
            foreach (var row in sortedProducts)
            {
                sortedTable.ImportRow(row);
            }

            return sortedTable;
        }

        /// <summary>
        /// Gets a DataRow for a product with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>A DataRow representing the product or null if not found.</returns>
        public static DataRow GetProductById(int id)
        {
            return productsTable.AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("Id") == id);
        }

        /// <summary>
        /// Adds a new product to the DataTable.
        /// </summary>
        /// <param name="newProduct">The new product to add.</param>
        public static void AddProduct(Product newProduct)
        {
            if (newProduct != null)
            {
                // Generate a new unique ID for the product
                newProduct.Id = productsTable.AsEnumerable().Max(row => row.Field<int>("Id")) + 1;

                // Add the new product to the DataTable
                productsTable.Rows.Add(newProduct.Id, newProduct.Name, newProduct.Category, newProduct.Price);
            }
        }

        /// <summary>
        /// Updates an existing product in the DataTable.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="updatedProduct">The updated product data.</param>
        public static void UpdateProduct(int id, Product updatedProduct)
        {
            if (updatedProduct != null)
            {
                // Find the existing product
                var existingProduct = productsTable.AsEnumerable()
                    .FirstOrDefault(row => row.Field<int>("Id") == id);

                if (existingProduct != null)
                {
                    // Update the existing product
                    existingProduct.SetField("Name", updatedProduct.Name);
                    existingProduct.SetField("Category", updatedProduct.Category);
                    existingProduct.SetField("Price", updatedProduct.Price);
                }
            }
        }

        /// <summary>
        /// Deletes a product from the DataTable.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        public static void DeleteProduct(int id)
        {
            // Find the existing product
            var productToDelete = productsTable.AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("Id") == id);

            if (productToDelete != null)
            {
                // Remove the product from the DataTable
                productsTable.Rows.Remove(productToDelete);
            }
        }
    }
}
