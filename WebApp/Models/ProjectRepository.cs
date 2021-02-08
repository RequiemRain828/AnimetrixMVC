using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public interface ProjectRepository
    {
        Project GetProject(int Id);
        IEnumerable<Project> GetAllProject();
        Project Add(Project project);
        Project Update(Project projectChanges);
        Project Delete(int id);
        ProjectAssignment Assign(ProjectAssignment assignment);
        ProjectAssignment DeleteAssign(ProjectAssignment assign);

    }

    public class SQLProjectRepository: ProjectRepository
    {
        private readonly AppDbContext context;

        public SQLProjectRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Project Add(Project project)
        {
            context.Projects.Add(project);
            //context.SaveChanges();
            return project;
        }

        public Project Delete(int id)
        {
            Project project = context.Projects.Find(id);
            if (project != null)
            {
                context.Projects.Remove(project);
                context.SaveChanges();
            }
            return project;
        }

        public IEnumerable<Project> GetAllProject()
        {
            return context.Projects;
        }

        public Project GetProject(int Id)
        {
            return context.Projects.Find(Id);
        }

        public Project Update(Project projectChanges)
        {
            var project = context.Projects.Attach(projectChanges);
            project.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return projectChanges;
        }

        public ProjectAssignment Assign(ProjectAssignment assignment)
        {
            context.Assignment.Add(assignment);
            context.SaveChanges();
            return assignment;
        }

        public ProjectAssignment DeleteAssignId(int id)
        {
            ProjectAssignment project = context.Assignment.Find(id);
            if (project != null)
            {
                context.Assignment.Remove(project);
                context.SaveChanges();
            }
            return project;
        }

        public ProjectAssignment DeleteAssign(ProjectAssignment assign)
        {           
            context.Assignment.Remove(assign);
            context.SaveChanges();           
            return assign;
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }

}
