using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PLS.Entities.Dtos;

public class TagAddDto
{
    [DisplayName("Tag Name")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [MaxLength(100, ErrorMessage = "{0} cannot exceed {1} characters.")]
    [MinLength(1, ErrorMessage = "{0} must be more than {1} characters.")]
    public string TagName { get; set; }
    
    [DisplayName("Tag Description")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [MaxLength(500, ErrorMessage = "{0} cannot exceed {1} characters.")]
    [MinLength(3, ErrorMessage = "{0} must be more than {1} characters.")]
    public string TagDescription { get; set; }

    [DisplayName("Is Active?")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    public bool IsActive { get; set; }
    
    public int[]? TagIds { get; set; }
    
    public string? Note { get; set; }
}