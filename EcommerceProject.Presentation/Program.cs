using ECommerce.Data;
using EcommerceProject.DAL.IdentityApplication;
using EcommerceProject.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EcommerceProject.DAL.UnitOfWork;

namespace EcommerceProject.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
             // أضف هذا الـ using لو مش موجود

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<EcommerceProject.Presentation.Services.Interfaces.IWishlistService, EcommerceProject.Presentation.Services.WishlistService>();
            builder.Services.AddScoped<EcommerceProject.Presentation.Services.Interfaces.IReviewService, EcommerceProject.Presentation.Services.ReviewService>();
            var con = builder.Configuration.GetConnectionString("con");
            builder.Services.AddDbContext<EcommerceContext>(options => options.UseSqlServer(con));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // أضف هذا السطر
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<EcommerceContext>();
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
