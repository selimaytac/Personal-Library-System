using PLS.Entities.Concrete;
using PLS.Shared.Entities.Abstract;

namespace PLS.Entities.Dtos;

public class TagDto : DtoGetBase
{
    public Tag Tag { get; set; }
}