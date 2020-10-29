using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MessManagementSystem.Models;

namespace MessManagementSystem.Controllers
{
    public class MemberEntryController : Controller
    {
        ProjectContext dbContext = new ProjectContext();
        // GET: MemberEntry
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMemberEntry()
        {
                var dataList = (from m in dbContext.memberEntries
                                select new
                                {
                                    m.Id,
                                    m.Name,
                                    m.Email,
                                    m.ContactNo,
                                    m.Address,
                                    m.Status

                                }).ToList();
                return Json(dataList, JsonRequestBehavior.AllowGet);                    
        }

        public ActionResult SaveMemberEntry(MemberEntry memberEntry)
        {
            try
            {
                using (dbContext = new ProjectContext())

                {
                    //Name must be two (2) to seven (7) characters long.
                    //var a = memberEntry.Name.Length;
                    //if (memberEntry.Name.Length<2||memberEntry.Name.Length>7)
                    //    throw new Exception("Name Invalid");

                    //Name check
                    //if (dbContext.memberEntries.Any(m => m.Name == memberEntry.Name))
                    //    throw new Exception("Name already exists! Try new one");
                    //Email check
                    //if (dbContext.memberEntries.Any(m => m.Email == memberEntry.Email))
                    //    throw new Exception("Email already exists! Try new one");
                    //Contact No check
                    //if (dbContext.memberEntries.Any(m => m.ContactNo == memberEntry.ContactNo))
                    //    throw new Exception("Contact no already exists! Try new one");

                    // memberEntry.Status = true;

                    //if (string.IsNullOrEmpty(memberEntry.Name))
                    //{
                    //    ModelState.AddModelError(nameof(memberEntry.Name), "The name is required");
                    //}
                    //if (ModelState.IsValid)
                    //{
                        dbContext.memberEntries.Add(memberEntry);
                        int rowAff = dbContext.SaveChanges();
                        return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
                    //}
                    //return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
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