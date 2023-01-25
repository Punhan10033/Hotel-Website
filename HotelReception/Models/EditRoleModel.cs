using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelReception.Models
{
	public class EditRoleModel
	{
		public EditRoleModel()
		{
			Users = new List<string>();
		}
		public string Id { get; set; }
		[Required]
		[Display(Name ="Enter the Role Name")]
		public string RoleName { get; set; }
		public List<string> Users { get; set; }
	}
}
