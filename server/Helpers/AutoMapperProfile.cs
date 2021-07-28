using AutoMapper;

using TheGarageAPI.Entities;
using TheGarageAPI.Models.DataUser;

namespace TheGarageAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        //More Information in https://www.thecodebuzz.com/configure-automapper-asp-net-core-profile-map-object/
        //CreateMap<Source, Destination>();

        public AutoMapperProfile()
        {

            CreateMap<AuthenticateRequest, DataUser>()
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.Email))
            ;
            CreateMap<RegisterRequest, DataUser>()
                .ForMember(dest => dest.DataUserId, src => src.MapFrom(src => src.DataUserId))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.SecondName, src => src.MapFrom(src => src.SecondName))
                .ForMember(dest => dest.FirstSurname, src => src.MapFrom(src => src.FirstSurname))
                .ForMember(dest => dest.SecondSurname, src => src.MapFrom(src => src.SecondSurname))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.Email))
                .ForMember(dest => dest.Mobile, src => src.MapFrom(src => src.Mobile))
                .ForMember(dest => dest.ProfilePicture, src => src.MapFrom(src => src.ProfilePicture))
                .ForMember(dest => dest.UserTypeId, src => src.MapFrom(src => src.UserTypeId))
            ;
            CreateMap<UpdateRequest, DataUser>()
                .ForMember(dest => dest.DataUserId, src => src.MapFrom(src => src.DataUserId))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.SecondName, src => src.MapFrom(src => src.SecondName))
                .ForMember(dest => dest.FirstSurname, src => src.MapFrom(src => src.FirstSurname))
                .ForMember(dest => dest.SecondSurname, src => src.MapFrom(src => src.SecondSurname))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.Email))
                .ForMember(dest => dest.Mobile, src => src.MapFrom(src => src.Mobile))
                .ForMember(dest => dest.ProfilePicture, src => src.MapFrom(src => src.ProfilePicture))
                .ForMember(dest => dest.UserTypeId, src => src.MapFrom(src => src.UserTypeId))
            ;
            CreateMap<DataUser, AuthenticateResponse>();
        }
        
    }
}