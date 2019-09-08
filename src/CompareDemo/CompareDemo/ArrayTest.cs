using Compare.Comparer;
using Compare.ComplexType;
using Compare.SimpleType;
using System;
using Xunit;

namespace CompareDemo
{
    public class ArrayTest
    {
        /// <summary>
        /// 1维度+简单: int[]
        /// </summary>
        [Fact]
        public void Clone1DimWithSimple()
        {
            var teacher1 = new Member()
            {
                MemberType = MemberType.Teacher,
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName",
                Age = 40,
                Birthday = null,
                AnnualIncome = 400000.00M,
                Teacher = null
            };
            var teacher2 = new Member()
            {
                MemberType = MemberType.Teacher,
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName",
                Age = 40,
                Birthday = null,
                AnnualIncome = 400000.00M,
                Teacher = null
            };
            var student1 = new Member()
            {
                MemberType = MemberType.Student,
                FirstName = "1",
                MiddleName = "1",
                LastName = "1",
                Age = 11,
                Birthday = new DateTime(2019, 1, 1, 00, 00, 00, DateTimeKind.Utc),
                AnnualIncome = 111.00M,
                Teacher = teacher1
            };
            var student2 = new Member()
            {
                MemberType = MemberType.Student,
                FirstName = "1",
                MiddleName = "1",
                LastName = "1",
                Age = 11,
                Birthday = new DateTime(2019, 1, 1, 00, 00, 00, DateTimeKind.Utc),
                AnnualIncome = 111.00M,
                Teacher = teacher1
            };
            var student3 = new Member()
            {
                MemberType = MemberType.Student,
                FirstName = "1",
                MiddleName = "1",
                LastName = "1",
                Age = 11,
                Birthday = new DateTime(2019, 1, 1, 00, 00, 00, DateTimeKind.Utc),
                AnnualIncome = 111.00M,
                Teacher = teacher2
            };
            var student4 = new Member()
            {
                MemberType = MemberType.Student,
                FirstName = "1",
                MiddleName = "1",
                LastName = "1",
                Age = 11,
                Birthday = new DateTime(2019, 1, 1, 00, 00, 00, DateTimeKind.Utc),
                AnnualIncome = 111.00M,
                Teacher = null
            };

            Assert.True(teacher1.AbsolutelyEqual(teacher2, "Id"));
            Assert.Equal(teacher1, teacher2, new MemberEqualityComparer());

            Assert.False(student1.AbsolutelyEqual(student2, "Id"));
            Assert.NotEqual(student1, student2, new MemberEqualityComparer());

            Assert.True(student1.AbsolutelyEqual(student3, "Id"));
            Assert.Equal(student1, student3, new MemberEqualityComparer());

            Assert.False(student3.AbsolutelyEqual(student4, "Id"));
            Assert.NotEqual(student3, student4, new MemberEqualityComparer());

            Assert.False(student3.AbsolutelyEqual(student3, "Id"));
            Assert.NotEqual(student3, student3, new MemberEqualityComparer());

            Assert.False(student4.AbsolutelyEqual(student4, "Id"));
            Assert.NotEqual(student4, student4, new MemberEqualityComparer());
        }
    }
}