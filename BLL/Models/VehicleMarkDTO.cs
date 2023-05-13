namespace BLL.Models;

public class VehicleMarkDTO
{
    public int Id { get; set; }

    public string Type { get; set; }

    public virtual ICollection<int> ApplicationIds { get; set; }
}