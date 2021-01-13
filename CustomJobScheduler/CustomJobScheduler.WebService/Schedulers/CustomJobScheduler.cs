using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomJobScheduler.DbService.Interfaces;
using CustomJobScheduler.SharedService.Models;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

namespace CustomJobScheduler.WebService.Schedulers
{
    public static class CustomJobScheduler
    {
        public static async Task<IScheduler> Create(bool start = true)
        { 
            var jobService = ServiceConfigurationInstance.ServiceProvider.GetService<IJobService>();
            var triggerService = ServiceConfigurationInstance.ServiceProvider.GetService<ITriggerService>();

            var allJob = await jobService.GetAllJobAsync();
            var allTrigger = await triggerService.GetAllTriggerAsync();
            var allTriggerType = await triggerService.GetAllTriggerTypeAsync();

            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();

            foreach (var job in allJob)
            {
                var jobDetail = new CustomJobBuilder().CreateJob(job);
                var triggers = allTrigger.FindAll(x => x.JobId == job.Id);
                
                if (!triggers.Any())
                {
                    await scheduler.AddJob(jobDetail, true);
                }
                else
                {
                    var triggerList = new List<ITrigger>();

                    foreach (var trigger in triggers)
                    {
                        var triggerType = allTriggerType.FirstOrDefault(x => x.Id == trigger.Id);
                        var triggerDetail = new CustomTriggerBuilder().CreateTrigger(trigger, triggerType);
                        triggerList.Add(triggerDetail);
                    }

                    await scheduler.ScheduleJob(jobDetail, triggerList, true);
                }
            }
            
            if (start) await scheduler.Start();
            return scheduler;
        }
    }
}
