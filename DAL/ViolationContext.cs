using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DAL;

public class ViolationContext : IdentityDbContext<User, Role, int>
{
    public ViolationContext(DbContextOptions options) : base(options)
    {
    }

    public ViolationContext() : base()
    {

    }

    public ViolationContext(DbContextOptions<ViolationContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Dyploma;User Id=sa;Password=MyStrongPassword_123;TrustServerCertificate = True").EnableSensitiveDataLogging(); ;
    }

    public DbSet<Application> Applications { get; set; }
    public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<VehicleColor> VehicleColors { get; set; }
    public DbSet<VehicleMark> VehicleMarks { get; set; }
    public DbSet<VehicleType> VehicleTypes { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<Violation> Violations { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var adminRole = new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" };
        var userRole = new Role { Id = 2, Name = "User", NormalizedName = "USER" };

        builder.Entity<Role>().HasData(adminRole, userRole);

        var adminData = new User
        {
            Id = 1,
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            UserName = "Admin",
            NormalizedUserName = "ADMIN"
        };

        var adminProfile = new UserProfile
        {
            Id = adminData.Id,
            FirstName = "Admin",
            LastName = "Admin"
        };

        var userData = new User
        {
            Id = 2,
            Email = "user@gmail.com",
            NormalizedEmail = "USER@GMAIL.COM",
            UserName = "User",
            NormalizedUserName = "USER"
        };

        var userProfile = new UserProfile
        {
            Id = userData.Id,
            FirstName = "User",
            LastName = "User"
        };

        var hasher = new PasswordHasher<User>();
        var adminPassword = hasher.HashPassword(adminData, "A1234");

        adminData.PasswordHash = adminPassword;

        builder.Entity<User>().HasData(adminData);
        builder.Entity<UserProfile>().HasData(adminProfile);
        
        builder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int> { RoleId = adminRole.Id, UserId = adminProfile.Id },
            new IdentityUserRole<int> { RoleId = userRole.Id, UserId = adminProfile.Id });

        var userPassword = hasher.HashPassword(userData, "D1234");
        userData.PasswordHash = userPassword;

        builder.Entity<UserProfile>().HasData(userProfile);
        builder.Entity<User>().HasData(userData);
        builder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int> { RoleId = userRole.Id, UserId = userProfile.Id });

        //builder.Entity<Application>().HasData(new Application);
        builder.Entity<ApplicationStatus>().HasData(
            new ApplicationStatus { Id = 1, Status = "Waiting" },
            new ApplicationStatus { Id = 2, Status = "Rejected" },
            new ApplicationStatus { Id = 3, Status = "Approved" });

        //builder.Entity<Photo>().HasData(userProfile);
        builder.Entity<VehicleColor>().HasData(
            new VehicleColor { Id = 1, Type = "Blue" },
            new VehicleColor { Id = 2, Type = "White" });

        builder.Entity<VehicleMark>().HasData(
            new VehicleMark { Id = 1, Type = "Mazda" },
            new VehicleMark { Id = 2, Type = "Opel" });

        builder.Entity<VehicleType>().HasData(
            new VehicleType { Id = 1, Type = "Automobile" },
            new VehicleType { Id = 2, Type = "Motorcycle" });

        //builder.Entity<Video>().HasData(userProfile);
        builder.Entity<Violation>().HasData(
            new Violation { Id = 1, Type = "Parked in wrong place" },
            new Violation { Id = 2, Type = "Unfastened seat belt" });

        builder.Entity<Photo>().HasData(new Photo { Id = 1, FileName = "dummy", FilePath = "dummy", Hash = new[] {Byte.MinValue} });
        builder.Entity<Video>().HasData(new Video { Id = 1, FileName = "dummy", FilePath = "dummy", Hash = new[] { Byte.MinValue } });

        builder.Entity<Application>().HasData(
            new Application{
                Id = 1,
                UserId = 1,
                ViolationId = 1, 
                StatusId = 1, 
                Geolocation = "dummy", 
                VehicleTypeId = 1,
                VehicleColorId = 1, 
                VehicleMarkId = 1,
                PublicationTime = DateTime.Today,
                ViolationTime = DateTime.Today,
                VehicleNumber = "dummy",
                PhotoId = 1,
                VideoId = 1
            });

        builder.Entity<UserProfile>().HasOne(x => x.AppUser).WithOne(x => x.UserProfile).HasForeignKey<UserProfile>(x => x.Id);
    }
}
