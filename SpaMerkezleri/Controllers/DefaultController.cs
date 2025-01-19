using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        private readonly Context _context;

        public DefaultController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var isletmeTipleri = _context.Isletmelers.Where(u => u.Status == "Onaylandı").Select(u => u.ISletmeTipi).Distinct().ToList();

            ViewBag.IsletmeTipleri = isletmeTipleri ?? new List<string>();

            ViewBag.Img = _context.AboutMes.FirstOrDefault()?.AboutImg ?? "/images/about/default.png";
            ViewBag.Logo = _context.AboutMes.FirstOrDefault()?.LogoImg ?? "/images/about/default.png";
            //Console.WriteLine(deneme());
            return View();
        }
        //public string deneme()
        //{
        //    string password = "";

        //    // PasswordHasher sınıfını kullanarak şifreyi hash'le
        //    var passwordHasher = new PasswordHasher<object>();
        //    string hashedPassword = passwordHasher.HashPassword(null, password);

        //    // Hashlenmiş şifreyi ekrana yazdır
        //    return hashedPassword;
        //}
    }
}
