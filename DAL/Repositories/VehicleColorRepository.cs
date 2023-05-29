using DAL.Interfaces;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class VehicleColorRepository : IRepository<VehicleColor>
{
    private readonly ViolationContext _db;
    public VehicleColorRepository(ViolationContext db)
    {
        _db = db;
    }

    public async Task<IQueryable<VehicleColor>> GetAllAsync()
    {
        var vehicleColors = await _db.VehicleColors.ToListAsync();
        return vehicleColors.AsQueryable();
    }

    public async Task<VehicleColor> GetByIdAsync(int id)
    {
        return await _db.VehicleColors.FindAsync(id);
    }

    public async Task AddAsync(VehicleColor entity)
    {
        await _db.VehicleColors.AddAsync(entity);
    }

    public void Update(VehicleColor entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteById(int id)
    {
        var vehicleColors = _db.VehicleColors.Find(id);
        if (vehicleColors != null)
        {
            _db.VehicleColors.Remove(vehicleColors);
        }
    }
}