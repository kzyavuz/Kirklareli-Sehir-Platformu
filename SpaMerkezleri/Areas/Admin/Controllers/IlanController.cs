using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
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

namespace SpaMerkezleri.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class IlanController : Controller
    {

        private readonly Context _context;
        private readonly IIlanService _ilanService;
        private readonly UserManager<AppUser> _userManager;

        public IlanController(Context context, IIlanService ilanService, UserManager<AppUser> userManager)
        {
            _context = context;
            _ilanService = ilanService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var values = _context.Ilanlars.Include(x => x.AppUser).Where(x => x.Status == "Onay Bekliyor").OrderByDescending(x => x.ilanDate).ToList();
            ViewBag.TotalIlan = _context.Ilanlars.Where(x => x.Status == "Onay Bekliyor").Count();
            return View(values);
        }

        public IActionResult ApprovedListIlan()
        {
            var values = _context.Ilanlars.Include(x => x.AppUser).Where(x => x.Status == "Onaylandı").OrderByDescending(x => x.ApprovalDateTime).ToList();
            ViewBag.TotalIlan = _context.Ilanlars.Where(x => x.Status == "Onaylandı").Count();
            return View(values);
        }

        public IActionResult RejectedListIlan()
        {
            var values = _context.Ilanlars.Include(x => x.AppUser).Where(x => x.Status == "Reddedildi").OrderByDescending(x => x.RejectionDateTime).ToList();
            ViewBag.TotalIlan = _context.Ilanlars.Where(x => x.Status == "Reddedildi").Count();
            return View(values);
        }

        [HttpPost]
        public IActionResult ConvertToTrue(int id, string returnURL)
        {
            _ilanService.ConvertToTrueIlan(id);
            return RedirectToAction(returnURL);
        }

        [HttpPost]
        public IActionResult ConvertToFalse(int id, string returnURL)
        {
            _ilanService.ConvertToFalseIlan(id);
            return RedirectToAction(returnURL);
        }

        [HttpPost]
        public IActionResult StandOutTrue(int id)
        {
            _ilanService.ConvertStandOutTrue(id);
            return RedirectToAction("ApprovedListIlan");
        }

        [HttpPost]
        public IActionResult StandOutFalse(int id)
        {
            _ilanService.ConvertStandOutFalse(id);
            return RedirectToAction("ApprovedListIlan");
        }

        [HttpGet]
        public IActionResult AddIlan()
        {
            var ilanTipleri = _context.IlanFormIsletmelers
            .Select(x => x.Name)
            .Distinct()
            .ToList();

            ViewBag.IlanTipleri = ilanTipleri ?? new List<string>();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddIlan(IlanModel model)
        {
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
                IlanBasligi = model.IlanBasligi,
                ApprovalDateTime = model.ApprovalDateTime,
                AppUserId = (await _userManager.GetUserAsync(User)).Id,
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

        [HttpPost]
        public IActionResult DeleteIlan(int id)
        {
            var ilan = _ilanService.TGetByID(id);
            if (ilan == null)
            {
                return NotFound();
            }

            // Mevcut resmi sil
            if (!string.IsNullOrEmpty(ilan.ImagePath))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/IlanImage/", ilan.ImagePath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            // İlanı veritabanından sil
            _ilanService.TDelete(ilan);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateIlan(int id)
        {

            var ilanTipleri = _context.IlanFormIsletmelers
            .Select(x => x.Name)
            .Distinct()
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
                İlanKonumu = ilan.İlanKonumu,
                IlanBasligi = ilan.IlanBasligi,
                ApprovalDateTime = ilan.ApprovalDateTime,
                ImagePath = ilan.ImagePath
            };

            return View(model);
        }

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
