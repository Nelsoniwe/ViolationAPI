using System.Reflection;
using API.Mappers;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Services;
using BLL.Utility;
using DAL;
using DAL.Interfaces;
using DAL.Interfaces.BaseInterfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseExceptionHandler("/error");

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ViolationsAPI"));

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors();

        services.AddDbContext<ViolationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Dyploma"), x =>
                x.MigrationsAssembly("DAL")));

        services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<ViolationContext>();


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = JwtAuthOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = JwtAuthOptions.Audience,

                ValidateLifetime = true,

                IssuerSigningKey = JwtAuthOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };

        });

        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<IRepository<ApplicationStatus>, ApplicationStatusRepository>();
        services.AddScoped<IRepository<VehicleType>, VehicleTypeRepository>();
        services.AddScoped<IRepository<VehicleMark>, VehicleMarkRepository>();
        services.AddScoped<IRepository<VehicleColor>, VehicleColorRepository>();
        services.AddScoped<IRepository<Violation>, ViolationRepository>();
        services.AddScoped<IRepository<Photo>, PhotoRepository>();
        services.AddScoped<IRepository<Video>, VideoRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<IApplicationStatusService, ApplicationStatusService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVehicleColorService, VehicleColorService>();
        services.AddScoped<IVehicleMarkService, VehicleMarkService>();
        services.AddScoped<IVehicleTypeService, VehicleTypeService>();
        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<IViolationService, ViolationService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddAutoMapper(typeof(AutoMapperBLL));
        services.AddAutoMapper(typeof(AutoMapperPL));

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ViolationsApi",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "Bearer",
                Description = "Enter JWT token into field"
            });



            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });


        services.AddControllers();
    }
}