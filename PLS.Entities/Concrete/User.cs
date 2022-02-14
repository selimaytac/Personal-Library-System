using System.Text.Json.Serialization;
using PLS.Shared.Entities.Abstract;
using PLS.Shared.Entities.Concrete;

namespace PLS.Entities.Concrete;

public class User : EntityBase, IEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public byte[] PasswordHash { get; set; }
    [JsonIgnore]
    public byte[] PasswordSalt { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public ICollection<Source> Sources { get; set; }
}