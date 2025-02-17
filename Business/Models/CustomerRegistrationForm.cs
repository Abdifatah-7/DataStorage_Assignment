
namespace Business.Models;

// här registerar man användaren, och man behöver inte id för att man kan inte veta vilken id man har eller får
public class CustomerRegistrationForm
{
    public string CustomerName { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!;
}
