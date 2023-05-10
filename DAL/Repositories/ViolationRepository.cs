using DAL.Interfaces;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ViolationRepository : IViolationRepository
{
    private readonly ViolationContext _db;
    public ViolationRepository(ViolationContext db)
    {
        _db = db;
    }

    public async Task<IQueryable<Violation>> GetAllAsync()
    {
        var violations = await _db.Violations.ToListAsync();
        return violations.AsQueryable();
    }

    public async Task<Violation> GetByIdAsync(int id)
    {
        return await _db.Violations.FindAsync(id);
    }

    public async Task AddAsync(Violation entity)
    {
        await _db.Violations.AddAsync(entity);
    }

    public void Update(Violation entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteById(int id)
    {
        var violation = _db.Violations.Find(id);
        if (violation != null)
        {
            _db.Violations.Remove(violation);
        }
    }
}