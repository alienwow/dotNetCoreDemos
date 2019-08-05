using System;
using System.Threading.Tasks;

using Natasha;

namespace NatashaDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            FirstAction();
            await FirstAsyncAction();
        }

        static void FirstAction()
        {
            var action = FastMethodOperator.New
                .Param<string>("str1")
                .Param(typeof(string), "str2")
                .MethodBody("return str1+str2;")
                .Return<string>()
                .Complie<Func<string, string, string>>();

            var result = action("Hello ", "World!");
            Console.WriteLine(result);
        }

        static async Task FirstAsyncAction()
        {
            var action = FastMethodOperator.New
                .UseAsync()
                .Param<string>("str1")
                .Param(typeof(string), "str2")
                .MethodBody("return str1+str2;")
                .Return<Task<string>>()
                .Complie<Func<string, string, Task<string>>>();

            var result = await action("Hello ", "Async World!");
            Console.WriteLine(result);
        }
    }
}
