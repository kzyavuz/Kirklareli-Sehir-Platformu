using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaMerkezleri.Areas.Admin.Models;
using System;
using System.IO;

namespace SpaMerkezleri.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public IActionResult Index()
        {
            var data = _bannerService.TGetList();
            return View(data);
        }

        [HttpGet]
        public IActionResult AddBanner()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBanner(bannerModel p)
        {
            banner banner = new banner();
            if (p.bannerImage != null)
            {
                var extension = Path.GetExtension(p.bannerImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/bannerImage/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.bannerImage.CopyTo(stream);
                banner.BannerImg = newimagename;
            }
            banner.BannerUrl = p.bannerUrl;
            banner.BannerStatus = false;
            _bannerService.TAdd(banner);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateBanner(int id)
        {
            var service = _bannerService.TGetByID(id);
            if (service == null)
            {
                return NotFound();
            }

            var model = new bannerModel
            {
                id = service.BannerID,
                bannerUrl = service.BannerUrl,
                ExistingImagePath = service.BannerImg // Mevcut resim dosyası yolunu sakla
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateBanner(bannerModel model)
        {
            if (ModelState.IsValid)
            {
                var service = _bannerService.TGetByID(model.id);
                if (service == null)
                {
                    return NotFound();
                }

                service.BannerUrl = model.bannerUrl;

                if (model.NewImageFile != null)
                {
                    // Eski resim dosyasını sil
                    if (!string.IsNullOrEmpty(service.BannerImg))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/bannerImage/", service.BannerImg);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Yeni resim dosyasını yükle
                    var extension = Path.GetExtension(model.NewImageFile.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/bannerImage/", newImageName);
                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                    {
                        model.NewImageFile.CopyTo(stream);
                    }

                    // Yeni resim dosyası bilgisi güncellenir
                    service.BannerImg = newImageName;
                }

                _bannerService.TUpdate(service);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteBanner(int id)
        {
            var service = _bannerService.TGetByID(id);
            if (service == null)
            {
                return NotFound();
            }

            // Resim dosyası silinir
            if (!string.IsNullOrEmpty(service.BannerImg))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/bannerImage/", service.BannerImg);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            // Servis silinir
            _bannerService.TDelete(service);

            return RedirectToAction("Index");
        }

        public IActionResult Status(int id)
        {
            var banner = _bannerService.TGetByID(id);
            if (banner != null)
            {
                banner.BannerStatus = !banner.BannerStatus;
                _bannerService.TUpdate(banner);
            }
            return RedirectToAction("Index");
        }
    }
}
