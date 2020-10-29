using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MessManagementSystem.Models; 

namespace MessManagementSystem.Controllers
{
    public class MealEntryController : Controller
    {
        ProjectContext dbContext = new ProjectContext();
        // GET: MealEntry
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMemberInfo()
        {
            var list = (from m in dbContext.memberEntries
                        select new
                        {
                            m.Id,
                            m.Name
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMealEntry()
        {
            var dataList = (from meal in dbContext.mealEntries.AsEnumerable()
                            join m in dbContext.memberEntries on meal.MemberId equals m.Id
                            select new
                            {
                                meal.Id,
                                meal.MemberId,
                                MemberName = m.Name,
                                meal.Breakfast,
                                meal.Lunch,
                                meal.Dinner,
                                meal.TotalMeal,
                                Date = meal.Date.ToJavaScriptMilliseconds()

                            }).ToList();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveMealEntry(MealEntry mealEntry)
        {
            try
            {
                using (dbContext = new ProjectContext())

                {
                    mealEntry.TotalMeal = mealEntry.Breakfast + mealEntry.Dinner + mealEntry.Lunch;                 
                    dbContext.mealEntries.Add(mealEntry);
                    int rowAff = dbContext.SaveChanges();

                    return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception exception)
            {

                //while (exception.InnerException != null)
                //{
                //    exception = exception.InnerException;
                //}               
                return Json(exception.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}