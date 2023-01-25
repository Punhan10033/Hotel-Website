using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.IServices;
using DAL.UnitOfWork;
using DTO;
using DTO;
using Entities;
using X.PagedList;

namespace BLL.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _work;
        public ReservationService(IMapper mapper,IUnitOfWork work)
        {
            _mapper=mapper;
            _work=work;
        }
        public async Task Add(ReservationToAddDto reservation)
        {
            Reservation reservation1=_mapper.Map<Reservation>(reservation);
           
            await _work._reservationrepository.Add(reservation1);
            await _work.Commit();
        }

      

        public async Task Delete(int Id)
        {
            await _work._reservationrepository.Delete(Id);
            await _work.Commit();
        }

        public  IPagedList<Reservation> GetAsync(int pageNumber, int pageSize, string searchString)
        {
           return   _work._reservationrepository.Get(pageNumber, pageSize,searchString);
        }

        public async Task<ReservationToAddDto> GetById(int? Id)
        {
            Reservation reservation=await _work._reservationrepository.GetById(Id);
            ReservationToAddDto res=_mapper.Map<ReservationToAddDto>(reservation);
            return res;
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            return await _work._reservationrepository.GetReservations();
        }
        public async Task Update(ReservationToAddDto reservation)
        {
            Reservation res = _mapper.Map<Reservation>(reservation);
            await _work._reservationrepository.Update(res);
        }
    }
}
