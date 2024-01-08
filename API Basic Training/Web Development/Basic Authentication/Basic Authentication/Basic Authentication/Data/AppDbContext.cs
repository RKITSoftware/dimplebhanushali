using Basic_Authentication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Basic_Authentication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AuthenticationString")
        {}

        public DbSet<Employee> employees { get; set; }
        public DbSet<User> users { get; set; }
    }
}