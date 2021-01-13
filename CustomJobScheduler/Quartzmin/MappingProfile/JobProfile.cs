using System;
using AutoMapper;
using CustomJobScheduler.DbService.DbModels;
using Quartzmin.Models;

namespace Quartzmin.MappingProfile
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<JobPropertiesViewModel, Job>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => Guid.NewGuid()))
                .ForMember(dest => dest.JobKey, opt => opt.MapFrom(scr => scr.Group.GetKey(scr.JobName)));
        }
    }
}
