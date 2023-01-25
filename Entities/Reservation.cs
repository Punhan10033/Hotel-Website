using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Reservation
    {
        [Key]
        public int BookingId { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public double TotalPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ArrivalDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDate { get; set; }
    }
}
