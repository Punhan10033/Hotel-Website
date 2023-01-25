using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.IRepository;
using DTO.CustomerDTO;
using Entities;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DAL.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Context1 _context;
        public CustomerRepository(Context1 context1)
        {
            _context=context1;
        }
       

        public async Task Add(Customer customer)
        {
            
            string created = DateTime.Now.ToString("dd/MM/yyyy");
            customer.CreatedAt=Convert.ToDateTime(created);
            await _context.Customers.AddAsync(customer);
        }

        public async Task<Customer> ByPinCode(string PinCode)
        {
          return  await _context.Customers.Where(x => (String.IsNullOrEmpty(PinCode))||x.PinCode.ToUpper() == PinCode.ToUpper()).FirstOrDefaultAsync();
        }

        public async Task Delete(int CustomerId)
        {
             _context.Customers.Remove(await _context.Customers.FindAsync(CustomerId));
        }

       
        public async Task<Customer> Get(int? CustomerId)
        {
           return await _context.Customers.FindAsync(CustomerId);
        }

        public   IPagedList<Customer> Get(int pageNumber, int pageSize, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return _context.Customers.Where(x=>x.PinCode.ToUpper()==searchString.ToUpper()).ToPagedList();
            }
            else
            {
                return _context.Customers.ToPagedList(pageNumber, pageSize);
            }
        }

        public IPagedList<Customer> GetLastRecord()
        {
            int max = _context.Customers.Max(x => x.CustomerId);
            return _context.Customers.Where(x => x.CustomerId == max).ToPagedList();
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.Entry(customer).Property("CreatedAt").IsModified = false;
        }

        public   IPagedList<Customer> ByIdPaged(int? Id)
        {
            return  _context.Customers.Where(x=>x.CustomerId==Id).ToPagedList();
        }
    }
}
