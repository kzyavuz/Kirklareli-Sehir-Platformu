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
    public class IsletmeTipiController : Controller
    {
        private readonly IIsletmeTipleriService _isletmeTipleriService;

        public IsletmeTipiController(IIsletmeTipleriService isletmeTipleriService)
        {
            _isletmeTipleriService = isletmeTipleriService;
        }

        public IActionResult Index()
        {
            var result = _isletmeTipleriService.TGetList().OrderBy(x => x.Name).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult AddIsletmeTipi()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddIsletmeTipi(IsletmeTipleri isletmeTipleri)
        {
            _isletmeTipleriService.TAdd(isletmeTipleri);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateIsletmeTipi(int id)
        {
            var result = _isletmeTipleriService.TGetByID(id);
            return View(result);
        }

        [HttpPost]
        public IActionResult UpdateIsletmeTipi(IsletmeTipleri isletmeTipleri)
        {
            _isletmeTipleriService.TUpdate(isletmeTipleri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteIsletmeTipi(int id)
        {
            var result = _isletmeTipleriService.TGetByID(id);
            _isletmeTipleriService.TDelete(result);
            return RedirectToAction("Index");
        }
    }
}
