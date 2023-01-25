using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Floor
    {
        [Key]
        public int FloorId { get; set; }
        public string FloorName { get; set; }
    }
}
