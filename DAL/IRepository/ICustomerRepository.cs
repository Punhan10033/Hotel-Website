using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.CustomerDTO;
using Entities;
using X.PagedList;

namespace DAL.IRepository
{
    public interface ICustomerRepository
    {
        Task Add(Customer customer);
        Task Update(Customer customer);
        Task Delete(int CustomerId);
        IPagedList<Customer> Get(int pageNumber,int pageSize, string searchString);
        Task<List<Customer>> GetCustomers();
        Task<Customer> Get(int? CustomerId);
        Task<Customer> ByPinCode(string PinCode);
        IPagedList<Customer> GetLastRecord();
        IPagedList<Customer>ByIdPaged(int? Id);
    }
}
