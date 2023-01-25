using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL.IRepository
{
    public interface IFloorRepository
    {
        Task Add(Floor floor);
        Task Update(Floor floor);
        Task Delete(int Id);
        Task<Floor> Get(int Id);
        Task<List<Floor>> Get();
    }
}
