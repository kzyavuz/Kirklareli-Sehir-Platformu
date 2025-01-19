using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace SpaMerkezleri.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public IActionResult Index()
        {
            var values = _aboutService.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAbout(AboutMe about, IFormFile aboutImg, IFormFile aboutBlogImg, IFormFile aboutSektorImg, IFormFile aboutIlanImg, IFormFile logoImg, IFormFile aboutCityStartImg)
        {
            if (!ModelState.IsValid)
            {
                return View(about);
            }

            about.AboutImg = aboutImg != null ? SaveImage(aboutImg) : null;
            about.AboutBlogImg = aboutBlogImg != null ? SaveImage(aboutBlogImg) : null;
            about.AboutSektorImg = aboutSektorImg != null ? SaveImage(aboutSektorImg) : null;
            about.AboutIlanImg = aboutIlanImg != null ? SaveImage(aboutIlanImg) : null;
            about.LogoImg = logoImg != null ? SaveImage(logoImg) : null;
            about.AboutCityStartImg = aboutCityStartImg != null ? SaveImage(aboutCityStartImg) : null;

            _aboutService.TAdd(about);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            var values = _aboutService.TGetByID(id);
            if (values == null)
            {
                return NotFound();
            }
            return View(values);
        }

        [HttpPost]
        public IActionResult UpdateAbout(AboutMe about, IFormFile aboutImg, IFormFile aboutBlogImg, IFormFile aboutSektorImg, IFormFile aboutIlanImg, IFormFile logoImg, IFormFile aboutCityStartImg)
        {
            var existingAbout = _aboutService.TGetByID(about.AboutID);

            if (!ModelState.IsValid)
            {
                return View(about);  // Validation hatalarını gösterir.
            }

            // Resim yükleme işlemleri
            about.AboutImg = aboutImg != null ? SaveImage(aboutImg, existingAbout.AboutImg) : existingAbout.AboutImg;
            about.AboutBlogImg = aboutBlogImg != null ? SaveImage(aboutBlogImg, existingAbout.AboutBlogImg) : existingAbout.AboutBlogImg;
            about.AboutSektorImg = aboutSektorImg != null ? SaveImage(aboutSektorImg, existingAbout.AboutSektorImg) : existingAbout.AboutSektorImg;
            about.AboutIlanImg = aboutIlanImg != null ? SaveImage(aboutIlanImg, existingAbout.AboutIlanImg) : existingAbout.AboutIlanImg;
            about.LogoImg = logoImg != null ? SaveImage(logoImg, existingAbout.LogoImg) : existingAbout.LogoImg;
            about.AboutCityStartImg = aboutCityStartImg != null ? SaveImage(aboutCityStartImg, existingAbout.AboutCityStartImg) : existingAbout.AboutCityStartImg;

            _aboutService.TUpdate(about);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteAbout(int id)
        {
            var values = _aboutService.TGetByID(id);
            if (values == null)
            {
                return NotFound();
            }

            // Fotoğrafların silinmesi
            DeleteImage(values.AboutImg);
            DeleteImage(values.AboutBlogImg);
            DeleteImage(values.AboutSektorImg);
            DeleteImage(values.AboutIlanImg);
            DeleteImage(values.LogoImg);
            DeleteImage(values.AboutCityStartImg);

            _aboutService.TDelete(values);
            return RedirectToAction("Index");
        }

        // Resim Kaydetme ve Mevcut Resmi Silme Fonksiyonu
        private string SaveImage(IFormFile imageFile, string oldImagePath = null)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Eski resim varsa sil
                if (!string.IsNullOrEmpty(oldImagePath))
                {
                    DeleteImage(oldImagePath);
                }

                // Resmi kaydetmek için klasör oluşturulması
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                return "/images/about/" + uniqueFileName;
            }

            return null;
        }

        // Resim Silme Fonksiyonu
        private void DeleteImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }
    }
}
