using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessManagementSystem.Models;
using System.Net;
using System.Globalization;

namespace MessManagementSystem.Controllers
{
    public class MonthlyCalculationController : Controller
    {
        MealEntry mealEntry = new MealEntry();
        Payment payment = new Payment();
        ProjectContext dbContext = new ProjectContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMonthInfo()
        {
            string[] names = DateTimeFormatInfo.CurrentInfo.MonthNames;
            var list = new List<ComboList1>();
            int i = 0;
            foreach (var name in names)
            {
                if (name == "")
                    continue;
                i = i + 1;
                var month = new ComboList1
                {
                    Id = i,
                    Name = name
                };
                list.Add(month);
            }
            //return Json(list.ToList(), JsonRequestBehavior.AllowGet);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetYearInfo()
        {
            var thisYear = DateTime.Now.Year - 1;

            var list = new List<ComboList1>();
            for (int i = 0; i < 12; i++)
            {
                var year = new ComboList1
                {
                    Id = thisYear + i,
                    Name = (thisYear + i).ToString()
                };
                list.Add(year);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMonthlyCalculationInfo()
        {
            var list = (from mc in dbContext.monthlyCalculations.AsEnumerable()
                        select new
                        {
                            mc.Id,
                            YearName = mc.YearId,
                          //mc.MonthId, 
                            Name=mc.MonthId,
                            mc.TotalMeal,
                            mc.TotalCost,
                            mc.MealRate,
                            Date = mc.Date.ToJavaScriptMilliseconds()
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MonthlyCalculation(int yearId, int monthId, DateTime date)
        {
            //ActionResult rtn = Json(0, JsonRequestBehavior.DenyGet);

            //try
            //{
                var find = dbContext.monthlyCalculations.Where(r => r.YearId == yearId && r.MonthId == monthId).ToList();
                dbContext.monthlyCalculations.RemoveRange(find);

                var find2 = dbContext.memberCosts.Where(r => r.YearId == yearId && r.MonthId == monthId).ToList();
                dbContext.memberCosts.RemoveRange(find2);



                decimal tMeal = (from meal in dbContext.mealEntries
                                 where meal.Date.Month == monthId && meal.Date.Year == yearId
                                 select meal.TotalMeal).DefaultIfEmpty(0).Sum();

                decimal tExpense = (from expense in dbContext.expenses
                                    where expense.Date.Month == monthId && expense.Date.Year == yearId
                                    select expense.TotalExpense).DefaultIfEmpty(0).Sum();

                MonthlyCalculation monthlyCalculation = new MonthlyCalculation();
                monthlyCalculation.MonthId = monthId;
                monthlyCalculation.YearId = yearId;
                monthlyCalculation.TotalMeal = tMeal;
                monthlyCalculation.TotalCost = tExpense;
                monthlyCalculation.MealRate = tExpense / tMeal;
                monthlyCalculation.Date = date;
                //Change...monthlyCalculation.Date = Date;

                dbContext.monthlyCalculations.Add(monthlyCalculation);
                dbContext.SaveChanges();

                var allMembers = dbContext.memberEntries.ToList();

                foreach (var member in allMembers)
                {
                    decimal totalDeposit = dbContext.payments
                                       .Where(p => p.Date.Month == monthId && p.Date.Year == yearId && p.MemberId == member.Id)
                                       .Select(p => p.Amount)
                                       .DefaultIfEmpty(0)
                                       .Sum();

                    decimal totalMeal = dbContext.mealEntries
                                       .Where(me => me.Date.Month == monthId && me.Date.Year == yearId && me.MemberId == member.Id)
                                       .Select(me => me.TotalMeal)
                                       .DefaultIfEmpty(0)
                                       .Sum();

                    decimal totalExpense = totalMeal * monthlyCalculation.MealRate;
                    decimal balance = totalDeposit - totalExpense;

                    MemberCost memberCost = new MemberCost();
                    memberCost.MemberId = member.Id;
                    memberCost.CalculationId = monthlyCalculation.Id;
                    memberCost.TotalDeposit = totalDeposit;
                    memberCost.TotalMeal = totalMeal;
                    memberCost.TotalCost = totalExpense;
                    memberCost.Balance = balance;
                    memberCost.YearId = yearId;
                    memberCost.MonthId = monthId;

                    dbContext.memberCosts.Add(memberCost);
                    dbContext.SaveChanges();
                }
            //}
            //catch(Exception ex)
            //{
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            //}


            //return rtn;

        }      


        //public ActionResult MonthlyCalculation(MonthlyCalculation monthlyCalculation)
        //{
        //    try
        //    {
        //        using (dbContext = new ProjectContext())

        //        {               
        //            decimal tMeal = (from meal in dbContext.mealEntries
        //                             where meal.Date.Month == monthlyCalculation.MonthId && meal.Date.Year == monthlyCalculation.YearId
        //                             select meal.TotalMeal).DefaultIfEmpty(0).Sum();

        //            decimal tExpense = (from expense in dbContext.expenses
        //                               where expense.Date.Month == monthlyCalculation.MonthId && expense.Date.Year == monthlyCalculation.YearId
        //                               select expense.TotalExpense).DefaultIfEmpty(0).Sum();                   

        //            monthlyCalculation.TotalMeal = tMeal;
        //            monthlyCalculation.TotalCost = tExpense;
        //            monthlyCalculation.MealRate = tExpense / tMeal;
        //            dbContext.monthlyCalculations.Add(monthlyCalculation);
        //            dbContext.SaveChanges();
        //            return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception exception)
        //    {           
        //        return Json(exception.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }

    public class ComboList1
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}