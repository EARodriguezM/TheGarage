using AutoMapper;

using TheGarageAPI.Entities;
using TheGarageAPI.Models.Slot;

namespace TheGarageAPI.Helpers.AutoMapperProfile
{
    public class SlotProfile : Profile
    {
        //More Information in https://www.thecodebuzz.com/configure-automapper-asp-net-core-profile-map-object/
        //CreateMap<Source, Destination>();

        public SlotProfile()
        {
            CreateMap<RegisterRequest, Slot>()
            .ForMember(dest => dest.SlotStatusId, src => src.MapFrom(src => src.SlotStatusId))
            .ForMember(dest => dest.Floor, src => src.MapFrom(src => src.Floor))
            .ForMember(dest => dest.Location, src => src.MapFrom(src => src.Location))
            .ForMember(dest => dest.SlotStatusId, src => src.MapFrom(src => src.SlotStatusId))
            ;

            CreateMap<UpdateRequest, Slot>()
            .ForMember(dest => dest.SlotStatusId, src => src.MapFrom(src => src.SlotStatusId))
            .ForMember(dest => dest.Floor, src => src.MapFrom(src => src.Floor))
            .ForMember(dest => dest.Location, src => src.MapFrom(src => src.Location))
            .ForMember(dest => dest.SlotStatusId, src => src.MapFrom(src => src.SlotStatusId))
            ;
        }
    }
}