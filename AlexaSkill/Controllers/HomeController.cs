using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlexaSkill.Models;
using AlexaSkillGorillas.Data;
using AlexaSkillGorillas.BL.Services;
using AlexaSkillGorillas.BL.Models;

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
            string form = Request.QueryString["form"];
            ViewBag.form = form;
            var model = new FormModel();
            using (var db = new AlexaGorillas_dbEntities())
            {
                model.Projects = db.Projects.Include("Employees").ToList();
                model.Employees = db.Employees.ToList();
                model.Skills = db.Skills.ToList();
            }
            
                return View(model);
        }

        public ActionResult ServiceForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitProject(string projectName)
        {
            //Save Project
            
                ProjectModel projectModel = new ProjectModel();
                projectModel.Name = projectName;
                ProjectService projectService = new ProjectService();
                projectService.AddProject(projectModel);
                //Project newProject = new Project();
                //newProject.Name = projectName;
                //db.Projects.Add(newProject);
                ////IDENTITY_INSERT is off... It causes issues to save data
                //db.SaveChanges();
            
            return RedirectToAction("Form");
        }

        [HttpPost]
        public ActionResult SubmitEmployee(string FirstName, string LastName, string Email, string SlackId)
        {
            //Save Employee
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.FirstName = FirstName;
            employeeModel.LastName = LastName;
            employeeModel.Email = Email;
            employeeModel.SlackId = SlackId;
            EmployeeService employeeService = new EmployeeService();
            employeeService.AddEmployee(employeeModel);

                return RedirectToAction("Form");
        }

        [HttpPost]
        public ActionResult SubmitSkill(string projectName)
        {
            //Save Skill
            return RedirectToAction("Form");
        }

        [HttpPost]
        public ActionResult SubmitProjectEmployee(string projectName)
        {
            //Save Relations
            return RedirectToAction("Form");
        }
    }
}
