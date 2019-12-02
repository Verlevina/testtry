using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;


namespace NUnitTestProject1.Helpers
{
    class Starter
    {
        public static void OpenSite(RemoteWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://yandex.ru");
            var yaSearch = driver.FindElement(By.CssSelector(".input_size_ws-head.input__control"));
            yaSearch.SendKeys("Find Something to me");
        }
    }
}
