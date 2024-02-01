using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORMDemo.Models
{
    /// <summary>
    /// Represents a product entity in the database.
    /// </summary>
    [Table("prdct01")]
    public class prdct01
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int t01f01 { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string t01f02 { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal t01f03 { get; set; }
    }
}
