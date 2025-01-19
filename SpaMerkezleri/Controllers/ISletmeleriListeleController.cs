using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaMerkezleri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SpaMerkezleri.Controllers
{
    [AllowAnonymous]
    public class ISletmeleriListeleController : Controller
    {
        private readonly IIsletmelerService _ısletmelerService;

        public ISletmeleriListeleController(IIsletmelerService ısletmelerService)
        {
            _ısletmelerService = ısletmelerService;
        }

        public IActionResult Index(string isletmeTipi, int page = 1)
        {
            int pageSize = 12;
            var isletmeler = _ısletmelerService.TGetList().Where(x => x.Status == "Onaylandı").AsQueryable();

            if (!string.IsNullOrEmpty(isletmeTipi))
            {
                isletmeler = isletmeler.Where(i => i.ISletmeTipi == isletmeTipi);
            }

            // Alfabetik sıralama
            isletmeler = isletmeler.OrderBy(i => i.ISletmeAdi);

            var totalItems = isletmeler.Count();
            var isletmelerPaged = isletmeler.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var model = new IsletmeViewModel
            {
                Isletmeler = isletmelerPaged,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                isletmeTipi = isletmeTipi
            };

            return View(model);
        }

        public IActionResult StandOutIndex()
        {
            var result = _ısletmelerService.TGetList().Where(x => x.StandOutStatus == true).OrderByDescending(x => x.StandOutDateTime).ToList();
            ViewBag.TotalIsletme = result.Count();

            return View(result);
        }

        public IActionResult IsletmeDetayları(int id)
        {
            var values = _ısletmelerService.TGetByID(id);

            if (values == null)
            {
                return NotFound();
            }
            return View(values);
        }
    }
}
