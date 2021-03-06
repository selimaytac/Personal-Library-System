using PLS.Shared.Entities.Abstract;
using PLS.Shared.Entities.Concrete;

namespace PLS.Entities.Concrete;

public class Category : EntityBase, IEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Source> Sources { get; set; }
}