using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using NatashaMvc3._1Lib.Models;

namespace NatashaMvc3._1Lib
{
    public class TestLibController : Controller
    {
        private readonly ILogger<TestLibController> _logger;

        public TestLibController(ILogger<TestLibController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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
