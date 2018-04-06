using AlexaSkillGorillas.BL.Models;
using AlexaSkillGorillas.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace AlexaSkillGorillas.BL.Services
{
    public class EmployeeService
    {
        private AlexaGorillas_dbEntities db = new AlexaGorillas_dbEntities();

        //TODO: Change 1 and 0 responses to appropiate understantable values
        public int DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return 0;
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return 1;
        }


        //TODO: ADD A LOT MORE VALIDATIONS AND STATUS CODES
        public int AddEmployee(EmployeeModel newEmployee)
        {

            db.Employees.Add(new Employee
            {
                First_Name = newEmployee.FirstName,
                Last_Name = newEmployee.LastName,
                Email = newEmployee.Email,
                SlackId = newEmployee.SlackId
            });

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(newEmployee.Id))
                {
                    return 0;
                }
                else
                {
                    throw;
                }
            }
            return 1;
        }

        public int UpdateEmployee(int id, EmployeeModel employee)
        {
            //TODO: Double check if there is a requirement to map EmployeeModel to Employee entity for updating using EF
            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return 404;
                }
                else
                {
                    throw;
                }
            }
            return 200;
        }

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

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }
    }
}
