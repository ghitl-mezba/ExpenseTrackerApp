using ExpenseTrackerApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Controllers
{
    public class ExpenseCategoryController : Controller
    {
        private readonly ExpenseTrackerContext _Db;

        public ExpenseCategoryController(ExpenseTrackerContext Db) {
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
                
                var catData = _Db.ExpenseCategorys.ToList();
                ViewBag.Msg = msg;
                return View(catData);
            }
            catch(Exception) {

                return View();
            }
           
        }

       
        public IActionResult Create()
        {
            string userName = HttpContext.Session.GetString("userName");
            if (userName == "")
            {
                return RedirectToAction("Index", "Home");
            }

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseCategory obj)
        {
            try {
                if (ModelState.IsValid) {

                    var chkExist = _Db.ExpenseCategorys.Any(x => x.CategoryName == obj.CategoryName);
                    if (chkExist)
                    {
                        ModelState.AddModelError("CategoryName", "Category name already exists");
                        return View();
                    }
                    else { 
                        _Db.ExpenseCategorys.Add(obj);
                        await _Db.SaveChangesAsync();
                        return RedirectToAction("Index", new { msg = "Category name saved successfully" });
                    }
                    
                }
                return View();
            }
            catch (Exception) {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                else {
                    var editId = await _Db.ExpenseCategorys.FindAsync(id);
                    if (editId != null)
                    {
                        return View(editId);
                    }
                    else {
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
        public async Task<IActionResult> Edit(ExpenseCategory obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    var chkExist = _Db.ExpenseCategorys.Any(x => x.CategoryName == obj.CategoryName);
                    if (chkExist)
                    {
                        var chkIdExist = _Db.ExpenseCategorys.Where(f => f.ID == obj.ID).Any(x => x.CategoryName == obj.CategoryName);
                        if (chkIdExist)
                        {
                            _Db.Update(obj);
                            await _Db.SaveChangesAsync();
                            return RedirectToAction("Index", new { msg = "Category name updated successfully" });
                        }
                        else {
                            ModelState.AddModelError("CategoryName", "Category name already exists");
                            return View();
                        }
                    }
                    else
                    {
                        _Db.Update(obj);
                        await _Db.SaveChangesAsync();
                        return RedirectToAction("Index", new { msg = "Category name updated successfully" });
                    }

                }
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var delId = await _Db.ExpenseCategorys.FindAsync(id);
                if (delId != null) {
                    _Db.ExpenseCategorys.Remove(delId);
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("Index", new { msg = "Category name deleted successfully" });
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }




    }
}
