﻿using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MyPortfolio.Controllers
{
    public class ProjectController : Controller
    {
        MyAcademyPortfolioProjectEntities db = new MyAcademyPortfolioProjectEntities();
        // GET: Project
        public ActionResult Index()
        {
            var values = db.TblProjects.ToList();
            return View(values);
        }
        public ActionResult AddProject()
        {
            var categories = db.TblCategories.ToList();
            List<SelectListItem> categoryList = (from x in categories
                                                 select new SelectListItem
                                                 {
                                                     Text=x.CategoryName,
                                                     Value=x.CategoryId.ToString()
                                                 }).ToList();
            ViewBag.category=categoryList;
            return View();

        }
        [HttpPost]
        public ActionResult AddProject(TblProjects project)
        {
            db.TblProjects.Add(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateProject(int id)
        {
            var categories = db.TblCategories.ToList();
            List<SelectListItem> categoryList = (from x in categories
                                                 select new SelectListItem
                                                 {
                                                     Text = x.CategoryName,
                                                     Value = x.CategoryId.ToString()
                                                 }).ToList();
            ViewBag.category = categoryList;

            var value = db.TblProjects.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateProject(TblProjects projects)
        {
            var value = db.TblProjects.Find(projects.ProjectId);
            value.ImageUrl = projects.ImageUrl;
            value.ProjectName = projects.ProjectName;
            value.GithubUrl = projects.GithubUrl;
            value.CategoryId = projects.CategoryId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteProject(int id)
        {
            var value = db.TblProjects.Find(id);
            db.TblProjects.Remove(value);
            return RedirectToAction("Index");
        }
    }
}