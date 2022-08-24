using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using AutoMapper;
using Conversion.API.Policies;
using Conversion.Core.Contracts;
using Conversion.Core.Converters;
using Conversion.DataAccess;
using Conversion.DataAccess.Core;
using Conversion.DataAccess.Interfaces;
using Conversion.DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Conversion.API
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
            services.AddControllers();

            services.AddScoped<ITemperatureConverter, TemperatureConverter>();
            services.AddScoped<IMassConverter, MassConverter>();
            services.AddScoped<ILengthConverter, LengthConverter>();

            ConfigureDatabase(services);

            services.AddScoped<IUnitOfWork<ConversionDbContext>, UnitOfWork<ConversionDbContext>>();
            services.AddScoped(typeof(IRepository<ConversionHistory>), typeof(EfRepository<ConversionDbContext, ConversionHistory>));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Add Identity service
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ConversionDbContext>()
                    .AddDefaultTokenProviders();

            // Adding Authentication with Jwt Bearer  
            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidAudience = Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                            ClockSkew = TimeSpan.Zero
                        };
                    });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(UserRoles.Admin, Policy.AdminPolicy());
                config.AddPolicy(UserRoles.User, Policy.UserPolicy());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Conversion REST API",
                    Description = "Used to get conversions from metric to imperial and vice versa"
                });

                c.AddFluentValidationRules();
            });
        }

        public virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ConversionDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConversionDB")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Conversion Web API v1");
            });

            app.UseCors(x => x.WithOrigins(Configuration["AllowedOrigin"])
                              .AllowAnyMethod()
                              .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
