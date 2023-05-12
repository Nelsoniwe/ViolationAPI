using DAL.Interfaces;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using DAL.Repositories;

namespace DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly ViolationContext _db;

    public UnitOfWork(ViolationContext db, IUserProfileRepository userProfileRepository, IUserRepository userRepository,
        IRoleRepository roleRepository, IApplicationRepository applicationRepository, IRepository<VehicleType> vehicleTypeRepository,
        IRepository<VehicleMark> vehicleMarkRepository, IRepository<VehicleColor> vehicleColorRepository, IRepository<Violation> violationRepository,
        IRepository<Photo> photoRepository, IRepository<Video> videoRepository, IRepository<ApplicationStatus> applicationStatusRepository)
    {
        _db = db;
        UserProfileRepository = userProfileRepository;
        UserRepository = userRepository;
        RoleRepository = roleRepository;
        ApplicationRepository = applicationRepository;
        VehicleTypeRepository = vehicleTypeRepository;
        VehicleMarkRepository = vehicleMarkRepository;
        VehicleColorRepository = vehicleColorRepository;
        ViolationRepository = violationRepository;
        PhotoRepository = photoRepository;
        VideoRepository = videoRepository;
        ApplicationStatusRepository = applicationStatusRepository;
    }

    public IUserProfileRepository UserProfileRepository { get; }
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public IApplicationRepository ApplicationRepository { get; }
    public IRepository<ApplicationStatus> ApplicationStatusRepository { get; }
    public IRepository<VehicleType> VehicleTypeRepository { get; }
    public IRepository<VehicleMark> VehicleMarkRepository { get; }
    public IRepository<VehicleColor> VehicleColorRepository { get; }
    public IRepository<Violation> ViolationRepository { get; }
    public IRepository<Photo> PhotoRepository { get; }
    public IRepository<Video> VideoRepository { get; }

    public async Task<int> SaveAsync()
    {
        return await _db.SaveChangesAsync();
    }
}