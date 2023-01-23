using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;
using TelefonRehberi.API.Controllers;
using TelefonRehberi.Business.Abstract;
using TelefonRehberi.Business.Concrete;
using TelefonRehberi.DataAccess;
using TelefonRehberi.DataAccess.Abstract;
using TelefonRehberi.DataAccess.Concrete;
using TelefonRehberi.Entities;

namespace TelefonRehberi.API
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
            services.AddControllers().AddFluentValidation(x=> { x.RegisterValidatorsFromAssemblyContaining<Startup>(); });



            services.AddTransient<MyDbContext>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IPersonService, PersonManager>();

            services.AddTransient<IAuthService, AuthManager>();


            



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidAudience = Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero,

                };
            });

            /* services.AddCors(options =>
                 options.AddDefaultPolicy(builder =>
                     builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
                 )
             );*/

            services.AddCors(options =>
                 options.AddDefaultPolicy(builder =>
                 builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            
            app.UseAuthorization();

            

            
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            

            
        }
    }
}
