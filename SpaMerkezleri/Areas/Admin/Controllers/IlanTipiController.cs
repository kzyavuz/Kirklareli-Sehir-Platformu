using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SpaMerkezleri.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class IlanTipiController : Controller
    {
        private readonly IIlanFormIsletmelerService _ilanFormIsletmelerService;

        public IlanTipiController(IIlanFormIsletmelerService ilanFormIsletmelerService)
        {
            _ilanFormIsletmelerService = ilanFormIsletmelerService;
        }

        public IActionResult Index()
        {
            var result = _ilanFormIsletmelerService.TGetList().OrderBy(x => x.Name).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult AddIlanTipi()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddIlanTipi(IlanFormIsletmeler ilanFormIsletmeler)
        {
            _ilanFormIsletmelerService.TAdd(ilanFormIsletmeler);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateIlanTipi(int id)
        {
            var result = _ilanFormIsletmelerService.TGetByID(id);
            return View(result);
        }

        [HttpPost]
        public IActionResult UpdateIlanTipi(IlanFormIsletmeler ilanFormIsletmeler)
        {
            _ilanFormIsletmelerService.TUpdate(ilanFormIsletmeler);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteIlanTipi(int id)
        {
            var result = _ilanFormIsletmelerService.TGetByID(id);
            _ilanFormIsletmelerService.TDelete(result);
            return RedirectToAction("Index");
        }
    }
}
