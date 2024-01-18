using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Token_Auth.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public string Email { get; set; }

        public static List<User> lstUsers = new List<User>
        {
            new User { UserId = 1, UserName="dimple",Password="12345",Roles="admin",Email="abc@gmail.com"},
            new User { UserId = 2, UserName="ankit",Password="12345",Roles="superadmin",Email="xyz@gmail.com"},
            new User { UserId = 3, UserName="shiva",Password="12345",Roles="user",Email="shiva@gmail.com"},
        };

    }
}