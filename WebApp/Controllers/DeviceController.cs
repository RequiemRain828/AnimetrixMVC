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
    public class DeviceController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly DeviceRepository _deviceRepository;
        private readonly AppDbContext context;

        public DeviceController(UserManager<ApplicationUser> userManager, DeviceRepository deviceRepository, AppDbContext context)
        {
            this.userManager = userManager;
            _deviceRepository = deviceRepository;
            this.context = context;
        }

        public JsonResult GetAllLocation()
        {
            //var data = context.Devices.ToList();
            var userid = userManager.GetUserId(HttpContext.User);
            var data = from m in context.Devices join u in context.ApplicationUsers on m.UserId equals u.Id where u.Id == userid select m;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public ViewResult AddObservation(int id)
        {
            return View();
        }

        public IActionResult MyDevices()
        {
            //var model = _deviceRepository.GetAllDevice();
            var userid = userManager.GetUserId(HttpContext.User);
            var model = from m in context.Devices join u in context.ApplicationUsers on m.UserId equals u.Id where u.Id == userid select m;
            return View(model);         
        }

        public ViewResult EditDevice()
        {
            return View();
        }

        public ViewResult DeviceDetails(int? id)
        {
            var userid = userManager.GetUserId(HttpContext.User);
            DeviceDetailsViewModel deviceDetailsViewModel = new DeviceDetailsViewModel()
            {
                Device = _deviceRepository.GetDevice(id ?? 1),
                PageTitle = "Device Details",
                ObservationList = from c in context.Observations join u in context.Devices on c.DeviceId equals u.DeviceId join a in context.ApplicationUsers on u.UserId equals a.Id where a.Id == userid select c
            };

            return View(deviceDetailsViewModel);
        }

        [HttpGet]
        public IActionResult AddDevice()
        {
            ViewBag.userid = userManager.GetUserId(HttpContext.User);
            return View();
        }

        [HttpPost]
        public RedirectToActionResult AddDevice(Device device)
        {
            Device newDevice = new Device { UserId = userManager.GetUserId(HttpContext.User), Nickname = device.Nickname, Longitude = device.Longitude, Latitude = device.Latitude};
            _deviceRepository.Add(newDevice);
            //return RedirectToAction("index", "home");
            return RedirectToAction("mydevices", "device");
        }
        
    }
}
