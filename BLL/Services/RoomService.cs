using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.IServices;
using DAL.UnitOfWork;
using DTO.RoomDto;
using Entities;

namespace BLL.Services
{
    public class RoomService : IRoomServices
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        public RoomService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
            _mapper = mapper;
        }



        public async Task Add(RoomToAddDto room)
        {
            Room room2 = _mapper.Map<Room>(room);

            await _unitofwork._roomrepository.AddNewRoom(room2);
            await _unitofwork.Commit();
        }

        public async Task DeleteAsync(int Id)
        {
            await _unitofwork._roomrepository.Delete(Id);
            await _unitofwork.Commit();
        }

        public async Task<List<RoomToListDto>> GetAsync()
        {
            List<Room> rooms = await _unitofwork._roomrepository.Get();
            List<RoomToListDto> lst = _mapper.Map<List<RoomToListDto>>(rooms);
            return lst;
        }

        public async Task<RoomToListDto> GetByIdAsync(int Id)
        {
            Room room = await _unitofwork._roomrepository.GetById(Id);
            RoomToListDto room1 = _mapper.Map<RoomToListDto>(room);
            return room1;
        }

        public async Task<List<string>> GetPicNames(int Id)
        {
            Room room = await _unitofwork._roomrepository.GetById(Id);
            List<string> pics = room.RoomPictures.Select(x => x.Image).ToList();
            return pics;
        }

        public async Task<List<RoomTypeToListDto>> GetRoomTypesAsync()
        {
            List<RoomType> roomtypes = await _unitofwork._roomrepository.GetTpes();
            List<RoomTypeToListDto> roomtypes2 = _mapper.Map<List<RoomTypeToListDto>>(roomtypes);
            return roomtypes2;
        }
        public async Task<List<Floor>> GetFloorsAsync()
        {
            return await _unitofwork._roomrepository.GetFloors();
        }

        public async Task<RoomToAddDto> GetByIdNullabel(int? Id)
        {
            Room room=await _unitofwork._roomrepository.GetByIdNullabe(Id);
            RoomToAddDto roomToAdd = _mapper.Map<RoomToAddDto>(room);
            return roomToAdd;
        }

        public async Task Update(RoomToAddDto room)
        {
            Room room2 = _mapper.Map<Room>(room);
            await _unitofwork._roomrepository.Update(room2);
            await _unitofwork.Commit();
        }

        public async Task<List<RoomPictures>> RoomPictures()
        {
            return await _unitofwork._roomrepository.GetRoomPictures();
        }

        public async Task UpdateRoomPics(RoomPictures pics)
        {
            await _unitofwork._roomrepository.UpdateRoomPictures(pics);
            await _unitofwork.Commit();
        }

        public async Task Update(RoomToListDto room)
        {
            Room room1 = _mapper.Map<Room>(room);
            await _unitofwork._roomrepository.Update(room1);
            await _unitofwork.Commit();
        }

        public async Task DeletePic(int Id)
        {
           await  _unitofwork._roomrepository.DeleteRoomPic(Id);
        }
    }
}
