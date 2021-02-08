using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ProjectRepository _projectRepository;
        private readonly AppDbContext context;
        private readonly DeviceRepository _deviceRepository;

        public List<ApplicationUser> UsersList = new List<ApplicationUser>();
        public ProjectController(UserManager<ApplicationUser> userManager, ProjectRepository projectRepository, AppDbContext context, DeviceRepository deviceRepository)
        {
            this.userManager = userManager;
            _projectRepository = projectRepository;
            this.context = context;
            _deviceRepository = deviceRepository;
        }
        public ViewResult MyProjects()
        {
            var userid = userManager.GetUserId(HttpContext.User);
            //var model = _projectRepository.GetAllProject();
            var model = from c in context.Projects join d in context.Assignment on c.ProjectId equals d.ProjectId join f in context.ApplicationUsers on d.UsersId equals f.Id where f.Id == userid select c;
            return View(model);
        }

        public ViewResult ProjectDetails(int? id)
        {
            var userid = userManager.GetUserId(HttpContext.User);
            //ViewBag.projectId = id;
            ProjectDetailsViewModel projectDetailsViewModel = new ProjectDetailsViewModel()
            {
                Project = _projectRepository.GetProject(id ?? 1),
                PageTitle = "Project Details",
                AvailDevices = from m in context.Devices join u in context.ApplicationUsers on m.UserId equals u.Id where u.Id == userid && m.ProjectId == (null) select m,
                Devices = from m in context.Devices join u in context.ApplicationUsers on m.UserId equals u.Id where u.Id == userid && m.ProjectId != (null) select m,
                Users = from a in context.ApplicationUsers join d in context.Assignment on a.Id equals d.UsersId where d.ProjectId == id && a.Id != userid select a,
                UserList = (from c in userManager.Users where c.Id != userid select c),
                //AvailUsers.Add()
                //from a in context.ApplicationUsers join d in context.Assignment on a.Id equals d.UsersId where a.Id != userid select a
                //from c in userManager.Users where c.Id != userid select c
            };
            
            var list = from c in userManager.Users where c.Id != userid select c;
            foreach(var item in list)
            {
                UsersList.Add(item);
            }
            ViewBag.List = UsersList;
            return View(projectDetailsViewModel);
        }

        [HttpGet]
        public ViewResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public RedirectToActionResult CreateProject(Project project)
        {
            
            Project newProject = _projectRepository.Add(project);
            ProjectAssignment newAssignment = new ProjectAssignment { ProjectId = newProject.ProjectId, UsersId = userManager.GetUserId(HttpContext.User) };
            _projectRepository.Assign(newAssignment);
            return RedirectToAction("projectdetails", new { id = newProject.ProjectId });
        }

        [HttpPost]
        public RedirectToActionResult AddDevices(ProjectDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Device device = _deviceRepository.GetDevice(model.Device.DeviceId);
                device.ProjectId = model.Project.ProjectId;
                _deviceRepository.Update(device);
            }
            
            return RedirectToAction("projectdetails", new { id = model.Project.ProjectId });
        }

        [HttpPost]
        public RedirectToActionResult RemoveDevices(ProjectDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Device device = _deviceRepository.GetDevice(model.Device.DeviceId);
                device.ProjectId = (null);
                _deviceRepository.Update(device);
            }

            return RedirectToAction("projectdetails", new { id = model.Project.ProjectId });

        }


        [HttpPost]
        public RedirectToActionResult AddMembers(ProjectDetailsViewModel model)
        {
            //if (ModelState.IsValid)
            {
                ProjectAssignment newAssignment = new ProjectAssignment { ProjectId = model.Project.ProjectId, UsersId = model.User.Id };
                _projectRepository.Assign(newAssignment);
                var selectedUser = userManager.Users.FirstOrDefault(u => u.Id == model.User.Id);
                UsersList.Remove(selectedUser);
                ViewBag.List = UsersList;
                ViewBag.AssignID = newAssignment.Id;
            }
            return RedirectToAction("projectdetails", new { id = model.Project.ProjectId });
        }

        [HttpPost]
        public RedirectToActionResult RemoveMembers(ProjectDetailsViewModel model)
        {
            //if (ModelState.IsValid)
            {
                //var selectedAssign.First( from a in context.ApplicationUsers join d in context.Assignment on a.Id equals d.UsersId where d.UsersId == model.User.Id select d.Id);
                var selectedAssign = context.Assignment.FirstOrDefault(u => u.UsersId == model.User.Id);
                var selectedUser = userManager.Users.FirstOrDefault(u => u.Id == model.User.Id);
                _projectRepository.DeleteAssign(selectedAssign);
                UsersList.Add(selectedUser);
                ViewBag.List = UsersList;
            }
            return RedirectToAction("projectdetails", new { id = model.Project.ProjectId });
        }

        [HttpGet]
        public ViewResult ProjectData(int? id)
        {
            var userid = userManager.GetUserId(HttpContext.User);
            ProjectDataViewModel projectdataViewModel = new ProjectDataViewModel()
            {
                ObservationList = from c in context.Observations join u in context.Devices on c.DeviceId equals u.DeviceId join a in context.Projects on u.ProjectId equals a.ProjectId where u.ProjectId == id select c
            };
            return View(projectdataViewModel);
        }
    }
}
