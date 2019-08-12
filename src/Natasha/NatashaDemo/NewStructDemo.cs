using System;
using Natasha;

namespace NatashaDemo
{
    public class NewStructDemo
    {
        public static void NewStructTest()
        {
            var testStructName = NewStruct.Create(builder => builder
                .Namespace("TestNamespace")
                .OopAccess(AccessTypes.Public)
                .OopName("TestStructName")
                .Ctor(item => item
                    .MemberAccess(AccessTypes.Public)
                    .Param<string>("name")
                    .Body("Name=name;_age=1;")
                )
                .OopBody(@"
                    public void Test(){}"
                )
                .PublicStaticField<string>("Name")
                .PrivateStaticField<int>("_age")
            );

            var type = testStructName.Type;
            Console.WriteLine(type.Name);

            var error = testStructName.Exception;
            Console.WriteLine(error);
        }
    }
}
