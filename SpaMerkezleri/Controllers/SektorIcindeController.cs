using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SpaMerkezleri.Controllers
{
    [AllowAnonymous]
    public class SektorIcindeController : Controller
    {
        private readonly Context _context;

        public SektorIcindeController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(string categoryName)
        {
            var values = _context.Sektors.Include(x => x.AppUser).AsQueryable();

            if (!string.IsNullOrEmpty(categoryName))
            {
                values = values.Where(x => x.CategoryName == categoryName); // Kategoriye göre filtreleme
            }

            var filteredBlogs = values.OrderByDescending(x => x.Created).Take(8).ToList();
            ViewBag.TotalBlogs = values.Count();
            ViewBag.Img = _context.AboutMes.FirstOrDefault()?.AboutSektorImg ?? "/images/about/default.png";
            ViewBag.Categories = _context.Sektors.Select(c => c.CategoryName).Distinct().ToList();

            return View(filteredBlogs);
        }

        public IActionResult Post(int id)
        {
            var blog = _context.Sektors.Include(x => x.AppUser).FirstOrDefault(x => x.Id == id);

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        [HttpGet]
        public IActionResult GetMoreBlogs(int page, string categoryName)
        {
            int pageSize = 8;
            var blogs = _context.Sektors.Include(x => x.AppUser).AsQueryable();

            if (!string.IsNullOrEmpty(categoryName))
            {
                blogs = blogs.Where(x => x.CategoryName == categoryName); // Kategoriye göre filtreleme
            }

            var paginatedBlogs = blogs.OrderByDescending(x => x.Created)
                                      .Skip(page * pageSize)
                                      .Take(pageSize)
                                      .ToList();

            return PartialView("_BlogPartial", paginatedBlogs);
        }
    }
}
