using AutoMapper;
using Azure;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class VehicleTypeService : IVehicleTypeService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    public VehicleTypeService(IUnitOfWork uow, IMapper mapper)
    {
        _db = uow;
        _mapper = mapper;
    }
    public async Task<int> AddVehicleType(VehicleTypeDTO tag)
    {
        if (string.IsNullOrEmpty(tag.Type))
            throw new ViolationException("VehicleType were empty");
        if (await(await _db.VehicleTypeRepository.GetAllAsync()).FirstOrDefaultAsync(x => x.Type == tag.Type) != null)
            throw new ViolationException("VehicleType already exist");

        await _db.VehicleTypeRepository.AddAsync(_mapper.Map<VehicleType>(tag));
        await _db.SaveAsync();
        return tag.Id;
    }

    public async Task<IEnumerable<VehicleTypeDTO>> GetAllVehicleTypes()
    {
        return _mapper.Map<IEnumerable<VehicleTypeDTO>>(await _db.VehicleTypeRepository.GetAllAsync());
    }

    public async Task<VehicleTypeDTO> GetVehicleTypeById(int id)
    {
        var tag = await _db.VehicleTypeRepository.GetByIdAsync(id);
        if (tag == null)
            throw new NotFoundException("Type not found");
        return _mapper.Map<VehicleTypeDTO>(tag);
    }

    public async Task DeleteVehicleTypeById(int id)
    {
        if (await _db.VehicleTypeRepository.GetByIdAsync(id) == null)
            throw new NotFoundException("Type not found");
        _db.VehicleTypeRepository.DeleteById(id);
        await _db.SaveAsync();
    }

    public async Task<VehicleTypeDTO> GetVehicleTypeByName(string name)
    {
        var tag = await(await _db.VehicleTypeRepository.GetAllAsync()).FirstOrDefaultAsync(x => x.Type == name);
        if (tag == null)
            throw new NotFoundException("Type not found");
        return _mapper.Map<VehicleTypeDTO>(tag);
    }
}