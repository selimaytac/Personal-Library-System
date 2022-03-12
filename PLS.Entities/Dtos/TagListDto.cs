using PLS.Entities.Concrete;
using PLS.Shared.Entities.Abstract;

namespace PLS.Entities.Dtos;

public class TagListDto : DtoGetBase
{
    public ICollection<Tag> Tags { get; set; }
}