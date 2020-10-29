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
    public class CostMealController : Controller
    {
        MealEntry mealEntry = new MealEntry();
        Payment payment = new Payment();
        ProjectContext dbContext = new ProjectContext();
        // GET: CostMeal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMonthInfo()
        {
            string[] names = DateTimeFormatInfo.CurrentInfo.MonthNames;
            var list = new List<ComboList>();
            int i = 0;
            foreach (var name in names)
            {
                if (name == "")
                    continue;
                i = i + 1;
                var month = new ComboList
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
            var thisYear = DateTime.Now.Year-20;

            var list = new List<ComboList>();
            for (int i=0; i<51; i++)
            {
                var year = new ComboList
                {
                    Id = thisYear+i,
                    Name = (thisYear + i).ToString()
                };
                list.Add(year);
            }
           
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCostMealInfo()
        {
            var list = (from cm in dbContext.costMeals.AsEnumerable()
                        join m in dbContext.months on cm.MonthId equals m.Id
                        join y in dbContext.years on cm.YearId equals y.Id
                        select new
                        {
                            cm.Id,
                            cm.MonthId,
                            MonthName = m.Name,
                            cm.YearId,
                            YearName = y.Name,
                            cm.TotalMeal,
                            cm.TotalAmount,
                            cm.MealRate,
                            Date = cm.Date.ToJavaScriptMilliseconds()
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult MonthlyCalculation(int monthId, int yearId)
        public ActionResult MonthlyCalculation(CostMeal costMeal)
        {
            try
            {
                using (dbContext = new ProjectContext())

                {
                    //CostMeal costMeal = new CostMeal();

                    decimal tMeal = (from meal in dbContext.mealEntries
                                     where meal.Date.Month == costMeal.MonthId && meal.Date.Year == costMeal.YearId
                                     select meal.TotalMeal).DefaultIfEmpty(0).Sum();

                    decimal tAmount = (from c in dbContext.payments
                                       where c.Date.Month == costMeal.MonthId && c.Date.Year == costMeal.YearId
                                       select c.Amount).DefaultIfEmpty(0).Sum();

                    //decimal tMeal2 = dbContext.mealEntries.Where(meal => meal.Date.Month == costMeal.MonthId).Select(m=>m.TotalMeal).DefaultIfEmpty(0).Sum();

                    costMeal.TotalMeal = tMeal;
                    costMeal.TotalAmount = tAmount;
                    costMeal.MealRate = tAmount / tMeal;
                    mealEntry.TotalMeal = mealEntry.Breakfast + mealEntry.Dinner + mealEntry.Lunch;


                    var allMemberIds = dbContext.memberEntries.Select(m => m.Id).ToList();

                    foreach (var mId in allMemberIds)
                    {
                        decimal totalDeposit = dbContext.payments
                                           .Where(p => p.Date.Month == costMeal.MonthId && p.Date.Year == costMeal.YearId && p.MemberId == mId)
                                           .Select(p => p.Amount)
                                           .DefaultIfEmpty(0)
                                           .Sum();

                        decimal totalMeal = dbContext.mealEntries
                                           .Where(me => me.Date.Month == costMeal.MonthId && me.Date.Year == costMeal.YearId && me.MemberId == mId)
                                           .Select(me => me.TotalMeal)
                                           .DefaultIfEmpty(0)
                                           .Sum();


                        decimal totalCost = totalMeal * costMeal.MealRate;
                        decimal balance = totalDeposit - totalCost;

                        MemberCost memberCost = new MemberCost();
                        //memberCost.


                    }



                    dbContext.costMeals.Add(costMeal);
                    dbContext.SaveChanges();

                    //var memberCosts = (from mbr in dbContext.memberEntries
                    //                   join pmt in dbContext.payments.Where(p => p.Date.Month == costMeal.MonthId && p.Date.Year == costMeal.YearId) on mbr.Id equals pmt.MemberId
                    //                   join ml in dbContext.mealEntries.Where(p => p.Date.Month == costMeal.MonthId && p.Date.Year == costMeal.YearId) on mbr.Id equals ml.MemberId
                    //                   group new { pmt, ml } by new { pMemberId = pmt.MemberId, mMemberId = ml.MemberId } into mbrGrp
                    //                   select new
                    //                   {
                    //                       TotalDeposit = mbrGrp.Sum(m => m.pmt.Amount),
                    //                       TotalMeal = mbrGrp.Sum(m => m.ml.TotalMeal)
                    //                   }).ToList();


                                      

                    return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception exception)
            {           
                return Json(exception.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }

    public class ComboList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }   
}