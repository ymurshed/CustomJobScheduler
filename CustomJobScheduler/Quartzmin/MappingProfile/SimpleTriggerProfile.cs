using System;
using AutoMapper;
using Quartzmin.Models;
using TriggerType = CustomJobScheduler.DbService.DbModels.TriggerType;

namespace Quartzmin.MappingProfile
{
    public class SimpleTriggerProfile : Profile
    {
        public SimpleTriggerProfile()
        {
            CreateMap<SimpleTriggerViewModel, TriggerType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => Guid.NewGuid()))
                .ForMember(dest => dest.RepeatUnit, opt => opt.MapFrom(scr => (short) scr.RepeatUnit));
        }
    }
}
