using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SpaMerkezleri.ViewComponents.Default
{
    public class _HomeBlog : ViewComponent
    {
        private readonly IBlogService _blogService;

        public _HomeBlog(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            var data = _blogService.TGetList().OrderByDescending(b => b.Created).Take(4).ToList();
            return View(data);
        }
    }
}
