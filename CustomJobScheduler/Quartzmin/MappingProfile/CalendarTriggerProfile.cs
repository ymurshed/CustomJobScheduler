using System;
using AutoMapper;
using Quartzmin.Models;
using TriggerType = CustomJobScheduler.DbService.DbModels.TriggerType;

namespace Quartzmin.MappingProfile
{
    public class CalendarTriggerProfile : Profile
    {
        public CalendarTriggerProfile()
        {
            CreateMap<CalendarTriggerViewModel, TriggerType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => Guid.NewGuid()))
                .ForMember(dest => dest.RepeatUnit, opt => opt.MapFrom(scr => (short) scr.RepeatUnit));
        }
    }
}
