using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Http;

namespace DTO.RoomDto
{
    public class RoomToAddDto
    {
        public int? RoomId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "RoomNumber")]
        public int RoomNumber { get; set; }
        public int? CustomerId { get; set; }
        public List<RoomPictures> RoomPictures { get; set; }
        public string Description { get; set; }
        public int RoomTypeId { get; set; }
        public int FloorId { get; set; }
        public double Price { get; set; }
    

       

    }
}
