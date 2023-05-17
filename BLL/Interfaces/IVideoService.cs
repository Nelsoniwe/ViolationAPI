using BLL.Models;

namespace BLL.Interfaces;

public interface IVideoService
{
    Task AddVideo(VideoDTO tag);
    Task<IEnumerable<VideoDTO>> GetAllVideos();
    Task<VideoDTO> GetVideoById(int id);
    Task DeleteVideoById(int id);
}