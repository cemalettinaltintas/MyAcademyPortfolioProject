using MyPortfolio.Helper;
using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyPortfolio.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        MyAcademyPortfolioProjectEntities db = new MyAcademyPortfolioProjectEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginUserList()
        {
            var users = db.TblAdmins.ToList();
            return View(users);
        }
        [HttpPost]
        public ActionResult Index(TblAdmins admin)
        {
            var durum = false;
            foreach (var item in db.TblAdmins.ToList())
            {
                if (item.Password== PasswordHasher.HashPassword(admin.Password))
                {
                    durum = true; break;
                }
            }

            //var value = db.TblAdmins.FirstOrDefault(x => x.UserName == admin.UserName && x.Password ==admin.Password);
            if (durum)
            {
                var value = db.TblAdmins.FirstOrDefault(x => x.UserName == admin.UserName);
                if (value != null)
                {
                    FormsAuthentication.SetAuthCookie(value.UserName, false);
                    //Giriş yapıldığı sürece bilgi tutar session
                    Session["userName"] = value.UserName;
                    return RedirectToAction("Index", "About");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
                    ////return View();
                }
            }


            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Default");
        }

        public ActionResult AddLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddLogin(TblAdmins admin)
        {
            admin.Password = PasswordHasher.HashPassword(admin.Password);
            db.TblAdmins.Add(admin);
            db.SaveChanges();
            return RedirectToAction("LoginUserList");
        }

        public ActionResult UpdateLogin(int id)
        {
            var value = db.TblAdmins.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateLogin(TblAdmins admin)
        {
            var value = db.TblAdmins.Find(admin.AdminId);
            value.UserName = admin.UserName;
            value.Password = PasswordHasher.HashPassword(admin.Password);
            db.SaveChanges();
            return RedirectToAction("LoginUserList");
        }
        public ActionResult DeleteLogin(int id)
        {
            var value = db.TblAdmins.Find(id);
            db.TblAdmins.Remove(value);
            db.SaveChanges();
            return RedirectToAction("LoginUserList");
        }
    }
}