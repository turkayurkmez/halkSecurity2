using DataProtectionOnServer.Models;
using DataProtectionOnServer.Security;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataProtectionOnServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _environment = hostEnvironment;
        }

        public IActionResult Index()
        {
            string amanCokGizliCumle = "Aman cok gizli sifre falan var";
            DataProtector protector = new DataProtector(_environment.ContentRootPath);
            var length = protector.EncryptData(amanCokGizliCumle);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}