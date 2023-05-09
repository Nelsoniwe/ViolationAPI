using DAL.Models;

namespace DAL.Interfaces.BaseInterfaces;

/// <summary>
/// UOF Interface
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// User profile repository
    /// </summary>
    IUserProfileRepository UserProfileRepository { get; }

    /// <summary>
    /// User repository
    /// </summary>
    IUserRepository UserRepository { get; }

    /// <summary>
    /// Role repository
    /// </summary>
    IRoleRepository RoleRepository { get; }

    /// <summary>
    /// Application repository
    /// </summary>
    IApplicationRepository ApplicationRepository { get; }

    /// <summary>
    /// VehicleType repository
    /// </summary>
    IRepository<VehicleType> VehicleTypeRepository { get; }

    /// <summary>
    /// VehicleMark repository
    /// </summary>
    IRepository<VehicleMark> VehicleMarkRepository { get; }

    /// <summary>
    /// VehicleColor repository
    /// </summary>
    IRepository<VehicleColor> VehicleColorRepository { get; }

    /// <summary>
    /// Violation repository
    /// </summary>
    IRepository<Violation> ViolationRepository { get; }

    /// <summary>
    /// Photo repository
    /// </summary>
    IRepository<Photo> PhotoRepository { get; }

    /// <summary>
    /// Video repository
    /// </summary>
    IRepository<Video> VideoRepository { get; }

    /// <summary>
    /// Asynchronously save db
    /// </summary>
    Task<int> SaveAsync();
}