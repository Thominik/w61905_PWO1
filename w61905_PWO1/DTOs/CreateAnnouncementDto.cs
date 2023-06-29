using System.ComponentModel.DataAnnotations;

namespace w61905_PWO1.DTOs;

public class CreateAnnouncementDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Category { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    [Range(1, Double.PositiveInfinity)]
    public long Price { get; set; }
    [Required]
    public string Location { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public IFormFile File { get; set; }
}