using Antlr.Runtime.Tree;
using MyPortfolio.Models;
using MyPortfolio.Settings;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        // GET: About
        MyAcademyPortfolioProjectEntities db = new MyAcademyPortfolioProjectEntities();
        public ActionResult Index()
        {

            var values = db.TblAbouts.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAbout(TblAbouts abouts)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/About/" + dosyaadi + uzanti;
                //Resmi klasörün içine atalım.
                Request.Files[0].SaveAs(Server.MapPath(yol));

                abouts.ImagUrl = "/Image/About/" + dosyaadi + uzanti;
            }
            db.TblAbouts.Add(abouts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAbout(int id)
        {
            var about = db.TblAbouts.Find(id);

            if (System.IO.File.Exists(Server.MapPath(about.ImagUrl)))
            {
                System.IO.File.Delete(Server.MapPath(about.ImagUrl));
            }

            db.TblAbouts.Remove(about);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateAbout(int id)
        {
            var about = db.TblAbouts.Find(id);
            return View(about);
        }

        [HttpPost]
        public ActionResult UpdateAbout(TblAbouts about)
        {
            var result = db.TblAbouts.Find(about.AboutId);
            if (about.ImagUrl != null)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/About/" + dosyaadi + uzanti;
                //Resmi klasörün içine atalım.
                Request.Files[0].SaveAs(Server.MapPath(yol));

                if (System.IO.File.Exists(Server.MapPath(result.ImagUrl)))
                {
                    System.IO.File.Delete(Server.MapPath(result.ImagUrl));
                }
                result.ImagUrl = "/Image/About/" + dosyaadi + uzanti;
            }

            result.Title = about.Title;
            result.Description = about.Description;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}