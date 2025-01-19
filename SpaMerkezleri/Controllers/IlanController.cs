using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Controllers
{
    [AllowAnonymous]
    public class IlanController : Controller
    {
        private readonly Context _context;

        public IlanController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(string ilanCesidi)
        {
            var ilanlar = _context.Ilanlars.Include(x => x.AppUser)
                                           .Where(x => x.Status == "Onaylandı")
                                           .OrderByDescending(x => x.ApprovalDateTime)
                                           .ToList();

            if (!string.IsNullOrEmpty(ilanCesidi))
            {
                ilanlar = ilanlar.Where(x => x.IlanCesidi == ilanCesidi).ToList();
            }

            ViewBag.TotalIlan = ilanlar.Count();
            ViewBag.IlanTurleri = _context.Ilanlars.Where(x => x.Status == "Onaylandı")
                                                   .Select(x => x.IlanCesidi)
                                                   .Distinct()
                                                   .OrderBy(x => x)
                                                   .ToList();
            ViewBag.Img = _context.AboutMes.FirstOrDefault()?.AboutIlanImg ?? "/images/about/default.png";

            return View(ilanlar);
        }


        public IActionResult StandOutIndex()
        {
            var ilanlar = _context.Ilanlars.Include(x => x.AppUser).Where(x => x.Status == "Onaylandı" && x.StandOutStatus == true).OrderByDescending(x => x.StandOutDateTime).ToList();

            ViewBag.TotalIlan = _context.Ilanlars.Where(x => x.StandOutStatus == true).Count();
            ViewBag.IlanTurleri = _context.Ilanlars.Where(x => x.Status == "Onaylandı" && x.StandOutStatus == true).Select(x => x.IlanCesidi).Distinct().OrderBy(x => x).ToList();

            return View(ilanlar);
        }

        public IActionResult IlanUrl()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MSignUp", "MemberLogin", new { area = "Member", returnUrl = Url.Action("AddIlan", "Ilan", new { area = "Member" }) });
            }
            else
            {
                return RedirectToAction("AddIlan", "Ilan", new { area = "Member" });
            }
        }

        public IActionResult Details(int id)
        {
            var ilan = _context.Ilanlars.Include(x => x.AppUser).FirstOrDefault(x => x.IlanID == id);

            if (ilan == null)
            {
                return NotFound();
            }

            ViewBag.Latitude = ilan.Latitude;
            ViewBag.Longitude = ilan.Longitude;

            return View(ilan);
        }
    }
}
