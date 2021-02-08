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
    public class CommunityController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AppDbContext context;

        public CommunityController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        public IActionResult MyTeam()
        {
            var userid = userManager.GetUserId(HttpContext.User);
            var currentUser = userManager.Users.FirstOrDefault(u => u.Id == userid);
            var model = currentUser.GetTeamList();
            return View(model);
        }

        [HttpGet]
        public IActionResult TeamList()
        {
            var userid = userManager.GetUserId(HttpContext.User);
            
            TeamListViewModel teamListViewModel = new TeamListViewModel
            {
                User = userManager.Users.FirstOrDefault(u => u.Id == userid),
                AllUsers = from m in userManager.Users where m.Id != userid select m,
                            
            };
            
            return View(teamListViewModel);
        }
        [HttpPost]
        public RedirectToActionResult TeamList(TeamListViewModel model)
        {
            var userid = userManager.GetUserId(HttpContext.User);
            //var id = model.User.Id;

            if (ModelState.IsValid)
            {
                var currentUser = userManager.Users.FirstOrDefault(u => u.Id == userid);
                var selectedUser = userManager.Users.FirstOrDefault(u => u.Id == model.User.Id);
                //currentUser.Team = new List<ApplicationUser>();
                //model.MyTeam.Add(selectedUser);
                currentUser.Team.Add(selectedUser);
            }

            return RedirectToAction("teamlist", "community");
        }
    }
}
