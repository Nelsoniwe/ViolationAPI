using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class ViolationService : IViolationService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    public ViolationService(IUnitOfWork uow, IMapper mapper)
    {
        _db = uow;
        _mapper = mapper;
    }
    public async Task AddViolation(ViolationDTO tag)
    {
        if (string.IsNullOrEmpty(tag.Type))
            throw new ViolationException("ViolationType were empty");
        if ((await _db.ViolationRepository.GetAllAsync()).ToList().FirstOrDefault(x => x.Type == tag.Type) != null)
            throw new ViolationException("ViolationType already exist");

        await _db.ViolationRepository.AddAsync(_mapper.Map<Violation>(tag));
        await _db.SaveAsync();
    }

    public async Task<IEnumerable<ViolationDTO>> GetAllViolations()
    {
        return _mapper.Map<IEnumerable<ViolationDTO>>(await _db.ViolationRepository.GetAllAsync());
    }

    public async Task<ViolationDTO> GetViolationById(int id)
    {
        var tag = await _db.ViolationRepository.GetByIdAsync(id);
        if (tag == null)
            throw new NotFoundException("Violation not found");
        return _mapper.Map<ViolationDTO>(tag);
    }

    public async Task DeletViolationById(int id)
    {
        if (await _db.ViolationRepository.GetByIdAsync(id) == null)
            throw new NotFoundException("Violation not found");
        _db.ViolationRepository.DeleteById(id);
        await _db.SaveAsync();
    }

    public async Task<ViolationDTO> GetViolationByName(string name)
    {
        var tag = (await _db.ViolationRepository.GetAllAsync()).ToList().FirstOrDefault(x => x.Type == name);
        if (tag == null)
            throw new NotFoundException("Violation not found");
        return _mapper.Map<ViolationDTO>(tag);
    }
}