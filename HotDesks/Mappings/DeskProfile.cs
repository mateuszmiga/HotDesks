using AutoMapper;
using Domain.Entities;
using HotDesks.Api.Dto;

namespace HotDesks.Api.Mappings
{
    public class DeskProfile : Profile
    {
        public DeskProfile()
        {
            CreateMap<Desk, DeskDto>().ReverseMap();
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }

        
    }
}
