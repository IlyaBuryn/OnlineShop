using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace OnlineShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });



            services.AddMvc();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.User.RequireUniqueEmail = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ICustomGenericService<Dto_PaymentType>, PaymentManager>();
            services.AddScoped<ICustomGenericService<Dto_DeliveryType>, DeliveryTypeManager>();
            services.AddScoped<ICustomGenericServiceAsync<Dto_DeliveryDetails>, DeliveryManager>();
            services.AddScoped<ICustomGenericServiceAsyncMembers<Dto_DeliveryStatus>, DeliveryStatusManager>();

            services.AddScoped<IDescriptionRepository, MongoDescriptionRepository>();
            services.AddScoped<IReviewRepository, MongoReviewRepository>();
            services.AddScoped<IUserRoleManager, UserRoleManager>();

            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IApplicationUserManager, AppUserManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IProductReviewManager, ProductReviewManager>();
            services.AddScoped<IProductSpecManager, ProductSpecManager>();
            services.AddScoped<IProductTypesManager, ProductTypesManager>();
            services.AddScoped<ISpecialTagManager, SpecialTagManager>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }
            app.UseSession();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{Area=Customer}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "Delivery",
                    areaName: "Delivery",
                    pattern: "Delivery/{controller=Delivery}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Products}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

            app.UseCookiePolicy();
        }
    }
}
