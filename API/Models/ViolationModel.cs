namespace API.Models;

public class ViolationModel
{
    public int Id { get; set; }

    public string Type { get; set; }

    public virtual ICollection<int> ApplicationIds { get; set; }
}