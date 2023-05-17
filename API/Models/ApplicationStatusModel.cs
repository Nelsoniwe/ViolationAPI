namespace API.Models;

public class ApplicationStatusModel
{
    public int Id { get; set; }

    public string Status { get; set; }

    public ICollection<int> ApplicationIds { get; set; }
}