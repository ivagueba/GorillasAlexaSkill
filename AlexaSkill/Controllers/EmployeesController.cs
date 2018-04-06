using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AlexaSkill.Models;
using AlexaSkillGorillas.BL.Models;
using AlexaSkillGorillas.BL.Services;
using AlexaSkillGorillas.Data;

namespace AlexaSkill.Controllers
{
    public class EmployeesController : ApiController
    {
        private AlexaGorillas_dbEntities db = new AlexaGorillas_dbEntities();
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
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }
    }
}