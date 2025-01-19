using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class BlogCategoryController : Controller
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        public IActionResult Index()
        {
            var data = _blogCategoryService.TGetList().OrderBy(x=>x.CategoryName).ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult AddBlogCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBlogCategory(BlogCategory blogCategory)
        {
            _blogCategoryService.TAdd(blogCategory);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateBlogCategory(int id)
        {
            var values = _blogCategoryService.TGetByID(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult UpdateBlogCategory(BlogCategory blogCategory)
        {
            _blogCategoryService.TUpdate(blogCategory);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteBlogCategory(int id)
        {
            var result = _blogCategoryService.TGetByID(id);
            _blogCategoryService.TDelete(result);
            return RedirectToAction("Index");
        }
    }
}
