using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DAL;

public class ViolationContext : IdentityDbContext<User, Role, int>
{
    //public ViolationContext(DbContextOptions options) : base(options)
    //{
    //}

    //public ViolationContext() : base()
    //{

    //}

    public ViolationContext(DbContextOptions<ViolationContext> options) : base(options)
    {
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
            new VehicleColor { Id = 2, Type = "White" },
            new VehicleColor { Id = 3, Type = "Red" },
            new VehicleColor { Id = 4, Type = "Black" },
            new VehicleColor { Id = 5, Type = "Silver" },
            new VehicleColor { Id = 6, Type = "Gray" },
            new VehicleColor { Id = 7, Type = "Green" },
            new VehicleColor { Id = 8, Type = "Yellow" },
            new VehicleColor { Id = 9, Type = "Orange" },
            new VehicleColor { Id = 10, Type = "Brown" },
            new VehicleColor { Id = 11, Type = "Purple" },
            new VehicleColor { Id = 12, Type = "Pink" },
            new VehicleColor { Id = 13, Type = "Gold" },
            new VehicleColor { Id = 14, Type = "Beige" },
            new VehicleColor { Id = 15, Type = "Teal" },
            new VehicleColor { Id = 16, Type = "Navy" },
            new VehicleColor { Id = 17, Type = "Magenta" },
            new VehicleColor { Id = 18, Type = "Turquoise" },
            new VehicleColor { Id = 19, Type = "Lime" },
            new VehicleColor { Id = 20, Type = "Cyan" });

        builder.Entity<VehicleMark>().HasData(
            new VehicleMark { Id = 1, Type = "Mazda" },
            new VehicleMark { Id = 2, Type = "Opel" },
            new VehicleMark { Id = 3, Type = "Toyota" },
            new VehicleMark { Id = 4, Type = "Honda" },
            new VehicleMark { Id = 5, Type = "Ford" },
            new VehicleMark { Id = 6, Type = "Chevrolet" },
            new VehicleMark { Id = 7, Type = "Volkswagen" },
            new VehicleMark { Id = 8, Type = "Nissan" },
            new VehicleMark { Id = 9, Type = "Hyundai" },
            new VehicleMark { Id = 10, Type = "BMW" },
            new VehicleMark { Id = 11, Type = "Mercedes-Benz" },
            new VehicleMark { Id = 12, Type = "Audi" },
            new VehicleMark { Id = 13, Type = "Kia" },
            new VehicleMark { Id = 14, Type = "Subaru" },
            new VehicleMark { Id = 15, Type = "Lexus" },
            new VehicleMark { Id = 16, Type = "Mitsubishi" },
            new VehicleMark { Id = 17, Type = "Suzuki" },
            new VehicleMark { Id = 18, Type = "Chrysler" },
            new VehicleMark { Id = 19, Type = "Volvo" },
            new VehicleMark { Id = 20, Type = "Jaguar" },
            new VehicleMark { Id = 21, Type = "Land Rover" },
            new VehicleMark { Id = 22, Type = "Porsche" },
            new VehicleMark { Id = 23, Type = "Maserati" },
            new VehicleMark { Id = 24, Type = "Tesla" },
            new VehicleMark { Id = 25, Type = "Ferrari" },
            new VehicleMark { Id = 26, Type = "Lamborghini" },
            new VehicleMark { Id = 27, Type = "Bugatti" },
            new VehicleMark { Id = 28, Type = "McLaren" },
            new VehicleMark { Id = 29, Type = "Aston Martin" },
            new VehicleMark { Id = 30, Type = "Alfa Romeo" },
            new VehicleMark { Id = 31, Type = "Bentley" },
            new VehicleMark { Id = 32, Type = "Rolls-Royce" },
            new VehicleMark { Id = 33, Type = "Fiat" },
            new VehicleMark { Id = 34, Type = "Jeep" },
            new VehicleMark { Id = 35, Type = "Dodge" },
            new VehicleMark { Id = 36, Type = "Peugeot" },
            new VehicleMark { Id = 37, Type = "Renault" },
            new VehicleMark { Id = 38, Type = "Citroën" },
            new VehicleMark { Id = 39, Type = "Seat" },
            new VehicleMark { Id = 40, Type = "Škoda" },
            new VehicleMark { Id = 41, Type = "Fiat" },
            new VehicleMark { Id = 42, Type = "Mini" },
            new VehicleMark { Id = 43, Type = "Lada" },
            new VehicleMark { Id = 44, Type = "Saab" },
            new VehicleMark { Id = 45, Type = "Pontiac" },
            new VehicleMark { Id = 46, Type = "Hummer" },
            new VehicleMark { Id = 47, Type = "Acura" },
            new VehicleMark { Id = 48, Type = "Infiniti" },
            new VehicleMark { Id = 49, Type = "Cadillac" },
            new VehicleMark { Id = 50, Type = "Buick" });

