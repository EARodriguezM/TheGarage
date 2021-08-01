using AutoMapper;

using TheGarageAPI.Entities;
using TheGarageAPI.Models.Vehicle;

namespace TheGarageAPI.Helpers.AutoMapperProfile
{
    public class VehicleProfile : Profile
    {
        //More Information in https://www.thecodebuzz.com/configure-automapper-asp-net-core-profile-map-object/
        //CreateMap<Source, Destination>();
        public VehicleProfile()
        {
            CreateMap<RegisterRequest, Vehicle>()
            .ForMember(dest => dest.VehiclePlate, src => src.MapFrom(src => src.VehiclePlate))
            .ForMember(dest => dest.PlateCity, src => src.MapFrom(src => src.PlateCity))
            .ForMember(dest => dest.VehicleTypeId, src => src.MapFrom(src => src.VehicleTypeId))
            .ForMember(dest => dest.Brand, src => src.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Line, src => src.MapFrom(src => src.Line))
            .ForMember(dest => dest.Model, src => src.MapFrom(src => src.Model))
            .ForMember(dest => dest.Color, src => src.MapFrom(src => src.Color))
            .ForMember(dest => dest.Description, src => src.MapFrom(src => src.Description))
            ;

            CreateMap<UpdateRequest, Vehicle>()
            .ForMember(dest => dest.VehiclePlate, src => src.MapFrom(src => src.VehiclePlate))
            .ForMember(dest => dest.PlateCity, src => src.MapFrom(src => src.PlateCity))
            .ForMember(dest => dest.VehicleTypeId, src => src.MapFrom(src => src.VehicleTypeId))
            .ForMember(dest => dest.Brand, src => src.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Line, src => src.MapFrom(src => src.Line))
            .ForMember(dest => dest.Model, src => src.MapFrom(src => src.Model))
            .ForMember(dest => dest.Color, src => src.MapFrom(src => src.Color))
            .ForMember(dest => dest.Description, src => src.MapFrom(src => src.Description))
            ;
        }
    }
}