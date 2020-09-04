using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyPortal.Context;
using CompanyPortal.Manager;
using CompanyPortal.Models;
using CompanyPortal.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CompanyPortal
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
            services.AddDbContext<CompanyContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:CompanyPortal"]));
            services.AddControllers();
            AddSwaggerDocument(services);
            services.AddScoped<UserManager>();
            services.AddScoped<CompanyManager>();
            services.Configure<MailSetting>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();
            services.Configure<Token>(Configuration.GetSection("ApplicationSettings"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwaggerUi3();

            app.UseOpenApi();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        public void AddSwaggerDocument(IServiceCollection service)
        {
            service.AddSwaggerDocument(document =>
            {
                document.PostProcess = d =>
                {
                    d.Info.Version = "V1";
                    d.Info.Title = "Company Portal Service";
                    d.Info.Description = "Platform to Know about the companies";
                };
            });
        }
    }
}
