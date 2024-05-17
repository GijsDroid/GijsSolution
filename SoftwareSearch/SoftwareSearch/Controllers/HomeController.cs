using Microsoft.AspNetCore.Mvc;
using SoftwareSearch.Models;
using SoftwareSearch.repositories;
using System.Diagnostics;

namespace SoftwareSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            var softwareRepository = new SoftwareRepository();

            var view = new HomeViewModel();

            view.Softwares = softwareRepository.Search(searchString);

            return View(view);
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
