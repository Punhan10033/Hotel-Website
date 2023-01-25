using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DTO;
using Entities;
using X.PagedList;

namespace BLL.IServices
{
    public interface IReservationService
    {
        Task Add(ReservationToAddDto reservation);
        IPagedList<Reservation> GetAsync(int pageNumber, int pageSize, string searchString);
        Task<ReservationToAddDto> GetById(int? Id);
        Task Update(ReservationToAddDto reservation);
        Task Delete(int Id);
        Task<List<Reservation>> GetReservationsAsync();
    }
}
