using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Webscraper.Tests
{
    [TestClass]
    public class URLTest
    {
        [Priority(1)]
        [Owner("Mathew Holcroft")]
        [TestMethod]
        public void DirectoryExists()
        {
            Assert.IsTrue(Directory.Exists(@"C:\Output"));
        }
    }
}
