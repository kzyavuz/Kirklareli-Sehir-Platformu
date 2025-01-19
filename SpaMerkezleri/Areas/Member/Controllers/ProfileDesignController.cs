using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpaMerkezleri.Areas.Member.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "member")]
    [Route("Member/[controller]/[action]")]
    public class ProfileDesignController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileDesignController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserEditModel p)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(p.password))
            {
                var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, p.password);
                user.PasswordHash = newPasswordHash;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Profile", new { area = "Member" });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(p);
                }
            }
            return View(p);
        }

    }
}

