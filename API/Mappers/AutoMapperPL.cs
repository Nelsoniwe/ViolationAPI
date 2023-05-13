using API.Models;
using AutoMapper;
using BLL.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Mappers;

public class AutoMapperPL : Profile
{
    public AutoMapperPL()
    {
        CreateMap<UserDTO, RegisterModel>()
            .ReverseMap();
        CreateMap<UserModel, UserDTO>()
            .ReverseMap();
        CreateMap<ApplicationModel, ApplicationDTO>()
            .ReverseMap();
        CreateMap<ApplicationStatusModel, ApplicationStatusDTO>()
            .ReverseMap();
        CreateMap<PhotoModel, PhotoDTO>()
            .ReverseMap();
        CreateMap<VideoModel, VideoDTO>()
            .ReverseMap();
        CreateMap<VehicleColorModel, VehicleColorDTO>()
            .ReverseMap();
        CreateMap<VehicleMarkModel, VehicleMarkDTO>()
            .ReverseMap();
        CreateMap<VehicleTypeModel, VehicleTypeDTO>()
            .ReverseMap();
        CreateMap<ViolationModel, ViolationDTO>()
            .ReverseMap();
    }
}