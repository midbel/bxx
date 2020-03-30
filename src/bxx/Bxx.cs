using System;
using System.Text;
using System.IO;

namespace bxx
{
    public interface IEncoder
    {
        string Encode(string str);
        string Encode(byte[] str);
    }

    public interface IDecoder
    {
        string Decode(string str);
        string Decode(byte[] str);
    }

    namespace bxx.strings.extensions
    {
        public static class ExtendString
        {
            public static string ToBase64Std(this String str)
            {
                return Base64.Std.Encode(str);
            }

            public static string ToBase64Url(this String str)
            {
                return Base64.Url.Encode(str);
            }

            public static string ToBase32Std(this String str)
            {
                return Base32.Std.Encode(str);
            }

            public static string ToBase32Hex(this String str)
            {
                return Base32.Hex.Encode(str);
            }

            public static string ToBase16(this String str)
            {
                return Base16.Std.Encode(str);
            }
        }
    }

}