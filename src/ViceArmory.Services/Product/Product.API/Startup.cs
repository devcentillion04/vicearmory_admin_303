using Account.DataContract.Entities;
using Account.Repository.Data;
using Account.Repository.Data.Interfaces;
using Account.Repository.Repositories;
using Account.Repository.Repositories.Interfaces;
using Account.Service.Services;
using Account.Service.Services.Interfaces;
using Authenticate.DataContract;
using Authenticate.Repository;
using Authenticate.Repository.Data;
using Authenticate.Repository.Data.Interfaces;
using Authenticate.Repository.Interfaces;
using Authenticate.Service.Helpers;
using Authenticate.Service.Interfaces;
using Authenticate.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Product.Repositories;
using Product.Repositories.Data;
using Product.Repositories.Data.Interfaces;
using Product.Repositories.Interfaces;
using Product.Service.Services;
using Product.Service.Services.Interfaces;
using System.Text;

namespace Product.API
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
            services.AddScoped<IAuthenticateContext, AuthenticateContext>();
            services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IProductContext, ProductContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAccountContext, AccountContext>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IFileStorageService, FileStorageService>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<FileStorageConfig>(Configuration.GetSection("FileStorageConfig"));
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product.API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
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

            })
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"])) //Configuration["JwtToken:SecretKey"]  
                };
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product.API v1"));
            }
            app.UseMiddleware<JwtMiddleware>();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
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
