using AutoMapper;
using Sat.Recruitment.Api.Controllers.Dtos;
using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Helpers;

namespace Sat.Recruitment.Api.Controllers.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserRequest, User>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType.ToString()))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserTypeConverter.UserTypeFromString(src.UserType)));
        }

    }
}

