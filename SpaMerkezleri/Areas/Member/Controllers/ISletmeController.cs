using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
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

namespace SpaMerkezleri.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "member")]
    [Route("Member/[controller]/[action]")]
    public class ISletmeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IIsletmelerService _ısletmelerService;
        private readonly Context _context;

        public ISletmeController(UserManager<AppUser> userManager, IIsletmelerService ısletmelerService, Context context)
        {
            _userManager = userManager;
            _ısletmelerService = ısletmelerService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var isletmeler = await _context.Isletmelers.OrderByDescending(x => x.MessageDate).Where(i => i.AppUserId == user.Id).ToListAsync();
            return View(isletmeler);
        }


        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isletme = await _context.Isletmelers.FindAsync(id);
            if (isletme == null)
            {
                return NotFound();
            }

            // Resim dosyasını silme
            if (!string.IsNullOrEmpty(isletme.IsletmeResimi))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/IsletmelerinResmi", isletme.IsletmeResimi);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            // logo dosyasını silme
            if (!string.IsNullOrEmpty(isletme.IsletmeLogo))
            {
                var imagePathe = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/IsletmelerinResmi", isletme.IsletmeLogo);
                if (System.IO.File.Exists(imagePathe))
                {
                    System.IO.File.Delete(imagePathe);
                }
            }

            _context.Isletmelers.Remove(isletme);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("StandartUyeUpdate/{id}")]
        public IActionResult StandartUyeUpdate(int id)
        {
            var isletmeTipleri = _context.IsletmeTipleris
            .Select(x => x.Name)
            .Distinct()
            .OrderBy(x => x)
            .ToList();

            ViewBag.IsletmeTipleri = isletmeTipleri ?? new List<string>();

            var memberCard = _ısletmelerService.TGetByID(id);
            var model = new UpdateIsletmeForm
            {
                IsletmeID = memberCard.IsletmeID,
                IsletmeUyeAdi = memberCard.IsletmeUyeAdi,
                IsletmeUyeSoyAdi = memberCard.IsletmeUyeSoyAdi,
                IsletmeUyeMail = memberCard.IsletmeUyeMail,
                IsletmeTelefonNumarasi = memberCard.IsletmeTelefonNumarasi,
                ISletmeAdi = memberCard.ISletmeAdi,
                IsletmeInstagram = memberCard.IsletmeInstagram,
                IsletmeAcikKonum = memberCard.IsletmeAcikKonum,
                ISletmeNot = memberCard.ISletmeNot,
                LogoYolu = memberCard.IsletmeLogo,
                IsletmeTipi = memberCard.ISletmeTipi,
                ExistingImagePath = memberCard.IsletmeResimi,
                ExistingImagePath1 = memberCard.IsletmeResimi1,
                ExistingImagePath2 = memberCard.IsletmeResimi2,
                ExistingImagePath3 = memberCard.IsletmeResimi3,
                ExistingImagePath4 = memberCard.IsletmeResimi4,
                ExistingImagePath5 = memberCard.IsletmeResimi5,
                ExistingImagePath6 = memberCard.IsletmeResimi6

            };
            return View(model);
        }

        [HttpPost]
        [Route("StandartUyeUpdate/{id}")]
        public async Task<IActionResult> StandartUyeUpdate(UpdateIsletmeForm p)
        {
            if (ModelState.IsValid)
            {
                var existingIsletme = _ısletmelerService.TGetByID(p.IsletmeID);

                if (existingIsletme == null)
                {
                    return NotFound();
                }

                // Resimleri işle
                string[] imageFields = { "IsletmeResimi", "IsletmeResimi1", "IsletmeResimi2", "IsletmeResimi3", "IsletmeResimi4", "IsletmeResimi5", "IsletmeResimi6", "IsletmeLogo" };
                foreach (var field in imageFields)
                {
                    var property = typeof(UpdateIsletmeForm).GetProperty(field);
                    var existingProperty = typeof(Isletmeler).GetProperty(field);
                    var file = property.GetValue(p) as IFormFile;
                    var existingFileName = existingProperty.GetValue(existingIsletme) as string;

                    var newFileName = await UpdateImageAsync(file, existingFileName);
                    existingProperty.SetValue(existingIsletme, newFileName);
                }

                // Diğer alanları güncelle
                existingIsletme.IsletmeUyeAdi = p.IsletmeUyeAdi;
                existingIsletme.IsletmeUyeSoyAdi = p.IsletmeUyeSoyAdi;
                existingIsletme.IsletmeUyeMail = p.IsletmeUyeMail;
                existingIsletme.IsletmeTelefonNumarasi = p.IsletmeTelefonNumarasi;
                existingIsletme.ISletmeAdi = p.ISletmeAdi;
                existingIsletme.ISletmeTipi = p.IsletmeTipi;
                existingIsletme.IsletmeInstagram = p.IsletmeInstagram;
                existingIsletme.IsletmeAcikKonum = p.IsletmeAcikKonum;
                existingIsletme.ISletmeNot = p.ISletmeNot;
                existingIsletme.MessageDate = DateTime.Now;

                _ısletmelerService.TUpdate(existingIsletme);
                return RedirectToAction("Index");
            }
            return View(p);
        }

        private async Task<string> UpdateImageAsync(IFormFile newImage, string existingImagePath)
        {
            if (newImage != null)
            {
                // Eski dosyayı sil
                if (!string.IsNullOrEmpty(existingImagePath))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/IsletmelerinResmi/", existingImagePath);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Yeni dosyayı kaydet
                var extension = Path.GetExtension(newImage.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/IsletmelerinResmi/", newImageName);

                using (var stream = new FileStream(location, FileMode.Create))
                {
                    await newImage.CopyToAsync(stream);
                }

                return newImageName;
            }

            return existingImagePath;
        }
    }
}