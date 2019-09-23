using System;
using System.Linq.Expressions;

namespace ExpressionTreeDemo01
{
    public static class 算术运算符
    {
        public static void Run()
        {
            // +    Expression.Add
            // -    Expression.Subtract
            // *    Expression.Multiply
            // /    Expression.Divide
            // %    Expression.Modulo
            // ++   Expression.PreIncrementAssign、Expression.PostIncrementAssign
            // --   Expression.PreDecrementAssign、Expression.PostDecrementAssign

            Console.WriteLine($"---------------------------{nameof(算术运算符)}-------------------------");
            Run0Action();
            Run0Func();

            RunVarAssign();
            Console.WriteLine($"---------------------------{nameof(算术运算符)}-------------------------");
        }

        static void Run0Action()
        {
            ParameterExpression a = Expression.Parameter(typeof(int), nameof(a));
            ParameterExpression b = Expression.Parameter(typeof(int), nameof(b));

            BinaryExpression addExp = Expression.Add(a, b);

            MethodCallExpression methodCall = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), addExp);
            Expression<Action<int, int>> lambda = Expression.Lambda<Action<int, int>>(methodCall, a, b);
            lambda.Compile()(100, 200);
        }

        static void Run0Func()
        {
            ParameterExpression a = Expression.Parameter(typeof(int), nameof(a));
            ParameterExpression b = Expression.Parameter(typeof(int), nameof(b));

            BinaryExpression subtractExp = Expression.Subtract(a, b);

            Expression<Func<int, int, int>> lambda = Expression.Lambda<Func<int, int, int>>(subtractExp, a, b);
            Console.WriteLine(lambda.Compile()(300, 400));
        }

        static void RunVarAssign()
        {
            Console.WriteLine($"\t---------------------------变量赋值-------------------------");
            Console.Write("\t");
            ParameterExpression a = Expression.Parameter(typeof(int), nameof(a));
            ParameterExpression b = Expression.Parameter(typeof(int), nameof(b));

            ParameterExpression varSum = Expression.Variable(typeof(int), "sum");
            BinaryExpression varExp = Expression.Assign(varSum, Expression.Multiply(a, b));

            MethodCallExpression methodCall = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), varSum);
            BlockExpression block = Expression.Block(new ParameterExpression[] { varSum }, varExp, methodCall);
            Expression<Action<int, int>> lambda = Expression.Lambda<Action<int, int>>(block, a, b);
            lambda.Compile()(200, 300);
            Console.WriteLine($"\t---------------------------变量赋值-------------------------");
        }
    }
}