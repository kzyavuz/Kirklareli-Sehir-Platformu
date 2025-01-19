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
    public class SektorCategoryController : Controller
    {

        private readonly ISektorCategoryService _sektorCategoryService;

        public SektorCategoryController(ISektorCategoryService sektorCategoryService)
        {
            _sektorCategoryService = sektorCategoryService;
        }

        public IActionResult Index()
        {
            var data = _sektorCategoryService.TGetList().OrderBy(x => x.CategoryName).ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult AddSektorCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSektorCategory(SektorCategory sektorCategory)
        {
            _sektorCategoryService.TAdd(sektorCategory);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateSektorCategory(int id)
        {
            var values = _sektorCategoryService.TGetByID(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult UpdateSektorCategory(SektorCategory sektorCategory)
        {
            _sektorCategoryService.TUpdate(sektorCategory);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteSektorCategory(int id)
        {
            var result = _sektorCategoryService.TGetByID(id);
            _sektorCategoryService.TDelete(result);
            return RedirectToAction("Index");
        }
    }
}
