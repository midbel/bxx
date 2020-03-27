using System;

namespace bxx.cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Base32.Std.Decode("MZXQ===="));
        }
    }
}
