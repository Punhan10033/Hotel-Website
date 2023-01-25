using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.RoomDto;
using Entities;

namespace BLL.IServices
{
    public interface IRoomServices
    {
        Task Add(RoomToAddDto room);
        Task<List<RoomToListDto>> GetAsync();
        Task DeleteAsync(int Id);
        //Task Update(R);
        Task<RoomToListDto> GetByIdAsync(int Id);
        Task<RoomToAddDto> GetByIdNullabel(int? Id);
        Task<List<string>> GetPicNames(int Id);
        Task<List<RoomTypeToListDto>> GetRoomTypesAsync();
        Task<List<Floor>> GetFloorsAsync();
        Task Update(RoomToAddDto room);
        Task Update(RoomToListDto room);
        Task<List<RoomPictures>> RoomPictures();
        Task UpdateRoomPics(RoomPictures pics);
        Task DeletePic(int Id);
    }
}
