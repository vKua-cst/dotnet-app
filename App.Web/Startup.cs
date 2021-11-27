using App.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using App.Services;

namespace App.Web
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
            var cn = Configuration.GetConnectionString("db");

            services.AddDbContext<ApplicationDbContext>(
                builder =>
                {
                    builder.UseSqlServer(cn);
                    builder.EnableSensitiveDataLogging(true);
                }
            );

            services.AddHttpContextAccessor();

            services.AddAuthentication("cookie")
                    .AddCookie(
                    "cookie", builder =>
                    {
                        builder.LoginPath = "/auth/connect";
                        builder.LogoutPath = "/auth/disconnect";
                        builder.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                        builder.SlidingExpiration = true;
                        builder.Cookie.IsEssential = true;
                    });

            services.AddScoped<IAuthService, AuthService>();

            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation();
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

            app.UseAuthentication();

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
