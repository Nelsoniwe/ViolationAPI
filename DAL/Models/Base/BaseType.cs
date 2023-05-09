using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Base;

public class BaseType : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Type { get; set; }
}