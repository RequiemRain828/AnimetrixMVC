using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Observation
    {
        public int ObservationID { get; set; }
        public DateTime Timestamp { get; set; }
        public double Weight { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Length { get; set; }
        public string photoPath { get; set; }
        public string Sciencename { get; set; }
        public string Commonname { get; set; }
        public bool Validatestatus { get; set; }
        public int DeviceId { get; set; }
    }
}
