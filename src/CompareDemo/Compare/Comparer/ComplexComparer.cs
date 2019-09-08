using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Compare.Extention;

namespace Compare.Comparer
{
    public static class ComplexComparer
    {

        /// <summary>
        /// 绝对相等，即值相等，且不是同一个引用，同为 null 也相等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static bool AbsolutelyEqual<T>(this T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                if (self == to) return false;

                Type type = typeof(T);
                var ignoreList = new List<string>(ignore);
                var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .Where(x =>
                                    !ignoreList.Contains(x.Name) &&
                                    x.GetIndexParameters().Length == 0
                                )
                                .ToList();

                foreach (PropertyInfo pi in props)
                {
                    object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                    object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                    if ((selfValue == null && toValue != null) || (selfValue != null && toValue == null))
                    {
                        return false;
                    }
                    else if (selfValue != null && toValue != null)
                    {
                        if (pi.GetUnderlyingType().IsSimpleType()) // SimpleType
                        {
                            if (!selfValue.Equals(toValue)) return false;
                        }
                        else // ComplexType
                        {
                            // 引用相同 || 值不同  false
                            if (selfValue == toValue || !AbsolutelyEqual(selfValue, toValue, ignore))
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            return self == to;
        }

        // /// <summary>
        // /// 绝对相等，即值相等，且不是同一个引用，同为 null 也相等
        // /// </summary>
        // /// <typeparam name="T"></typeparam>
        // public static bool AbsolutelyEqual<T>(T self, T to, params string[] ignore) where T : class
        // {
        //     if (self != null && to != null)
        //     {
        //         Type type = typeof(T);
        //         var ignoreList = new List<string>(ignore);
        //         var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
        //                         .Where(x =>
        //                             !ignoreList.Contains(x.Name) &&
        //                             x.GetIndexParameters().Length == 0
        //                         )
        //                         .ToList();

        //         foreach (PropertyInfo pi in props)
        //         {
        //             object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
        //             object toValue = type.GetProperty(pi.Name).GetValue(to, null);

        //             if ((selfValue == null && toValue != null) || (selfValue != null && toValue == null))
        //             {
        //                 return false;
        //             }
        //             else if (selfValue != null && toValue != null)
        //             {
        //                 if (pi.GetUnderlyingType().IsSimpleType()) // SimpleType
        //                 {
        //                     if (!selfValue.Equals(toValue)) return false;
        //                 }
        //                 else // ComplexType
        //                 {
        //                     // 引用相同 || 值不同  false
        //                     if (selfValue == toValue || !AbsolutelyEqual(selfValue, toValue, ignore))
        //                     {
        //                         return false;
        //                     }
        //                 }
        //             }
        //         }
        //         return true;
        //     }
        //     return self == to;
        // }

        /// <summary>
        /// 引用相同 || 值相等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static bool Equal<T>(T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                        // if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        if (selfValue != toValue && (selfValue == null || !Equal(selfValue, toValue, ignore)))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return self == to;
        }
        /// <summary>
        /// 引用相同 || 值相等 Linq版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static bool EqualLinq<T>(this T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                var type = typeof(T);
                var ignoreList = new List<string>(ignore);
                var unequalProperties =
                    from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    where !ignoreList.Contains(pi.Name) &&
                            pi.GetUnderlyingType().IsSimpleType() &&
                            pi.GetIndexParameters().Length == 0
                    let selfValue = type.GetProperty(pi.Name).GetValue(self, null)
                    let toValue = type.GetProperty(pi.Name).GetValue(to, null)
                    where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                    select selfValue;
                return !unequalProperties.Any();
            }
            return self == to;
        }
    }
}