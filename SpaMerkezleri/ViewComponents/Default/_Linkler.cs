using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.ViewComponents.Default
{
    public class _Linkler : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
