using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using UnitTestProject1.Helpers;

[TestClass]
public class FindDataTests : BaseTest
{
    private IWebDriver _browser = new ChromeDriver(Directory.GetCurrentDirectory());

    [TestMethod]
    public void SendSearchData()
    {
      
        _browser.Navigate().GoToUrl(baseUrl);
        var page = GetPage(_browser);
        page.SearchField.SendKeys("Iwill find you");
        page.SubmitButton.Submit();

    }

    private Page GetPage(IWebDriver browser)
    {
        var result = new Page
        {
            SearchField = browser.FindElement(By.CssSelector("input#text")),
            SubmitButton = browser.FindElement(By.CssSelector("button.button"))
        };
        return result;
    }
}

public class Page
{
    public IWebElement SearchField { get; set; }

    public IWebElement SubmitButton { get; set; }
}