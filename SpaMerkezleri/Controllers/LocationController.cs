using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;

namespace SpaMerkezleri.Controllers
{
    [AllowAnonymous]
    public class LocationController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public LocationController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult GetIl()
        {
            var ilJsonPath = Path.Combine(_env.WebRootPath, "secim", "il.json");
            var ilJson = System.IO.File.ReadAllText(ilJsonPath);
            var ilData = JsonSerializer.Deserialize<dynamic>(ilJson);
            return Json(ilData);
        }

        public IActionResult GetIlce()
        {
            var ilceJsonPath = Path.Combine(_env.WebRootPath, "secim", "ilce.json");
            var ilceJson = System.IO.File.ReadAllText(ilceJsonPath);
            var ilceData = JsonSerializer.Deserialize<dynamic>(ilceJson);
            return Json(ilceData);
        }
    }
}
