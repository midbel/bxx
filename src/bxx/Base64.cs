using System.Text;

namespace bxx
{
    public class Base64
    {
        private static readonly string std = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        private static readonly string url = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

        public static readonly Base64 Std = new Base64(std);
        public static readonly Base64 Url = new Base64(url);

        private string alphabet;

        private Base64(string alp)
        {
            alphabet = alp;
        }

        public string Encode(string str)
        {
            return Encode(str, true);
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
            var buf = new StringBuilder(str.Length * 2);
            int i = 0;
            while (i < str.Length && i + 3 <= str.Length)
            {
                var a = str[i] >> 2;
                var b = ((str[i] & 0x03) << 4) | (str[i + 1] >> 4);
                var c = (str[i + 1] & 0x0F) << 2 | str[i + 2] >> 6;
                var d = str[i + 2] & 0x3F;

                buf.Append(alphabet[a]);
                buf.Append(alphabet[b]);
                buf.Append(alphabet[c]);
                buf.Append(alphabet[d]);

                i += 3;
            }
            if (i < str.Length)
            {
                int rest = str.Length - i;
                int padchar = 0;
                switch (rest)
                {
                    case 1:
                        var e = str[i] >> 2;
                        var f = (str[i] & 0x03) << 4;

                        buf.Append(alphabet[e]);
                        buf.Append(alphabet[f]);
                        padchar = 2;
                        break;
                    case 2:
                        var g = str[i] >> 2;
                        var h = (str[i] & 0x03) << 4 | (str[i + 1] >> 4);
                        var j = (str[i + 1] & 0x0F) << 2;

                        buf.Append(alphabet[g]);
                        buf.Append(alphabet[h]);
                        buf.Append(alphabet[j]);
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
            return "";
        }
    }
}