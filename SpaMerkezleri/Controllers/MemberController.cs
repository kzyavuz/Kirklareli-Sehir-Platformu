using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaMerkezleri.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace SpaMerkezleri.Controllers
{

    [Authorize]
    public class MemberController : Controller
    {
        private readonly IIsletmelerService _isletmelerService;
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public MemberController( IIsletmelerService isletmelerService, Context context, UserManager<AppUser> userManager)
        {
            _isletmelerService = isletmelerService;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult StandartUye()
        {

            var isletmeTipleri = _context.IsletmeTipleris
            .Select(x => x.Name)
            .Distinct()
            .OrderBy(x => x)
            .ToList();

            ViewBag.IsletmeTipleri = isletmeTipleri ?? new List<string>();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StandartUye(IsletmeForm p)
        {
            var user = await _userManager.GetUserAsync(User);
            var now = DateTime.Now;

            if (user != null && user.UyeTipi != "PROFESYONEL")
            {
                var userBusinessCount = _context.Isletmelers
                    .Where(x => x.AppUserId == user.Id)
                    .Count();

                if (userBusinessCount >= 1)
                {
                    TempData["ErrorMessage"] = "Yalnızca 1 işletme yayınlayabilirsiniz.";
                    return RedirectToAction("StandartUye"); // İlan ekleme sayfasına yönlendirme
                }
            }

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var s = new Isletmeler();
            s.AppUserId = user.Id;
            s.IsletmeUyeAdi = p.IsletmeUyeAdi;
            s.IsletmeUyeSoyAdi = p.IsletmeUyeSoyAdi;
            s.IsletmeUyeMail = p.IsletmeUyeMail;
            s.IsletmeTelefonNumarasi = p.IsletmeTelefonNumarasi;
            s.ISletmeAdi = p.ISletmeAdi;
            s.ISletmeTipi = p.IsletmeTipi;
            s.Status = "Onay Bekliyor";
            s.IsletmeAcikKonum = p.IsletmeAcikKonum;
            s.ISletmeNot = p.ISletmeNot;
            s.IsletmeInstagram = p.IsletmeInstagram;
            p.MessageDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            s.MessageDate = p.MessageDate;

            try
            {
                s.IsletmeResimi = await SaveImageAsync(p.IsletmeResimi);
                s.IsletmeResimi1 = await SaveImageAsync(p.IsletmeResimi1);
                s.IsletmeResimi2 = await SaveImageAsync(p.IsletmeResimi2);
                s.IsletmeResimi3 = await SaveImageAsync(p.IsletmeResimi3);
                s.IsletmeResimi4 = await SaveImageAsync(p.IsletmeResimi4);
                s.IsletmeResimi5 = await SaveImageAsync(p.IsletmeResimi5);
                s.IsletmeResimi6 = await SaveImageAsync(p.IsletmeResimi6);
                s.IsletmeLogo = await SaveImageAsync(p.IsletmeLogo);

                _isletmelerService.TAdd(s);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Isletme", new { area = "Member" });
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Bir hata oluştu: " + ex.InnerException?.Message);
                return View(p);
            }
        }


        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile == null) return null;

            var extension = Path.GetExtension(imageFile.FileName);
            var newImageName = Guid.NewGuid() + extension;
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/IsletmelerinResmi/", newImageName);

            using (var stream = new FileStream(location, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return newImageName;
        }
    }
}
