using AutoMapper;
using PLS.Entities.Concrete;
using PLS.Entities.Dtos;

namespace PLS.Services.AutoMapper.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserAddDto, User>()
            .ForMember(
                dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(x => 3));
        
        CreateMap<UserUpdateDto, User>()
            .ForMember(
                dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
    }
}