using AutoMapper;
using ReservationSystemMVC.Domain.Entities;
using ReservationSystemMVC.Web.Models;

namespace ReservationSystemMVC.Services.Mappers;

public class EntityToDtoProfile : Profile
{
    public EntityToDtoProfile()
    {
        CreateMap<ChiefEntity, ChiefDto>();
        CreateMap<ClassroomEntity, ClassroomDto>();
        CreateMap<StudentEntity, StudentDto>();
        CreateMap<ReservationRequestEntity, ReservationRequestDto>();
    }
}