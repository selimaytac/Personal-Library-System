using PLS.Shared.Entities.Abstract;

namespace PLS.Entities.Dtos;

public class SourceListDto : DtoGetBase
{
    public ICollection<SourceDto> Sources { get; set; }
}