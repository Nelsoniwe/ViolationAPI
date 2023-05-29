namespace API.Models;

/// <summary>
/// Filter model for application search
/// </summary>
public class ApplicationFilter
{
    public int VehicleMarkId { get; set; }
    public int ViolationId { get; set; }
    public int VehicleTypeId { get; set; }
    public int VehicleColorId { get; set; }
    public string? VehicleNumber { get; set; }
    public int StatusId { get; set; }
    public DateTime PublicationTime { get; set; }
    public DateTime ViolationTime { get; set; }
    public string? UserEmail { get; set; }
}