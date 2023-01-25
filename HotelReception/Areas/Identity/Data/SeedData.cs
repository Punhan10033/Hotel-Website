using Microsoft.AspNetCore.Identity;

namespace HotelReception.Areas.Identity.Data
{
    public static class SeedData
    {
        public static void Seed(UserManager<ReceptionUser> userManager, RoleManager<IdentityRole> _roleManager)
        {
            SeedRoles(_roleManager);
            SeedUsers(userManager);
        }
        public static void SeedUsers(UserManager<ReceptionUser> userManager)
        {
            if (userManager.FindByNameAsync("Punhan").Result == null)
            {
              
                var identityuser = new IdentityUser()
                {
                    Email = "punhan1003@gmail.com",
                    EmailConfirmed=true,
                };
                var user1 = new ReceptionUser()
                {
                    UserName=identityuser.Email,
                    FirstName = "Punhan",
                    LastName = "Ibadov",
                    Email=identityuser.Email,
                    EmailConfirmed = identityuser.EmailConfirmed,

                };


                var result = userManager.CreateAsync(user1, "password").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user1, "Administrator").Wait();
                }
            }
        }
        public static  void SeedRoles(RoleManager<IdentityRole> _roleManager)
        {
            if (!_roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole()
                {
                    Name = ("Administrator")
                };
                var result = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                var role = new IdentityRole()
                {
                    Name = ("User")
                };
                var result = _roleManager.CreateAsync(role);
            }
            if (!_roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new IdentityRole()
                {
                    Name = ("Employee")
                };
                var result = _roleManager.CreateAsync(role).Result;
            }
        }
    }
}
