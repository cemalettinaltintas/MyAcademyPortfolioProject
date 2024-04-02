using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class SocialMediaController : Controller
    {
        MyAcademyPortfolioProjectEntities db = new MyAcademyPortfolioProjectEntities();
        // GET: SocialMedia
        public ActionResult Index()
        {
            var value = db.TblScialMedias.ToList();
            return View(value);
        }
        [HttpGet]
        public ActionResult AddSocialMedia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSocialMedia(TblScialMedias sm)
        {
            db.TblScialMedias.Add(sm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateSocialMedia(int id) {
            var value = db.TblScialMedias.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateSocialMedia(TblScialMedias sm)
        {
            var value = db.TblScialMedias.Find(sm.ScialMediaId);
            value.ScialMediaName = sm.ScialMediaName;
            value.Url = sm.Url;
            value.Icon = sm.Icon;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteSocialMedia(int id)
        {
            var value = db.TblScialMedias.Find(id);
            db.TblScialMedias.Remove(value); 
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}