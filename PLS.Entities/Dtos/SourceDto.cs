using PLS.Entities.Concrete;
using PLS.Shared.Entities.Abstract;

namespace PLS.Entities.Dtos;

public class SourceDto : DtoGetBase
{
    public Source Source { get; set; }
}