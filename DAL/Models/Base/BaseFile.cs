using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Base;

public class BaseFile : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string FileName { get; set; }

    [Required]
    [MaxLength(100)]
    public string FilePath { get; set; }

    [Required]
    public string Hash { get; set; }
}