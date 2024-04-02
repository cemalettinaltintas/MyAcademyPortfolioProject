using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class CategoryController : Controller
    {
        MyAcademyPortfolioProjectEntities db = new MyAcademyPortfolioProjectEntities();
        // GET: Category
        public ActionResult Index()
        {
            var values = db.TblCategories.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(TblCategories category)
        {
            db.TblCategories.Add(category);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult DeleteCategory(int id)
        {
            var category = db.TblCategories.Find(id);
            db.TblCategories.Remove(category);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult UpdateCategory(int id) {
            var category = db.TblCategories.Find(id);    
            return View(category);
        }
        [HttpPost]
        public ActionResult UpdateCategory(TblCategories category)
        {
            var value = db.TblCategories.Find(category.CategoryId);
            value.CategoryName=category.CategoryName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}