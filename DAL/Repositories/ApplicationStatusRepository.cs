using DAL.Interfaces;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ApplicationStatusRepository : IRepository<ApplicationStatus>
{
    private readonly ViolationContext _db;
    public ApplicationStatusRepository(ViolationContext db)
    {
        _db = db;
    }

    public async Task<IQueryable<ApplicationStatus>> GetAllAsync()
    {
        var applicationStatuses = await _db.ApplicationStatuses.ToListAsync();
        return applicationStatuses.AsQueryable();
    }

    public async Task<ApplicationStatus> GetByIdAsync(int id)
    {
        return await _db.ApplicationStatuses.FindAsync(id);
    }

    public async Task AddAsync(ApplicationStatus entity)
    {
        await _db.ApplicationStatuses.AddAsync(entity);
    }

    public void Update(ApplicationStatus entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteById(int id)
    {
        var applicationStatus = _db.ApplicationStatuses.Find(id);
        if (applicationStatus != null)
        {
            _db.ApplicationStatuses.Remove(applicationStatus);
        }
    }
}