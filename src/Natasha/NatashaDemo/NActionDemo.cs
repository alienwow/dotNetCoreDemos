using Natasha;

namespace NatashaDemo
{
    public class NActionDemo
    {
        public static void NActionDelegate()
        {
            var action = NAction<string, string>
                            .Delegate(@"
                            string result = arg1 +"" ""+ arg2;
                            Console.WriteLine(result);
                            ");
            action("Hello", "NAction World!");
        }
        
        // TODO async Action?
        //public static async Task NActionAsyncDelegate()
        //{
        //    var action = NAction<string, string>
        //                    .AsyncDelegate(@"
        //                    string result = arg1 +"" ""+ arg2;
        //                    Console.WriteLine(result);
        //                    ");

        //    await action("Hello", "NAction Async World!");
        //}

        public static void NActionUnsafeDelegate()
        {
            var action = NAction<string, string>
                            .UnsafeDelegate(@"
                            string result = arg1 +"" ""+ arg2;
                            Console.WriteLine(result);
                            ");
            action("Hello", "NAction Unsafe World!");
        }

        // TODO async Action?
        //public static async Task NActionUnsafeAsyncDelegate()
        //{
        //    var action = NAction<string, string>
        //                    .UnsafeAsyncDelegate(@"
        //                    string result = arg1 +"" ""+ arg2;
        //                    Console.WriteLine(result);
        //                    ");

        //    await action("Hello", "NAction Unsafe Async World!");
        //}

    }
}
