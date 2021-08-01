using AutoMapper;

using TheGarageAPI.Entities;
using TheGarageAPI.Models.VehicleType;

namespace TheGarageAPI.Helpers.AutoMapperProfile
{
    public class VechicleTypeProfile : Profile
    {
        //More Information in https://www.thecodebuzz.com/configure-automapper-asp-net-core-profile-map-object/
        //CreateMap<Source, Destination>();
        public VechicleTypeProfile()
        {
            CreateMap<RegisterRequest, VehicleType>()
                .ForMember(dest => dest.VehicleTypeId, src => src.MapFrom(src => src.VehicleTypeId))
                .ForMember(dest => dest.Description, src => src.MapFrom(src => src.Description))
            ;
            CreateMap<UpdateRequest, VehicleType>()
                .ForMember(dest => dest.VehicleTypeId, src => src.MapFrom(src => src.VehicleTypeId))
                .ForMember(dest => dest.Description, src => src.MapFrom(src => src.Description))
            ;
        }
    }
}