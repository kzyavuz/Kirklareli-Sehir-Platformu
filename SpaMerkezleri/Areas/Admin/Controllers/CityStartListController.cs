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
    public class CityStartListController : Controller
    {
        private readonly ICityStartListService _cityStartListService;

        public CityStartListController(ICityStartListService cityStartListService)
        {
            _cityStartListService = cityStartListService;
        }

        public IActionResult Index()
        {
            var result = _cityStartListService.TGetList().OrderBy(x => x.CategoryName).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult AddCityStartList()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCityStartList(CityStartList cityStartList)
        {
            _cityStartListService.TAdd(cityStartList);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateCityStartList(int id)
        {
            var result = _cityStartListService.TGetByID(id);
            return View(result);
        }

        [HttpPost]
        public IActionResult UpdateCityStartList(CityStartList cityStartList)
        {
            _cityStartListService.TUpdate(cityStartList);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteCityStartList(int id)
        {
            var result = _cityStartListService.TGetByID(id);
            _cityStartListService.TDelete(result);
            return RedirectToAction("Index");
        }
    }
}

