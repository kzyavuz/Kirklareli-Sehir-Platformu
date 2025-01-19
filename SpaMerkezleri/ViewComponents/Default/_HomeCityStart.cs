using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.ViewComponents.Default
{
    public class _HomeCityStart: ViewComponent
    {
        private readonly ICityStartService _cityStartService;

        public _HomeCityStart(ICityStartService cityStartService)
        {
            _cityStartService = cityStartService;
        }

        public IViewComponentResult Invoke()
        {
            var data = _cityStartService.TGetList().Take(6).ToList();
            return View(data);
        }
    }
}
