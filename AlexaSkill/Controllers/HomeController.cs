using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlexaSkill.Models;
using AlexaSkillGorillas.Data;

namespace AlexaSkill.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Form()
        {
            var model = new FormModel();
            using (var db = new AlexaGorillas_dbEntities())
            {
                model.Projects = db.Projects.ToList();
                model.Employees = db.Employees.ToList();
                model.Skills = db.Skills.ToList();
            }
            
                return View(model);
        }

        public ActionResult ServiceForm()
        {
            return View();
        }
    }
}
