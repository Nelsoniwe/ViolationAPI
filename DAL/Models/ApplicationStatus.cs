using System.ComponentModel.DataAnnotations;
using DAL.Models.Base;

namespace DAL.Models;

public class ApplicationStatus : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Status { get; set; }
}