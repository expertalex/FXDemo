using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using FXDemo.Filters;
using FXDemo.Contracts;
using FXDemo.Services;
using FXDemo.Data;
using FXDemo.Infrastructure;
using Newtonsoft.Json;

namespace FXDemo
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

            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IStatisticService, StatisticService>();
            services.AddScoped<IMatchService, MatchService>();
            // TODO: Inject remaining services

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
            });

            // Add Sqlite for Mac Dev
            services.AddDbContext<FXDataContext>(options => options.UseSqlite(Configuration.GetConnectionString("FXDataContext")));

            // Add InMemoryDB for quick dev
            // services.AddDbContext<FXDataContext>(options => options.UseInMemoryDatabase(databaseName: "FXMemoryDB"));

            // TODO: Add & Configure SQL Database for Prod


            // Register the Swagger services
            services.AddSwaggerDocument();

            services.AddApiVersioning(o => {
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                    o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.ReportApiVersions = true;

            });

            services.AddAutoMapper(typeof(Startup));

            /*  
            services.AddCors( options => {
            });
            */


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
