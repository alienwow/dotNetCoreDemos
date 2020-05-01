using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Natasha;

namespace NatashaMvc2._2Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var assembly = Assembly.LoadFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/NatashaMvc2.2Lib.dll");

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApplicationPartManager(appManager =>
                {
                    AssemblyComplier oop = new AssemblyComplier
                    {
                        EnumCRTarget = ComplierResultTarget.File
                    };
                    oop.Add(ControllerTest);
                    // var type = oop.GetType("TestController");
                    // var ass = oop.GetAssembly();

                    appManager.ApplicationParts.Add(new AssemblyPart(oop.GetAssembly()));
                    // appManager.ApplicationParts.Add(new AssemblyPart(type.Assembly));
                    appManager.ApplicationParts.Add(new AssemblyPart(assembly));
                });

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(
                    new EmbeddedFileProvider(assembly, "NatashaMvc2._2Lib")
                );
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private readonly string ControllerTest = @"
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NatashaMvc2._2Demo.Models;

namespace NatashaMvc2._2Demo.Controllers
{
    public class TestController : Controller
    {
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

";
    }
}
