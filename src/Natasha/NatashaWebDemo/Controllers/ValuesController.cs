using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Natasha.Operator;

namespace NatashaWebDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var action = FastMethodOperator.New
                            .UseAsync()
                            .Param<string>("str1")
                            .Param(typeof(string), "str2")
                            .MethodBody("return str1+str2;")
                            .Return<Task<string>>()
                            .Complie<Func<string, string, Task<string>>>();

            var result = await action("Hello ", $"`{id}` Async World!");
            return result;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
