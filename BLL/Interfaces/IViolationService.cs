﻿using BLL.Models;

namespace BLL.Interfaces;

public interface IViolationService
{
    Task AddViolation(ViolationDTO tag);
    Task<IEnumerable<ViolationDTO>> GetAllViolations();
    Task<ViolationDTO> GetViolationById(int id);
    Task DeletViolationById(int id);
    Task<ViolationDTO> GetViolationByName(string name);
}