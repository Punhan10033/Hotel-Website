using System.Threading.Tasks;
using HotelReception.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelReception.Controllers
{
	public class AuthController : Controller
	{
        private readonly SignInManager<ReceptionUser> _signInManager;
		public AuthController(SignInManager<ReceptionUser> signInManager)
		{
			_signInManager=signInManager;
		}

		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "HotelReception", new { Area = "" });
		}

	}
}
