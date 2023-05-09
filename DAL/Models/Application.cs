using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DAL.Models.Base;

namespace DAL.Models;

public class Application : BaseEntity
{
    [ForeignKey("User")]
    public int UserId { get; set; }
    public UserProfile User { get; set; }

    [ForeignKey("VehicleMark")]
    public int VehicleMarkId { get; set; }
    public VehicleMark VehicleMark { get; set; }

    [ForeignKey("Violation")]
    public int ViolationId { get; set; }
    public Violation Violation { get; set; }

    [ForeignKey("VehicleType")]
    public int VehicleTypeId { get; set; }
    public VehicleType VehicleType { get; set; }

    [ForeignKey("VehicleColor")]
    public int VehicleColorId { get; set; }
    public VehicleColor VehicleColor { get; set; }

    [Required]
    [MaxLength(50)]
    public string VehicleNumber { get; set; }

    [ForeignKey("ApplicationStatus")]
    public int StatusId { get; set; }
    public ApplicationStatus ApplicationStatus { get; set; }

    [Required]
    [MaxLength(50)]
    public string Geolocation { get; set; }

    [Required]
    public DateTime PublicationTime { get; set; }

    [Required]
    public TimeSpan ViolationTime { get; set; }

    [ForeignKey("Photo")]
    public int? PhotoId { get; set; }
    public virtual Photo Photo { get; set; }

    [ForeignKey("Video")]
    public int? VideoId { get; set; }
    public virtual Video Video { get; set; }
}