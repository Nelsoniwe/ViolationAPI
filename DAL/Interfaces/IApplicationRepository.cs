using DAL.Interfaces.BaseInterfaces;
using System.Reflection.Metadata;
using DAL.Models;

namespace DAL.Interfaces;

/// <summary>
/// Repository interface to work with Applications
/// </summary>
public interface IApplicationRepository : IRepository<Application>, IRepositoryExpanded<Application> { }