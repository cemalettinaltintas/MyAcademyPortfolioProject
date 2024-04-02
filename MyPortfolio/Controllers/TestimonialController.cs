using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class TestimonialController : Controller
    {
        MyAcademyPortfolioProjectEntities db = new MyAcademyPortfolioProjectEntities();
        // GET: Testimonial
        public ActionResult Index()
        {
            var values = db.TblTestimonials.ToList();
            return View(values);
        }
        public ActionResult AddTestimonial()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTestimonial(TblTestimonials testimonial)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/Testimonial/" + dosyaadi + uzanti;
                //Resmi klasörün içine atalım.
                Request.Files[0].SaveAs(Server.MapPath(yol));

                testimonial.ImageUrl = "/Image/Testimonial/" + dosyaadi + uzanti;
            }

            testimonial.Status = false;
            testimonial.CommentDate = DateTime.Now.Date;
            db.TblTestimonials.Add(testimonial);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UpdateTestimonial(int id)
        {
            var value = db.TblTestimonials.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateTestimonial(TblTestimonials testimonial)
        {
            var value = db.TblTestimonials.Find(testimonial.TestimonialId);

            //Request.Files.Count > 0
            if (testimonial.ImageUrl != null)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/Testimonial/" + dosyaadi + uzanti;
                //Resmi klasörün içine atalım.
                Request.Files[0].SaveAs(Server.MapPath(yol));

                if (System.IO.File.Exists(Server.MapPath(value.ImageUrl)))
                {
                    System.IO.File.Delete(Server.MapPath(value.ImageUrl));
                }

                value.ImageUrl = "/Image/Testimonial/" + dosyaadi + uzanti;
            }
            value.NameSurname = testimonial.NameSurname;
            value.Title = testimonial.Title;
            value.Comments = testimonial.Comments;
            value.Status = testimonial.Status;
            value.CommentDate = testimonial.CommentDate;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTestimonial(int id)
        {
            var value = db.TblTestimonials.Find(id);

            if (System.IO.File.Exists(Server.MapPath(value.ImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath(value.ImageUrl));
            }
            db.TblTestimonials.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}