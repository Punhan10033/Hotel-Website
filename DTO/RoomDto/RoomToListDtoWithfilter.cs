using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using Entities;

namespace DTO.RoomDto
{
    public class RoomToListDtoWithfilter
    {
        public List<RoomToListDto> Rooms { get; set; }
        [NotMapped]
        public List<Floor> FloorsList { get; set; }
    }
}
