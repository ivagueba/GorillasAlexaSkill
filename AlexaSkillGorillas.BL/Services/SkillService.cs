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
    public class SkillService
    {
        private AlexaGorillas_dbEntities db = new AlexaGorillas_dbEntities();

        public List<SkillModel> GetSkills()
        {
            var skills = db.Skills;
            var result = new List<SkillModel>();

            //TODO: Use an automapper for this
            foreach (var skill in skills)
            {
                result.Add(new SkillModel
                {
                    Id = skill.Id,
                    Name = skill.Name
                });
            }
            return result;
        }

        public SkillModel GetSkill(int id)
        {
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return new SkillModel
                {
                    Id = skill.Id,
                    Name = skill.Name
                };
            }
            return null;
        }

        public int UpdateSkill(int id, SkillModel skill)
        {
            //TODO: Double check if there is a requirement to map EmployeeModel to Employee entity for updating using EF
            db.Entry(skill).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
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

        //TODO: ADD A LOT MORE VALIDATIONS AND STATUS CODES
        public int AddSkill(SkillModel newSkill)
        {
            db.Skills.Add(new Skill
            {
                Name = newSkill.Name
            });
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SkillExists(newSkill.Id))
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

        //TODO: Change 1 and 0 responses to appropiate understantable values
        public int DeleteSkill(int id)
        {
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return 0;
            }
            db.Skills.Remove(skill);
            db.SaveChanges();
            return 1;
        }

        private bool SkillExists(int id)
        {
            return db.Skills.Count(e => e.Id == id) > 0;
        }
    }
}
