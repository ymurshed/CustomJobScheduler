using System;
using AutoMapper;
using Quartzmin.Models;
using TriggerType = CustomJobScheduler.DbService.DbModels.TriggerType;

namespace Quartzmin.MappingProfile
{
    public class DailyTriggerProfile : Profile
    {
        public DailyTriggerProfile()
        {
            CreateMap<DailyTriggerViewModel, TriggerType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => Guid.NewGuid()))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(scr => scr.StartTime.Value.Ticks))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(scr => scr.EndTime.Value.Ticks))
                .ForMember(dest => dest.RepeatUnit, opt => opt.MapFrom(scr => (short) scr.RepeatUnit))
                .ForMember(dest => dest.Friday, opt => opt.MapFrom(scr => scr.DaysOfWeek.Friday))
                .ForMember(dest => dest.Saturday, opt => opt.MapFrom(scr => scr.DaysOfWeek.Saturday))
                .ForMember(dest => dest.Sunday, opt => opt.MapFrom(scr => scr.DaysOfWeek.Sunday))
                .ForMember(dest => dest.Monday, opt => opt.MapFrom(scr => scr.DaysOfWeek.Monday))
                .ForMember(dest => dest.Tuesday, opt => opt.MapFrom(scr => scr.DaysOfWeek.Tuesday))
                .ForMember(dest => dest.Wednessday, opt => opt.MapFrom(scr => scr.DaysOfWeek.Wednesday))
                .ForMember(dest => dest.Thursday, opt => opt.MapFrom(scr => scr.DaysOfWeek.Thursday))
                .ForMember(dest => dest.AreOnlyWeekendEnabled, opt => opt.MapFrom(scr => scr.DaysOfWeek.AreOnlyWeekendEnabled))
                .ForMember(dest => dest.AreOnlyWeekdaysEnabled, opt => opt.MapFrom(scr => scr.DaysOfWeek.AreOnlyWeekdaysEnabled));
        }
    }
}
