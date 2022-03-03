using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using ViceArmory.DAL.Interface;
using ViceArmory.DAL.Repository;
using ViceArmory.DTO.RequestObject.ApiConfiguration;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.DTO.ResponseObject.AppSettings;

namespace ViceArmory.API
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
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddScoped<IAuthenticateContext, AuthenticateContext>();
            services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();

            services.AddScoped<IProductContext, ProductContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAccountContext, AccountContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMerchantUserRepository, MerchantUserRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserAccessRepository, UserAccessRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMerchantUserRepository, MerchantUserRepository>();

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IWeeklyAdsRepository, WeeklyAdsRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserAccessRepository, UserAccessRepository>(); 
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IApiConfigurationService, ApiConfigurationService>();
            services.AddScoped<IApiConfigurationService, ApiConfigurationService>(); 
            services.AddScoped<IItemImageRepository, ItemImageRepository>();
            services.AddScoped<ILogContext, LogContext>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<ProjectSettings>(Configuration.GetSection("ProjectSettings"));
            services.Configure<FileStorageConfigResponseDTO>(Configuration.GetSection("FileStorageConfig"));
            services.Configure<ApiConfigurationSetting>(Configuration.GetSection("ApiConfiguration"));
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ViceArmory.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });

            });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"])) //Configuration["JwtToken:SecretKey"]  
                };
            });
            services.AddCors();
            services.AddHealthChecks()
                    .AddMongoDb(Configuration["DatabaseSettings:ConnectionString"], "MongoDb Health", HealthStatus.Degraded);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/log.txt");
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ViceArmory.API v1"));
            }
            app.UseDeveloperExceptionPage();
            // app.UseSession();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


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
