using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class ProjectRegistrationForm
{
    [Required]
    public string ProjectName { get; set; } = null!;

    [Required]
    public decimal TotalPrice { get; set; }

    public string? Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int StatusId { get; set; }
}
