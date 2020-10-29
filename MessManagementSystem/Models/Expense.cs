using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessManagementSystem.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public decimal TotalExpense { get; set; }
        public DateTime Date { get; set; }
        public string Details { get; set; }
    }
}