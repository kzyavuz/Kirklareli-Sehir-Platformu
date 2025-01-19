using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpaMerkezleri.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class RolController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RolController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateRol()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRol(CreateRoleModel roleModel)
        {
            AppRole appRole = new AppRole()
            {
                Name = roleModel.RoleName
            };
            var result = await _roleManager.CreateAsync(appRole);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            await _roleManager.DeleteAsync(values);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            UpdateRoleModel updateRoleModel = new UpdateRoleModel
            {
                Id = values.Id,
                RoleName = values.Name
            };
            return View(updateRoleModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleModel updateRoleModel)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == updateRoleModel.Id);
            values.Name = updateRoleModel.RoleName;
            await _roleManager.UpdateAsync(values);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AdminList()
        {
            var users = await _userManager.GetUsersInRoleAsync("admin");
            var orderUser = users.OrderByDescending(x => x.CreateDateTime).ToList();
            return View(orderUser);
        }

        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.GetUsersInRoleAsync("member");
            var orderUser = users.OrderByDescending(x => x.CreateDateTime).ToList();
            return View(orderUser);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            TempData["UserId"] = user.Id;
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            List<RoleAssignModel> roleAssignModels = new List<RoleAssignModel>();
            foreach (var item in roles)
            {
                RoleAssignModel model = new RoleAssignModel();
                model.RoleID = item.Id;
                model.RoleName = item.Name;
                model.RoleExist = userRoles.Contains(item.Name);
                roleAssignModels.Add(model);
            }
            return View(roleAssignModels);
        }


        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignModel> model)
        {
            var userid = (int)TempData["UserId"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userid);
            foreach (var item in model)
            {
                if (item.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("AdminList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id);
            if (userToDelete == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (currentUser == null)
            {
                return RedirectToAction("AdminList");
            }

            // Admin kullanıcısının Id'sini doğru şekilde ayarlayın
            int adminUserId = currentUser.Id;

            if (userToDelete.Id == adminUserId)
            {
                ModelState.AddModelError("", "Admin kendini silemez");
                return RedirectToAction("AdminList");
            }

            var result = await _userManager.DeleteAsync(userToDelete);
            if (result.Succeeded)
            {
                return RedirectToAction("AdminList");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction("AdminList");
        }
    }
}
