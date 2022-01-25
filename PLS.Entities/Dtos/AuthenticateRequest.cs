using System.ComponentModel.DataAnnotations;

namespace PLS.Entities.Dtos;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}