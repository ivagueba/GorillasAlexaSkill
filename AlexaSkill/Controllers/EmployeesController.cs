using System.Collections.Generic;
using System.Web.Http;
using AlexaSkill.Models;
using AlexaSkillGorillas.BL.Models;
using AlexaSkillGorillas.BL.Services;

namespace AlexaSkill.Controllers
{
    public class EmployeesController : ApiController
    {
        private EmployeeService service = new EmployeeService();

        // GET: api/Employees
        public GenericResponse<List<EmployeeModel>> GetEmployees()
        {
            var result = new GenericResponse<List<EmployeeModel>>();
            var employees = service.GetEmployees();

            //TODO: CREATE RESPONSE OBJECT
            result.Data = employees;
            return result;
        }

        // GET: api/Employees/5
        public GenericResponse<EmployeeModel> GetEmployee(int id)
        {
            var result = new GenericResponse<EmployeeModel>();
            var employee = service.GetEmployee(id);
            if (employee == null)
            {
                result.StatusCode = 404; //TODO: ENUM THIS
                result.Message = "NOT FOUND";
            }
            result.Data = employee;
            return result;
        }

        // PUT: api/Employees/5
        public GenericResponse<string> PutEmployee(int id, EmployeeModel employee)
        {
            if (!ModelState.IsValid)
            {
                return new GenericResponse<string>
                {
                    StatusCode = 400,
                    Message = "BAD REQUEST"
                };
            }

            if (id != employee.Id)
            {
                return new GenericResponse<string>
                {
                    StatusCode = 400,
                    Message = "BAD REQUEST"
                };
            }
            var result = new GenericResponse<string>
            {
                StatusCode = service.UpdateEmployee(id, employee)
            };

            return result;
        }

        // POST: api/Employees
        public GenericResponse<string> PostEmployee(EmployeeModel employee)
        {
            //TODO: THIS VALIDATION CAN BE EXTRACTED INTO A VALIDATION METHOD FOR REUSABILITY
            if (!ModelState.IsValid)
            {
                return new GenericResponse<string>
                {
                    StatusCode = 400,
                    Message = "BAD REQUEST"
                };
            }

            if (service.AddEmployee(employee) == 1)
            {
                return new GenericResponse<string>
                {
                    StatusCode = 200,
                    Message = "NO CONTENT"
                };
            }

            return new GenericResponse<string>
            {
                StatusCode = 500,
                Message = "SERVER ERROR"
            };
        }

        // DELETE: api/Employees/5
        public GenericResponse<string> DeleteEmployee(int id)
        {
            if (service.DeleteEmployee(id) == 1)
            {
                return new GenericResponse<string>
                {
                    StatusCode = 200,
                    Message = "NO CONTENT"
                };
            }

            return new GenericResponse<string>
            {
                StatusCode = 500,
                Message = "SERVER ERROR"
            };
        }


    }
}