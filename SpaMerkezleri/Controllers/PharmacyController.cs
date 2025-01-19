using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Controllers
{
    [AllowAnonymous]
    public class PharmacyController : Controller
    {
        private readonly IPharmacyService _pharmacyService;

        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        public IActionResult Index(string district)
        {
            var data = _pharmacyService.TGetList();
            var districts = data.Select(p => p.District).Distinct().OrderBy(d => d).ToList();

            ViewBag.Districts = districts;
            ViewBag.DateTime = DateTime.Now.ToString("dd MMMM yyyy - dddd");
            if (!string.IsNullOrEmpty(district))
            {
                data = data.Where(p => p.District == district).ToList();
            }

            return View(data);
        }
    }

}
