using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScribbleBL.UrlGeneration;

namespace Scribble.Test.ScribbleBLTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBaseUrlValidator()
        {
            var urlfactory = new Base62ShortUrlFactory();

            Assert.AreEqual(urlfactory.ValidateUrl("gJz"), true);
            Assert.AreEqual(urlfactory.ValidateUrl("gJ!"),false);
            Assert.AreEqual(urlfactory.ValidateUrl("g~12.asdf-="),false);
        }
    }
}
