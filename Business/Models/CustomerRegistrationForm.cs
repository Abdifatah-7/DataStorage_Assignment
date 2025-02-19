
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

// här registerar man användaren, och man behöver inte id för att man kan inte veta vilken id man har eller får
public class CustomerRegistrationForm
{
    [Required]
    public string CustomerName { get; set; } = null!;
    [Required]
    public string CustomerEmail { get; set; } = null!;
}
