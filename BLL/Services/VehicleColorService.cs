using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class VehicleColorService : IVehicleColorService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    public VehicleColorService(IUnitOfWork uow, IMapper mapper)
    {
        _db = uow;
        _mapper = mapper;
    }

    public async Task AddVehicleColor(VehicleColorDTO tag)
    {
        if (string.IsNullOrEmpty(tag.Type))
            throw new ViolationException("Color were empty");
        if ((await _db.VehicleColorRepository.GetAllAsync()).ToList().FirstOrDefault(x => x.Type == tag.Type) != null)
            throw new ViolationException("Color already exist");

        await _db.VehicleColorRepository.AddAsync(_mapper.Map<VehicleColor>(tag));
        await _db.SaveAsync();
    }

    public async Task<IEnumerable<VehicleColorDTO>> GetAllVehicleColors()
    {
        return _mapper.Map<IEnumerable<VehicleColorDTO>>(await _db.VehicleColorRepository.GetAllAsync());
    }

    public async Task<VehicleColorDTO> GetVehicleColorById(int id)
    {
        var tag = await _db.VehicleColorRepository.GetByIdAsync(id);
        if (tag == null)
            throw new NotFoundException("Color not found");
        return _mapper.Map<VehicleColorDTO>(tag);
    }

    public async Task DeleteVehicleColorById(int id)
    {
        if (await _db.VehicleColorRepository.GetByIdAsync(id) == null)
            throw new NotFoundException("Color not found");
        _db.VehicleColorRepository.DeleteById(id);
        await _db.SaveAsync();
    }

    public async Task<VehicleColorDTO> GetVehicleColorByName(string name)
    {
        var tag = (await _db.VehicleColorRepository.GetAllAsync()).ToList().FirstOrDefault(x => x.Type == name);
        if (tag == null)
            throw new NotFoundException("Color not found");
        return _mapper.Map<VehicleColorDTO>(tag);
    }
}