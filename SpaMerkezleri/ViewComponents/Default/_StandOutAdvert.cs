using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SpaMerkezleri.ViewComponents.Default
{
    public class _StandOutAdvert: ViewComponent
    {
        private readonly Context _context;

        public _StandOutAdvert(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var data = _context.Ilanlars.Include(x=>x.AppUser).Where(x => x.StandOutStatus == true).OrderByDescending(b => b.StandOutDateTime).Take(4).ToList();
            return View(data);
        }
    }
}
