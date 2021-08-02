using AutoMapper;

using TheGarageAPI.Entities;
using TheGarageAPI.Models.SlotStatus;

namespace TheGarageAPI.Helpers.AutoMapperProfile
{
    public class SlotStatusProfile : Profile
    {
        //More Information in https://www.thecodebuzz.com/configure-automapper-asp-net-core-profile-map-object/
        //CreateMap<Source, Destination>();
        public SlotStatusProfile()
        {
            CreateMap<RegisterRequest, SlotStatus>()
            .ForMember(dest => dest.SlotStatusId, src => src.MapFrom(src => src.SlotStatusId))
            .ForMember(dest => dest.Description, src => src.MapFrom(src => src.Description))
            ;
            CreateMap<UpdateRequest, SlotStatus>()
            .ForMember(dest => dest.SlotStatusId, src => src.MapFrom(src => src.SlotStatusId))
            .ForMember(dest => dest.Description, src => src.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, src => src.MapFrom(src => src.Status))
            ;
        }
    }
}