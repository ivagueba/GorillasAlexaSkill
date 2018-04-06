using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AlexaSkill.Models;
using AlexaSkillGorillas.BL.Models;
using AlexaSkillGorillas.BL.Services;
using AlexaSkillGorillas.Data;

namespace AlexaSkill.Controllers
{
    public class SkillsController : ApiController
    {
        private SkillService service = new SkillService();

        // GET: api/Skills
        public GenericResponse<List<SkillModel>> GetSkills()
        {
            var result = new GenericResponse<List<SkillModel>>();
            var skills = service.GetSkills();

            //TODO: CREATE RESPONSE OBJECT WITH STATUS CODE AND MESSAGE
            result.Data = skills;
            return result;
        }

        // GET: api/Skills/5
        public GenericResponse<SkillModel> GetSkill(int id)
        {
            var result = new GenericResponse<SkillModel>();
            var skill = service.GetSkill(id);
            if (skill == null)
            {
                result.StatusCode = 404; //TODO: ENUM THIS
                result.Message = "NOT FOUND";
            }
            result.Data = skill;
            return result;
        }

        public GenericResponse<string> PutSkill(int id, SkillModel skill)
        {
            if (!ModelState.IsValid)
            {
                return new GenericResponse<string>
                {
                    StatusCode = 400,
                    Message = "BAD REQUEST"
                };
            }
            if (id != skill.Id)
            {
                return new GenericResponse<string>
                {
                    StatusCode = 400,
                    Message = "BAD REQUEST"
                };
            }
            var result = new GenericResponse<string>
            {
                StatusCode = service.UpdateSkill(id, skill)
            };

            return result;
        }

        // POST: api/Skills
        public GenericResponse<string> PostSkill(SkillModel skill)
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

            if (service.AddSkill(skill) == 1)
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

        // DELETE: api/Skills/5
        [ResponseType(typeof(Skill))]
        public GenericResponse<string> DeleteSkill(int id)
        {
            if (service.DeleteSkill(id) == 1)
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