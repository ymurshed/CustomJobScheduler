using System;
using System.Collections.Generic;
using System.Linq;
using CustomJobScheduler.DbService.DbModels;
using CustomJobScheduler.SharedService.Interfaces;
using CustomJobScheduler.SharedService.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;

namespace CustomJobScheduler.WebService.Schedulers
{
    public class CustomJobBuilder
    {
        private readonly IJsonService _jsonService;
        private readonly IOptions<SharedOptions> _sharedOptions;

        public CustomJobBuilder()
        {
            _jsonService = ServiceConfigurationInstance.ServiceProvider.GetService<IJsonService>();
            _sharedOptions = ServiceConfigurationInstance.ServiceProvider.GetService<IOptions<SharedOptions>>();
        }

        private bool HasKeyMatched(string key)
        {
            return _sharedOptions.Value.JobDataKey.ConvertAll(x => x.ToLower()).Contains(key.ToLower().Trim());
        }

        private string GetJobClassType()
        {
            var jobClass = _sharedOptions.Value.JobClass;
            var cls = jobClass.FirstOrDefault();
            return cls?.Split(",").ToList().FirstOrDefault();
        }

        public IJobDetail CreateJob(Job job)
        {
            var jobData = (Dictionary<string, object>)_jsonService.GetObjectFromJsonString(job.JobData);
            var jobDataToMap = jobData.Where(x => HasKeyMatched(x.Key)).ToDictionary(x => x.Key, x => x.Value);

            var jobDataMapObject = new JobDataMap();
            foreach (var (key, value) in jobDataToMap)
            {
                jobDataMapObject.Add(key, value);
            }
            
            var jobDetail = JobBuilder.Create()
                            .OfType(Type.GetType(GetJobClassType()))
                            .WithIdentity(job.JobName, job.Group)
                            .WithDescription(job.Description)
                            .UsingJobData(jobDataMapObject)
                            .StoreDurably()
                            .Build();
            
            if (job.Recovery.HasValue && job.Recovery.Value)
            {
                jobDetail = jobDetail.GetJobBuilder().RequestRecovery(job.Recovery.Value).Build();
            }

            return jobDetail;
        }
    }
}
