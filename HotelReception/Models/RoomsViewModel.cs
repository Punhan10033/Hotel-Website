using System.Collections.Generic;
using DTO.RoomDto;
using Entities;

namespace HotelReception.Models
{
    public class RoomsViewModel
    {
        public List<RoomToListDto>Rooms { get; set; }
        public Floor Floor { get; set; }
        public int FloorId { get; set; }
        public RoomType RoomType { get; set; }
    }
}
