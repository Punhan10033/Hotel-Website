using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Entities
{
    public class Room
    {
       
        [Key]
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public Customer Customer { get; set; }
        public int? CustomerId { get; set; }
        public List<RoomPictures> RoomPictures { get; set; }
        public string Description { get; set; }
        public RoomType RoomType { get; set; }
        public int RoomTypeId { get; set; }
        public Floor Floor { get; set; }
        public int FLoorId { get; set; }
        public double Price { get; set; }
      


    }
}
