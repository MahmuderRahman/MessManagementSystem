using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using MessManagementSystem.Models;
using System.Net;

namespace MessManagementSystem.Controllers
{
    public class PaymentController : Controller
    {
        ProjectContext dbContext = new ProjectContext();
        // GET: Payment
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

        public ActionResult GetPayment()
        {
            var dataList = (from p in dbContext.payments.AsEnumerable()
                            join m in dbContext.memberEntries on p.MemberId equals m.Id
                            select new
                            {
                                p.Id,
                                p.MemberId,
                                MemberName = m.Name,  
                                p.Amount,
                                Date = p.Date.ToJavaScriptMilliseconds()

                            }).ToList();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SavePayment(Payment payment)
        {
            try
            {
                using (dbContext = new ProjectContext())

                {
                    dbContext.payments.Add(payment);
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