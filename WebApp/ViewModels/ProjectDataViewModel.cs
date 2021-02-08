using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class ProjectDataViewModel
    {
        public Project Project { get; set; }
        public ApplicationUser User { get; set; }
        public Device Device { get; set; }
        public IEnumerable<Observation> ObservationList { get; set; }
    }
}
