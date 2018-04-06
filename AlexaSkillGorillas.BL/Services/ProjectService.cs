using AlexaSkillGorillas.BL.Models;
using AlexaSkillGorillas.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AlexaSkillGorillas.BL.Services
{
    public class ProjectService
    {
        private AlexaGorillas_dbEntities db = new AlexaGorillas_dbEntities();

        public List<ProjectModel> GetProjects()
        {
            var projects = db.Projects;
            var result = new List<ProjectModel>();

            //TODO: Use an automapper for this
            foreach (var project in projects)
            {
                result.Add(new ProjectModel
                {
                    Id = project.Id,
                    Name = project.Name
                });
            }
            return result;
        }

        public ProjectModel GetProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return new ProjectModel
                {
                    Id = project.Id,
                    Name = project.Name
                };
            }
            return null;
        }

        public int UpdateProject(int id, ProjectModel project)
        {
            //TODO: Double check if there is a requirement to map EmployeeModel to Employee entity for updating using EF
            db.Entry(project).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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
        public int AddProject(ProjectModel newProject)
        {
            db.Projects.Add(new Project
            {
                Name = newProject.Name
            });
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProjectExists(newProject.Id))
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
        public int DeleteProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return 0;
            }
            db.Projects.Remove(project);
            db.SaveChanges();
            return 1;
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.Id == id) > 0;
        }
    }
}
