using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MessManagementSystem.Models;

namespace MessManagementSystem.Controllers
{
    public class PaymentReportController : Controller
    {
        ProjectContext dbContext = new ProjectContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMonthInfo()
        {
            string[] names = DateTimeFormatInfo.CurrentInfo.MonthNames;
            var list = new List<ComboList3>();
            int i = 0;
            foreach (var name in names)
            {
                if (name == "")
                    continue;
                i = i + 1;
                var month = new ComboList3
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
            var thisYear = DateTime.Now.Year - 1;

            var list = new List<ComboList3>();
            for (int i = 0; i < 12; i++)
            {
                var year = new ComboList3
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
            var list = (from m in dbContext.memberEntries.Where(p=>p.Status==true)                        
                        select new
                        {
                            m.Id,
                            m.Name
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPaymentInformation(int yearId, int monthId, int memberId)
        {
            var totalPayment = dbContext.payments.Where(p => p.Date.Month == monthId && p.Date.Year == yearId && p.Member.Status==true)
                               .GroupBy(c => c.Member)
                                .Select(c => new
                                {
                                    MemberId = c.Key.Id,
                                    Name = c.Key.Name,
                                    Amount = c.Sum(p => p.Amount),
                                    c.Key.Status
                                }).ToList();
            if (memberId > 0)
            {
                totalPayment = totalPayment.Where(c => c.MemberId == memberId).ToList();

            }

            return Json(totalPayment, JsonRequestBehavior.AllowGet);

            //if (memberId == 0)
            //{
            //    var members1 = (from p in dbContext.payments
            //                    join m in dbContext.memberEntries on p.MemberId equals m.Id

            //                    select new
            //                    {
            //                        p.Id,
            //                        MemberId = m.Id,
            //                        MemberName = m.Name,
            //                        totalDeposit = p.Amount
            //                    });

            //    return Json(members1, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    var members = (from p in dbContext.payments
            //                   join m in dbContext.memberEntries on p.MemberId equals m.Id
            //                   where p.MemberId == memberId
            //                   select new
            //                   {                                 
            //                       MemberId = m.Id,
            //                       MemberName = m.Name,
            //                       totalDeposit = totalDeposit1
            //                   });
            //    return Json(members, JsonRequestBehavior.AllowGet);
            //}

        }
        public ActionResult GetDetailsInfo(int yearId, int monthId, int memberId)
        {        
            var paymentDetails = dbContext.payments
               .Where(p => p.MemberId == memberId && p.Date.Month == monthId && p.Date.Year == yearId)
                               .OrderBy(p => p.Date)
                               .Select(p => new                             
                               {
                                   p.Member.Name,
                                   p.Amount,
                                   p.Date
                               }).ToList();
           

            //Date Convert 
            var result = paymentDetails
                .Select(p => new
                {
                    p.Amount,
                    p.Name,
                    Date = p.Date.ToString("dd-MMM-yyyy")
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
public class ComboList3
{
    public int Id { get; set; }
    public string Name { get; set; }
}