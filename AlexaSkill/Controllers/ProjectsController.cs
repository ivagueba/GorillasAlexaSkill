using System.Collections.Generic;
using System.Web.Http;
using AlexaSkill.Helper;
using AlexaSkill.Models;
using AlexaSkillGorillas.BL.Models;
using AlexaSkillGorillas.BL.Services;

namespace AlexaSkill.Controllers
{
    public class ProjectsController : ApiController
    {
        private readonly ProjectService _service;
        private readonly SignalRClientMethodsHelper _signalRClientMethodsHelper;

        public ProjectsController()
        {
            _signalRClientMethodsHelper = new SignalRClientMethodsHelper();
            _service = new ProjectService();
        }

        // GET: api/Projects
        public GenericResponse<List<ProjectModel>> GetProjects()
        {
            var result = new GenericResponse<List<ProjectModel>>();
            var skills = _service.GetProjects();

            //TODO: CREATE RESPONSE OBJECT WITH STATUS CODE AND MESSAGE
            result.Data = skills;
            return result;
        }

        // GET: api/Projects/5
        public GenericResponse<ProjectModel> GetProject(int id)
        {
            var result = new GenericResponse<ProjectModel>();
            var project = _service.GetProject(id);
            if (project == null)
            {
                result.StatusCode = 404; //TODO: ENUM THIS
                result.Message = "NOT FOUND";
            }
            result.Data = project;
            return result;
        }

        // PUT: api/Projects/5
        public GenericResponse<string> PutProject(int id, ProjectModel project)
        {
            if (!ModelState.IsValid)
            {
                return new GenericResponse<string>
                {
                    StatusCode = 400,
                    Message = "BAD REQUEST"
                };
            }
            if (id != project.Id)
            {
                return new GenericResponse<string>
                {
                    StatusCode = 400,
                    Message = "BAD REQUEST"
                };
            }

            var statusCode = _service.UpdateProject(id, project);
            if (statusCode == 200)
            {
                _signalRClientMethodsHelper.RefreshEmployeesList();
            }
            
            return new GenericResponse<string>
            {
                StatusCode = statusCode
            };
        }

        // POST: api/Projects
        public GenericResponse<string> PostProject(ProjectModel project)
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

            if (_service.AddProject(project) == 1)
            {
                _signalRClientMethodsHelper.RefreshProjectsList();
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

        // DELETE: api/Projects/5
        public GenericResponse<string> DeleteProject(int id)
        {
            if (_service.DeleteProject(id) == 1)
            {
                _signalRClientMethodsHelper.RefreshProjectsList();
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