using DigitalMind.YoYoApp.Application.Interfaces;
using DigitalMind.YoYoApp.Application.Providers;
using DigitalMind.YoYoApp.Application.Services;
using DigitalMind.YoYoApp.Application.ViewModel;
using DigitalMind.YoYoApp.Domain.Interfaces;
using DigitalMind.YoYoApp.Infra.Context;
using DigitalMind.YoYoApp.Infra.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DigitalMind.YoYoApp
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

            services.AddDbContext<YoYoTestDbContext>(opt => opt.UseInMemoryDatabase("TestDatabase"));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAthleteShuttleServices, AthleteShuttleServices>();
          
            services.AddTransient<IStopWatchViewModel, StopWatchViewModel>();

            services.AddSingleton<IStopWatchProvider, StopWatchProvider>();

            services.AddSingleton<IAthleteListProvider, AthleteListProvider>();

            services.AddControllersWithViews();


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
            }
            app.UseStaticFiles();

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
