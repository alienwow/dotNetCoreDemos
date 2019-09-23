using System;
using System.Linq.Expressions;

namespace ExpressionTreeDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            
            IsByRefDemo.Run();

            算术运算符.Run();

            关系运算符.Run();

            逻辑运算符.Run();

            位运算符.Run();

            赋值运算符.Run();

            其他运算符.Run();

            分支语句.Run();

        }

    }
}
