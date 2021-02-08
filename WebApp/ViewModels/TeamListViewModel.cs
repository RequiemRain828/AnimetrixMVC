using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class TeamListViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<ApplicationUser> AllUsers { get; set; }
        public List<ApplicationUser> MyTeam = new List<ApplicationUser>();
    }
}
