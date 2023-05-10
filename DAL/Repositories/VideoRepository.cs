using DAL.Interfaces;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class VideoRepository : IVideoRepository
{
    private readonly ViolationContext _db;
    public VideoRepository(ViolationContext db)
    {
        _db = db;
    }

    public async Task<IQueryable<Video>> GetAllAsync()
    {
        var videos = await _db.Videos.ToListAsync();
        return videos.AsQueryable();
    }

    public async Task<Video> GetByIdAsync(int id)
    {
        return await _db.Videos.FindAsync(id);
    }

    public async Task AddAsync(Video entity)
    {
        await _db.Videos.AddAsync(entity);
    }

    public void Update(Video entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteById(int id)
    {
        var video = _db.Videos.Find(id);
        if (video != null)
        {
            _db.Videos.Remove(video);
        }
    }
}