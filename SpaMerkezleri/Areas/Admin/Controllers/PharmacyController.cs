using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class PharmacyController : Controller
    {
        private readonly IPharmacyService _pharmacyService;
        private readonly ICityDistrictService _cityDistrictService;

        public PharmacyController(IPharmacyService pharmacyService, ICityDistrictService cityDistrictService)
        {
            _pharmacyService = pharmacyService;
            _cityDistrictService = cityDistrictService;
        }

        public IActionResult Index()
        {
            var data = _pharmacyService.TGetList();
            return View(data);
        }

        [HttpGet]
        public IActionResult AddPharmacy()
        {
            ViewBag.District = _cityDistrictService.TGetList().OrderBy(x=>x.Name).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult AddPharmacy(Pharmacy pharmacy)
        {
            _pharmacyService.TAdd(pharmacy);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdatePharmacy(int id)
        {
            ViewBag.District = _cityDistrictService.TGetList().OrderBy(x => x.Name).ToList();

            var values = _pharmacyService.TGetByID(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult UpdatePharmacy(Pharmacy pharmacy)
        {
            _pharmacyService.TUpdate(pharmacy);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeletePharmacy(int id)
        {
            var values = _pharmacyService.TGetByID(id);
            _pharmacyService.TDelete(values);
            return RedirectToAction("Index");
        }
    }
}
