using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atrico.Lib.DomainModel.Profiling
{
    internal class ProfileValueObject
    {
        private static readonly int[] _ints;
        private static readonly float[] _floats;
        private static readonly string[] _strings;

        static ProfileValueObject()
        {
            var ints = new List<int>();
            var floats = new List<float>();
            var strings = new List<string>();

            for (var i = 0; i < 200; ++i)
            {
                ints.Add(i);
                floats.Add(i);
                var sb = new StringBuilder();
                for (var ch = 0; ch < 5; ++ch)
                {
                    var chi = (ch + i)%26;
                    sb.Append((char)('A' + chi));
                }
                strings.Add(sb.ToString());
            }
            _ints = ints.ToArray();
            _floats = floats.ToArray();
            _strings = strings.ToArray();
        }

        private TestValueObject[] _objects;

        public void Create()
        {
            // Create objects
            _objects = (from i in _ints from f in _floats from s in _strings select new TestValueObject(i, f, s)).ToArray();
        }

        public void Compare()
        {
            TestValueObject prev = null;
            foreach (var obj in _objects)
            {
                if (prev != null)
                {
                    var equ = prev.Equals(obj);
                }
                prev = obj;
            }
        }
    }
}