        builder.Entity<VehicleType>().HasData(
            new VehicleType { Id = 1, Type = "Automobile" },
            new VehicleType { Id = 2, Type = "Motorcycle" });

        //builder.Entity<Video>().HasData(userProfile);
        builder.Entity<Violation>().HasData(
            new Violation { Id = 1, Type = "Parked in wrong place" },
            new Violation { Id = 2, Type = "Unfastened seat belt" },
            new Violation { Id = 3, Type = "Speeding" },
            new Violation { Id = 4, Type = "Running a red light" },
            new Violation { Id = 5, Type = "Driving under the influence" },
            new Violation { Id = 6, Type = "Using a mobile phone while driving" },
            new Violation { Id = 7, Type = "Driving without a valid license" },
            new Violation { Id = 8, Type = "Failure to yield right of way" },
            new Violation { Id = 9, Type = "Illegal parking" },
            new Violation { Id = 10, Type = "Reckless driving" },
            new Violation { Id = 11, Type = "Tailgating" },
            new Violation { Id = 12, Type = "Failure to use turn signals" },
            new Violation { Id = 13, Type = "Improper passing" },
            new Violation { Id = 14, Type = "Driving with expired registration" },
            new Violation { Id = 15, Type = "Driving without insurance" },
            new Violation { Id = 16, Type = "Failure to stop for a pedestrian" },
            new Violation { Id = 17, Type = "Illegal U-turn" },
            new Violation { Id = 18, Type = "Driving on the wrong side of the road" },
            new Violation { Id = 19, Type = "Driving with tinted windows" },
            new Violation { Id = 20, Type = "Failure to use headlights" },
            new Violation { Id = 21, Type = "Driving with a suspended license" },
            new Violation { Id = 22, Type = "Failure to obey traffic signs" },
            new Violation { Id = 23, Type = "Driving without proper lights" },
            new Violation { Id = 24, Type = "Illegal lane change" },
            new Violation { Id = 25, Type = "Driving with a cracked windshield" },
            new Violation { Id = 26, Type = "Driving with excessive noise" },
            new Violation { Id = 27, Type = "Failure to use a child safety seat" },
            new Violation { Id = 28, Type = "Failure to yield to emergency vehicles" },
            new Violation { Id = 29, Type = "Driving the wrong way on a one-way street" },
            new Violation { Id = 30, Type = "Failure to dim headlights" });

        builder.Entity<Photo>().HasData(new Photo { Id = 1, FileName = "dummy", FilePath = "dummy", Hash = "dummy" });
        builder.Entity<Video>().HasData(new Video { Id = 1, FileName = "dummy", FilePath = "dummy", Hash = "dummy" });

        builder.Entity<Application>().HasData(
            new Application
            {
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
                VideoId = 1,
                AdminComment = "",
                UserComment = ""
            });

        builder.Entity<UserProfile>().HasOne(x => x.AppUser).WithOne(x => x.UserProfile).HasForeignKey<UserProfile>(x => x.Id);
    }
}
