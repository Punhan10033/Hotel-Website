using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.IRepository;
using Entities;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DAL.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly Context1 _context;
        public ReservationRepository(Context1 context1)
        {
            _context=context1;
        }
        public async Task Add(Reservation reservation)
        {
            string arrivaldate = DateTime.Now.ToString();
            reservation.ArrivalDate=DateTime.Parse(arrivaldate);
            await _context.Reservations.AddAsync(reservation);
        }

        public async Task Delete(int Id)
        {
            _context.Reservations.Remove(await _context.Reservations.FindAsync(Id));
        }

        public IPagedList<Reservation> Get(int pageNumber, int pageSize, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return _context.Reservations.Include(x => x.Room).ThenInclude(x => x.RoomPictures).Include(x => x.Customer).Where(x => x.Customer.PinCode.ToUpper() == searchString.ToUpper()).ToPagedList();
            }
            else
            {
                return _context.Reservations.Include(x => x.Room).ThenInclude(x => x.RoomPictures).Include(x => x.Customer).ToPagedList(pageNumber, pageSize);
            }
        }

        public async Task<Reservation> GetById(int? Id)
        {
            return await _context.Reservations.Include(x=>x.Customer).Include(x=>x.Room).SingleOrDefaultAsync(x=>x.BookingId==Id);
        }

        public async Task<List<Reservation>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task Update(Reservation reservation)
        {
             _context.Reservations.Update(reservation);
        }
    }
}
