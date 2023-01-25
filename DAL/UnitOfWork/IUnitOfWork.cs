using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.IRepository;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICustomerRepository _customerrepository { get; set; }
        IRoomRepository _roomrepository { get; set; }
        IReservationRepository _reservationrepository { get; set; }
        Task Commit();
    }
}
