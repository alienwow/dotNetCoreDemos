using System;
using Natasha;

namespace NatashaDemo
{
    public class NewClassDemo
    {
        public static void NewClassTest()
        {
            var testClassName = NewClass.Create(builder => builder
                .Namespace("TestNamespace")
                .OopAccess(AccessTypes.Public)
                .OopName("TestClassName")
                .Ctor(item => item
                    .MemberAccess(AccessTypes.Public)
                    .Param<string>("name")
                    .Body("Name=name;")
                )
                .OopBody(@"
                    private void Test(){}"
                )
                .PublicStaticField<string>("Name")
                .PrivateField<int>("_age")
            );

            var type = testClassName.Type;
            Console.WriteLine(type.Name);

            var error = testClassName.Exception;
            Console.WriteLine(error);
        }


        public static void NewClassStaticCtorTest()
        {
            var testClassName = NewClass.Create(builder => builder
                .Namespace("TestNamespace")
                .OopAccess(AccessTypes.Public)
                .OopName("TestClassName")
                .Ctor(item => item
                    .MemberModifier(Modifiers.Static)
                    .Body("Name=\"nametest\";")
                )
                .OopBody(@"
                    private void Test(){}"
                )
                .PublicStaticField<string>("Name")
                .PrivateField<int>("_age")
            );

            var type = testClassName.Type;
            Console.WriteLine(type.Name);

            var error = testClassName.Exception;
            Console.WriteLine(error);
        }
    }
}
