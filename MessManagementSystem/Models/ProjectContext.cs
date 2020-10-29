using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MessManagementSystem.Models
{
    public class ProjectContext:DbContext
    {

        public ProjectContext() : base("DBCS")
        {

        }
        public DbSet<MemberEntry> memberEntries { get; set; }
        public DbSet<MealEntry> mealEntries { get; set; }
        public DbSet<Month> months { get; set; }
        public DbSet<Year> years { get; set; }
        public DbSet<Payment> payments { get; set; }  
        public DbSet<CostMeal> costMeals { get; set; }  
        public DbSet<MonthlyCalculation> monthlyCalculations { get; set; }  
        public DbSet<MemberCost> memberCosts { get; set; }  
        public DbSet<Expense> expenses { get; set; }
        public DbSet<PaymentReport> paymentReports { get; set; }
    }
}