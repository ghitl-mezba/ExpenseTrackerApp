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
    public class ExpenseController : Controller
    {
        private readonly ExpenseTrackerContext _Db;

        public ExpenseController(ExpenseTrackerContext Db)
        {
            _Db = Db;
        }

       

        public IActionResult Index(string msg)
        {
            string userName = HttpContext.Session.GetString("userName");
            if (userName == "")
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                ViewBag.Msg = msg;
                return View();
            }
            catch (Exception)
            {

                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Index(ExpenseVM obj)
        {
            try
            {
                List<ExpenseCategory> expCat = new List<ExpenseCategory>();
                List<Expense> exp = new List<Expense>();

                var expenseData = (from a in _Db.ExpenseCategorys
                                   join b in _Db.Expenses.Where(f => f.ExpenseDate >= obj.FromDate && f.ExpenseDate < obj.ToDate.AddHours(6)) on a.ID equals b.ExpenseCategory
                                   select new ExpenseVM { expCatVM = a, expVM = b }).ToList();
                return View(expenseData);

            }
            catch (Exception)
            {

                return View();
            }

        }
        public IActionResult Entry(string msg = null)
        {
            string userName = HttpContext.Session.GetString("userName");
            if (userName == "")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ExpenseCategoryDDList = _Db.ExpenseCategorys.ToList();
            ViewBag.Msg = msg;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Entry(Expense obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _Db.Expenses.Add(obj);
                    await _Db.SaveChangesAsync();
                    return RedirectToAction("Entry", new { msg = "Expenses saved successfully" });
                }
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Entry");
            }
        }

        public async Task<IActionResult> Edit(int? id, string msg)
        {
            try
            {

                ViewBag.Msg = msg;
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var editId = await _Db.Expenses.FindAsync(id);
                    if (editId != null)
                    {
                        ViewBag.ExpenseCategoryDDList = _Db.ExpenseCategorys.ToList();
                        return View(editId);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Expense obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _Db.Update(obj);
                    await _Db.SaveChangesAsync();
                    return RedirectToAction("Edit", new { msg = "Expenses updated successfully" });
                }
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Edit");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var delId = await _Db.Expenses.FindAsync(id);
                if (delId != null)
                {
                    _Db.Expenses.Remove(delId);
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("Index", new { msg = "Expenses deleted successfully" });
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }


    }
}
