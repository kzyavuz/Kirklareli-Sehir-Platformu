using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SpaMerkezleri.ViewComponents.Default
{
    public class _HomeSektor : ViewComponent
    {
        private readonly ISectorService _sectorService;

        public _HomeSektor(ISectorService sectorService)
        {
            _sectorService = sectorService;
        }

        public IViewComponentResult Invoke()
        {
            var data = _sectorService.TGetList().OrderByDescending(b => b.Created).Take(4).ToList();
            return View(data);
        }
    }
}
