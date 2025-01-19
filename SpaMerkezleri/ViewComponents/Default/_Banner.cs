using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace SpaMerkezleri.ViewComponents
{
    public class _Banner : ViewComponent
    {
        private readonly IBannerService _bannerService;

        public _Banner(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public IViewComponentResult Invoke()
        {
            var activeBanners = _bannerService.TGetList().Where(x => x.BannerStatus == true).ToList();

            if (activeBanners.Any())
            {
                var random = new Random();
                var randomBanner = activeBanners[random.Next(activeBanners.Count)];
                return View(randomBanner);
            }

            return Content("");
        }
    }
}
