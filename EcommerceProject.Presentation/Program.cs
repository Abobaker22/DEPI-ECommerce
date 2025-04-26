using ECommerce.Data;
using EcommerceProject.DAL.IdentityApplication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace EcommerceProject.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var con = builder.Configuration.GetConnectionString("con");
            builder.Services.AddDbContext<EcommerceContext>(options => options.UseSqlServer(con));

            // Register Identity
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<EcommerceContext>(); // to make it register with my DbContext

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

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            app.Run();
        }
    }
}
