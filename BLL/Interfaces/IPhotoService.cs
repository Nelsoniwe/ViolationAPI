using BLL.Models;

namespace BLL.Interfaces;

public interface IPhotoService
{
    Task AddPhoto(PhotoDTO tag);
    Task<IEnumerable<PhotoDTO>> GetAllPhotos();
    Task<PhotoDTO> GetPhotoByHash(string hash);
    Task<PhotoDTO> GetPhotoById(int id);
    Task DeletePhotoById(int id);
}