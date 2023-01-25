using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AdministratorCreateRoleDTO 
    {
        public string Id { get; set; }
        [Required]
        [Display(Name ="Roloe Name")]
        public string RoleName { get; set; } 
    }
}
