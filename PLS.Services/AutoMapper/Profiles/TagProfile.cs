using AutoMapper;
using PLS.Entities.Concrete;
using PLS.Entities.Dtos;

namespace PLS.Services.AutoMapper.Profiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<TagAddDto, Tag>()
            .ForMember(
                dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
        
        CreateMap<TagUpdateDto, Tag>()
            .ForMember(
                dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
    }
}