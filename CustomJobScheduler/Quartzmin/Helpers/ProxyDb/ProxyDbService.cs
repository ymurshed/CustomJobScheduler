using System;
using System.Threading.Tasks;
using AutoMapper;
using CustomJobScheduler.DbService.DbModels;
using CustomJobScheduler.DbService.Interfaces;
using Quartzmin.Models;
using TriggerType = CustomJobScheduler.DbService.DbModels.TriggerType;

namespace Quartzmin.Helpers.ProxyDb
{
    public class ProxyDbService : IProxyDbService
    {
        private readonly IMapper _mapper;
        private readonly IJobService _jobService;
        private readonly ITriggerService _triggerService;

        public ProxyDbService(IMapper mapper, IJobService jobService, ITriggerService triggerService)
        {
            _mapper = mapper;
            _jobService = jobService;
            _triggerService = triggerService;
        }

        public async Task SaveJob(JobPropertiesViewModel jobPropertiesViewModel, JobDataMapItemBase[] jobDataMapItemBases, bool shouldUpdate = false)
        {
            var job = _mapper.Map<Job>(jobPropertiesViewModel);
            job.JobData = jobDataMapItemBases.GetJobDataAsJsonString();

            if (shouldUpdate)
            {
                await _jobService.UpdateJobAsync(job);
            }
            else
            {
                await _jobService.SaveJobAsync(job);
            }
        }

        public async Task SaveTrigger(TriggerPropertiesViewModel triggerPropertiesViewModel, bool shouldUpdate = false)
        {
            TriggerType triggerType;
            var trigger = _mapper.Map<Trigger>(triggerPropertiesViewModel);

            switch (triggerPropertiesViewModel.Type) 
            {
                case Models.TriggerType.Cron:
                    triggerType = _mapper.Map<TriggerType>(triggerPropertiesViewModel.Cron);
                    break;
                case Models.TriggerType.Simple:
                    triggerType = _mapper.Map<TriggerType>(triggerPropertiesViewModel.Simple);
                    break;
                case Models.TriggerType.Daily:
                    triggerType = _mapper.Map<TriggerType>(triggerPropertiesViewModel.Daily);
                    break;
                case Models.TriggerType.Calendar:
                    triggerType = _mapper.Map<TriggerType>(triggerPropertiesViewModel.Calendar);
                    break;
                default: throw new ArgumentOutOfRangeException();
            }

            triggerType.Id = trigger.Id;
            trigger.JobId = (await _jobService.GetJobAsync(triggerPropertiesViewModel.Job)).Id;

            if (shouldUpdate)
            {
                await _triggerService.UpdateTriggerAsync(trigger, triggerType);
            }
            else
            {
                await _triggerService.SaveTriggerAsync(trigger, triggerType);
            }
        }

        public async Task DeleteJob(string key)
        {
            await _jobService.DeleteJobAsync(key);
        }

        public async Task DeleteTrigger(string key)
        {
            await _triggerService.DeleteTriggerAsync(key);
        }
    }
}
