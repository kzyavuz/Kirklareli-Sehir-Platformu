using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=KOZLOW\\SQLEXPRESS;database=kirklareliSehir; integrated security=true;");
        }
        public DbSet<Isletmeler> Isletmelers { get; set; }
        public DbSet<Uyelikler> Uyeliklers { get; set; }
        public DbSet<Ilanlar> Ilanlars { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<banner> Banners { get; set; }
        public DbSet<Sektor> Sektors { get; set; }
        public DbSet<AboutMe> AboutMes { get; set; }
        public DbSet<IlanFormIsletmeler> IlanFormIsletmelers { get; set; }
        public DbSet<IsletmeTipleri> IsletmeTipleris { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<SektorCategory> SektorCategories { get; set; }
        public DbSet<CityStart> CityStarts { get; set; }
        public DbSet<CityStartList> CityStartLists { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<CityDistrict> CityDistricts { get; set; }
    }
}
