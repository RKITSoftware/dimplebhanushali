using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public int Sem { get; set; }
    }
}