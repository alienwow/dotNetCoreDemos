using System;
using System.Threading.Tasks;

using Natasha;

namespace NatashaDemo
{
    public class NFuncDemo
    {
        public static void NFuncDelegate()
        {
            var action = NFunc<string, string, string>
                            .Delegate(@"
                            string result = arg1 +"" ""+ arg2;
                            return result;
                            ");
            Console.WriteLine(action("Hello", "NFunc World!"));
        }

        public static async Task NFuncAsyncDelegate()
        {
            var action = NFunc<string, string, Task<string>>
                            .AsyncDelegate(@"
                            string result = arg1 +"" ""+ arg2;
                            return result;
                            ");
            Console.WriteLine(await action("Hello", "NFunc Async World!"));
        }

        public static void NFuncUnsafeDelegate()
        {
            var action = NFunc<string, string, string>
                            .UnsafeDelegate(@"
                            string result = arg1 +"" ""+ arg2;
                            return result;
                            ");
            Console.WriteLine(action("Hello", "NFunc Unsafe World!"));
        }

        public static async Task NFuncUnsafeAsyncDelegate()
        {
            var action = NFunc<string, string, Task<string>>
                            .UnsafeAsyncDelegate(@"
                            string result = arg1 +"" ""+ arg2;
                            return result;
                            ");
            Console.WriteLine(await action("Hello", "NFunc Unsafe Async World!"));
        }
    }
}
