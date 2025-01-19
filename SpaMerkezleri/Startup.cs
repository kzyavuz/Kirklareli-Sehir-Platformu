using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SpaMerkezleri
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
            // DbContext ve Identity ayarlarý
            services.AddDbContext<Context>();

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                // Þifre gereksinimlerini özelleþtir
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;
                options.User.AllowedUserNameCharacters = "abcçdefgðhýijklmnoöpqrsþtuüvwxyzABCÇDEFGÐHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789_";
            })
            .AddEntityFrameworkStores<Context>()
            .AddDefaultTokenProviders();

            //// Ýþ mantýðý ve veri eriþim katmaný baðýmlýlýklarýnýn eklenmesi;
            services.AddScoped<IAboutService, AboutManager>();
            services.AddScoped<IAboutDal, EFAboutDal>();

            services.AddScoped<IBannerService, BannerManager>();
            services.AddScoped<IBannerDal, EFBannerDal>();

            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<IBlogDal, EFBlogDal>();

            services.AddScoped<IIlanService, IlanManager>();
            services.AddScoped<IIlanDal, EFIlanDal>();

            services.AddScoped<IIsletmelerService, IsletmeManager>();
            services.AddScoped<IIsletmelerDal, EFIsletmelerDal>();

            services.AddScoped<ISectorService, SectorManager>();
            services.AddScoped<ISektorDal, EFSektorDal>();

            services.AddScoped<IUyelikService, UyelerManager>();
            services.AddScoped<IUyeliklerDal, EFUyeliklerDal>();

            services.AddScoped<IIlanFormIsletmelerService, IlanFormIsletmelerManager>();
            services.AddScoped<IIlanFormIsletmelerDal, EFIlanFormIsletmelerDal>();

            services.AddScoped<IIsletmeTipleriService, IsletmeTipleriManager>();
            services.AddScoped<IIsletmeTipleriDal, EFIsletmeTipleriDal>();

            services.AddScoped<IBlogCategoryService, BlogCategoryManager>();
            services.AddScoped<IBlogCategoryDal, EFBlogCategoryDal>();

            services.AddScoped<ISektorCategoryService, SektorCategoryManager>();
            services.AddScoped<ISektorCategoryDal, EFSektorCategoryDal>();

            services.AddScoped<ICityStartService, CityStartManager>();
            services.AddScoped<ICityStartDal, EFCityStartDal>();

            services.AddScoped<ICityStartListService, CityStartListManager>();
            services.AddScoped<ICityStartListDal, EFCityStartListDal>();

            services.AddScoped<IPharmacyService, PharmacyManager>();
            services.AddScoped<IPharmacyDal, EFPharmacyDal>();
            
            services.AddScoped<ICityDistrictService, CityDistrictManager>();
            services.AddScoped<ICityDistrictDal, EFCityDistrictDal>();

            // MVC ve Authorization Policy ayarlarý
            services.AddControllersWithViews(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            // Giriþ yolu ayarlarý
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Member/MemberLogin/MSignUp/";
            });
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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Area route tanýmý
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                // Genel default route tanýmý
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Default}/{action=Index}/{id?}");
            });
        }
    }
}
