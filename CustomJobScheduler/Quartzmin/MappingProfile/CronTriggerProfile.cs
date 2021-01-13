using System;
using AutoMapper;
using Quartzmin.Models;
using TriggerType = CustomJobScheduler.DbService.DbModels.TriggerType;

namespace Quartzmin.MappingProfile
{
    public class CronTriggerProfile : Profile
    {
        public CronTriggerProfile()
        {
            CreateMap<CronTriggerViewModel, TriggerType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => Guid.NewGuid()));
        }
    }
}
