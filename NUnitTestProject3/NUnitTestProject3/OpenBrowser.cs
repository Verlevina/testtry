using NUnit.Framework;
using NUnitTestProject3.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Text;

namespace Tests
{
    [TestFixture]
    public class OpenBrowsers
    {
       private IWebDriver driver = new ChromeDriver(Directory.GetCurrentDirectory());
        private StringBuilder VerificateErrors;
        [SetUp]
        public void Setup()
        {          
            VerificateErrors = new StringBuilder();
            int num = 4;
            num.ToString();
        }

        [Test]
        public void OpenYandex()
        {
            SeleniumCommon.GoToUrl(driver, @"https://yandex.ru");
            /* 
            driver.FindElement(By.CssSelector(".input__control")).Clear();
            driver.FindElement(By.CssSelector(".input__control")).SendKeys("я найду тебя");
            */

            Assert.Pass();
        }
        [Test]
        public void OpenRambler()
        {
            SeleniumCommon.GoToUrl(driver,@"https://rambler.ru");
            Assert.Pass();
        }
        [TearDown]
        public void TearDown()
        {
            try {
                driver.Close();
            } catch {
                //ignore
            }
           
        
    }
    }
}