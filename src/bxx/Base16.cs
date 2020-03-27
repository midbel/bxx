using System;
using System.Text;

namespace bxx
{
    public sealed class Base16
    {
        public static Base16 Std = new Base16();

        private const string alphabet = "0123456789ABCDEF";

        private Base16()
        {

        }

        public string Decode(string str)
        {
            return Decode(Encoding.ASCII.GetBytes(str));
        }

        public string Decode(byte[] str)
        {
            if (str.Length % 2 != 0)
            {
                throw new ArgumentException();
            }
            var buf = new StringBuilder();
            for (int i = 0; i < str.Length; i += 2)
            {
                int a = alphabet.IndexOf((char)str[i]);
                int b = alphabet.IndexOf((char)str[i + 1]);

                var c = (char)((a << 4) | b);
                buf.Append(c);
            }
            return buf.ToString();
        }

        public string Encode(string str)
        {
            return Encode(Encoding.ASCII.GetBytes(str));
        }

        public string Encode(byte[] str)
        {
            var buf = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                var a = str[i] >> 4;
                var b = str[i] & 0x0F;

                buf.Append(alphabet[a]);
                buf.Append(alphabet[b]);
            }

            return buf.ToString();
        }
    }
}
