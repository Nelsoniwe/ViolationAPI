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

public class PhotoService : IPhotoService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;
    private FileManager _fileManager = new FileManager();

    public PhotoService(IUnitOfWork uow, IMapper mapper)
    {
        _db = uow;
        _mapper = mapper;
    }

    public async Task AddPhoto(PhotoDTO photo)
    {
        if (string.IsNullOrEmpty(photo.FileName))
            throw new ViolationException("FileName were empty");

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(photo.data);
            photo.Hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        //if ((await _db.PhotoRepository.GetAllAsync()).ToList().FirstOrDefault(x => x.Hash == photo.Hash) != null)
        //    throw new ViolationException("File already exist");

        photo.FilePath = await _fileManager.WriteFileAsync(photo.FileName, photo.data);
        await _db.PhotoRepository.AddAsync(_mapper.Map<Photo>(photo));
        await _db.SaveAsync();
    }

    public async Task<IEnumerable<PhotoDTO>> GetAllPhotos()
    {
        return _mapper.Map<IEnumerable<PhotoDTO>>(await _db.PhotoRepository.GetAllAsync());
    }

    public async Task<PhotoDTO> GetPhotoByHash(string hash)
    {
        var photos = await _db.PhotoRepository.GetAllAsync();
        var photo = photos.FirstOrDefault(x => x.Hash == hash);

        if (photo == null)
            return null;
        else
        {
            return _mapper.Map<PhotoDTO>(photo);
        }
    }

    public async Task<PhotoDTO> GetPhotoById(int id)
    {
        var photo = await _db.PhotoRepository.GetByIdAsync(id);
        if (photo == null)
            throw new NotFoundException("Photo not found");

        var photoDto = _mapper.Map<PhotoDTO>(photo);

        photoDto.data = await _fileManager.ReadFileAsync(photoDto.FilePath);
        return photoDto;
    }

    public async Task DeletePhotoById(int id)
    {
        if (await _db.VehicleMarkRepository.GetByIdAsync(id) == null)
            throw new NotFoundException("Photo not found");
        _db.PhotoRepository.DeleteById(id);
        await _db.SaveAsync();
    }
}