using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessManagementSystem.Models
{
    public class PaymentReport
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public int PaymentId { get; set; }
    }
}