using System;
using AutoMapper;
using CustomJobScheduler.DbService.DbModels;
using Quartzmin.Models;

namespace Quartzmin.MappingProfile
{
    public class TriggerProfile : Profile
    {
        public TriggerProfile()
        {
            CreateMap<TriggerPropertiesViewModel, Trigger>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => Guid.NewGuid()))
                .ForMember(dest => dest.Job, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(scr => (short) scr.Type))
                .ForMember(dest => dest.TriggerKey, opt => opt.MapFrom(scr => scr.TriggerGroup.GetKey(scr.TriggerName)));
        }
    }
}
