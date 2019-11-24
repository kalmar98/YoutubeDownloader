using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Launcher.Models;
using Launcher.Services;
using Launcher.Services.Contracts;

namespace Launcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IHomeService homeService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            this.logger = logger;
            this.homeService = homeService;
        }

        public IActionResult Index()
        {
            ViewData["destination"] = homeService.ExportDirectories;

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

        public async Task<IActionResult> Download()
        {
            string url = HttpContext.Request.Query["url"].ToString();
            string destination = HttpContext.Request.Query["destination"].ToString();

            await homeService.Download(url, destination);

            return RedirectToAction(nameof(Index));
        }
    }
}
