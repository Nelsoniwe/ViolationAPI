using System.Security.Cryptography;
using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using DAL.Managers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BLL.Services;

public class VideoService : IVideoService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;
    private FileManager _fileManager = new FileManager();

    public VideoService(IUnitOfWork uow, IMapper mapper)
    {
        _db = uow;
        _mapper = mapper;
    }
    public async Task AddVideo(VideoDTO video)
    {
        if (string.IsNullOrEmpty(video.FileName))
            throw new ViolationException("VideoName were empty");

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(video.data);
            video.Hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        //if ((await _db.VideoRepository.GetAllAsync()).ToList().FirstOrDefault(x => x.Hash == video.Hash) != null)
        //    throw new ViolationException("Video already exist");

        video.FilePath = await _fileManager.WriteFileAsync(video.FileName, video.data);
        await _db.VideoRepository.AddAsync(_mapper.Map<Video>(video));
        await _db.SaveAsync();
    }

    public async Task<IEnumerable<VideoDTO>> GetAllVideos()
    {
        return _mapper.Map<IEnumerable<VideoDTO>>(await _db.VideoRepository.GetAllAsync());
    }

    public async Task<VideoDTO> GetVideoByHash(string hash)
    {
        var videos = await _db.VideoRepository.GetAllAsync();
        var video = videos.FirstOrDefault(x => x.Hash == hash);

        if (video == null)
            return null;
        else
        {
            return _mapper.Map<VideoDTO>(video);
        }
    }

    public async Task<VideoDTO> GetVideoById(int id)
    {
        var video = await _db.VideoRepository.GetByIdAsync(id);
        if (video == null)
            throw new NotFoundException("Video not found");

        var videoDto = _mapper.Map<VideoDTO>(video);
        videoDto.data = await _fileManager.ReadFileAsync(videoDto.FilePath);
        return videoDto;
    }

    public async Task DeleteVideoById(int id)
    {
        if (await _db.VehicleMarkRepository.GetByIdAsync(id) == null)
            throw new NotFoundException("Video not found");
        _db.VideoRepository.DeleteById(id);
        await _db.SaveAsync();
    }
}