using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "member")]
    [Route("Member/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProfesyonelMemberUpdate()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null && user.UyeTipi == "STANDART")
            {
                user.Status = "Onay Bekliyor";

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Üyelik yükseltme talebiniz başarıyla gönderildi.";
                    return RedirectToAction("Index");
                }
                else
                {
                    // Hata mesajını TempData'ya ekliyoruz
                    TempData["ErrorMessage"] = "Üyelik yükseltme işlemi başarısız oldu. Lütfen tekrar deneyin.";
                    return RedirectToAction("Index");
                }
            }

            TempData["ErrorMessage"] = "Kullanıcı bulunamadı.";
            return RedirectToAction("Index");
        }

    }
}
