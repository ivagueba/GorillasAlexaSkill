using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AlexaSkillGorillas.Data;

namespace AlexaSkill.Controllers
{
    public class SkillsController : ApiController
    {
        private AlexaGorillas_dbEntities db = new AlexaGorillas_dbEntities();

        // GET: api/Skills
        public IQueryable<Skill> GetSkills()
        {
            return db.Skills;
        }

        // GET: api/Skills/5
        [ResponseType(typeof(Skill))]
        public IHttpActionResult GetSkill(int id)
        {
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        // PUT: api/Skills/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSkill(int id, Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skill.Id)
            {
                return BadRequest();
            }

            db.Entry(skill).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
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

        // POST: api/Skills
        [ResponseType(typeof(Skill))]
        public IHttpActionResult PostSkill(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Skills.Add(skill);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SkillExists(skill.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = skill.Id }, skill);
        }

        // DELETE: api/Skills/5
        [ResponseType(typeof(Skill))]
        public IHttpActionResult DeleteSkill(int id)
        {
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return NotFound();
            }

            db.Skills.Remove(skill);
            db.SaveChanges();

            return Ok(skill);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SkillExists(int id)
        {
            return db.Skills.Count(e => e.Id == id) > 0;
        }
    }
}