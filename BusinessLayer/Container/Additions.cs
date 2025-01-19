//using BusinessLayer.Abstract;
//using BusinessLayer.Concrete;
//using DataAccessLayer.Abstract;
//using DataAccessLayer.EntityFramework;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BusinessLayer.Container
//{
//    public static class Additions
//    {
//        public static void ContainerDependencies(this IServiceCollection services)
//        {
//            services.AddScoped<IAboutService, AboutManager>();
//            services.AddScoped<IAboutDal, EFAboutDal>();

//            services.AddScoped<IBannerService, BannerManager>();
//            services.AddScoped<IBannerDal, EFBannerDal>();

//            services.AddScoped<IBannerService, BannerManager>();
//            services.AddScoped<IBlogDal, EFBlogDal>();

//            services.AddScoped<IIlanService, IlanManager>();
//            services.AddScoped<IIlanDal, EFIlanDal>();

//            services.AddScoped<IIsletmelerService, IsletmeManager>();
//            services.AddScoped<IIsletmelerDal, EFIsletmelerDal>();

//            services.AddScoped<ISectorService, SectorManager>();
//            services.AddScoped<ISektorDal, EFSektorDal>();

//            services.AddScoped<IUyelikService, UyelerManager>();
//            services.AddScoped<IUyeliklerDal, EFUyeliklerDal>();
//        }
//    }
//}