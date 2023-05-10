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