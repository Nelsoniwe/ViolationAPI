using BLL.Models;

namespace BLL.Interfaces;

public interface IVehicleMarkService
{
    Task AddVehicleMark(VehicleMarkDTO tag);
    Task<IEnumerable<VehicleMarkDTO>> GetAllVehicleMarks();
    Task<VehicleMarkDTO> GetVehicleMarkById(int id);
    Task DeleteVehicleMarkById(int id);
    Task<VehicleMarkDTO> GetVehicleMarkByName(string name);
}