using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ObservationController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ObservationRepository _observationRepository;
        private readonly AppDbContext context;

        public ObservationController(UserManager<ApplicationUser> userManager, ObservationRepository observationRepository, AppDbContext context)
        {
            this.userManager = userManager;
            _observationRepository = observationRepository;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult CreateObservation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateObservation(int id, Observation record)
        {
            ViewBag.DeviceId = id;
            Observation newRecord = new Observation { DeviceId = id, Timestamp = record.Timestamp, Weight = record.Weight, Humidity = record.Humidity, 
                Validatestatus = false, Temperature = record.Temperature, Length = record.Length, Sciencename = record.Sciencename, Commonname = record.Commonname};
            _observationRepository.Add(newRecord);
            return View();
        }
    }
}
