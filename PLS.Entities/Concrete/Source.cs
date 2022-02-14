using PLS.Shared.Entities.Abstract;
using PLS.Shared.Entities.Concrete;

namespace PLS.Entities.Concrete;

public class Source : EntityBase, IEntity
{
    public string SourceName { get; set; }
    public string Link { get; set; }
    public string Language { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}