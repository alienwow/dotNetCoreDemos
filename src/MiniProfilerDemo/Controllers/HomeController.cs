using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniProfilerDemo.Models;
using StackExchange.Profiling;

namespace MiniProfilerDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // 分析代码段
            return MiniProfiler.Current.Inline<IActionResult>(() => { return View(); }, "第1.1步");
        }

        public IActionResult Privacy()
        {
            // 分析代码段 多层嵌套
            using (MiniProfiler.Current.Step("第1.1步"))
            {
                Console.WriteLine("test");
                return MiniProfiler.Current.Inline<IActionResult>(() => { return View(); }, "第1.1.1步");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // 自定义分析
            using (CustomTiming customTiming = MiniProfiler.Current.CustomTiming(category: "http", commandString: string.Empty, executeType: "GET", includeStackTrace: true))
            {
                customTiming.CommandString = "vito-Test";
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}
