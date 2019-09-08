using System;
using System.Linq;
using System.Reflection;

namespace Compare.Extention
{
    /// <summary>
    /// https://buildplease.com/pages/testing-deep-equalilty/
    /// https://stackoverflow.com/questions/506096/comparing-object-properties-in-c-sharp
    /// </summary>
    public static class TypeExtensions
    {
        private static readonly Type[] _internalStructs = new Type[]
                                            {
                                                typeof(String),
                                                typeof(Decimal),
                                                typeof(DateTime),
                                                typeof(DateTimeOffset),
                                                typeof(TimeSpan),
                                                typeof(Guid)
                                            };
        /// <summary>
        /// Determine whether a type is simple (String, Decimal, DateTime, etc) 
        /// or complex (i.e. custom class with public properties and methods).
        /// </summary>
        /// <see cref="http://stackoverflow.com/questions/2442534/how-to-test-if-type-is-primitive"/>
        public static bool IsSimpleType(
           this Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                _internalStructs.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }

        public static Type GetUnderlyingType(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                default:
                    throw new ArgumentException
                    (
                       "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }
    }

}