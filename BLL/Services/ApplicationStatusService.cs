using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class ApplicationStatusService : IApplicationStatusService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    public ApplicationStatusService(IUnitOfWork uow, IMapper mapper)
    {
        _db = uow;
        _mapper = mapper;
    }

    public async Task<int> AddApplicationStatus(ApplicationStatusDTO tag)
    {
        if (string.IsNullOrEmpty(tag.Status))
            throw new ViolationException("ApplicationStatus were empty");
        if (await(await _db.ApplicationStatusRepository.GetAllAsync()).FirstOrDefaultAsync(x => x.Status == tag.Status) != null)
            throw new ViolationException("ApplicationStatus already exist");

        await _db.ApplicationStatusRepository.AddAsync(_mapper.Map<ApplicationStatus>(tag));
        await _db.SaveAsync();
        return tag.Id;
    }

    public async Task<IEnumerable<ApplicationStatusDTO>> GetAllApplicationStatuses()
    {
        return _mapper.Map<IEnumerable<ApplicationStatusDTO>>(await _db.ApplicationStatusRepository.GetAllAsync());
    }

    public async Task<ApplicationStatusDTO> GetApplicationStatusById(int id)
    {
        var tag = await _db.ApplicationStatusRepository.GetByIdAsync(id);
        if (tag == null)
            throw new NotFoundException("ApplicationStatus not found");
        return _mapper.Map<ApplicationStatusDTO>(tag);
    }

    public async Task DeleteApplicationStatusById(int id)
    {
        if (await _db.ApplicationStatusRepository.GetByIdAsync(id) == null)
            throw new NotFoundException("ApplicationStatus not found");
        _db.ApplicationStatusRepository.DeleteById(id);
        await _db.SaveAsync();
    }

    public async Task<ApplicationStatusDTO> GetApplicationStatusByName(string name)
    {
        var tag = await(await _db.ApplicationStatusRepository.GetAllAsync()).FirstOrDefaultAsync(x => x.Status == name);
        if (tag == null)
            throw new NotFoundException("ApplicationStatus not found");
        return _mapper.Map<ApplicationStatusDTO>(tag);
    }
}