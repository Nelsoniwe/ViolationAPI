using DAL.Interfaces;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PhotoRepository : IRepository<Photo>
{
    private readonly ViolationContext _db;

    public PhotoRepository(ViolationContext db)
    {
        _db = db;
    }

    public async Task<IQueryable<Photo>> GetAllAsync()
    {
        var photos = await _db.Photos.ToListAsync();
        return photos.AsQueryable();
    }

    public async Task<Photo> GetByIdAsync(int id)
    {
        return await _db.Photos.FindAsync(id);
    }

    public async Task AddAsync(Photo entity)
    {
        await _db.Photos.AddAsync(entity);
    }

    public void Update(Photo entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteById(int id)
    {
        var photo = _db.Photos.Find(id);
        if (photo != null)
        {
            _db.Photos.Remove(photo);
        }
    }
}