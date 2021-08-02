using AutoMapper;

using TheGarageAPI.Entities;
using TheGarageAPI.Models.UserType;

namespace TheGarageAPI.Helpers.AutoMapperProfile
{
    public class UserTypeProfile : Profile
    {
        //More Information in https://www.thecodebuzz.com/configure-automapper-asp-net-core-profile-map-object/
        //CreateMap<Source, Destination>();
        public UserTypeProfile()
        {
            CreateMap<RegisterRequest, UserType>()
            .ForMember(dest => dest.UserTypeId, src => src.MapFrom(src => src.UserTypeId))
            .ForMember(dest => dest.Description, src => src.MapFrom(src => src.Description))
            ;
            CreateMap<UpdateRequest, UserType>()
            .ForMember(dest => dest.UserTypeId, src => src.MapFrom(src => src.UserTypeId))
            .ForMember(dest => dest.Description, src => src.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, src => src.MapFrom(src => src.Status))
            ;
        }
        
    }
}