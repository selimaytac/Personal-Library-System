using PLS.Entities.Concrete;
using PLS.Shared.Entities.Abstract;

namespace PLS.Entities.Dtos;

public class SourceListDto : DtoGetBase
{
    public ICollection<Source> Sources { get; set; }
}