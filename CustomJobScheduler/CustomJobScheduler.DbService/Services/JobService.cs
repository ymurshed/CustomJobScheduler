using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomJobScheduler.DbService.DbModels;
using CustomJobScheduler.DbService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomJobScheduler.DbService.Services
{
    public class JobService : IJobService
    {
        private readonly Context _context;
        
        public JobService(Context context)
        {
            _context = context;
        }

        public async Task SaveJobAsync(Job job)
        {
            await _context.Job.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateJobAsync(Job job)
        {
            var jobKey = $"{job.OldGroup}.{job.OldJobName}";
            var jobId = await _context.Job.AsNoTracking().Where(x => x.JobKey == jobKey).Select(x => x.Id).FirstOrDefaultAsync();
            
            job.Id = jobId;
           
            _context.Job.Update(job);
            _context.SaveChanges();
        }

        public async Task DeleteJobAsync(string jobKey)
        {
            var job = await _context.Job.FirstOrDefaultAsync(x => x.JobKey == jobKey);
            var triggers = await _context.Trigger.Where(x => x.JobId == job.Id).ToListAsync();

            foreach (var trigger in triggers)
            {
                var triggerType = await _context.TriggerType.FirstOrDefaultAsync(x => x.Id == trigger.Id);
                _context.TriggerType.Remove(triggerType);
                _context.Trigger.Remove(trigger);
            }
            
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
        }

        public async Task<Job> GetJobAsync(string jobKey)
        {
            return await _context.Job.FirstOrDefaultAsync(x => x.JobKey == jobKey);
        }

        public async Task<List<Job>> GetAllJobAsync()
        {
            return await _context.Job.ToListAsync();
        }
    }
}
