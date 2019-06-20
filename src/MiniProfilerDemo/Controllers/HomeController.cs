﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MiniProfilerDemo.Models;

using StackExchange.Profiling;

namespace MiniProfilerDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly Lazy<BloggingContext> _bloggingContext;
        public BloggingContext BloggingContext => _bloggingContext.Value;

        public HomeController(Lazy<BloggingContext> bloggingContext)
        {
            _bloggingContext = bloggingContext;
        }

        public async Task<IActionResult> Index()
        {
            using (MiniProfiler.Current.Step("第1.1步"))
            {
                await BloggingContext.Blogs.ToListAsync();
            }

            // 分析代码段
            return MiniProfiler.Current.Inline<IActionResult>(() => View(), "第1.2步");
        }

        [Authorize]
        public IActionResult Privacy()
        {
            // 分析代码段 多层嵌套
            using (MiniProfiler.Current.Step("第1.1步"))
            {
                Console.WriteLine("test");
                return MiniProfiler.Current.Inline<IActionResult>(() => { return View(); }, "第1.1.1步");
            }
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
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
