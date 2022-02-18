using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PLS.Entities.Concrete;
using PLS.Shared.Entities.Abstract;

namespace PLS.Entities.Dtos;

public class UserAddDto
{
    [DisplayName("User Name")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [MaxLength(50, ErrorMessage = "{0} cannot exceed {1} characters.")]
    [MinLength(5, ErrorMessage = "{0} must be more than {1} characters.")]
    public string UserName { get; set; }

    [DisplayName("Email")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [MaxLength(70, ErrorMessage = "{0} cannot exceed {1} characters.")]
    [MinLength(8, ErrorMessage = "{0} must be more than {1} characters.")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a correct e-mail address.")]
    public string Email { get; set; }

    [DisplayName("Password")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [MaxLength(40, ErrorMessage = "{0} cannot exceed {1} characters.")]
    [MinLength(6, ErrorMessage = "{0} must be more than {1} characters.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DisplayName("Is Active?")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    public bool IsActive { get; set; }
    
    public string? Note { get; set; } = null;
}