using AlexaSkillGorillas.BL.Models;
using AlexaSkillGorillas.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexaSkillGorillas.BL.Services
{
    public class EmployeeService
    {
        private AlexaGorillas_dbEntities db = new AlexaGorillas_dbEntities();


        public EmployeeModel GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return new EmployeeModel
                {
                    Id = employee.Id,
                    FirstName = employee.First_Name,
                    LastName = employee.Last_Name,
                    Email = employee.Email,
                    ProjectId = employee.Project_Id.ToString(),
                    SlackId = employee.SlackId
                };
            }
            return null;
        }

        public List<EmployeeModel> GetEmployees()
        {
            var employees = db.Employees;
            var result = new List<EmployeeModel>();

            //TODO: Use an automapper for this
            foreach (var employee in employees)
            {
                result.Add(new EmployeeModel
                {
                    Id = employee.Id,
                    FirstName = employee.First_Name,
                    LastName = employee.Last_Name,
                    Email = employee.Email,
                    ProjectId = employee.Project_Id.ToString(),
                    SlackId = employee.SlackId
                });
            }
            return result;
        }
    }
}
