
using AlexaSkillGorillas.Data;
using AlexaSkillGorillas.Domain.Models;
using System.Collections.Generic;

namespace AlexaSkillGorillas.Domain.Services
{
    public class EmployeeServices
    {
        private AlexaGorillas_dbEntities db = new AlexaGorillas_dbEntities();

        public List<EmployeeModel> GetEmployee()
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
