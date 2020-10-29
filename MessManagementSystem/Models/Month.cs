using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessManagementSystem.Models
{
    public class Month
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static implicit operator Month(string v)
        {
            throw new NotImplementedException();
        }
    }
}