using ActionMethod.BL.Services;
using ActionMethod.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ActionMethod.BL.Implementations
{
    /// <summary>
    /// Implementation of the product repository interface using a DataTable as storage.
    /// </summary>
    public class BLProductRepository : IProductRepository
    {
        #region Private Members
        private static readonly DataTable _products;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for Initialising Data Table.
        /// </summary>
        static BLProductRepository()
        {
            // Initialize the DataTable with appropriate columns
            _products = new DataTable();
            _products.Columns.Add("Id", typeof(int));
            _products.Columns.Add("Name", typeof(string));
            _products.Columns.Add("Price", typeof(string));
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        /// <returns>An enumerable collection of products.</returns>
        public IEnumerable<PRO01> GetAll()
        {
            // Convert DataTable rows to PRO01 objects
            return _products.AsEnumerable().Select(row => new PRO01
            {
                O01F01 = row.Field<int>("Id"),
                O01F02 = row.Field<string>("Name"),
                O01F03 = row.Field<string>("Price")
            });
        }

        /// <summary>
        /// Retrieves a product by its ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID, or null if not found.</returns>
        public PRO01 GetById(int id)
        {
            // Find the DataRow with the specified id
            DataRow row = _products.AsEnumerable().FirstOrDefault(r => r.Field<int>("Id") == id);
            if (row != null)
            {
                // Convert DataRow to PRO01 object
                return new PRO01
                {
                    O01F01 = row.Field<int>("Id"),
                    O01F02 = row.Field<string>("Name"),
                    O01F03 = row.Field<string>("Price")
                };
            }
            return null;
        }

        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="product">The product to add.</param>
        public void Add(PRO01 product)
        {
            // Add a new row to the DataTable
            DataRow newRow = _products.NewRow();
            newRow["Id"] = _products.Rows.Count + 1;
            newRow["Name"] = product.O01F02;
            newRow["Price"] = product.O01F03;
            _products.Rows.Add(newRow);
        }

        /// <summary>
        /// Updates an existing product in the repository.
        /// </summary>
        /// <param name="product">The product to update.</param>
        public void Update(PRO01 product)
        {
            // Find the DataRow with the specified id
            DataRow row = _products.AsEnumerable().FirstOrDefault(r => r.Field<int>("Id") == product.O01F01);
            if (row != null)
            {
                // Update the DataRow
                row["Name"] = product.O01F02;
                row["Price"] = product.O01F03;
            }
        }

        /// <summary>
        /// Deletes a product from the repository by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        public void Delete(int id)
        {
            // Find the DataRow with the specified id
            DataRow row = _products.AsEnumerable().FirstOrDefault(r => r.Field<int>("Id") == id);
            if (row != null)
            {
                // Delete the DataRow
                _products.Rows.Remove(row);
            }
        }

        #endregion
    }
}
