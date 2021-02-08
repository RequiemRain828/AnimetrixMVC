using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string Nickname { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set;  }
        public string UserId { get; set; }
        public int? ProjectId { get; set; }
        public virtual ICollection<Observation> Observations { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Project Project { get; set; }
    }
}
