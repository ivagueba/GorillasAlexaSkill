using AlexaSkillGorillas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexaSkill.Models
{
    public class FormModel
    {
        public List<Project> Projects { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Skill> Skills { get; set; }
        public FormModel()
        {
            Projects = new List<Project>();
            Employees = new List<Employee>();
            Skills = new List<Skill>();
        }
    }
}