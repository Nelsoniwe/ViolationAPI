using BLL.Models;

namespace BLL.Interfaces;

public interface IVehicleColorService
{
    Task<int> AddVehicleColor(VehicleColorDTO tag);
    Task<IEnumerable<VehicleColorDTO>> GetAllVehicleColors();
    Task<VehicleColorDTO> GetVehicleColorById(int id);
    Task DeleteVehicleColorById(int id);
    Task<VehicleColorDTO> GetVehicleColorByName(string name);
    Task UpdateVehicleColorAsync(VehicleColorDTO item);
}