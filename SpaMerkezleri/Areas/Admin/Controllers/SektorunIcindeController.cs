using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaMerkezleri.Areas.Admin.Models;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpaMerkezleri.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class SektorunIcindeController : Controller
    {
        private readonly Context _context;
        private readonly ISectorService _sectorService;

        public SektorunIcindeController(Context context, ISectorService sectorService)
        {
            _context = context;
            _sectorService = sectorService;
        }

        public async Task<IActionResult> Index(string categoryName)
        {
            ViewBag.SektorCategory = _context.SektorCategories.Select(x => x.CategoryName).Distinct().ToList();

            var values = _context.Sektors.Include(x => x.AppUser).AsQueryable();

            if (!string.IsNullOrEmpty(categoryName))
            {
                values = values.Where(x => x.CategoryName == categoryName);
            }

            ViewBag.sektorCount = values.Count();
            ViewBag.SelectedCategory = categoryName;

            var list = await values.OrderByDescending(x => x.Created).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult AddBlog()
        {
            ViewBag.SektorCategory = _sectorService.TListSektorCategory();

            return View();
        }

        [HttpPost]
        public IActionResult AddBlog(AddBlogModel p)
        {
            // Kullanıcı kimliğini al
            var adminUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Kullanıcı kimliğini int'e çevir
            if (int.TryParse(adminUserId, out int userId))
            {
                // Blog nesnesi oluştur
                Sektor s = new Sektor();

                // Eğer resim varsa işle
                if (p.Image != null)
                {
                    var extension = Path.GetExtension(p.Image.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/sektorImage/", newImageName);

                    // Resmi kaydet
                    using (var stream = new FileStream(location, FileMode.Create))
                    {
                        p.Image.CopyTo(stream);
                    }

                    // Resim yolu Blog nesnesine ekle
                    s.ImagePath = newImageName;
                }

                // Diğer Blog özelliklerini ayarla
                s.Title = p.Title;
                s.SubTitle = p.Description;
                s.Content = p.DetailDescription;
                s.Created =DateTime.Now;
                s.AppUserID = userId;
                s.CategoryName = p.SektorCategory;

                try
                {
                    _sectorService.TAdd(s);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Sektor yazısı eklenirken bir hata oluştu.");

                }
            }
            ModelState.AddModelError("", "Kullanıcı kimliği alınamadı.");
            return View(p);
        }

        [HttpPost]
        public IActionResult DeleteBlog(int id)
        {
            var blog = _sectorService.TGetByID(id);
            if (blog == null)
            {
                return NotFound();
            }

            // Resim dosyasını sil
            if (!string.IsNullOrEmpty(blog.ImagePath))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/sektorImage/", blog.ImagePath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _sectorService.TDelete(blog);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateBlog(int id)
        {
            ViewBag.SektorCategory = _sectorService.TListSektorCategory();

            var blog = _sectorService.TGetByID(id);
            if (blog == null)
            {
                return NotFound();
            }

            var model = new UpdateBlogModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.SubTitle,
                DetailDescription = blog.Content,
                SektorCategory = blog.CategoryName,
                ImageUrl = blog.ImagePath // Mevcut resim dosyası yolunu sakla
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateBlog(UpdateBlogModel model)
        {
            if (ModelState.IsValid)
            {
                var adminUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(adminUserId, out int userId))
                {
                    var _bM = _sectorService.TGetByID(model.Id);
                    if (_bM == null)
                    {
                        return NotFound();
                    }

                    _bM.Title = model.Title;
                    _bM.SubTitle = model.Description;
                    _bM.Content = model.DetailDescription;
                    _bM.CategoryName = model.SektorCategory;

                    if (model.NewImageFile != null)
                    {
                        // Eski resim dosyasını sil
                        if (!string.IsNullOrEmpty(_bM.ImagePath))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/sektorImage/", _bM.ImagePath);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Yeni resim dosyasını yükle
                        var extension = Path.GetExtension(model.NewImageFile.FileName);
                        var newImageName = Guid.NewGuid() + extension;
                        var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/sektorImage/", newImageName);
                        using (var stream = new FileStream(newImagePath, FileMode.Create))
                        {
                            model.NewImageFile.CopyTo(stream);
                        }

                        // Yeni resim dosyası bilgisi güncellenir
                        _bM.ImagePath = newImageName;
                    }

                    _sectorService.TUpdate(_bM);
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
