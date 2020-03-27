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


    public class Encoder
    {

        private IEncoder inner;
        private int size = 76;
        private const int linelen = 76;

        public Encoder(IEncoder encoder)
        {
            inner = encoder;
            switch (encoder)
            {
                case Base64 _:
                    size = (76 / 4) * 3;
                    break;
                case Base32 _:
                    size = (72 / 8) * 5;
                    break;
                case Base16 _:
                    size = 76;
                    break;
                default:
                    throw new ArgumentException($"unsupported IEncoder type {encoder.GetType()}");
            }
        }

        public void Encode(Stream reader, Stream writer)
        {
            BufferedStream br = new BufferedStream(reader);
            using (BufferedStream bw = new BufferedStream(writer))
            {
                while (true)
                {
                    byte[] buffer = new byte[size];
                    int read = br.Read(buffer, 0, buffer.Length);
                    if (read > 0)
                    {
                        byte[] data = new byte[read];
                        Array.Copy(buffer, data, read);

                        string str = inner.Encode(data);
                        bw.Write(Encoding.ASCII.GetBytes(str), 0, str.Length);
                    }
                    if (read < buffer.Length)
                    {
                        break;
                    }
                    bw.WriteByte((byte)'\r');
                    bw.WriteByte((byte)'\n');
                }
            }
        }
    }
}