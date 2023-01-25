using System;
using HotelReception.Areas.Identity.Data;
using HotelReception.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

[assembly: HostingStartup(typeof(HotelReception.Areas.Identity.IdentityHostingStartup))]
namespace HotelReception.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {

            builder.ConfigureServices((context, services) =>
            {


                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AppDbContextConnection")));
                //services.AddDefaultIdentity<ReceptionUser>(options =>
                //{
                //    options.SignIn.RequireConfirmedAccount = true;
                //    options.Password.RequireLowercase = false;
                //    options.Password.RequireUppercase = false;
                //    options.Password.RequireDigit = false;
                //    options.Password.RequireNonAlphanumeric = false;
                //    options.Password.RequiredLength = 4;
                //    options.SignIn.RequireConfirmedAccount = false;

                //}
                //).AddEntityFrameworkStores<AppDbContext>();

                services.AddIdentity<ReceptionUser, IdentityRole>(options =>
                {
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 4;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                }
                    )
                        .AddDefaultUI()
                        .AddEntityFrameworkStores<AppDbContext>()
                        .AddDefaultTokenProviders();
                services.AddControllersWithViews();
                services.AddRazorPages();

                services.AddAuthorization(options =>
                {
                    options.AddPolicy("readpolicy",
                        builder => builder.RequireRole("Admin", "Manager", "User"));
                    options.AddPolicy("writepolicy",
                        builder => builder.RequireRole("Admin", "Manager"));
                });
            });
        }
    }
}