using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessManagementSystem.Models
{
    public class MemberCost
    {
        public int Id { get; set; }
        public int MemberId { get; set; }       
        public int CalculationId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public decimal TotalDeposit { get; set; }       
        public decimal TotalMeal { get; set; }       
        public decimal TotalCost { get; set; }         
        public decimal Balance { get; set; }
    }
}