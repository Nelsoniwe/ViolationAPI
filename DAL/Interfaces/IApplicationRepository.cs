using DAL.Interfaces.BaseInterfaces;
using DAL.Models;

namespace DAL.Interfaces;

public interface IApplicationRepository : IRepository<Application>
{
    public Task<IQueryable<Application>> GetByFilter(
        int vehicleMarkId,
        int violationId,
        int vehicleTypeId,
        int vehicleColorId,
        string vehicleNumber,
        int statusId,
        DateTime? publicationTime,
        DateTime? violationTime,
        int userId);
}