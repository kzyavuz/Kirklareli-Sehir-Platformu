using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Controllers
{
    [AllowAnonymous]
    public class CityStartController : Controller
    {
        private readonly ICityStartService _cityStartService;
        private readonly IAboutService _aboutService;

        public CityStartController(ICityStartService cityStartService, IAboutService aboutService)
        {
            _cityStartService = cityStartService;
            _aboutService = aboutService;
        }

        public IActionResult Index(string categoryName)
        {
            var data = _cityStartService.TGetList().AsQueryable();

            if (!string.IsNullOrEmpty(categoryName))
            {
                data = data.Where(x => x.CategoryName == categoryName); // Kategoriye göre filtreleme
            }

            var values = data.Take(24).ToList();

            ViewBag.TotalCount = data.Count();
            ViewBag.Img = _aboutService.TGetList().FirstOrDefault()?.AboutCityStartImg ?? "/images/about/default.png";
            ViewBag.Categories = _cityStartService.TGetList().Select(c => c.CategoryName).Distinct().ToList();

            return View(values);
        }

        public IActionResult Post(int id)
        {
            var blog = _cityStartService.TGetByID(id);
            return View(blog);
        }

        [HttpGet]
        public IActionResult GetMoreCityStart(int page, string categoryName)
        {
            int pageSize = 24;
            var data = _cityStartService.TGetList().AsQueryable();

            if (!string.IsNullOrEmpty(categoryName))
            {
                data = data.Where(x => x.CategoryName == categoryName);
            }

            var cityStarts = data.OrderByDescending(x => x.CreateDateTime)
                                 .Skip(page * pageSize)
                                 .Take(pageSize)
                                 .ToList();

            return PartialView("_CityStartPartial", cityStarts);
        }

    }
}

