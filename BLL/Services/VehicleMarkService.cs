using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class VehicleMarkService : IVehicleMarkService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    public VehicleMarkService(IUnitOfWork uow, IMapper mapper)
    {
        _db = uow;
        _mapper = mapper;
    }

    public async Task<int> AddVehicleMark(VehicleMarkDTO tag)
    {
        if (string.IsNullOrEmpty(tag.Type))
            throw new ViolationException("VehicleMark were empty");
        if ((await _db.VehicleMarkRepository.GetAllAsync()).ToList().FirstOrDefault(x => x.Type == tag.Type) != null)
            throw new ViolationException("VehicleMark already exist");

        await _db.VehicleMarkRepository.AddAsync(_mapper.Map<VehicleMark>(tag));
        await _db.SaveAsync();
        return tag.Id;
    }

    public async Task<IEnumerable<VehicleMarkDTO>> GetAllVehicleMarks()
    {
        return _mapper.Map<IEnumerable<VehicleMarkDTO>>(await _db.VehicleMarkRepository.GetAllAsync());
    }

    public async Task<VehicleMarkDTO> GetVehicleMarkById(int id)
    {
        var tag = await _db.VehicleMarkRepository.GetByIdAsync(id);
        if (tag == null)
            throw new NotFoundException("Mark not found");
        return _mapper.Map<VehicleMarkDTO>(tag);
    }

    public async Task DeleteVehicleMarkById(int id)
    {
        if (await _db.VehicleMarkRepository.GetByIdAsync(id) == null)
            throw new NotFoundException("Mark not found");
        _db.VehicleMarkRepository.DeleteById(id);
        await _db.SaveAsync();
    }

    public async Task<VehicleMarkDTO> GetVehicleMarkByName(string name)
    {
        var tag = await(await _db.VehicleMarkRepository.GetAllAsync()).FirstOrDefaultAsync(x => x.Type == name);
        if (tag == null)
            throw new NotFoundException("Mark not found");
        return _mapper.Map<VehicleMarkDTO>(tag);
    }
}