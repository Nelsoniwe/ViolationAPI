using BLL.Models;

namespace BLL.Interfaces;

public interface IApplicationService
{
    Task AddApplication(ApplicationDTO tag);
    Task<IEnumerable<ApplicationDTO>> GetAllApplications();
    Task<ApplicationDTO> GetApplicationById(int id);
    Task DeleteApplicationById(int id);
    Task UpdateApplicationAsync(ApplicationDTO item);

    Task<IEnumerable<ApplicationDTO>> GetAllUserApplications(int userId);
    Task<IEnumerable<ApplicationDTO>> GetAllApplicationsByStatusId(int statusId);

    Task<IEnumerable<ApplicationDTO>> GetApplicationsByFilter(ApplicationFilterDTO filter);

    Task<IEnumerable<ApplicationDTO>> GetAllVehicleApplications(string vehicleNumber);
}