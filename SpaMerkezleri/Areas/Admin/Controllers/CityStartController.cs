using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaMerkezleri.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpaMerkezleri.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class CityStartController : Controller
    {
        private readonly Context _context;
        private readonly ICityStartService _cityStartService;

        public CityStartController(Context context, ICityStartService cityStartService)
        {
            _context = context;
            _cityStartService = cityStartService;
        }

        public async Task<IActionResult> Index(string categoryName)
        {
            // Tüm kategorileri ViewBag içine atıyoruz
            ViewBag.Category = await _context.CityStartLists.Select(x => x.CategoryName).Distinct().ToListAsync();

            // Tüm kayıtları sorgulamak için AsQueryable kullanıyoruz
            var values = _context.CityStarts.AsQueryable();

            // Kategori seçildiyse, filtre uygula
            if (!string.IsNullOrEmpty(categoryName))
            {
                values = values.Where(x => x.CategoryName == categoryName);
            }

            // Toplam yazı sayısını ViewBag'e atıyoruz
            ViewBag.TotalCityStar = await values.CountAsync();

            // Sonuçları tarihe göre sıralayıp listeliyoruz
            var list = await values.OrderByDescending(x => x.CreateDateTime).ToListAsync();

            return View(list);
        }


        [HttpGet]
        public IActionResult AddCityStart()
        {
            ViewBag.Category = _context.CityStartLists.Select(x => x.CategoryName).Distinct().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddCityStart(AddBlogModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            CityStart cityStart = new();

            if (model.Image != null && model.Image.Length > 0)
            {
                try
                {
                    var extension = Path.GetExtension(model.Image.FileName).ToLower();
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("", "Geçersiz dosya formatı. Lütfen sadece resim yükleyin.");
                        return View(model);
                    }

                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/CityStart/", newImageName);

                    using (var stream = new FileStream(location, FileMode.Create))
                    {
                        model.Image.CopyTo(stream);
                    }

                    cityStart.ImagePath = newImageName;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Resim yükleme sırasında bir hata oluştu.");
                    return View(model);
                }
            }

            cityStart.Title = model.Title;
            cityStart.Content = model.DetailDescription;
            cityStart.CreateDateTime = DateTime.Now;
            cityStart.CategoryName = model.BlogCategory;

            _cityStartService.TAdd(cityStart);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult DeleteCityStart(int id)
        {
            var blog = _cityStartService.TGetByID(id);
            if (blog == null)
            {
                return NotFound();
            }

            // Resim dosyasını sil
            if (!string.IsNullOrEmpty(blog.ImagePath))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/CityStart/", blog.ImagePath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _cityStartService.TDelete(blog);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateCityStart(int id)
        {
            ViewBag.Category = _context.CityStartLists.Select(x => x.CategoryName).Distinct().ToList();
            var blog = _cityStartService.TGetByID(id);

            if (blog == null)
            {
                return NotFound();
            }

            var model = new UpdateBlogModel
            {
                Id = blog.Id,
                Title = blog.Title,
                BlogCategory = blog.CategoryName,
                DetailDescription = blog.Content,
                ImageUrl = blog.ImagePath
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateCityStart(UpdateBlogModel model)
        {
            if (ModelState.IsValid)
            {
                var _bM = _cityStartService.TGetByID(model.Id);
                if (_bM == null)
                {
                    return NotFound();
                }

                _bM.Title = model.Title;
                _bM.Content = model.DetailDescription;
                _bM.CategoryName = model.BlogCategory;

                if (model.NewImageFile != null)
                {
                    // Eski resim dosyasını sil
                    if (!string.IsNullOrEmpty(_bM.ImagePath))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/CityStart/", _bM.ImagePath);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Yeni resim dosyasını yükle
                    var extension = Path.GetExtension(model.NewImageFile.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/CityStart/", newImageName);
                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                    {
                        model.NewImageFile.CopyTo(stream);
                    }

                    // Yeni resim dosyası bilgisi güncellenir
                    _bM.ImagePath = newImageName;
                }

                _cityStartService.TUpdate(_bM);
                return RedirectToAction("Index");
            }

            // Validasyon hatası varsa formu tekrar göster
            return View(model);
        }

    }
}
