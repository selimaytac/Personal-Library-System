using System.Text.Json.Serialization;
using PLS.Shared.Entities.Abstract;
using PLS.Shared.Entities.Concrete;

namespace PLS.Entities.Concrete;

public class Role : EntityBase, IEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    [JsonIgnore]
    public ICollection<User> Users { get; set; }
}