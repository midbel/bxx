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
}