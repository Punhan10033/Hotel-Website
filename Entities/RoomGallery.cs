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
    public class RoomPictures
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
       
    }
}
