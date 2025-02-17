namespace Business.Models;

// Här hämtar vi upp kunds info
public class Customer
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!;
}
