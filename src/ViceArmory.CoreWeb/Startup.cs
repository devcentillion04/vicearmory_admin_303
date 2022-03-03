using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using ViceArmory.DAL.Interface;
using ViceArmory.DAL.Repository;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.Middleware.Interface;
using ViceArmory.Middleware.Service;

namespace ViceArmory.CoreWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<INewsletterService, NewsletterService>();
            services.AddScoped<IWeeklyAdsService, WeeklyAdsService>();
            services.AddScoped<IAdminLoginService, AdminLoginService>();
            services.AddScoped<ILogContext, LogContext>();
            
            services.Configure<APISettings>(Configuration.GetSection("APISettings"));
            services.Configure<ProjectSettings>(Configuration.GetSection("ProjectSettings"));
            services.AddMvcCore()
                    .AddDataAnnotations();
            services.AddControllersWithViews().AddRazorRuntimeCompilation(); 
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

             loggerFactory.AddFile("Logs/log.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                 name: "Admin",
                 areaName: "Admin",
                 pattern: "{area:exists}/{controller=AdminLogin}/{action=Index}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
