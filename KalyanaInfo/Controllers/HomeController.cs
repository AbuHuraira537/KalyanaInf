using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KalyanaInfo.Models;
using Microsoft.EntityFrameworkCore;

namespace KalyanaInfo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly kalyanadiaryContext _context;
        public HomeController(ILogger<HomeController> logger,kalyanadiaryContext con)
        {
            _logger = logger;
            _context = con;
        }

        public IActionResult Index()
        {
            var kalyanadiaryContext = _context.Post.Include(p => p.User);

            return View(kalyanadiaryContext.ToList());
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
