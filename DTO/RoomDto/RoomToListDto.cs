using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Http;

namespace DTO.RoomDto
{
    public class RoomToListDto
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public int? CustomerId { get; set; }
        public List<RoomPictures> RoomPictures { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public RoomType RoomType { get; set; }
        public Floor Floor { get; set; }
    
    }
}
