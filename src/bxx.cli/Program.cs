using System;
using System.Text;
using System.IO;

namespace bxx.cli
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = @"Man is distinguished, not only by his reason, but by this singular passion from other animals, 
which is a lust of the mind, that by a perseverance of delight in the continued and indefatigable 
generation of knowledge, exceeds the short vehemence of any carnal pleasure.";
            var enc = new Encoder(Base64.Std);

            using FileStream file = File.OpenWrite("test.txt");
            using (Stream ms = new MemoryStream(Encoding.ASCII.GetBytes(text)))
            {
                enc.Encode(ms, file);
            }
        }
    }
}
