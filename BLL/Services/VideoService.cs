using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class VideoService : IVideoService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    public VideoService(IUnitOfWork uow, IMapper mapper)
    {
        _db = uow;
        _mapper = mapper;
    }
    public async Task<int> AddVideo(VideoDTO video)
    {
        if (string.IsNullOrEmpty(video.FileName))
            throw new ViolationException("VideoName were empty");
        if (string.IsNullOrEmpty(video.FilePath))
            throw new ViolationException("VideoPath were empty");
        if (await(await _db.VideoRepository.GetAllAsync()).FirstOrDefaultAsync(x => x.Hash == video.Hash) != null)
            throw new ViolationException("Video already exist");

        await _db.VideoRepository.AddAsync(_mapper.Map<Video>(video));
        await _db.SaveAsync();
        return video.Id;
    }

    public async Task<IEnumerable<VideoDTO>> GetAllVideos()
    {
        return _mapper.Map<IEnumerable<VideoDTO>>(await _db.VideoRepository.GetAllAsync());
    }

    public async Task<VideoDTO> GetVideoById(int id)
    {
        var video = await _db.VideoRepository.GetByIdAsync(id);
        if (video == null)
            throw new NotFoundException("Video not found");
        return _mapper.Map<VideoDTO>(video);
    }

    public async Task DeleteVideoById(int id)
    {
        if (await _db.VehicleMarkRepository.GetByIdAsync(id) == null)
            throw new NotFoundException("Video not found");
        _db.VideoRepository.DeleteById(id);
        await _db.SaveAsync();
    }
}