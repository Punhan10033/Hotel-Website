using System.Collections.Generic;
using HotelReception.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelReception.Models
{
    public class UserWithRoles
    {
        public ReceptionUser User { get; set; }

        public List<SelectListItem> Roles { get; set; }
    }
}
