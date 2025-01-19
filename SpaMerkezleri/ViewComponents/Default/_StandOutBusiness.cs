using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SpaMerkezleri.ViewComponents.Default
{
    public class _StandOutBusiness: ViewComponent
    {
        private readonly IIsletmelerService _isletmelerService;

        public _StandOutBusiness(IIsletmelerService isletmelerService)
        {
            _isletmelerService = isletmelerService;
        }

        public IViewComponentResult Invoke()
        {
            var data = _isletmelerService.TGetList().Where(x=> x.StandOutStatus == true && x.Status == "Onaylandı").OrderByDescending(b => b.StandOutDateTime).Take(4).ToList();
            return View(data);
        }
    }
}
