using System;
using System.Linq.Expressions;

namespace ExpressionTreeDemo01
{
    public static class 位运算符
    {
        public static void Run()
        {
            // - `&`  Expression.And
            // - `|`  Expression.Or
            // - `^`  Expression.ExclusiveOr
            // - `~`  Expression.OnesComplement
            // - `<<` Expression.LeftShift
            // - `>>` Expression.RightShift
            Console.WriteLine($"---------------------------{nameof(位运算符)}-------------------------");
            Run0Action();
            Run0Func();
            Console.WriteLine($"---------------------------{nameof(位运算符)}-------------------------");
        }

        static void Run0Action()
        {
            ParameterExpression a = Expression.Parameter(typeof(int), nameof(a));
            ParameterExpression b = Expression.Parameter(typeof(int), nameof(b));

            BinaryExpression andExp = Expression.And(a, b);

            MethodCallExpression methodCall = Expression.Call(null,
                                                    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }),
                                                    andExp
                                                );
            Expression<Action<int, int>> lambda = Expression.Lambda<Action<int, int>>(methodCall, a, b);
            var action = lambda.Compile();
            action(1, 2);
            action(2, 1);
        }

        static void Run0Func()
        {
            ParameterExpression a = Expression.Parameter(typeof(int), nameof(a));
            ParameterExpression b = Expression.Parameter(typeof(int), nameof(b));

            BinaryExpression orExp = Expression.Or(a, b);

            Expression<Func<int, int, int>> lambda = Expression.Lambda<Func<int, int, int>>(orExp, a, b);
            var func = lambda.Compile();
            Console.WriteLine(func(1, 2));
            Console.WriteLine(func(2, 1));
        }

    }
}