using ApplicationBLL.Interfaces;
using ApplicationBLL.Repositories;
using ApplicationDAL.Data.Context;
using ApplicationDAL.Models;
using ApplicationPL.Controllers.MappingProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApplicationPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationContext>(options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository , EmployeeRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(EmployeeMapping));
            builder.Services.AddAutoMapper(typeof(DepartmentProfile));

            builder.Services.AddSession(option => option.IdleTimeout = TimeSpan.FromMinutes(30));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(action => action.Password.RequireDigit = true).
                AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders(); // added class that have method to generate tokens



            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "Account/Login";
                options.AccessDeniedPath = "Home/Error";
            });

           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}


//ToDo 
//create new field in department that show number of emps in this department 
//if number of emps > 3 -> color it with red else color it with green

//Search
//Cascade relationShip
