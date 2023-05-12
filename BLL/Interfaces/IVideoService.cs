using BLL.Models;

namespace BLL.Interfaces;

public interface IVideoService
{
    Task<int> AddVideo(VideoDTO tag);
    Task<IEnumerable<VideoDTO>> GetAllVideos();
    Task<VideoDTO> GetVideoById(int id);
    Task DeleteVideoById(int id);
}