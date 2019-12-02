using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.IO;

namespace Tests
{
    public class Tests
    {
        private string _browser;

        private string _url;

        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            _browser = config["browser"];
            _url = config["url"];
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}