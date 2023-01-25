using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DTO.CustomerDTO
{
    public class CustomerToListDto
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PinCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
