using Account.DataContract.Entities;
using Account.Repository.Data;
using Account.Repository.Data.Interfaces;
using Account.Repository.Repositories;
using Account.Repository.Repositories.Interfaces;
using Account.Service.Services;
using Account.Service.Services.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Account.API
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
            services.AddScoped<IAccountContext, AccountContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMerchantUserService, MerchantUserService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserAccessService, UserAccessService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMerchantUserRepository, MerchantUserRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserAccessRepository, UserAccessRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Account.API", Version = "v1" });
            });
            services.AddHealthChecks()
                    .AddMongoDb(Configuration["DatabaseSettings:ConnectionString"], "MongoDb Health", HealthStatus.Degraded);
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Account.API v1"));
            }
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}