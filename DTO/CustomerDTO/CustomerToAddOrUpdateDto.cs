using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DTO.CustomerDTO
{
    public class CustomerToAddOrUpdateDto
    {
        public int? CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "PinCode")]
        public string PinCode { get; set; }



    }
}
