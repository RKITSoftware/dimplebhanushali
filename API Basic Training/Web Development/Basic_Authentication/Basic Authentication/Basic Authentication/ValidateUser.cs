using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Basic_Authentication
{
    public class ValidateUser
    {
        public static bool IsLogin(string username, string password)
        {
            return (username == "admin" && password == "password") ||
                    (username == "employee" && password == "password");
        }

        public static string[] GetUserRoles(string username)
        {
            // For demo purposes, assigning roles based on username
            if (username == "admin")
            {
                return new[] { "Admin" };
            }
            else if (username == "employee")
            {
                return new[] { "Employee" };
            }
            else
            {
                return null;
            }
        }

    }
}