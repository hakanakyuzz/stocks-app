using System.ComponentModel.DataAnnotations;

namespace WebApp.Dtos.Comment;

public class CreateCommentDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long.")]
    [MaxLength(50, ErrorMessage = "Title must be no more than 50 characters long.")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MinLength(5, ErrorMessage = "Content must be at least 5 characters long.")]
    [MaxLength(150, ErrorMessage = "Content must be no more than 50 characters long.")]
    public string Content { get; set; } = string.Empty;
}