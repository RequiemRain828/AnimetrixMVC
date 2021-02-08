using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class DeviceDetailsViewModel
    {
        public Device Device { get; set; }
        public string PageTitle { get; set; }
        public IEnumerable<Observation> ObservationList {get; set;}
    }
}
