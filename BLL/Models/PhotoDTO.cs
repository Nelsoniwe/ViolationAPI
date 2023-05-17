using System.ComponentModel.DataAnnotations;

namespace BLL.Models;

public class PhotoDTO
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string Hash { get; set; }
}