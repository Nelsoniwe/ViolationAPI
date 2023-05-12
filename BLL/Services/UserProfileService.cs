using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces.BaseInterfaces;

namespace BLL.Services;

public class UserProfileService : IUserProfileService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    public UserProfileService(IUnitOfWork uow, IMapper mapper)
    {
        _db = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserProfileDTO>> GetAllUserProfilesWithDetails()
    {
        return _mapper.Map<IEnumerable<UserProfileDTO>>(await _db.UserProfileRepository.GetAllWithDetailsAsync());
    }

    public async Task<UserProfileDTO> GetUserProfileByIdWithDetails(int id)
    {
        var user = await _db.UserProfileRepository.GetByIdWithDetailsAsync(id);
        if (user == null)
            throw new NotFoundException("User not found");
        return _mapper.Map<UserProfileDTO>(user);
    }

    public async Task<UserProfileDTO> GetByUserName(string name)
    {
        var user = _db.UserRepository.GetAll().FirstOrDefault(x => x.UserName == name);
        if (user == null)
            throw new NotFoundException("User not found");
        return _mapper.Map<UserProfileDTO>(await _db.UserProfileRepository.GetByIdWithDetailsAsync(user.Id));
    }

    public async Task<UserProfileDTO> GetUserProfileById(int id)
    {
        var user = await _db.UserProfileRepository.GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException("User not found");
        return _mapper.Map<UserProfileDTO>(user);
    }
}