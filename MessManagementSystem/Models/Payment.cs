using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessManagementSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }        

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public int MemberId { get; set; }
        //mapping
        public MemberEntry Member { get; set; }
    }
}