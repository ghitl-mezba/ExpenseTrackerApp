using ExpenseTrackerApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ExpenseTrackerContext _Db;
        public HomeController(ExpenseTrackerContext Db)
        {
            _Db = Db;
        }

       

        public IActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            return View();
        }

        [HttpPost]
        public IActionResult Login(User obj)
        {
            var chkLogin = _Db.Users.Where(f => f.UserName == obj.UserName && f.UserPassword == obj.UserPassword).FirstOrDefault();

            if (chkLogin != null)
            {
                string userName = chkLogin.UserName;
               

                HttpContext.Session.SetString("userName", userName);
                return RedirectToAction("Dashboard");
            }
            else {
                return RedirectToAction("Index", new { msg = "Invalid username or password" });
            }
           
        }
        public IActionResult Dashboard()
        {
            string userName = HttpContext.Session.GetString("userName");
            if (userName != "")
            {
                ViewBag.TotalAmount = _Db.Expenses.Sum(s => s.ExpenseAmount);
                return View();
            }
            else {
                return RedirectToAction("Index");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("userName", "");
            return RedirectToAction("Index");
        }


    }
}
