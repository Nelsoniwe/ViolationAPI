using DAL.Interfaces;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class VehicleTypeRepository : IVehicleTypeRepository
{
    private readonly ViolationContext _db;
    public VehicleTypeRepository(ViolationContext db)
    {
        _db = db;
    }

    public async Task<IQueryable<VehicleType>> GetAllAsync()
    {
        var vehicleTypes = await _db.VehicleTypes.ToListAsync();
        return vehicleTypes.AsQueryable();
    }

    public async Task<VehicleType> GetByIdAsync(int id)
    {
        return await _db.VehicleTypes.FindAsync(id);
    }

    public async Task AddAsync(VehicleType entity)
    {
        await _db.VehicleTypes.AddAsync(entity);
    }

    public void Update(VehicleType entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteById(int id)
    {
        var vehicleType = _db.VehicleTypes.Find(id);
        if (vehicleType != null)
        {
            _db.VehicleTypes.Remove(vehicleType);
        }
    }
}