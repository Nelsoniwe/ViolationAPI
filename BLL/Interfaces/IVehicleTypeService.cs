﻿using BLL.Models;

namespace BLL.Interfaces;

public interface IVehicleTypeService
{
    Task<int> AddVehicleType(VehicleTypeDTO tag);
    Task<IEnumerable<VehicleTypeDTO>> GetAllVehicleTypes();
    Task<VehicleTypeDTO> GetVehicleTypeById(int id);
    Task DeleteVehicleTypeById(int id);
    Task<VehicleTypeDTO> GetVehicleTypeByName(string name);
    Task UpdateVehicleTypeAsync(VehicleTypeDTO item);
}