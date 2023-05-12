using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class PhotoService : IPhotoService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    public PhotoService(IUnitOfWork uow, IMapper mapper)
    {
        _db = uow;
        _mapper = mapper;
    }

    public async Task<int> AddPhoto(PhotoDTO photo)
    {
        if (string.IsNullOrEmpty(photo.FileName))
            throw new ViolationException("FileName were empty");
        if (string.IsNullOrEmpty(photo.FilePath))
            throw new ViolationException("FilePath were empty");
        if (await(await _db.PhotoRepository.GetAllAsync()).FirstOrDefaultAsync(x => x.Hash == photo.Hash) != null)
            throw new ViolationException("File already exist");

        await _db.PhotoRepository.AddAsync(_mapper.Map<Photo>(photo));
        await _db.SaveAsync();
        return photo.Id;
    }

    public async Task<IEnumerable<PhotoDTO>> GetAllPhotos()
    {
        return _mapper.Map<IEnumerable<PhotoDTO>>(await _db.PhotoRepository.GetAllAsync());
    }

    public async Task<PhotoDTO> GetPhotoById(int id)
    {
        var photo = await _db.PhotoRepository.GetByIdAsync(id);
        if (photo == null)
            throw new NotFoundException("Photo not found");
        return _mapper.Map<PhotoDTO>(photo);
    }

    public async Task DeletePhotoById(int id)
    {
        if (await _db.VehicleMarkRepository.GetByIdAsync(id) == null)
            throw new NotFoundException("Photo not found");
        _db.PhotoRepository.DeleteById(id);
        await _db.SaveAsync();
    }
}