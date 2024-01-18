using System;
using System.Collections.Generic;
using System.Linq;
using Token_Auth.Models;

namespace Token_Auth
{
    public class UserRepo : IDisposable
    {
        public User ValidateUser(string username, string password)
        {
            return User.lstUsers.FirstOrDefault(
                user => user.UserName.Equals(username)
                   && user.Password == password);
        }

        public void Dispose()
        {
            // Implement IDisposable pattern if needed
        }
    }
}
