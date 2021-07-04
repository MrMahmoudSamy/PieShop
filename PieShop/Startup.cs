using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PieShop.Data;
using PieShop.Repositories;

namespace PieShop
{
    public class Startup
    {
        public IConfiguration Configuration { get;  }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(Configuration.GetConnectionString("CoffeeShop")));
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
            services.AddScoped<IProductsRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));
            services.AddHttpContextAccessor();
            services.AddSession();
           // services.AddMvc(option => option.EnableEndpointRouting = false);
               
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddRouting(option => {
                option.LowercaseUrls = true;
                option.AppendTrailingSlash = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "Slugy",
            //        template: "v/{id}/{Slug}",
            //        defaults: new { controller = "Product", action = "Index" });
            //});

            //app.UseMvc(routes =>
            //{


            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

               // endpoints.MapControllerRoute(
               //     name: "product",
               //pattern: "product/{*index}",
               //defaults: new { controller = "Product", action = "Index" });

                // endpoints.MapControllerRoute(
                //     name: "blog",
                //pattern: "blog/{*article}",
                //defaults: new { controller = "Blog", action = "Article" });


                //endpoints.MapControllerRoute(
                //name: "default",
                //pattern: "{controller=Home}/{action=Index}/{id?}");


                endpoints.MapRazorPages();
        });

        }
    }
}
