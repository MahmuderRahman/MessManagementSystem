using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MessManagementSystem.Models;
using System.Net;

namespace MessManagementSystem.Controllers
{
    public class ExpenseController : Controller
    {
        ProjectContext dbContext = new ProjectContext();
        // GET: Expense
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

        public ActionResult GetExpenseInfo()
        {
            var dataList = (from ex in dbContext.expenses.AsEnumerable()
                            join m in dbContext.memberEntries on ex.MemberId equals m.Id
                            select new
                            {
                                ex.Id,
                                ex.MemberId,
                                MemberName = m.Name,
                                ex.TotalExpense,
                                ex.Details,                        
                                Date = ex.Date.ToJavaScriptMilliseconds()

                            }).ToList();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveExpense(Expense expense)
        {
            try
            {
                using (dbContext = new ProjectContext())

                {
                    dbContext.expenses.Add(expense);
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