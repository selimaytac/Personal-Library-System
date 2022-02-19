using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PLS.Entities.Dtos;

public class SourceUpdateDto
{
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [Range(1, int.MaxValue, ErrorMessage = "{0} cannot be zero.")]
    public int Id { get; set; }
    
    [DisplayName("Source Name")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [MaxLength(100, ErrorMessage = "{0} cannot exceed {1} characters.")]
    [MinLength(3, ErrorMessage = "{0} must be more than {1} characters.")]
    public string SourceName { get; set; }
    
    [DisplayName("Source Link")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [MaxLength(500, ErrorMessage = "{0} cannot exceed {1} characters.")]
    [MinLength(3, ErrorMessage = "{0} must be more than {1} characters.")]
    public string Link { get; set; }

    [DisplayName("Language")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [MaxLength(50, ErrorMessage = "{0} cannot exceed {1} characters.")]
    [MinLength(2, ErrorMessage = "{0} must be more than {1} characters.")]
    public string Language { get; set; }
    
    [DisplayName("UserId")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [Range(1, int.MaxValue, ErrorMessage = "{0} cannot be zero.")]
    public int UserId { get; set; }

    [DisplayName("CategoryId")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [Range(1, int.MaxValue, ErrorMessage = "{0} cannot be zero.")]
    public int CategoryId { get; set; }
    
    [DisplayName("Is Active?")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    public bool IsActive { get; set; }
    
    public int[]? TagIds { get; set; }
    
    public string? Note { get; set; } = null;
}