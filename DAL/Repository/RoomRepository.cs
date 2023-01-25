using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DAL.DataContext;
using DAL.IRepository;
using DTO.RoomDto;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly Context1 _context;
        private readonly IConfiguration _configuration;
        public RoomRepository(Context1 context1,IConfiguration configuration)
        {
            _configuration=configuration;
            _context=context1;
        }
        public async Task AddNewRoom(Room room)
        {
            await _context.Rooms.AddAsync(room);
        }
        public async Task Delete(int Id)
        {
           _context.Rooms.Remove(await _context.Rooms.FindAsync(Id));
        }

        public async Task<List<Room>> Get()
        {
            
            List<Room> rooms = await _context.Rooms.Include(x=>x.RoomPictures).Include(x=>x.RoomType).Include(x=>x.Floor).ToListAsync();
            return rooms;
        }

      

        public async Task<Room> GetById(int Id)
        {

            return await _context.Rooms.Include(x => x.RoomType).Include(x => x.RoomPictures).Include(x => x.Floor)
                .Include(x => x.Customer).Where(x => x.RoomId == Id).FirstOrDefaultAsync(); 
        }

        public async Task<Room> GetByIdNullabe(int? Id)
        {
            return await _context.Rooms.Include(x => x.RoomType).Include(x => x.RoomPictures).Include(x => x.Floor)
                .Include(x => x.Customer).Where(x => x.RoomId == Id).FirstOrDefaultAsync(); 
        }

        public async Task<List<Floor>> GetFloors()
        {
            return await _context.Floors.ToListAsync();

        }

        public async Task<List<RoomType>> GetTpes()
        {
            return await _context.RoomTypes.ToListAsync();
        }

     

        public async Task Update(Room room)
        {
             _context.Rooms.Update(room);
        }



        public async Task<List<RoomPictures>> GetRoomPictures()
        {
            return await _context.RoomPictures.ToListAsync();
        }

        public  async Task UpdateRoomPictures(RoomPictures roomPictures)
        {
             _context.RoomPictures.Update(roomPictures);
        }

        public async Task DeleteRoomPic(int Id)
        {
            _context.RoomPictures.Remove(await _context.RoomPictures.FindAsync(Id));
        }
    }
}
