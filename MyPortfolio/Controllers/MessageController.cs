using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class MessageController : Controller
    {
        MyAcademyPortfolioProjectEntities db=new MyAcademyPortfolioProjectEntities();
        // GET: Message
        public ActionResult Index()
        {
            var values = db.TblMessages.Where(x=>x.IsRead==false).ToList();
            return View(values);
        }
        public ActionResult GetAllMessage()
        {
            var values = db.TblMessages.ToList();
            return View(values);
        }
        public ActionResult GetReadMessage()
        {
            var values = db.TblMessages.Where(x => x.IsRead == true).ToList();
            return View(values);
        }
        public ActionResult MarkAsRead(int id)
        {
            var value = db.TblMessages.Find(id);
            value.IsRead = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetMessageByNameSurname(string name)
        {
            var value = db.TblMessages.Where(x => x.Name.Contains(name)).ToList();
            return View("Index", value);
        }
    }
}