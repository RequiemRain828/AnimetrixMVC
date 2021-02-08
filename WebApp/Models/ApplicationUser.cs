using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Photopath { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<ProjectAssignment> Assignments { get; set; }

        [NotMapped]
        public List<ApplicationUser> Team = new List<ApplicationUser>();

        public IEnumerable<ApplicationUser> GetTeamList()
        {
            return Team;
        }
    }
}
