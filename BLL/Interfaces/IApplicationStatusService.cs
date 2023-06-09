﻿using BLL.Models;

namespace BLL.Interfaces;

public interface IApplicationStatusService
{
    Task AddApplicationStatus(ApplicationStatusDTO tag);
    Task<IEnumerable<ApplicationStatusDTO>> GetAllApplicationStatuses();
    Task<ApplicationStatusDTO> GetApplicationStatusById(int id);
    Task DeleteApplicationStatusById(int id);
    Task<ApplicationStatusDTO> GetApplicationStatusByName(string name);
}