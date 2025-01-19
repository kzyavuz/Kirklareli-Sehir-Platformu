using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.ViewComponents.Default
{
    public class _Header : ViewComponent
    {
        private readonly IAboutService _aboutService;

        public _Header(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public IViewComponentResult Invoke()
        {
            var data = _aboutService.TGetList();
            return View(data);
        }
    }
}
