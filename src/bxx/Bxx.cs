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

    public class Decoder
    {
        private IDecoder inner;
        private int linelen = 76;

        public Decoder(IDecoder decoder)
        {
            inner = decoder;
            switch (decoder)
            {
                case Base64 _:
                    linelen = 76;
                    break;
                case Base32 _:
                    linelen = 72;
                    break;
                case Base16 _:
                    linelen = 76;
                    break;
                default:
                    throw new ArgumentException($"unsupported IEncoder type {decoder.GetType()}");
            }
        }

        public void Decode(Stream reader, Stream Writer)
        {
            BufferedStream br = new BufferedStream(reader);
            using (BufferedStream bw = new BufferedStream(Writer))
            {
                byte[] buffer = new byte[linelen];
                while (true)
                {
                    int read = reader.Read(buffer, 0, buffer.Length);
                    if (read > 0)
                    {
                        string str = inner.Decode(buffer[..read]);
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


    public class Encoder
    {

        private IEncoder inner;
        private int size = 76;
        private int linelen = 76;

        public Encoder(IEncoder encoder)
        {
            inner = encoder;
            switch (encoder)
            {
                case Base64 _:
                    linelen = 76;
                    size = (linelen / 4) * 3;
                    break;
                case Base32 _:
                    linelen = 72;
                    size = (linelen / 8) * 5;
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
                byte[] buffer = new byte[size];
                while (true)
                {
                    int read = br.Read(buffer, 0, buffer.Length);
                    if (read > 0)
                    {
                        string str = inner.Encode(buffer[..read]);
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