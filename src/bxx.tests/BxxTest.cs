using System;
using Xunit;
using Xunit.Abstractions;

namespace bxx.tests
{
    public class Base64Test
    {

        [Fact]
        public void TestStdEncode()
        {
            Assert.Equal("", Base64.Std.Encode(""));
            Assert.Equal("Zg==", Base64.Std.Encode("f"));
            Assert.Equal("Zm8=", Base64.Std.Encode("fo"));
            Assert.Equal("Zm9v", Base64.Std.Encode("foo"));
            Assert.Equal("Zm9vYg==", Base64.Std.Encode("foob"));
            Assert.Equal("Zm9vYmE=", Base64.Std.Encode("fooba"));
            Assert.Equal("Zm9vYmFy", Base64.Std.Encode("foobar"));
        }

        [Fact]
        public void TestUrlEncode()
        {
            Assert.Equal("", Base64.Url.Encode(""));
            Assert.Equal("Zg==", Base64.Url.Encode("f"));
            Assert.Equal("Zm8=", Base64.Url.Encode("fo"));
            Assert.Equal("Zm9v", Base64.Url.Encode("foo"));
            Assert.Equal("Zm9vYg==", Base64.Url.Encode("foob"));
            Assert.Equal("Zm9vYmE=", Base64.Url.Encode("fooba"));
            Assert.Equal("Zm9vYmFy", Base64.Url.Encode("foobar"));
        }

        [Fact]
        public void TestUrlDecode()
        {
            Assert.Equal("foo", Base64.Url.Decode("Zm9v"));
            Assert.Equal("foob", Base64.Url.Decode("Zm9vYg=="));
            Assert.Equal("fooba", Base64.Url.Decode("Zm9vYmE="));
            Assert.Equal("foobar", Base64.Url.Decode("Zm9vYmFy"));
        }

        [Fact]
        public void TestStdDecode()
        {
            Assert.Equal("f", Base64.Std.Decode("Zg=="));
            Assert.Equal("fo", Base64.Std.Decode("Zm8="));
            Assert.Equal("foo", Base64.Std.Decode("Zm9v"));
            Assert.Equal("foob", Base64.Std.Decode("Zm9vYg=="));
            Assert.Equal("fooba", Base64.Std.Decode("Zm9vYmE="));
            Assert.Equal("foobar", Base64.Std.Decode("Zm9vYmFy"));
        }
    }

    public class Base32Test
    {
        [Fact]
        public void TestStdEncode()
        {
            Assert.Equal("", Base32.Std.Encode(""));
            Assert.Equal("MY======", Base32.Std.Encode("f"));
            Assert.Equal("MZXQ====", Base32.Std.Encode("fo"));
            Assert.Equal("MZXW6===", Base32.Std.Encode("foo"));
            Assert.Equal("MZXW6YQ=", Base32.Std.Encode("foob"));
            Assert.Equal("MZXW6YTB", Base32.Std.Encode("fooba"));
            Assert.Equal("MZXW6YTBOI======", Base32.Std.Encode("foobar"));
        }

        [Fact]
        public void TestHexEncode()
        {
            Assert.Equal("", Base32.Hex.Encode(""));
            Assert.Equal("CO======", Base32.Hex.Encode("f"));
            Assert.Equal("CPNG====", Base32.Hex.Encode("fo"));
            Assert.Equal("CPNMU===", Base32.Hex.Encode("foo"));
            Assert.Equal("CPNMUOG=", Base32.Hex.Encode("foob"));
            Assert.Equal("CPNMUOJ1", Base32.Hex.Encode("fooba"));
            Assert.Equal("CPNMUOJ1E8======", Base32.Hex.Encode("foobar"));
        }
    }

    public class Base16Test
    {
        [Fact]
        public void TestEncode()
        {
            Assert.Equal("", Base16.Std.Encode(""));
            Assert.Equal("66", Base16.Std.Encode("f"));
            Assert.Equal("666F", Base16.Std.Encode("fo"));
            Assert.Equal("666F6F", Base16.Std.Encode("foo"));
            Assert.Equal("666F6F62", Base16.Std.Encode("foob"));
            Assert.Equal("666F6F6261", Base16.Std.Encode("fooba"));
            Assert.Equal("666F6F626172", Base16.Std.Encode("foobar"));
        }

        [Fact]
        public void TestDecode()
        {
            Assert.Equal("foobar", Base16.Std.Decode("666F6F626172"));
        }
    }
}
