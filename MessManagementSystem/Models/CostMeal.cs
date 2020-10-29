using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;


namespace MessManagementSystem.Models
{
    public class CostMeal
    {
        public int Id { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public decimal TotalMeal { get; set; }
        public decimal TotalAmount { get; set; }  
        public decimal MealRate { get; set; }
        public DateTime Date { get; set; }
    }
}