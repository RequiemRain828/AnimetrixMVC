using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public virtual ICollection<ProjectAssignment> Assignments { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }

    public class ProjectAssignment
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Projects { get; set; }
        public string UsersId { get; set; }
        public virtual ApplicationUser User { get; set; }
        
    }
}
