using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using AspNetCoreTodo.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AspNetCoreTodo.Services;

namespace ASP_to_do
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Declaring (or "wiring up") which concrete class to use for each interface.
        /// </summary>
        /// <param name="services">adding things to the service container, 
        /// or the collection of services that ASP.NET Core knows about</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
           
            /// <summary>
            /// Telling ASP.NET Core to use FakeTodoItemService whenever ITodoItemService interface is requested.
            /// AddSingleton means that only one copy of the FakeTodoItemService is created,
            /// and it's reused whenever the service is requested. 
            /// </summary>
            /// <typeparam name="ITodoItemService">what the controller asks for</typeparam>
            /// <typeparam name="FakeTodoItemService">what ASP.NET Core automatically supply</typeparam>
            /// <returns></returns>
            services.AddSingleton<ITodoItemService, FakeTodoItemService>();

            /// <summary>
            /// Establishing a connection to database.
            /// </summary>
            /// <typeparam name="ApplicationDbContext">which context, connection string, database to use</typeparam>
            /// <returns></returns>
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            /// <summary>
            /// Updating service container: the new instance of TodoItemService will be created during each web request
            /// </summary>
            /// <typeparam name="ITodoItemService">the service class that interact with database</typeparam>
            /// <typeparam name="TodoItemService">replace FakeTodoItemService</typeparam>
            /// <returns></returns>
            // services.AddScoped<ITodoItemService, TodoItemService>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

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
