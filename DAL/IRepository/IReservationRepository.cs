using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using X.PagedList;

namespace DAL.IRepository
{
    public interface IReservationRepository
    {
        Task Add(Reservation reservation);
        Task Update(Reservation reservation);
        IPagedList<Reservation> Get(int pageNumber, int pageSize, string searchString);
        Task<Reservation> GetById(int? Id);
        Task Delete(int Id);
        Task<List<Reservation>> GetReservations();
    }
}
