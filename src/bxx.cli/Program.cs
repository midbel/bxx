using System;

namespace bxx.cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Base64.Std.Decode("Zm9vYmE="));
        }
    }
}
