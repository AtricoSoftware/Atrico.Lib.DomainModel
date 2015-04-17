using System;

namespace Atrico.Lib.DomainModel.Profiling
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var profile = new ProfileValueObject();
            var start = DateTimeOffset.Now;
            profile.Create();
            var created = DateTimeOffset.Now;
            profile.Compare();
            var compared = DateTimeOffset.Now;
            var end = DateTimeOffset.Now;
            Console.WriteLine("Created = {0}", created - start);
            Console.WriteLine("Compared = {0}", compared - created);
            Console.WriteLine("Total = {0}", end - start);
        }
    }
}