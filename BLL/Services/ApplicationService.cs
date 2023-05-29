using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using Application = DAL.Models.Application;

namespace BLL.Services;

public class ApplicationService : IApplicationService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    public ApplicationService(IUnitOfWork uow, IMapper mapper)
    {
        _db = uow;
        _mapper = mapper;
    }

    public async Task AddApplication(ApplicationDTO application)
    {
        if (string.IsNullOrEmpty(application.VehicleNumber))
            throw new ViolationException("VehicleNumber were empty");
        if (string.IsNullOrEmpty(application.Geolocation))
            throw new ViolationException("Geolocation were empty");
        if (application.VideoId == null && application.PhotoId == null)
            throw new ViolationException("File were empty");

        await _db.ApplicationRepository.AddAsync(_mapper.Map<Application>(application));
        await _db.SaveAsync();
    }

    public async Task<IEnumerable<ApplicationDTO>> GetAllApplications()
    {
        return _mapper.Map<IEnumerable<ApplicationDTO>>(await _db.ApplicationRepository.GetAllAsync());
    }

    public async Task<ApplicationDTO> GetApplicationById(int id)
    {
        var application = await _db.ApplicationRepository.GetByIdAsync(id);
        if (application == null)
            throw new NotFoundException("Application not found");
        return _mapper.Map<ApplicationDTO>(application);
    }

    public async Task DeleteApplicationById(int id)
    {
        if (await _db.ApplicationRepository.GetByIdAsync(id) == null)
            throw new NotFoundException("Application not found");
        _db.ApplicationRepository.DeleteById(id);
        await _db.SaveAsync();
    }

    public async Task UpdateApplicationAsync(ApplicationDTO application)
    {
        if (string.IsNullOrEmpty(application.VehicleNumber))
            throw new ViolationException("VehicleNumber were empty");
        if (string.IsNullOrEmpty(application.Geolocation))
            throw new ViolationException("Geolocation were empty");
        if (application.VideoId == null && application.PhotoId == null)
            throw new ViolationException("File were empty");

        _db.ApplicationRepository.Update(_mapper.Map<Application>(application));
        await _db.SaveAsync();
    }

    public async Task<IEnumerable<ApplicationDTO>> GetAllUserApplications(int userId)
    {
        var user = await _db.UserRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException("User not found");

        var userApplications = (await _db.ApplicationRepository.GetAllAsync()).Where(x => x.UserId == userId);

        if (!userApplications.Any())
            throw new NotFoundException("Applications not found");

        return _mapper.Map<IEnumerable<ApplicationDTO>>(userApplications);
    }

    public async Task<IEnumerable<ApplicationDTO>> GetAllApplicationsByStatusId(int statusId)
    {
        return _mapper.Map<IEnumerable<ApplicationDTO>>((await _db.ApplicationRepository.GetAllAsync()).Where(x=>x.StatusId == statusId));
    }
    public async Task<IEnumerable<ApplicationDTO>> GetApplicationsByFilter(ApplicationFilterDTO filter)
    {
        return _mapper.Map<IEnumerable<ApplicationDTO>>(await _db.ApplicationRepository.GetByFilter(filter.VehicleMarkId,
            filter.ViolationId,
            filter.VehicleTypeId,
            filter. VehicleColorId,
            filter.VehicleNumber,
            filter.StatusId,
            filter.PublicationTime,
            filter.ViolationTime));
    }

    public async Task<IEnumerable<ApplicationDTO>> GetAllVehicleApplications(string vehicleNumber)
    {
        var applications = _mapper.Map<IEnumerable<ApplicationDTO>>((await _db.ApplicationRepository.GetAllAsync()).Where(x => x.VehicleNumber == vehicleNumber));

        if(applications == null)
            throw new NotFoundException("Applications not found");

        return applications;
    }
}