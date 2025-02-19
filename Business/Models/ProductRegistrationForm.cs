using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class ProductRegistrationForm
{
    [Required]
    public string ProductName { get; set; } = null!;
    public decimal ProductPrice { get; set; }
}
