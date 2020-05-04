using System;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.ApplicationCore.Services;
using PharmacyNetwork.Infrastructure.Data;
using  PharmacyNetwork.Infrastructure.Identity;
using PharmacyNetwork.Infrastructure.Logging;

namespace PharmacyNetwork.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region Commands

        // add-migration InitMigration -Context PharmacyNetworkContext -Project Infrastructure -StartupProject Web
        // update-database -Context AppIdentityDbContext -Project Infrastructure -StartupProject Web

        #endregion

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CreateIdentityIfNotCreated(services);

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<IIncomesMedItemsService, IncomesMedItemsService>();

            // Add Identity DbContext
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            
            //Add PharmacyNetwork DbContext
            services.AddDbContext<PharmacyNetworkContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("PharmacyNetworkConnection"),
                    sqlServerOptionsAction: sqlOption =>
                    {
                        sqlOption.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
                });

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();
            services.AddMvc();
            services.AddRazorPages();

            services.AddMediatR(typeof(Startup));

            services.AddResponseCaching();
            services.AddHttpContextAccessor();

            services.AddDistributedMemoryCache();

            services.AddMemoryCache();

            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            services.AddSwaggerGen();
        }

        private static void CreateIdentityIfNotCreated(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();

            var existingUserManager = scope.ServiceProvider
                .GetService<UserManager<ApplicationUser>>();

            if (existingUserManager == null)
            {
                services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<AppIdentityDbContext>()
                    .AddDefaultTokenProviders();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
