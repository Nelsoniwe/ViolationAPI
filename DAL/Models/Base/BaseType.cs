using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Base;

public class BaseType : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Type { get; set; }
    public ICollection<Application> Applications { get; set; }

    public BaseType()
    {
        Applications ??= new HashSet<Application>();
    }
}