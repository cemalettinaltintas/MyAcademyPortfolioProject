using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class TeamController : Controller
    {
        MyAcademyPortfolioProjectEntities db = new MyAcademyPortfolioProjectEntities();
        // GET: Team
        public ActionResult Index()
        {
            var values = db.TblTeams.ToList();
            return View(values);
        }
        public ActionResult AddTeam() {

            return View();
        }
        [HttpPost]
        public ActionResult AddTeam(TblTeams team)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/Team/" + dosyaadi + uzanti;
                //Resmi klasörün içine atalım.
                Request.Files[0].SaveAs(Server.MapPath(yol));

                team.ImageUrl = "/Image/Team/" + dosyaadi + uzanti;
            }

            db.TblTeams.Add(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UpdateTeam(int id)
        {
            var value = db.TblTeams.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateTeam(TblTeams team)
        {
            var value = db.TblTeams.Find(team.TeamId);

            //Request.Files.Count > 0
            if (team.ImageUrl != null)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/Team/" + dosyaadi + uzanti;
                //Resmi klasörün içine atalım.
                Request.Files[0].SaveAs(Server.MapPath(yol));

                if (System.IO.File.Exists(Server.MapPath(value.ImageUrl)))
                {
                    System.IO.File.Delete(Server.MapPath(value.ImageUrl));
                }

                value.ImageUrl = "/Image/Team/" + dosyaadi + uzanti;
            }
            value.NameSurname=team.NameSurname;
            value.Description = team.Description;
            value.TwitterUrl = team.TwitterUrl;
            value.FacebookUrl = team.FacebookUrl;
            value.LinkedinUrl = team.LinkedinUrl;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteTeam(int id)
        {
            var value = db.TblTeams.Find(id);
            if (System.IO.File.Exists(Server.MapPath(value.ImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath(value.ImageUrl));
            }
            db.TblTeams.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}