using CinemaService.DataLayer;
using CinemaService.DataLayer.Models;
using CinemaService.DataLayer.Repositories;
using CinemaService.Web.Api.Library.Mapper;
using CinemaService.Web.Api.Library.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CinemaService.Web.Api
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

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString")));

            services.AddScoped<ICinemaShowRepository, CinemaShowRepository>();
            services.AddScoped<ISeatRepository, SeatRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<ICinemaShowService, CinemaShowService>();
            services.AddTransient<ISeatService, SeatService>();

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>(nameof(ApplicationDbContext));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            PrepDB.PrepPopulation(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/status");
                endpoints.MapControllers();
            });
        }
    }
}
