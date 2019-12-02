using NUnit.Framework;
using OpenQA.Selenium.IE;
using System.IO;
using NUnitTestProject1.Helpers;
using OpenQA.Selenium;

namespace Tests
{

    public class Tests
    {
        private InternetExplorerDriver _driver;
        [SetUp]
        public void Setup()
        {
            var path = Directory.GetCurrentDirectory();
            var driverService = InternetExplorerDriverService.CreateDefaultService(path);
            _driver = new InternetExplorerDriver(driverService);
        }

        [Test]
        public void Test1()
        {
            Starter.OpenSite(_driver);

            Assert.Pass();
        }
    }
}