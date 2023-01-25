using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.IServices;
using DAL.UnitOfWork;
using DTO.CustomerDTO;
using Entities;
using X.PagedList;

namespace BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CustomerService(IMapper mapper,IUnitOfWork work)
        {
            _mapper=mapper;
            _uow=work;
        }

        public async Task AddAsync(CustomerToAddOrUpdateDto customer)
        {
            Customer customer1=_mapper.Map<Customer>(customer);
            await _uow._customerrepository.Add(customer1);
            await _uow.Commit();
        }

        public IPagedList<Customer> ByIdPaged(int? Id)
        {
            return _uow._customerrepository.ByIdPaged(Id);
        }

        public async Task<List<Customer>> Customers()
        {
            List<Customer> Customers = await _uow._customerrepository.GetCustomers();
            return Customers;
        }

        public async Task Delete(int CustomerId)
        {
            await _uow._customerrepository.Delete(CustomerId);
            await _uow.Commit();
        }

		public IPagedList<Customer> GetAsync(int pageNumber, int pageSize,string searchstring)
		{
            IPagedList<Customer> customers = _uow._customerrepository.Get(pageNumber, pageSize, searchstring);
            return customers;
        }

		public async Task<CustomerToAddOrUpdateDto> GetByIdAsync(int? CustomerId)
        {
            Customer customer = await _uow._customerrepository.Get(CustomerId);
            CustomerToAddOrUpdateDto cus = _mapper.Map<CustomerToAddOrUpdateDto>(customer);
            return cus;
        }

        public async Task<Customer> GetByPinCodeAsync(string PinCode)
        {
          return await _uow._customerrepository.ByPinCode(PinCode.ToUpper());
        }

        public IPagedList<Customer> GetLastRecord()
        {
            return _uow._customerrepository.GetLastRecord();
        }

        public async Task<List<string>> GetPins()
        {
            var customers = await _uow._customerrepository.GetCustomers();
            List<string> pins = customers.Select(x => x.PinCode).ToList();
            return pins;
        }

        public async Task UpdateAsync(CustomerToAddOrUpdateDto customer)
        {
            Customer customer1 = _mapper.Map<Customer>(customer);
            await _uow._customerrepository.Update(customer1);
            await _uow.Commit();
        }

		
	}
}
