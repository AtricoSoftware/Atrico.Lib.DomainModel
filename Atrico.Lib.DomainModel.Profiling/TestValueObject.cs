namespace Atrico.Lib.DomainModel.Profiling
{
    public class TestValueObject : ValueObject<TestValueObject>
    {
        private readonly int _i;
        private readonly float _f;
        private readonly string _s;

        public TestValueObject(int i, float f, string s)
        {
            _i = i;
            _f = f;
            _s = s;
        }

        protected override int GetHashCodeImpl()
        {
            return _i.GetHashCode() ^ _f.GetHashCode() ^ _s.GetHashCode();
        }

        protected override bool EqualsImpl(TestValueObject other)
        {
            return _i.Equals(other._i) && _f.Equals(other._f) && _s.Equals(other._s);
        }
    }
}