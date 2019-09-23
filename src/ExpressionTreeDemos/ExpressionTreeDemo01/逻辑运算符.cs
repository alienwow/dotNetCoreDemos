using System;
using System.Linq.Expressions;

namespace ExpressionTreeDemo01
{
    public static class 逻辑运算符
    {
        public static void Run()
        {
            // &&   Expression.AndAlso
            // ||   Expression.OrElse
            // !    Expression.Not
            Console.WriteLine($"---------------------------{nameof(逻辑运算符)}-------------------------");
            Run0Action();
            Run0Func();
            Console.WriteLine($"---------------------------{nameof(逻辑运算符)}-------------------------");
        }

        static void Run0Action()
        {
            ParameterExpression a = Expression.Parameter(typeof(int), nameof(a));
            ParameterExpression b = Expression.Parameter(typeof(int), nameof(b));

            BinaryExpression greaterThanExp = Expression.GreaterThan(a, b);
            BinaryExpression lessThanExp = Expression.LessThan(a, b);
            BinaryExpression andAlsoExp = Expression.AndAlso(greaterThanExp, lessThanExp);

            MethodCallExpression methodCall = Expression.Call(null,
                                                    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(bool) }),
                                                    andAlsoExp
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

            BinaryExpression greaterThanExp = Expression.GreaterThan(a, b);
            BinaryExpression lessThanExp = Expression.LessThan(a, b);
            BinaryExpression orElseExp = Expression.OrElse(greaterThanExp, lessThanExp);

            Expression<Func<int, int, bool>> lambda = Expression.Lambda<Func<int, int, bool>>(orElseExp, a, b);
            var func = lambda.Compile();
            Console.WriteLine(func(1, 2));
            Console.WriteLine(func(2, 1));
        }
    }
}