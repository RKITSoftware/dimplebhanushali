using System.Collections.Generic;

namespace List_To_Set.Models
{
    /// <summary>
    /// Class Reprenting List into Set
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListToSet<T> : List<T>
    {
        /// <summary>
        /// Add Item to List (Set)
        /// </summary>
        /// <param name="item">item</param>
        /// <returns>Result</returns>
        public new string Add(T item)
        {
            // Check if the item already exists in the list
            if (!Contains(item))
            {
                // If not, add the item to the list
                base.Add(item);
                return $"Item {item} Added";
            }
            else
            {
                return "Duplicate Item Found";
            }
        }

        /// <summary>
        /// Remove Item From List (Set)
        /// </summary>
        /// <param name="item">item</param>
        /// <returns>Result</returns>
        public string Remove(T item)
        {
            // Check if the item exists in the list
            if (Contains(item))
            {
                // If it exists, remove the item from the list
                base.Remove(item);
                return $"Item {item} removed";
            }
            else
            {
                return "Item not found in the list";
            }
        }
    }
}
