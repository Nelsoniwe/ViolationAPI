using DAL.Interfaces;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class VehicleMarkRepository : IRepository<VehicleMark>
{
    private readonly ViolationContext _db;
    public VehicleMarkRepository(ViolationContext db)
    {
        _db = db;
    }

    public async Task<IQueryable<VehicleMark>> GetAllAsync()
    {
        var vehicleColors = await _db.VehicleMarks.ToListAsync();
        return vehicleColors.AsQueryable();
    }

    public async Task<VehicleMark> GetByIdAsync(int id)
    {
        return await _db.VehicleMarks.FindAsync(id);
    }

    public async Task AddAsync(VehicleMark entity)
    {
        await _db.VehicleMarks.AddAsync(entity);
    }

    public void Update(VehicleMark entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteById(int id)
    {
        var vehicleMarks = _db.VehicleMarks.Find(id);
        if (vehicleMarks != null)
        {
            _db.VehicleMarks.Remove(vehicleMarks);
        }
    }
}