using ORMDemo.Models;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;

namespace ORMDemo.BL
{
    /// <summary>
    /// Business Logic class for managing product-related operations.
    /// </summary>
    public class BLProduct
    {
        //// All ORM Query Methods.

        #region Public Methods
        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>List of products.</returns>
        public List<prdct01> GetAllProducts()
        {
            using (IDbConnection db = Connection.connectionFactory.OpenDbConnection())
            {
                if (db.TableExists<prdct01>())
                {
                    return db.Select<prdct01>();
                }
                else
                {
                    throw new InvalidOperationException("Table Does Not Exist");
                }
            }
        }

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="objProduct">Product to be added.</param>
        /// <returns>Message indicating the success of the operation.</returns>
        public string AddProduct(prdct01 objProduct)
        {
            using (var db = Connection.connectionFactory.OpenDbConnection())
            {
                if (db.TableExists<prdct01>())
                {
                    db.Insert(objProduct);
                    return "Product Added Successfully";
                }
                else
                {
                    throw new InvalidOperationException("Table Does Not Exist");
                }
            }
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">Identifier of the product.</param>
        /// <returns>The product with the specified identifier.</returns>
        public static prdct01 GetProduct(int id)
        {
            using (IDbConnection db = Connection.connectionFactory.OpenDbConnection())
            {
                if (db.TableExists<prdct01>())
                {
                    return db.SingleById<prdct01>(id);
                }
                else
                {
                    throw new InvalidOperationException("Table Does Not Exist");
                }
            }
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="id">Identifier of the product to be updated.</param>
        /// <param name="objProduct">Updated product data.</param>
        /// <returns>The updated product.</returns>
        public prdct01 EditProduct(prdct01 updatedFields)
        {
            using (IDbConnection db = Connection.connectionFactory.OpenDbConnection())
            {
                if (db.TableExists<prdct01>())
                {
                    prdct01 existingProduct = db.SingleById<prdct01>(updatedFields.t01f01);
                    if (existingProduct != null)
                    {
                        //db.UpdateOnlyFields(existingProduct,
                        //     new[] { nameof(prdct01.t01f02), nameof(prdct01.t01f03) },
                        //     q => q.t01f01 == existingProduct.t01f01);

                        // Update only specific fields
                        db.UpdateOnlyFields<prdct01>(
                            existingProduct,
                            new[] { nameof(prdct01.t01f02), nameof(prdct01.t01f03) },
                            q => q.t01f01 == updatedFields.t01f01);


                        return existingProduct;
                    }
                    else
                    {
                        throw new InvalidOperationException("Product not found");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Table Does Not Exist");
                }
            }
        }

        /// <summary>
        /// Removes a product from the database by its identifier.
        /// </summary>
        /// <param name="id">Identifier of the product to be removed.</param>
        /// <returns>The removed product.</returns>
        public prdct01 RemoveProduct(int id)
        {
            using (IDbConnection db = Connection.connectionFactory.OpenDbConnection())
            {
                if (db.TableExists<prdct01>())
                {
                    prdct01 objExistingProduct = db.SingleById<prdct01>(id);
                    db.Delete(objExistingProduct);
                    return objExistingProduct;
                }
                else
                {
                    throw new InvalidOperationException("Table Does Not Exist");
                }
            }
        }


        #endregion
    }
}