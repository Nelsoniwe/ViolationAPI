using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork uow, IMapper mapper)
    {
        _db = uow;
        _mapper = mapper;
    }
    public IEnumerable<UserDTO> GetAll()
    {
        return _mapper.Map<IEnumerable<UserDTO>>(_db.UserRepository.GetAll());
    }

    public async Task<UserDTO> GetById(int id)
    {
        return _mapper.Map<UserDTO>(await _db.UserRepository.GetByIdAsync(id));
    }

    public async Task<UserDTO> GetByEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new ViolationException("Email were empty");

        return _mapper.Map<UserDTO>(await _db.UserRepository.GetByEmailAsync(email));
    }

    public async Task<bool> UserCheckPassword(int userId, string password)
    {
        var user = await _db.UserRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException("User not found");

        return await _db.UserRepository.UserCheckPasswordAsync(user, password);
    }

    public async Task CreateUserAndAddToRole(UserDTO user, string password, string role)
    {
        if (string.IsNullOrEmpty(password)
            || string.IsNullOrEmpty(user.FirstName)
            || string.IsNullOrEmpty(user.LastName)
            || string.IsNullOrEmpty(user.UserName)
            || string.IsNullOrEmpty(user.Email))
            throw new ViolationException("Some of parameters were empty");

        if (user.FirstName.Contains(" ") || user.LastName.Contains(" "))
            throw new ViolationException("FirstName or LastName were contain whitespaces");

        if (password.Contains(" ") || user.Email.Contains(" ") || user.UserName.Contains(" "))
            throw new ViolationException("Email, password or username were contain whitespaces");

        if (await _db.UserRepository.GetByEmailAsync(user.Email) != null)
            throw new ViolationException("Such email already exist");

        if (await _db.UserRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == user.UserName) != null)
            throw new ViolationException("Such username already exist");

        if (string.IsNullOrEmpty(role))
            throw new ViolationException("Role were empty");

        if (!await _db.RoleRepository.RoleExistsAsync(role))
            throw new NotFoundException("Role not found");

        var result = await _db.UserRepository.UserAddAsync(_mapper.Map<User>(user), password);

        if (!result.Succeeded)
            throw new ViolationException(
                "Invalid password. Must be at least 6 characters long, have 1 uppercase & 1 lowercase character and special characters");

        await _db.UserRepository.AddToRoleAsync(_mapper.Map<User>(await _db.UserRepository.GetByEmailAsync(user.Email)), role);
        await _db.SaveAsync();
    }

    public async Task UpdateUser(UserDTO user)
    {
        if (string.IsNullOrEmpty(user.FirstName)
            || string.IsNullOrEmpty(user.LastName)
            || string.IsNullOrEmpty(user.UserName)
            || string.IsNullOrEmpty(user.Email))
            throw new ViolationException("Some of parameters were empty");

        if (user.FirstName.Contains(" ") || user.LastName.Contains(" "))
            throw new ViolationException("FirstName or LastName were contain whitespaces");


        if (user.Email.Contains(" ") || user.UserName.Contains(" "))
            throw new ViolationException("Email, password or username were contain whitespaces");

        var userFromDb = await _db.UserRepository.GetByIdWithDetailsAsync(user.Id);

        if (userFromDb == null)
            throw new NotFoundException("User not found");

        var userFromDbByEmail = await _db.UserRepository.GetByEmailAsync(user.Email);

        if (userFromDbByEmail != null && userFromDbByEmail.Id != userFromDb.Id)
            throw new ViolationException("Such email already exist");

        var userFromDbByUserName = await _db.UserRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == user.UserName);

        if (userFromDbByUserName != null && userFromDbByUserName.Id != userFromDb.Id)
            throw new ViolationException("Such username already exist");

        await _db.UserRepository.UpdateAsync(_mapper.Map<User>(user));
        await _db.SaveAsync();
    }

    public async Task DeleteById(int id)
    {
        if (await _db.UserRepository.GetByIdAsync(id) == null)
            throw new NotFoundException("User not found");
        await _db.UserRepository.DeleteByIdAsync(id);
    }

    public async Task<UserDTO> GetByIdWithDetails(int id)
    {
        return _mapper.Map<UserDTO>(await _db.UserRepository.GetByIdWithDetailsAsync(id));
    }

    public async Task<IdentityResult> AddToRole(int userId, string role)
    {
        var user = await _db.UserRepository.GetByIdWithDetailsAsync(userId);
        if (user == null)
            throw new NotFoundException("User not found");
        if (string.IsNullOrEmpty(role))
            throw new ViolationException("Role were empty");
        if (!await _db.RoleRepository.RoleExistsAsync(role))
            throw new NotFoundException("Role not found");
        var result = await _db.UserRepository.AddToRoleAsync(user, role);
        await _db.SaveAsync();
        return result;
    }

    public async Task<IEnumerable<string>> UserGetRoles(int userId)
    {
        var user = await _db.UserRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException("User not found");
        return await _db.UserRepository.UserGetRolesAsync(user);
    }

    public async Task<IdentityResult> UserChangePassword(int userId, string currentPassword, string newPassword)
    {
        if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword))
            throw new ViolationException("current or new password were empty");

        if (newPassword.Contains(" "))
            throw new ViolationException("Password were contain whitespaces");

        var user = await _db.UserRepository.GetByIdAsync(userId);

        if (user == null)
            throw new NotFoundException("User not found");

        var result = await _db.UserRepository.UserChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded)
        {
            throw new ViolationException("your current password was not correct or new password was invalid. " +
                                    "New password Must be at least 6 characters long, have 1 uppercase & 1 lowercase character and special characters");
        }
        return result;
    }
}