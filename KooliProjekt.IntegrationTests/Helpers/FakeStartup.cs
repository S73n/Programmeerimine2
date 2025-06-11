using System;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace KooliProjekt.IntegrationTests.Helpers
{
    public class FakeStartup //: Startup
    {
        public FakeStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            var dbGuid = Guid.NewGuid().ToString();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase($"TestDb_{dbGuid}");
            });

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddAutoMapper(GetType().Assembly);
            services.AddControllersWithViews()
                    .AddApplicationPart(typeof(HomeController).Assembly);

            // Register all services needed by controllers
            services.AddScoped<KooliProjekt.Service.IProductService, KooliProjekt.Service.ProductService>();
            services.AddScoped<KooliProjekt.Service.IOrderService, KooliProjekt.Service.OrderService>();
            services.AddScoped<KooliProjekt.Service.IInvoiceService, KooliProjekt.Service.InvoiceService>();
            services.AddScoped<KooliProjekt.Service.IIngredientService, KooliProjekt.Service.IngredientService>();
            services.AddScoped<KooliProjekt.Service.ICustomerService, KooliProjekt.Service.CustomerService>();
            services.AddScoped<KooliProjekt.Service.IProductCategoryService, KooliProjekt.Service.ProductCategoryService>();
            services.AddScoped<KooliProjekt.Service.IBatchService, KooliProjekt.Service.BatchService>();
            services.AddScoped<KooliProjekt.Service.IBeerService, KooliProjekt.Service.BeerService>();
            services.AddScoped<KooliProjekt.Service.IInvoiceLineService, KooliProjekt.Service.InvoiceLineService>();
            services.AddScoped<KooliProjekt.Service.IUserService, KooliProjekt.Service.UserService>();
            services.AddScoped<KooliProjekt.Service.IUserRoleService, KooliProjekt.Service.UserRoleService>();
            services.AddScoped<KooliProjekt.Service.ICommentService, KooliProjekt.Service.CommentService>();
            services.AddScoped<KooliProjekt.Service.ILogEntryService, KooliProjekt.Service.LogEntryService>();
            services.AddScoped<KooliProjekt.Service.IPhotoService, KooliProjekt.Service.PhotoService>();
            services.AddScoped<KooliProjekt.Service.ITastingLogService, KooliProjekt.Service.TastingLogService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{pathStr?}");
            });
        }

        //private void EnsureDatabase(ApplicationDbContext dbContext)
        //{
        //    dbContext.Database.EnsureDeleted();
        //    dbContext.Database.EnsureCreated();

        //    if (!dbContext.Degustation.Any() || !dbContext.Batch.Any() || !dbContext.BatchIngredient.Any() || !dbContext.Batchlog.Any() || !dbContext.Batch.Any() || !dbContext.User.Any())
        //    {
        //        SeedData.Initialize(dbContext);
        //    }
        //}
    }
}