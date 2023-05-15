namespace API.Models;

public class VehicleMarkModel
{
    public int Id { get; set; }

    public string Type { get; set; }

    public ICollection<int> ApplicationIds { get; set; }
}