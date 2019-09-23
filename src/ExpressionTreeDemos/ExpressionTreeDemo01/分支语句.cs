using System;
using System.Linq.Expressions;

namespace ExpressionTreeDemo01
{
    public static class 分支语句
    {
        public static void Run()
        {
            // if       Expression.IfThen
            // if-else  Expression.IfThenElse
            // switch   Expression.Switch、Expression.SwitchCase
            // ??       Expression.Coalesce
            // ?:       Expression.Condition
            Console.WriteLine($"---------------------------{nameof(分支语句)}-------------------------");
            Console.WriteLine($"\t---------------------------{nameof(RunIfThen)}-------------------------");
            RunIfThen();
            Console.WriteLine($"\t---------------------------{nameof(RunIfThenElse)}-------------------------");
            RunIfThenElse();
            Console.WriteLine($"\t---------------------------{nameof(RunSwitch)}-------------------------");
            RunSwitch();
            Console.WriteLine($"\t---------------------------{nameof(RunCoalesce)}-------------------------");
            RunCoalesce();
            Console.WriteLine($"\t---------------------------{nameof(RunCondition)}-------------------------");
            RunCondition();
            Console.WriteLine($"---------------------------{nameof(分支语句)}-------------------------");
        }

        static void RunIfThen()
        {
            ParameterExpression a = Expression.Parameter(typeof(int), nameof(a));

            ParameterExpression c = Expression.Parameter(typeof(int), nameof(c));
            BinaryExpression assignExp = Expression.Assign(c, Expression.Constant(10));

            MethodCallExpression methodCall = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), a);

            ConditionalExpression ifThenExp = Expression.IfThen(Expression.GreaterThan(a, c), methodCall);
            BlockExpression block = Expression.Block(new ParameterExpression[] { c }, c, assignExp, ifThenExp);

            Expression<Action<int>> lambda = Expression.Lambda<Action<int>>(block, a);
            var action = lambda.Compile();
            action(10);
            action(12);
        }

        static void RunIfThenElse()
        {
            ParameterExpression a = Expression.Parameter(typeof(int), nameof(a));
            ParameterExpression b = Expression.Parameter(typeof(int), nameof(b));

            ParameterExpression c = Expression.Parameter(typeof(int), nameof(c));
            BinaryExpression assignExp = Expression.Assign(c, Expression.Constant(10, typeof(int)));

            MethodCallExpression methodACall = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), a);
            MethodCallExpression methodBCall = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), b);

            ConditionalExpression ifThenElseExp = Expression.IfThenElse(Expression.GreaterThan(a, c), methodACall, methodBCall);
            BlockExpression block = Expression.Block(new ParameterExpression[] { c }, c, assignExp, ifThenElseExp);

            Expression<Action<int, int>> lambda = Expression.Lambda<Action<int, int>>(block, a, b);
            var action = lambda.Compile();
            action(3, 13);
            action(14, 4);
        }

        static void RunSwitch()
        {
            ParameterExpression a = Expression.Parameter(typeof(int), nameof(a));

            MethodCallExpression _default = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), Expression.Constant("非法的输入！", typeof(string)));

            SwitchCase case1 = Expression.SwitchCase(Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), Expression.Constant("a==1", typeof(string))),
                                new ConstantExpression[] { Expression.Constant(1, typeof(int)) });
            SwitchCase case2 = Expression.SwitchCase(Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), Expression.Constant("a==2", typeof(string))),
                                new ConstantExpression[] { Expression.Constant(2, typeof(int)) });
            SwitchCase case3 = Expression.SwitchCase(Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), Expression.Constant("a==3", typeof(string))),
                                new ConstantExpression[] { Expression.Constant(3, typeof(int)) });

            SwitchExpression switchExp = Expression.Switch(a, _default, new SwitchCase[] { case1, case2, case3 });

            Expression<Action<int>> lambda = Expression.Lambda<Action<int>>(switchExp, a);
            var action = lambda.Compile();
            action(1);
            action(2);
            action(3);
            action(4);
        }

        static void RunCoalesce()
        {
            ParameterExpression a = Expression.Parameter(typeof(object), nameof(a));
            ParameterExpression b = Expression.Parameter(typeof(object), nameof(b));

            BinaryExpression coalesceExp = Expression.Coalesce(a, b);
            MethodCallExpression methodCall = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(object) }), coalesceExp);

            Expression<Action<object, object>> lambda = Expression.Lambda<Action<object, object>>(methodCall, a, b);
            var action = lambda.Compile();
            action(null, 2);
            action(5, null);
            action(6, 9);
        }

        static void RunCondition()
        {
            ParameterExpression a = Expression.Parameter(typeof(int), nameof(a));
            ParameterExpression b = Expression.Parameter(typeof(int), nameof(b));

            MethodCallExpression methodACall = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), a);
            MethodCallExpression methodBCall = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), b);

            ConditionalExpression conditionalExp = Expression.Condition(Expression.GreaterThan(a, b), methodACall, methodBCall);

            Expression<Action<int, int>> lambda = Expression.Lambda<Action<int, int>>(conditionalExp, a, b);
            var action = lambda.Compile();
            action(1, 2);
            action(5, 8);
        }

    }
}