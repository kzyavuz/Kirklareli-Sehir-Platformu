using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SpaMerkezleri.ViewComponents.Default
{
    public class _TaxiButton : ViewComponent
    {
        private readonly IAboutService _aboutService;

        public _TaxiButton(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public IViewComponentResult Invoke()
        {
            var lastItem = _aboutService.TGetList()
                .OrderByDescending(item => item.AboutID)
                .FirstOrDefault();

            var TaxiLink = lastItem?.TaxiLink;

            return View("Default", TaxiLink);

        }
    }
}
