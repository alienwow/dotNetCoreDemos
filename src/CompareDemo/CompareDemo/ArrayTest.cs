using Compare.Comparer;
using Compare.ComplexType;
using Compare.SimpleType;
using System;
using Xunit;

namespace CompareDemo
{
    public class ArrayTest
    {

        [Fact]
        public void ComplexTypeArray()
        {
            var array1 = new Member[]{
                            new Member()
                            {
                                MemberType = MemberType.Teacher,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                Age = 40,
                                Birthday = null,
                                AnnualIncome = 400000.00M,
                                Teacher = null
                            },
                            new Member()
                            {
                                MemberType = MemberType.Teacher,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                Age = 40,
                                Birthday = null,
                                AnnualIncome = 400000.00M,
                                Teacher = null
                            }
                        };
            var array2 = new Member[]{
                            new Member()
                            {
                                MemberType = MemberType.Teacher,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                Age = 40,
                                Birthday = null,
                                AnnualIncome = 400000.00M,
                                Teacher = null
                            },
                            new Member()
                            {
                                MemberType = MemberType.Teacher,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                Age = 40,
                                Birthday = null,
                                AnnualIncome = 400000.00M,
                                Teacher = null
                            }
                        };
            var array11 = new Member[][]{
                            new Member[]{
                                new Member()
                                {
                                    MemberType = MemberType.Teacher,
                                    FirstName = "FirstName",
                                    MiddleName = "MiddleName",
                                    LastName = "LastName",
                                    Age = 40,
                                    Birthday = null,
                                    AnnualIncome = 400000.00M,
                                    Teacher = null
                                },
                                new Member()
                                {
                                    MemberType = MemberType.Teacher,
                                    FirstName = "FirstName",
                                    MiddleName = "MiddleName",
                                    LastName = "LastName",
                                    Age = 40,
                                    Birthday = null,
                                    AnnualIncome = 400000.00M,
                                    Teacher = null
                                }
                            }
                        };
            var array22 = new Member[][]{
                            new Member[]{
                                new Member()
                                {
                                    MemberType = MemberType.Teacher,
                                    FirstName = "FirstName",
                                    MiddleName = "MiddleName",
                                    LastName = "LastName",
                                    Age = 40,
                                    Birthday = null,
                                    AnnualIncome = 400000.00M,
                                    Teacher = null
                                },
                                new Member()
                                {
                                    MemberType = MemberType.Teacher,
                                    FirstName = "FirstName",
                                    MiddleName = "MiddleName",
                                    LastName = "LastName",
                                    Age = 40,
                                    Birthday = null,
                                    AnnualIncome = 400000.00M,
                                    Teacher = null
                                }
                            }
                        };
            Assert.Equal(array1, array2, new MemberEqualityComparer());
            Assert.NotEqual(array1, array1, new MemberEqualityComparer());
            Assert.Equal(array11, array22, new Member1JaggedArrayEqualityComparer());
            // Assert.Equal(array11, array22, new MemberEqualityComparer());
            // Assert.NotEqual(array11, array11, new MemberEqualityComparer());
        }

        [Fact]
        public void ComplexType()
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

            Assert.True(student2.AbsolutelyEqual(student3, "Id"));
            Assert.Equal(student2, student3, new MemberEqualityComparer());

            Assert.False(student3.AbsolutelyEqual(student4, "Id"));
            Assert.NotEqual(student3, student4, new MemberEqualityComparer());

            Assert.False(student3.AbsolutelyEqual(student3, "Id"));
            Assert.NotEqual(student3, student3, new MemberEqualityComparer());

            Assert.False(student4.AbsolutelyEqual(student4, "Id"));
            Assert.NotEqual(student4, student4, new MemberEqualityComparer());
        }
    }
}