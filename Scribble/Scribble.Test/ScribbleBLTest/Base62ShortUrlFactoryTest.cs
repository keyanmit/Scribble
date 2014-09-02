using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScribbleBL.UrlGeneration;

namespace Scribble.Test.ScribbleBLTest
{
    [TestClass]
    public class Base62ShortUrlFactoryTest
    {
        private Base62ShortUrlFactory shortUrlGenerator;

        public Base62ShortUrlFactoryTest()
        {
            shortUrlGenerator = new Base62ShortUrlFactory();
        }

        [TestMethod]
        public void TestGetShortUrl()
        {
            //creation of req objects            
            Assert.AreEqual(shortUrlGenerator.GetShortUrl(25), "z");
            Assert.AreEqual(shortUrlGenerator.GetShortUrl(26), "A");
            Assert.AreEqual(shortUrlGenerator.GetShortUrl(27), "B");
            Assert.AreEqual(shortUrlGenerator.GetShortUrl(52), "0");
            Assert.AreEqual(shortUrlGenerator.GetShortUrl(51), "Z");
            Assert.AreEqual(shortUrlGenerator.GetShortUrl(61), "9");
            Assert.AreEqual(shortUrlGenerator.GetShortUrl(62), "ba");
            Assert.AreEqual(shortUrlGenerator.GetShortUrl(63), "bb");
        }

        [TestMethod]
        public void TestGetUrlId()
        {
            var randomGen = new Random();            
            for (int i = 0; i < 10; i++)
            {
                var tmp = (UInt64)randomGen.Next(100);
                Assert.AreEqual(tmp, shortUrlGenerator.GetUrlId(shortUrlGenerator.GetShortUrl(tmp)));
            }
            
            for (int i = 0; i < 10; i++)
            {
                var tmp = 1000 + (UInt64)randomGen.Next(100);
                Assert.AreEqual(tmp, shortUrlGenerator.GetUrlId(shortUrlGenerator.GetShortUrl(tmp)));
            }

            for (int i = 0; i < 10; i++)
            {
                var tmp = 1000000 + (UInt64)randomGen.Next(100);
                Assert.AreEqual(tmp, shortUrlGenerator.GetUrlId(shortUrlGenerator.GetShortUrl(tmp)));
            }

            for (int i = 0; i < 10; i++)
            {
                var tmp = 4000000000 + (UInt64)randomGen.Next(100);
                Assert.AreEqual(tmp, shortUrlGenerator.GetUrlId(shortUrlGenerator.GetShortUrl(tmp)));
            }

        }
    }
}
