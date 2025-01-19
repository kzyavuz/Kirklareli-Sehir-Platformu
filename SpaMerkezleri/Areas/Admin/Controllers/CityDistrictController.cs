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
    public class CityDistrictController : Controller
    {
        private readonly ICityDistrictService _cityDistrictService;

        public CityDistrictController(ICityDistrictService cityDistrictService)
        {
            _cityDistrictService = cityDistrictService;
        }

        public IActionResult Index()
        {
            var result = _cityDistrictService.TGetList().OrderBy(x => x.Name).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult AddCityDistrict()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCityDistrict(CityDistrict cityDistrict)
        {
            _cityDistrictService.TAdd(cityDistrict);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateCityDistrict(int id)
        {
            var result = _cityDistrictService.TGetByID(id);
            return View(result);
        }

        [HttpPost]
        public IActionResult UpdateCityDistrict(CityDistrict cityDistrict)
        {
            _cityDistrictService.TUpdate(cityDistrict);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteCityDistrict(int id)
        {
            var result = _cityDistrictService.TGetByID(id);
            _cityDistrictService.TDelete(result);
            return RedirectToAction("Index");
        }
    }
}
