using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public interface ObservationRepository
    {
        Observation GetObservation(int Id);
        IEnumerable<Observation> GetAllObservation();
        Observation Add(Observation observation);
        Observation Update(Observation changes);
        Observation Delete(int id);
    }

    public class SQLObservationRepository : ObservationRepository
    {
        private readonly AppDbContext context;

        public SQLObservationRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Observation Add(Observation observation)
        {
            context.Observations.Add(observation);
            context.SaveChanges();
            return observation;
        }

        public IEnumerable<Observation> GetAllObservation()
        {
            return context.Observations;
        }

        public Observation GetObservation(int Id)
        {
            return context.Observations.Find(Id);
        }

        public Observation Delete(int id)
        {
            Observation record = context.Observations.Find(id);
            if (record != null)
            {
                context.Observations.Remove(record);
                context.SaveChanges();
            }
            return record;
        }

        public Observation Update(Observation changes)
        {
            var record = context.Observations.Attach(changes);
            record.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changes;
        }
    }
}
