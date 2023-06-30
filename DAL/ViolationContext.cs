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
            new ApplicationStatus { Id = 1, Status = "Очікується" },
            new ApplicationStatus { Id = 2, Status = "Скасовано" },
            new ApplicationStatus { Id = 3, Status = "Підтверджено" });

        //builder.Entity<Photo>().HasData(userProfile);
        builder.Entity<VehicleColor>().HasData(
            new VehicleColor { Id = 1, Type = "Синій" },
            new VehicleColor { Id = 2, Type = "Білий" },
            new VehicleColor { Id = 3, Type = "Червоний" },
            new VehicleColor { Id = 4, Type = "Чорний" },
            new VehicleColor { Id = 5, Type = "Сріблястий" },
            new VehicleColor { Id = 6, Type = "Сірий" },
            new VehicleColor { Id = 7, Type = "Зелений" },
            new VehicleColor { Id = 8, Type = "Жовтий" },
            new VehicleColor { Id = 9, Type = "Помаранчевий" },
            new VehicleColor { Id = 10, Type = "Коричневий" },
            new VehicleColor { Id = 11, Type = "Фіолетовий" },
            new VehicleColor { Id = 12, Type = "Рожевий" },
            new VehicleColor { Id = 13, Type = "Золотий" },
            new VehicleColor { Id = 14, Type = "Бежевий" },
            new VehicleColor { Id = 15, Type = "Бірюзовий" },
            new VehicleColor { Id = 16, Type = "Темно-синій" },
            new VehicleColor { Id = 17, Type = "Маджента" },
            new VehicleColor { Id = 18, Type = "Бірюзовий" },
            new VehicleColor { Id = 19, Type = "Лайм" },
            new VehicleColor { Id = 20, Type = "Синьо-зелений" });

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
            new VehicleType { Id = 1, Type = "Легковий автомобіль" },
            new VehicleType { Id = 2, Type = "Мотоцикл" },
            new VehicleType { Id = 3, Type = "Квадроцикл" },
            new VehicleType { Id = 4, Type = "Вантажівка" });

        //builder.Entity<Video>().HasData(userProfile);
        builder.Entity<Violation>().HasData(
            new Violation { Id = 1, Type = "Паркування у неправильному місці" },
            new Violation { Id = 2, Type = "Не застібнутий ремінь безпеки" },
            new Violation { Id = 3, Type = "Перевищення швидкості" },
            new Violation { Id = 4, Type = "Проїзд на червоне світло" },
            new Violation { Id = 5, Type = "Керування у стані сп'яніння" },
            new Violation { Id = 6, Type = "Використання мобільного телефону під час руху" },
            new Violation { Id = 7, Type = "Керування без дійсного посвідчення водія" },
            new Violation { Id = 8, Type = "Недотримання пріоритету" },
            new Violation { Id = 9, Type = "Неправильне паркування" },
            new Violation { Id = 10, Type = "Хуліганський стиль водіння" },
            new Violation { Id = 11, Type = "Різке торможення перед попереднім автомобілем" },
            new Violation { Id = 12, Type = "Недотримання правил включення поворотних сигналів" },
            new Violation { Id = 13, Type = "Неправильний обгін" },
            new Violation { Id = 14, Type = "Рух зі знятою реєстрацією" },
            new Violation { Id = 15, Type = "Рух без страховки" },
            new Violation { Id = 16, Type = "Невпорядковане зупиняння для пішохода" },
            new Violation { Id = 17, Type = "Заборонений поворот на 180 градусів" },
            new Violation { Id = 18, Type = "Рух у зустрічному напрямку" },
            new Violation { Id = 19, Type = "Рух з тонованими вікнами" },
            new Violation { Id = 20, Type = "Недотримання включення фар" },
            new Violation { Id = 21, Type = "Рух зі зупиненою дійсною посвідченням водія" },
            new Violation { Id = 22, Type = "Невиконання знаків дорожнього руху" },
            new Violation { Id = 23, Type = "Рух без належного освітлення" },
            new Violation { Id = 24, Type = "Неправомірна зміна смуги руху" },
            new Violation { Id = 25, Type = "Рух з тріснутим лобовим склом" },
            new Violation { Id = 26, Type = "Рух з надмірним шумом" },
            new Violation { Id = 27, Type = "Недотримання використання дитячого автокрісла" },
            new Violation { Id = 28, Type = "Невиконання вимог про уступ дороги аварійним службам" },
            new Violation { Id = 29, Type = "Рух у забороненому напрямку на вулиці з одностороннім рухом" },
            new Violation { Id = 30, Type = "Недотримання вимог про вимкнення світла фар" });


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
