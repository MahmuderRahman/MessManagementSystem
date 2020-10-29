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
    public class MemberCostController : Controller
    {
        ProjectContext dbContext = new ProjectContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMonthInfo()
        {
            string[] names = DateTimeFormatInfo.CurrentInfo.MonthNames;
            var list = new List<ComboList2>();
            int i = 0;
            foreach (var name in names)
            {
                if (name == "")
                    continue;
                i = i + 1;
                var month = new ComboList2
                {
                    Id = i,
                    Name = name
                };
                list.Add(month);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetYearInfo()
        {
            var thisYear = DateTime.Now.Year - 20;

            var list = new List<ComboList2>();
            for (int i = 0; i < 51; i++)
            {
                var year = new ComboList2
                {
                    Id = thisYear + i,
                    Name = (thisYear + i).ToString()
                };
                list.Add(year);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
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

        public ActionResult GetMemberInformation(int yearId, int monthId, int memberId)
        {
            //All Members
            if (memberId == 0)
            {
                var members = (from mc in dbContext.memberCosts
                               join m in dbContext.memberEntries on mc.MemberId equals m.Id
                               join cal in dbContext.monthlyCalculations on mc.CalculationId equals cal.Id
                                where mc.YearId == yearId && mc.MonthId == monthId
                                select new
                               {

                                   MemberId = m.Id,
                                   MemberName = m.Name,
                                   mc.TotalDeposit,
                                   mc.TotalMeal,
                                   mc.TotalCost,
                                   mc.Balance,
                                   cal.MealRate
                               });
                return Json(members, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //Single Members
                var singleMember = (from mc in dbContext.memberCosts
                               join m in dbContext.memberEntries on mc.MemberId equals m.Id
                               join cal in dbContext.monthlyCalculations on mc.CalculationId equals cal.Id
                               where mc.YearId == yearId && mc.MonthId == monthId && mc.MemberId == memberId
                               select new
                               {

                                   MemberId = m.Id,
                                   MemberName = m.Name,
                                   mc.TotalDeposit,
                                   mc.TotalMeal,
                                   mc.TotalCost,
                                   mc.Balance,
                                   cal.MealRate
                               });
                return Json(singleMember, JsonRequestBehavior.AllowGet);
            }
        }
    }
    }

    public class ComboList2
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
