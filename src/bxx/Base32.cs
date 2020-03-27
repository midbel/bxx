using System.Text;

namespace bxx
{
    public sealed class Base32
    {
        private static readonly string std = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        private static readonly string hex = "0123456789ABCDEFGHIJKLMNOPQRSTUV";

        public static readonly Base32 Std = new Base32(std);
        public static readonly Base32 Hex = new Base32(hex);

        private string alphabet;

        private Base32(string alp)
        {
            alphabet = alp;
        }

        public string Encode(string str)
        {
            return Encode(Encoding.ASCII.GetBytes(str));
        }

        public string Encode(byte[] str)
        {
            return Encode(str, true);
        }

        public string Encode(string str, bool padding)
        {
            return Encode(Encoding.ASCII.GetBytes(str), padding);
        }

        public string Encode(byte[] str, bool padding)
        {
            var buf = new StringBuilder();
            int i = 0;
            while (i + 5 <= str.Length)
            {
                var a = str[i] >> 3;
                var b = ((str[i] & 0x07) << 2) | (str[i + 1] >> 6);
                var c = (str[i + 1] & 0x3F) >> 1;
                var d = ((str[i + 1] & 0x01) << 4) | str[i + 2] >> 4;
                var e = ((str[i + 2] & 0x0F) << 1) | (str[i + 3] >> 7);
                var f = (str[i + 3] & 0x7F) >> 2;
                var g = ((str[i + 3] & 0x03) << 3) | (str[i + 4]) >> 5;
                var h = str[i + 4] & 0x1F;

                buf.Append(alphabet[a]);
                buf.Append(alphabet[b]);
                buf.Append(alphabet[c]);
                buf.Append(alphabet[d]);
                buf.Append(alphabet[e]);
                buf.Append(alphabet[f]);
                buf.Append(alphabet[g]);
                buf.Append(alphabet[h]);

                i += 5;
            }
            if (i < str.Length)
            {
                int rest = str.Length - i;
                int padchar = 0;
                switch (rest)
                {
                    case 1:
                        var a = str[i] >> 3;
                        var b = (str[i] & 0x07) << 2;

                        buf.Append(alphabet[a]);
                        buf.Append(alphabet[b]);

                        padchar = 6;
                        break;
                    case 2:
                        var c = str[i] >> 3;
                        var d = ((str[i] & 0x07) << 2) | (str[i + 1] >> 6);
                        var e = (str[i + 1] & 0x3F) >> 1;
                        var f = (str[i + 1] & 0x01) << 4;

                        buf.Append(alphabet[c]);
                        buf.Append(alphabet[d]);
                        buf.Append(alphabet[e]);
                        buf.Append(alphabet[f]);
                        padchar = 4;
                        break;
                    case 3:
                        var j = str[i] >> 3;
                        var k = ((str[i] & 0x07) << 2) | (str[i + 1] >> 6);
                        var l = (str[i + 1] & 0x3F) >> 1;
                        var m = (str[i + 1] & 0x01) << 4 | (str[i + 2] >> 4);
                        var n = (str[i + 2] & 0x0F) << 1;

                        buf.Append(alphabet[j]);
                        buf.Append(alphabet[k]);
                        buf.Append(alphabet[l]);
                        buf.Append(alphabet[m]);
                        buf.Append(alphabet[n]);

                        padchar = 3;
                        break;
                    case 4:
                        var o = str[i] >> 3;
                        var p = ((str[i] & 0x07) << 2) | (str[i + 1] >> 6);
                        var q = (str[i + 1] & 0x3F) >> 1;
                        var r = (str[i + 1] & 0x01) << 4 | (str[i + 2] >> 4);
                        var s = ((str[i + 2] & 0x0F) << 1) | (str[i + 3] >> 7);
                        var t = (str[i + 3] & 0x7F) >> 2;
                        var u = (str[i + 3] & 0x03) << 3;

                        buf.Append(alphabet[o]);
                        buf.Append(alphabet[p]);
                        buf.Append(alphabet[q]);
                        buf.Append(alphabet[r]);
                        buf.Append(alphabet[s]);
                        buf.Append(alphabet[t]);
                        buf.Append(alphabet[u]);

                        padchar = 1;
                        break;
                }
                if (padding && padchar > 0)
                {
                    buf.Append('=', padchar);
                }
            }
            return buf.ToString();
        }

        public string Decode(string str)
        {
            return Decode(Encoding.ASCII.GetBytes(str));
        }

        public string Decode(byte[] str)
        {
            var buf = new StringBuilder();
            for (int i = 0; i < str.Length; i += 8)
            {
                var a = (ulong)alphabet.IndexOf((char)str[i]);
                var b = (ulong)alphabet.IndexOf((char)str[i + 1]);
                var c = (ulong)alphabet.IndexOf((char)str[i + 2]);
                var d = (ulong)alphabet.IndexOf((char)str[i + 3]);
                var e = (ulong)alphabet.IndexOf((char)str[i + 4]);
                var f = (ulong)alphabet.IndexOf((char)str[i + 5]);
                var g = (ulong)alphabet.IndexOf((char)str[i + 6]);
                var h = (ulong)alphabet.IndexOf((char)str[i + 7]);

                if (str[i + 2] == '=')
                {
                    ulong z = (b >> 2) | a << 3;
                    buf.Append((char)z);
                }
                else if (str[i + 4] == '=')
                {
                    ulong z = (d >> 4) | (c << 1) | (b << 6) | (a << 11);

                    buf.Append((char)((z >> 8) & 0xFF));
                    buf.Append((char)(z & 0xFF));
                }
                else if (str[i + 5] == '=')
                {
                    ulong z = (e >> 1) | (d << 4) | (c << 9) | (b << 14) | (a << 19);

                    buf.Append((char)((z >> 16) & 0xFF));
                    buf.Append((char)((z >> 8) & 0xFF));
                    buf.Append((char)(z & 0xFF));
                }
                else if (str[i + 7] == '=')
                {
                    ulong z = (g >> 3) | (f << 2) | (e << 7) | (d << 12) | (c << 17) | (b << 22) | (a << 27);

                    buf.Append((char)((z >> 24) & 0xFF));
                    buf.Append((char)((z >> 16) & 0xFF));
                    buf.Append((char)((z >> 8) & 0xFF));
                    buf.Append((char)(z & 0xFF));
                }
                else
                {
                    ulong z = h | (g << 5) | (f << 10) | (e << 15) | (d << 20) | (c << 25) | (b << 30) | (a << 35);

                    buf.Append((char)((z >> 32) & 0xFF));
                    buf.Append((char)((z >> 24) & 0xFF));
                    buf.Append((char)((z >> 16) & 0xFF));
                    buf.Append((char)((z >> 8) & 0xFF));
                    buf.Append((char)(z & 0xFF));
                }
            }
            return buf.ToString();
        }
    }
}