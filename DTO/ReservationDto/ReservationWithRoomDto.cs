using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DTO.RoomDto;
using Entities;

namespace DTO
{
    public class ReservationToAddDto
    {
        public int? BookingId { get; set; }
        public RoomToListDto Room1 { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public double TotalPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ArrivalDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDate { get; set; }
    }
}
