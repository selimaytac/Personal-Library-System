using AutoMapper;
using PLS.Entities.Concrete;
using PLS.Entities.Dtos;

namespace PLS.Services.AutoMapper.Profiles;

public class SourceProfile : Profile
{
    public SourceProfile()
    {
        CreateMap<SourceAddDto, Source>()
            .ForMember(
                dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
        
        CreateMap<SourceUpdateDto, Source>()
            .ForMember(
                dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
    }
}