using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using AutoMapper;
using LightInject;
using Marketplace.API.Infrastructure.Middleware;
using Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddress;
using Marketplace.Common;
using Marketplace.Domain.Merchant;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Marketplace.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, false)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddAutoMapper(typeof(Startup), typeof(MerchantAddressDto), typeof(MerchantAddress));
            
            var section = Configuration.GetSection("Keys:TokenSecret");
            var key = Encoding.UTF8.GetBytes(section.Value);

            services.AddCors(options => options.AddPolicy("AllowAll",
                builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().SetPreflightMaxAge(TimeSpan.FromHours(1))));

            services.AddAuthentication();
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services
                .AddControllers()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                .AddControllersAsServices();


            services.AddMemoryCache();
            services.AddHealthChecks();

            services
                .AddSwaggerGen()
                .AddMvc();
            //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginReqValidator>());

            services.Configure<Appsettings>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cultures = new List<CultureInfo> {
                new CultureInfo("en-GB"),
                new CultureInfo("en-US"),
                new CultureInfo("en"),
                new CultureInfo("tr"),
                new CultureInfo("tr-TR")
            };

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseRequestLocalization(options => {
                    options.DefaultRequestCulture = new RequestCulture("en");
                    options.SupportedCultures = cultures;
                })
                .UseCors(x => x.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseHealthChecks("/hc")
                .UseSwagger()
                .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); })
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseExceptionMiddleware();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureContainer(IServiceContainer container)
        {
            container.RegisterFrom<CompositionRoot>();
        }
    }

}
