﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Natasha;

namespace NatashaWebDemo
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
            services.AddMvc()
                .ConfigureApplicationPartManager(appManager =>
                {
                    // var domain = NDomain.Create(DomainManagment.CurrentDomain);
                    // var type1 = domain.GetType(ControllerTest0);
                    // var type2 = domain.GetType(ControllerTest1);

                    AssemblyComplier oop = new AssemblyComplier();
                    oop.Add(ControllerTest0);
                    Type type1 = oop.GetType("TestController");

                    var feature = new ControllerFeature();
                    appManager.ApplicationParts.Add(new AssemblyPart(type1.Assembly));
                    //appManager.ApplicationParts.Add(new AssemblyPart(type2.Assembly));
                    appManager.PopulateFeature(feature);
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private readonly string ControllerTest0 = @"
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NatashaWebDemo.Controllers
{
    [Route(""api/[controller]"")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { ""value1"", ""value2"" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut(""{id}"")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete(""{id}"")]
        public void Delete(int id)
        {
        }
    }

    [Route(""api/[controller]"")]
    [ApiController]
    public class Test1Controller : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { ""value1"", ""value2"" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut(""{id}"")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete(""{id}"")]
        public void Delete(int id)
        {
        }
    }
}
";
        private readonly string ControllerTest1 = @"
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NatashaWebDemo.Controllers
{
    [Route(""api/[controller]"")]
    [ApiController]
    public class Test2Controller : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { ""value1"", ""value2"" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut(""{id}"")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete(""{id}"")]
        public void Delete(int id)
        {
        }
    }
}
";
    }
}
