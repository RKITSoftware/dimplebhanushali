using Filters_Web_API.Models;

namespace Filters_Web_API.BL
{
    /// <summary>
    /// BLuser Class
    /// </summary>
    public class BLUser
    {
        #region Public Members

        /// <summary>
        /// Declares list of users
        /// </summary>
        public static List<User> lstUser = new List<User>
        {
            new User { UserId = 1, UserName = "dimple", Password = "dimple123", R01F04 = enmUserRole.A}
        };

        #endregion

        #region Public Methods

        /// <summary>
        /// Validate object 
        /// </summary>
        /// <param name="objUSR01">Object of class USR01 to be validate</param>
        /// <returns>True if valid object, false otherwise</returns>
        public bool Validation(User objUSR01)
        {
            var user = lstUser.FirstOrDefault(u => u.UserName == objUSR01.UserName);

            if (user != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Adds user to lstUSer list
        /// </summary>
        /// <param name="objUSR01">Object of class USR01 to be add</param>
        /// <returns></returns>
        public string AddUser(User objUSR01)
        {
            objUSR01.UserId = lstUser.Count + 1;
            lstUser.Add(objUSR01);

            return "User added successfully.";
        }

        #endregion
    }
}
