using DAL.Interfaces.BaseInterfaces;
using System.Reflection.Metadata;
using DAL.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

/// <summary>
/// Repository interface to work with Applications
/// </summary>
public class ApplicationRepository : IApplicationRepository
{
    private readonly ViolationContext _db;
    public ApplicationRepository(ViolationContext db)
    {
        _db = db;
    }

    public async Task<IQueryable<Application>> GetAllAsync()
    {
        var applications = await _db.Applications.ToListAsync();
        return applications.AsQueryable();
    }

    public async Task<IQueryable<Application>> GetByFilter(
     int vehicleMarkId,
     int violationId,
     int vehicleTypeId,
     int vehicleColorId,
     string vehicleNumber,
     int statusId,
     DateTime? publicationTime,
     DateTime? violationTime,
     int userId)
    {
        IQueryable<Application> applications = null;

        if (vehicleMarkId != 0)
            applications = _db.Applications.Where(x=>x.VehicleMarkId == vehicleMarkId);
        if (violationId != 0)
            applications = applications == null ? _db.Applications.Where(x => x.ViolationId == violationId) : applications.Where(x => x.ViolationId == violationId);
        if (vehicleTypeId != 0)
            applications = applications == null ? _db.Applications.Where(x => x.VehicleTypeId == vehicleTypeId) : applications.Where(x => x.VehicleTypeId == vehicleTypeId);
        if (vehicleColorId != 0)
            applications = applications == null ? _db.Applications.Where(x => x.VehicleColorId == vehicleColorId) : applications.Where(x => x.VehicleColorId == vehicleColorId);
        if (!String.IsNullOrEmpty(vehicleNumber))
            applications = applications == null ? _db.Applications.Where(x => x.VehicleNumber == vehicleNumber) : applications.Where(x => x.VehicleNumber == vehicleNumber);
        if (statusId != 0)
            applications = applications == null ? _db.Applications.Where(x => x.StatusId == statusId) : applications.Where(x => x.StatusId == statusId);
        if (publicationTime != default(DateTime))
            applications = applications == null ? _db.Applications.Where(x => x.PublicationTime == publicationTime) : applications.Where(x => x.PublicationTime == publicationTime);
        if (violationTime != default(DateTime))
            applications = applications == null ? _db.Applications.Where(x => x.ViolationTime == violationTime) : applications.Where(x => x.ViolationTime == violationTime);
        if (userId != 0)
            applications = applications == null ? _db.Applications.Where(x => x.UserId == userId) : applications.Where(x => x.UserId == userId);

        return applications;
    }

    public async Task<Application> GetByIdAsync(int id)
    {
        return await _db.Applications.FindAsync(id);
    }

    public async Task AddAsync(Application entity)
    {
        await _db.Applications.AddAsync(entity);
    }

    public void Update(Application entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteById(int id)
    {
        var application = _db.Applications.Find(id);
        if (application != null)
        {
            _db.Applications.Remove(application);
        }
    }
}