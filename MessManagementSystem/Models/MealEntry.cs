using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessManagementSystem.Models
{
    public class MealEntry
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public decimal Breakfast { get; set; }

        public decimal Lunch { get; set; }

        public decimal Dinner { get; set; }

        public decimal TotalMeal { get; set; }        

        public DateTime Date { get; set; }
    }
}