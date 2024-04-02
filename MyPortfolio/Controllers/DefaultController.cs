using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        
        MyAcademyPortfolioProjectEntities db = new MyAcademyPortfolioProjectEntities();
        // GET: Default
        public ActionResult Index()
        {
            var value = db.TblContacts.First();
            ViewBag.NameSurname = value.NameSurname;
            ViewBag.Address=value.Address;
            ViewBag.Phone = value.Phone;
            ViewBag.Email = value.Email;

            var values = db.TblScialMedias.ToList();
            return View(values);
        }
        public PartialViewResult DefaultFeaturePartial()
        {
            var values = db.TblFeatures.ToList();
            return PartialView(values);
        }
        public PartialViewResult DefaultAboutPartial()
        {
            var values = db.TblAbouts.ToList();
            return PartialView(values);
        }
        [HttpGet]
        public PartialViewResult SendMessage()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult SendMessage(TblMessages message)
        {
            message.IsRead = false;
            message.MessageDate=DateTime.Now.Date;
            db.TblMessages.Add(message);
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult DefaultServicePartial()
        {
            var values = db.TblServices.Where(x=>x.Status==true).ToList();
            return PartialView(values);
        }
        public PartialViewResult DefaultSkillPartial()
        {
            var values = db.TblSkills.ToList();
            return PartialView(values);
        }
        public PartialViewResult DefaultProjectPartial() {
            var categories = db.TblCategories.ToList();
            ViewBag.categories = categories;

            var values = db.TblProjects.ToList();
            return PartialView(values);
        
        }

        public PartialViewResult DefaultExperiencePartial()
        {
            var values = db.TblExperiences.ToList();
            return PartialView(values);
        }
        public PartialViewResult DefaultTestimonialPartial()
        {
            var testimonials = db.TblTestimonials.ToList();
            return PartialView(testimonials);
        }

        public PartialViewResult DefaultTeamPartial()
        {
            var teams = db.TblTeams.ToList();
            return PartialView(teams);
        }
    }
}