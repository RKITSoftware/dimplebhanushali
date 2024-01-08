using Basic_Authentication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Basic_Authentication.Models;

namespace Basic_Authentication.User_Repository
{
    public class UserRepo : IDisposable
    {
        AppDbContext db = new AppDbContext();

        public User ValidateUser(string username, string password)
        {
            if (db != null && db.users != null)
            {
                return db.users.FirstOrDefault(user => user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
            else
            {
                return null;
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}