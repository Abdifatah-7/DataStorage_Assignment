namespace Business.Models;

public class Project
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ProjectNumber { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; } 

    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public int StatusTypeId { get; set; }
}
