using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.CustomerDTO;
using Entities;

namespace DAL.IRepository
{
    public interface IRoomRepository
    {
        Task AddNewRoom(Room room);
        Task Update(Room room);
        Task Delete(int Id);
        Task<List<Room>> Get();
        Task<Room> GetByIdNullabe(int? Id);
        Task<Room> GetById(int Id);
        Task<List<RoomType>> GetTpes();
        Task<List<Floor>> GetFloors();
        Task<List<RoomPictures>> GetRoomPictures();
        Task UpdateRoomPictures(RoomPictures roomPictures);
        Task DeleteRoomPic(int Id);
    }
}
