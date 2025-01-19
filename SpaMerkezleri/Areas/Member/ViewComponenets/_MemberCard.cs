using BusinessLayer.Abstract;
using DataAccessLayer.Concrete; // IdentityUser kullanımı için
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SpaMerkezleri.Areas.Member.ViewComponents
{
    public class _MemberCard : ViewComponent
    {
        private readonly IUyelikService _uyelikService;
        private readonly UserManager<AppUser> _userManager;

        public _MemberCard(IUyelikService uyelikService, UserManager<AppUser> userManager)
        {
            _uyelikService = uyelikService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                ViewBag.UyeTipi = user.UyeTipi;
                ViewBag.Status = user.Status;
            }

            var values = _uyelikService.TGetList();
            return View(values);
        }
    }
}
