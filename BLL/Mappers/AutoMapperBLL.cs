using AutoMapper;
using BLL.Models;
using DAL.Models;

namespace BLL.Mappers;

public class AutoMapperBLL : Profile
{
    public AutoMapperBLL()
    {
        CreateMap<User, UserDTO>()
            .ForMember(p => p.FirstName, c => c.MapFrom(src => src.UserProfile.FirstName))
            .ForMember(p => p.LastName, c => c.MapFrom(src => src.UserProfile.LastName))
            .ReverseMap();

        CreateMap<UserProfile, UserProfileDTO>()
            .ForMember(p => p.Email, c => c.MapFrom(src => src.AppUser.Email))
            .ForMember(p => p.UserName, c => c.MapFrom(src => src.AppUser.UserName))
            .ReverseMap();

        CreateMap<Application, ApplicationDTO>().ReverseMap();
        CreateMap<ApplicationStatus, ApplicationStatusDTO>().ReverseMap();
        CreateMap<Photo, PhotoDTO>().ReverseMap();
        CreateMap<VehicleColor, VehicleColorDTO>().ReverseMap();
        CreateMap<VehicleMark, VehicleMarkDTO>().ReverseMap();
        CreateMap<VehicleType, VehicleTypeDTO>().ReverseMap();
        CreateMap<Video, VideoDTO>().ReverseMap();
        CreateMap<Violation, ViolationDTO>().ReverseMap();
    }
}