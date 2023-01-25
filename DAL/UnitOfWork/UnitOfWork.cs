using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.IRepository;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomerRepository _customerrepository { get; set; }
        public IRoomRepository _roomrepository { get; set; }
        public IReservationRepository _reservationrepository { get; set; }

        private readonly Context1 _context;
        public UnitOfWork(IReservationRepository reservation, IRoomRepository roomRepository,Context1 context,ICustomerRepository customerRepository)
        {
            _roomrepository = roomRepository;
            _customerrepository=customerRepository;
            _context=context;
            _reservationrepository=reservation;
        }

        public async Task Commit()
        {
          await  _context.SaveChangesAsync();
        }
    }
}
