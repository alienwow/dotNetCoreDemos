using System;
using System.Linq;

namespace ExpressionTreeDemo01
{
    public static class IsByRefDemo
    {
        public static void Run()
        {
            Console.WriteLine($"---------------------------{nameof(IsByRefDemo)}-------------------------");

            int abc = 10;
            Func(ref abc);
            var type = ((System.Reflection.TypeInfo)typeof(IsByRefDemo)).GetDeclaredMethods("Func")
                .First()
                .GetParameters()
                .First()
                .ParameterType;
            Console.WriteLine($"IsByRef:{type.IsByRef}");

            Console.WriteLine($"---------------------------{nameof(IsByRefDemo)}-------------------------");

        }

        static void Func(ref int abc)
        {
            Console.WriteLine($"abc.GetType()IsByRef:{abc.GetType().IsByRef}");
        }

    }
}