using System;
using System.Collections.Generic;

namespace Dynamic_Data_Type.BL
{
    /// <summary>
    /// Business logic class for handling dynamic data.
    /// </summary>
    public class DynamicBL
    {
        private static List<dynamic> _dynamicDataList = new List<dynamic>();

        /// <summary>
        /// Adds dynamic data to the list.
        /// </summary>
        /// <param name="data">Dynamic data to be added.</param>
        public void AddDynamicData(dynamic data)
        {
            _dynamicDataList.Add(data);
        }

        /// <summary>
        /// Gets all dynamically received data.
        /// </summary>
        /// <returns>List of dynamic data.</returns>
        public List<dynamic> GetAllDynamicData()
        {
            return _dynamicDataList;
        }

        /// <summary>
        /// Gets dynamic data by index.
        /// </summary>
        /// <param name="id">Index of the item to retrieve.</param>
        /// <returns>Dynamic data at the specified index, or null if not found.</returns>
        public dynamic GetDynamicDataById(int id)
        {
            if (id >= 0 && id < _dynamicDataList.Count)
            {
                return _dynamicDataList[id];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Updates dynamic data at a specific index.
        /// </summary>
        /// <param name="id">Index of the item to update.</param>
        /// <param name="updatedData">Updated dynamic data.</param>
        /// <returns>True if the update is successful, false otherwise.</returns>
        public bool UpdateDynamicData(int id, dynamic updatedData)
        {
            try
            {
                if (id >= 0 && id < _dynamicDataList.Count)
                {
                    _dynamicDataList[id] = updatedData;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes dynamic data at a specific index.
        /// </summary>
        /// <param name="id">Index of the item to delete.</param>
        /// <returns>True if the deletion is successful, false otherwise.</returns>
        public bool DeleteDynamicDataById(int id)
        {
            try
            {
                if (id >= 0 && id < _dynamicDataList.Count)
                {
                    _dynamicDataList.RemoveAt(id);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes all dynamically received data.
        /// </summary>
        public void DeleteAllDynamicData()
        {
            _dynamicDataList.Clear();
        }
    }
}
