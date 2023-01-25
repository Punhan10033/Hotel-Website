using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.CustomerDTO;
using Entities;
using X.PagedList;

namespace BLL.IServices
{
    public interface ICustomerService
    {
        Task AddAsync(CustomerToAddOrUpdateDto customer);
        IPagedList<Customer> GetAsync(int pageNumber, int pageSize,string searchstring);
        Task Delete(int CustomerId);
        Task UpdateAsync(CustomerToAddOrUpdateDto customer);
        Task<CustomerToAddOrUpdateDto> GetByIdAsync(int? CustomerId);
        Task<List<string>> GetPins();
        Task<Customer> GetByPinCodeAsync(string PinCode);
        Task<List<Customer>> Customers();
        IPagedList<Customer> GetLastRecord();
        IPagedList<Customer> ByIdPaged(int? Id);
    }
}
