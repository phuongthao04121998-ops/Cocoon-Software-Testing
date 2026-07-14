using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI; 
namespace CocoonAutomation.Core
{
    public class SeleniumHelper : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        public SeleniumHelper()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        public void GoToUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);
            Wait(2000);
        }

        public void InputByCss(string cssSelector, string text)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(d => d.FindElement(By.CssSelector(cssSelector)));


            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Wait(500);

            element.Clear();
            element.SendKeys(text);
        }

        public void InputByName(string nameAttribute, string text)
        {
            var element = Driver.FindElement(By.Name(nameAttribute));
            element.Clear();
            element.SendKeys(text);
        }

        public void ClickByCss(string cssSelector)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(d => d.FindElement(By.CssSelector(cssSelector)));


            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Wait(800); 

            element.Click();
        }

        public void ClickByXPath(string xpath)
        {
            Driver.FindElement(By.XPath(xpath)).Click();
        }

        public void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public void RefreshPage()
        {
            Driver.Navigate().Refresh();
            Wait(2000);
        }

        public void Dispose()
        {
            Driver?.Quit();
            Driver?.Dispose();
        }
    }
}