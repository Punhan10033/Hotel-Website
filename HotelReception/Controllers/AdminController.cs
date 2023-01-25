using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages;
using DTO;
using HotelReception.Areas.Identity.Data;
using HotelReception.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace HotelReception.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ReceptionUser> _userManager;
        private readonly SignInManager<ReceptionUser> _signInManager;
        public AdminController(RoleManager<IdentityRole> roleManager,
            UserManager<ReceptionUser> userManager, SignInManager<ReceptionUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "Administrator, SpuerAdmin")]

        public IActionResult Roles(int? page, string SearchString)
        {
            ViewBag.BackgroundImage = "swimmingpool1.jpg";
            IPagedList<IdentityRole> roles = _roleManager.Roles.ToPagedList();
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            if (!String.IsNullOrEmpty(SearchString))
            {
                return View(_roleManager.Roles.Where(x => x.NormalizedName == SearchString.ToUpper()).ToPagedList());
            }
            else
            {
                return View(_roleManager.Roles.ToPagedList(pageNumber, pageSize));
            }
        }

        [Authorize(Roles = "Administrator, SpuerAdmin")]
        public async Task<IActionResult> CreateRole(AdministratorCreateRoleDTO administrator)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = administrator.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    ViewBag.Message = string.Format("Role Added Succesfully.");
                    return RedirectToAction(actionName: "Roles", controllerName: "Admin");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }



        [Authorize(Roles = "Administrator, SpuerAdmin")]
        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {
            if (Id is null)
            {
                return View("NotFound", "Please Add Role Id in the URL");
            }
            IdentityRole role = await _roleManager.FindByIdAsync(Id);
            if (role is null)
            {
                return View("NotFound", $"The Role as Id {Id} cannot be found");
            }
            EditRoleModel model = new EditRoleModel()
            {
                Id = role.Id,
                RoleName = role.Name,
            };
            foreach (ReceptionUser user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.Email);
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator, SpuerAdmin")]
        [HttpPost]
        public async Task<IActionResult> Update(EditRoleModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id={model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [Authorize(Roles = "Administrator, SpuerAdmin")]
        public async Task<IActionResult> DeleteRole(string Id, string confirm_value)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(Id);
            if (!(role is null))
            {
                var result = await _roleManager.DeleteAsync(role);
                return RedirectToAction("Roles");
            }
            else
            {
                return RedirectToAction("Roles");
            }
        }

        [Authorize(Roles = "Administrator,SuperAdmin")]
        public async Task<IActionResult> GetUsers(int? page, string searchString)
        {
            ViewBag.BackgroundImage = "swimmingpool1.jpg";
            IPagedList<ReceptionUser> users = _userManager.Users.ToPagedList();
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            List<IdentityRole> roles = _roleManager.Roles.ToList();
            Microsoft.AspNetCore.Mvc.Rendering.SelectList selectLists = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(roles, "Id", "Name");
            ViewBag.RoleSelectList = selectLists;
            if (!string.IsNullOrEmpty(searchString))
            {
                return View(users = users.Where(x => x.FirstName.ToUpper() == searchString.ToUpper()).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View(users);
            }
        }
        [Authorize(Roles = "Administrator, SpuerAdmin")]
        public async Task<IActionResult> EditUser(string Id)
        {
            // userWithRoles=new UserWithRoles();
            //List<ReceptionUser> users = _userManager.Users.ToList();
            //userWithRoles.Roles = _roleManager.Roles.ToList();
            //SelectList selectLists = new SelectList(userWithRoles.Roles, "Id", "Name");
            //ViewBag.RoleSelectList = selectLists;
            //var user = users.Where(x => x.Id == Id).FirstOrDefault();
            //userWithRoles.User = user;
            //return View(userWithRoles);
            ViewBag.BackgroundImage = "swimmingpool1.jpg";
            var user = await _userManager.FindByIdAsync(Id);
            var roles = _roleManager.Roles.ToList();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name)))).ToList();

            var vm = new UserWithRoles
            {
                User = user,
                Roles = roleItems
            };

            return View(vm);
        }

        [Authorize(Roles = "Administrator, SpuerAdmin")]
        public async Task<IActionResult> OnPostAsync(UserWithRoles data)
        {
            var user = await _userManager.FindByIdAsync(data.User.Id);
            if (user == null)
            {
                return NotFound();
            }
            user.FirstName = data.User.FirstName;
            user.LastName = data.User.LastName;
            user.Email = data.User.Email;

            //
            var rolesToAdd = new List<string>();
            var rolesToDelete=new List<string>();

            //
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in data.Roles)
            {
                var assignedInDb = userRoles.FirstOrDefault(x => x == role.Text);
                if (role.Selected)
                {
                    if (assignedInDb == null)
                    {
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assignedInDb != null)
                    {
                        rolesToDelete.Add(role.Text);
                    }
                }
            }
            if(rolesToAdd.Any())
            {
                await _userManager.AddToRolesAsync(user, rolesToAdd);
            }
            if (rolesToDelete.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, rolesToDelete);
            }
            return RedirectToAction("GetUsers", new {id=user.Id});
        }
    }
}
