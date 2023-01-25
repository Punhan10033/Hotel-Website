using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using DTO.CustomerDTO;
using DTO.RoomDto;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Utility
{
    public class Mapper1:Profile
    {
        public Mapper1()
        {
            //Customer
            CreateMap<Customer, CustomerToAddOrUpdateDto>().ReverseMap();
            CreateMap<CustomerToListDto,Customer>().ReverseMap();
            CreateMap<Room,RoomToAddDto>().ReverseMap();
            CreateMap<RoomToListDto, Room>().ReverseMap();
            CreateMap<RoomType, RoomTypeToListDto>().ReverseMap();
            CreateMap<Room,RoomToListDtoWithfilter>().ReverseMap();
            CreateMap<RoomToListDto, RoomToListDtoWithfilter>();
            CreateMap<Reservation,ReservationToAddDto>().ReverseMap();
            //CreateMap<IdentityRole,AdministratorCreateRoleDTO>().ReverseMap();
        }
    }
}
