using System.Text.Json.Serialization;
using PLS.Shared.Entities.Abstract;
using PLS.Shared.Entities.Concrete;

namespace PLS.Entities.Concrete;

public class Tag : EntityBase, IEntity
{
    public string TagName { get; set; }
    public string? TagDescription { get; set; }
    [JsonIgnore]
    public ICollection<Source> Sources { get; set; }
}