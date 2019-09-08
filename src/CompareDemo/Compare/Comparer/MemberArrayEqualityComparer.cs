using System.Collections.Generic;
using Compare.ComplexType;
using Xunit;

namespace Compare.Comparer
{
    public class MemberEqualityComparer : IEqualityComparer<Member>
    {
        public bool Equals(Member x, Member y)
        {
            if (x == null && y == null)
                return true;
            return ComplexComparer.AbsolutelyEqual(x, y, "Id");
        }

        public int GetHashCode(Member obj)
        {
            throw new System.NotImplementedException();
        }

    }

    public class Member1JaggedArrayEqualityComparer : IEqualityComparer<Member[]>
    {
        public bool Equals(Member[] x, Member[] y)
        {
            if (x == null && y == null)
                return true;

            if (x?.Length != y?.Length)
                return false;

            Assert.Equal(x, y, new MemberEqualityComparer());
            return true;
        }

        public int GetHashCode(Member[] obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Member2JaggedArrayEqualityComparer : IEqualityComparer<Member[][]>
    {
        public bool Equals(Member[][] x, Member[][] y)
        {
            if (x == null && y == null)
                return true;

            if (x?.Length != y?.Length)
                return false;

            Assert.Equal(x, y, new Member1JaggedArrayEqualityComparer());
            return true;
        }

        public int GetHashCode(Member[][] obj)
        {
            throw new System.NotImplementedException();
        }
    }
}