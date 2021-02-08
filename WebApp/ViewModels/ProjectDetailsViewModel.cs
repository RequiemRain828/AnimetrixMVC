using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public Project Project { get; set; }
        public string PageTitle { get; set; }
        public IEnumerable<Device> AvailDevices { get; set; }
        public IEnumerable<Device> Devices { get; set; }
        public Device Device { get; set; }
        public ApplicationUser User { get; set; }
        public IList<ApplicationUser> AvailUsers = new List<ApplicationUser>();
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<ApplicationUser> UserList { get; set; }
        public ProjectAssignment Assignment { get; set; }
    }
}
