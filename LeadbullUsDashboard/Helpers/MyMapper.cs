using Api.DTOS;
using AutoMapper;
using Core;

namespace Api.Helpers
{
    public class MyMapper:Profile
    {
        public MyMapper()
        {
            CreateMap<Lead, LeadDto>();
            CreateMap<ServiceProfile, ShowServiceProfileDto>();
            CreateMap<AddServiceProfileDto, ServiceProfile>();
            CreateMap<UserTask, UserTaskDto>();
        }
    }
}
