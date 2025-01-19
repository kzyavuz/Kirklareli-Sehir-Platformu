using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class ProfesyonelMemberController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public ProfesyonelMemberController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var values = _context.Users.Where(x => x.Status == "Onay Bekliyor" && x.UyeTipi == "STANDART").ToList();
            ViewBag.TotalRequests = values.Count;
            return View(values);
        }

        public IActionResult ApprovedProfesyonelUser()
        {
            var values = _context.Users.Where(x => x.Status == "Onaylandı" && x.UyeTipi == "PROFESYONEL").ToList();
            ViewBag.TotalRequests = values.Count;
            return View(values);
        }

        public IActionResult RejectProfesyonel()
        {
            var values = _context.Users.Where(x => x.Status == "Reddedildi" && x.UyeTipi == "STANDART").ToList();
            ViewBag.TotalRequests = values.Count;
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveRequest(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Status = "Onaylandı";
                user.UyeTipi = "PROFESYONEL";
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RejectRequest(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.UyeTipi = "STANDART";
                user.Status = "Reddedildi";
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");
        }
    }

}
