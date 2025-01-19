using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpaMerkezleri.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class UyeliklerController : Controller
    {
        private readonly IUyelikService _uyelikService;

        public UyeliklerController(IUyelikService uyelikService)
        {
            _uyelikService = uyelikService;
        }

        public IActionResult Index()
        {
            var values = _uyelikService.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddUyelik()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUyelik(Uyelikler uyelikler)
        {
            _uyelikService.TAdd(uyelikler);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteUyelik(int id)
        {
            var values = _uyelikService.TGetByID(id);
            _uyelikService.TDelete(values);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult UpdateUyelik(int id)
        {
            var values = _uyelikService.TGetByID(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult UpdateUyelik(Uyelikler uyelikler)
        {
            _uyelikService.TUpdate(uyelikler);
            return RedirectToAction("Index");
        }
    }
}
