using System;
using System.Linq.Expressions;

namespace ExpressionTreeDemo01
{
    public static class 赋值运算符
    {
        public static void Run()
        {
            // =    Expression.Assign
            // +=   Expression.AddAssign
            // -=   Expression.SubtractAssign
            // *=   Expression.MultiplyAssign
            // /=   Expression.DivideAssign
            // %=   Expression.ModuloAssign
            // <<=  Expression.LeftShiftAssign
            // >>=  Expression.RightShiftAssign
            // &=   Expression.AndAssign
            // |=   Expression.OrAssign
            // ^=   Expression.ExclusiveOrAssign
            Console.WriteLine($"---------------------------{nameof(赋值运算符)}-------------------------");
            Run0Action();
            Run0Func();
            Console.WriteLine($"---------------------------{nameof(赋值运算符)}-------------------------");
        }

        static void Run0Action()
        {
            // a += b
            ParameterExpression a = Expression.Parameter(typeof(int), nameof(a));
            ParameterExpression b = Expression.Parameter(typeof(int), nameof(b));

            BinaryExpression addAssignExp = Expression.AddAssign(a, b);

            MethodCallExpression methodCall = Expression.Call(null,
                                                    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }),
                                                    addAssignExp
                                                );
            Expression<Action<int, int>> lambda = Expression.Lambda<Action<int, int>>(methodCall, a, b);
            var action = lambda.Compile();
            action(1, 2);
            action(2, 3);

        }

        static void Run0Func()
        {
            // a -= b
            ParameterExpression a = Expression.Parameter(typeof(int), nameof(a));
            ParameterExpression b = Expression.Parameter(typeof(int), nameof(b));

            BinaryExpression addAssignExp = Expression.SubtractAssign(a, b);

            Expression<Func<int, int, int>> lambda = Expression.Lambda<Func<int, int, int>>(addAssignExp, a, b);
            var func = lambda.Compile();
            Console.WriteLine(func(1, 2));
            Console.WriteLine(func(100, 1));
        }

    }
}