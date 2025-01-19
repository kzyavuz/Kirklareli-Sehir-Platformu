using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaMerkezleri.Areas.Member.Models;
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
    public class IlanController : Controller
    {
        private readonly IIlanService _ilanService;
        private readonly UserManager<AppUser> _userManager;
        private readonly Context _context;


        public IlanController(IIlanService ilanService, UserManager<AppUser> userManager, Context context)
        {
            _ilanService = ilanService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var values = _ilanService.TGetList().OrderByDescending(x => x.ilanDate).Where(i => i.AppUserId == user.Id).ToList();
            ViewBag.TotalIlan = values.Where(x => x.Status == "Onaylandı").Count();
            return View(values);
        }


        [Route("AddIlan")]
        [HttpGet]
        public IActionResult AddIlan()
        {
            var ilanTipleri = _context.IlanFormIsletmelers
            .Select(x => x.Name)
            .Distinct()
            .OrderBy(x => x)
            .ToList();

            ViewBag.IlanTipleri = ilanTipleri ?? new List<string>();

            return View();
        }

        [Route("AddIlan")]
        [HttpPost]
        public async Task<IActionResult> AddIlan(IlanModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user.UyeTipi != "PROFESYONEL")
            {
                var today = DateTime.Today;
                var currentDayOfWeek = (int)today.DayOfWeek;
                var startOfWeek = today.AddDays(-currentDayOfWeek + (currentDayOfWeek == 0 ? -6 : 1));

                var weeklyIlanCount = _context.Ilanlars
                    .Where(x => x.AppUserId == user.Id && x.ilanDate >= startOfWeek && x.ilanDate < startOfWeek.AddDays(7))
                    .Count();

                if (weeklyIlanCount >= 1)
                {
                    TempData["ErrorMessage"] = "Haftalık ilan limitine ulaştınız. Yalnızca 1 ilan paylaşabilirsiniz. Pazartesi tekrar dene!";
                    return RedirectToAction("AddIlan");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var ilan = new Ilanlar
            {
                IlanYeri = model.IlanYeri,
                İlanTelefonNo = model.İlanTelefonNo,
                İlanAcikklamasi = model.İlanAcikklamasi,
                IlanCesidi = model.IlanCesidi,
                İlanKonumu = model.İlanKonumu,
                ApprovalDateTime = model.ApprovalDateTime,
                AppUserId = user.Id,
                IlanBasligi = model.IlanBasligi,
                Status = "Onay Bekliyor",
                StandOutStatus = false,
                ilanDate = DateTime.Now
            };

            if (model.Image != null && model.Image.Length > 0)
            {
                var extension = Path.GetExtension(model.Image.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/IlanImage/", newImageName);
                using (var stream = new FileStream(location, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
                ilan.ImagePath = newImageName;
            }

            _ilanService.TAdd(ilan);
            return RedirectToAction("Index");
        }



        [Route("DeleteIlan/{id}")]
        public IActionResult DeleteIlan(int id)
        {
            var ilanlar = _ilanService.TGetByID(id);

            if (ilanlar == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(ilanlar.ImagePath))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/IsletmelerinResmi", ilanlar.ImagePath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _ilanService.TDelete(ilanlar);
            return RedirectToAction("Index");
        }


        [Route("UpdateIlan/{id}")]
        [HttpGet]
        public IActionResult UpdateIlan(int id)
        {
            var ilanTipleri = _context.IlanFormIsletmelers
            .Select(x => x.Name)
            .Distinct()
            .OrderBy(x => x)
            .ToList();

            ViewBag.IlanTipleri = ilanTipleri ?? new List<string>();

            var ilan = _ilanService.TGetByID(id);
            if (ilan == null)
            {
                return NotFound();
            }

            var model = new IlanModel
            {
                IlanYeri = ilan.IlanYeri,
                İlanTelefonNo = ilan.İlanTelefonNo,
                İlanAcikklamasi = ilan.İlanAcikklamasi,
                IlanCesidi = ilan.IlanCesidi,
                IlanBasligi = ilan.IlanBasligi,
                İlanKonumu = ilan.İlanKonumu,
                ApprovalDateTime = ilan.ApprovalDateTime,
                ImagePath = ilan.ImagePath
            };

            return View(model);
        }

        [Route("UpdateIlan/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateIlan(IlanModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var ilan = _ilanService.TGetByID(id);
            if (ilan == null)
            {
                return NotFound();
            }

            ilan.IlanYeri = model.IlanYeri;
            ilan.İlanTelefonNo = model.İlanTelefonNo;
            ilan.İlanAcikklamasi = model.İlanAcikklamasi;
            ilan.IlanCesidi = model.IlanCesidi;
            ilan.IlanBasligi = model.IlanBasligi;
            ilan.İlanKonumu = model.İlanKonumu;
            ilan.ApprovalDateTime = model.ApprovalDateTime;

            if (model.Image != null && model.Image.Length > 0)
            {
                var extension = Path.GetExtension(model.Image.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/IlanImage/", newImageName);
                using (var stream = new FileStream(location, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
                ilan.ImagePath = newImageName;
            }

            _ilanService.TUpdate(ilan);
            return RedirectToAction("Index");
        }

    }
}
