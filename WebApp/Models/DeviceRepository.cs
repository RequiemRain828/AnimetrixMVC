using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public interface DeviceRepository
    {
        Device GetDevice(int Id);
        IEnumerable<Device> GetAllDevice();
        Device Add(Device project);
        Device Update(Device projectChanges);
        Device Delete(int id);
    }

    public class SQLDeviceRepository : DeviceRepository
    {
        private readonly AppDbContext context;

        public SQLDeviceRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Device Add(Device device)
        {
            context.Devices.Add(device);
            context.SaveChanges();
            return device;
        }

        public Device Delete(int id)
        {
            Device project = context.Devices.Find(id);
            if (project != null)
            {
                context.Devices.Remove(project);
                context.SaveChanges();
            }
            return project;
        }

        public IEnumerable<Device> GetAllDevice()
        {
            //var device = from m in context.Devices;
            return context.Devices;
        }

        public Device GetDevice(int Id)
        {
            return context.Devices.Find(Id);
        }

        public Device Update(Device changes)
        {
            var project = context.Devices.Attach(changes);
            project.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changes;
        }
    }
}
