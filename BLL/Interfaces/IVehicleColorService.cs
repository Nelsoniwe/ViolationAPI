using BLL.Models;

namespace BLL.Interfaces;

public interface IVehicleColorService
{
    Task AddVehicleColor(VehicleColorDTO tag);
    Task<IEnumerable<VehicleColorDTO>> GetAllVehicleColors();
    Task<VehicleColorDTO> GetVehicleColorById(int id);
    Task DeleteVehicleColorById(int id);
    Task<VehicleColorDTO> GetVehicleColorByName(string name);
}