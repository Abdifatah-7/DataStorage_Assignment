using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class StatusTypeRegistration
{
    [Required]
    public string StatusName { get; set; } = null!;
}

