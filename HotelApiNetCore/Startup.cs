using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HotelApiNetCore.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using HotelApiNetCore.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using HotelApiNetCore.Filters;
using HotelApiNetCore.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace HotelApiNetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(opt=> {
                var jsonOutputFormatter = opt.OutputFormatters.OfType<JsonOutputFormatter>().Single();

                opt.OutputFormatters.Remove(jsonOutputFormatter);

                opt.OutputFormatters.Add(new IonOutputFormatter(jsonOutputFormatter));

                opt.Filters.Add(typeof(JsonExceptionFilter));
            });

            services.Configure<HotelInfo>(Configuration.GetSection("Info"));


            services.AddApiVersioning(opt =>
            {
                opt.ApiVersionReader = new MediaTypeApiVersionReader();
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opt);
            });

            services.AddDbContext<HotelApiDbContext>(opt => opt.UseInMemoryDatabase());

            services.AddScoped<IRoomService, DefaultRoomService>();

            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                var context = app.ApplicationServices.GetService<HotelApiDbContext>();
                AddSeedData(context);
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }

        public void AddSeedData(HotelApiDbContext context)
        {
            context.Rooms.Add(new RoomEntity()
            {
                Id = new Guid("40d836ec-80f5-46b8-84eb-d26428eed573"),
                Name = "Oxford Suite",
                Rate = 10119
            });

            context.Rooms.Add(new RoomEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Driscoll Suite",
                Rate =  15010
            });

            context.Rooms.Add(new RoomEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Whatever the Suite is",
                Rate = 20000

            });

            context.SaveChanges();
        }
    }
}
