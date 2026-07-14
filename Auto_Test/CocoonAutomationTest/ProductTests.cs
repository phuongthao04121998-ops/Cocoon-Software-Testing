using NUnit.Framework;
using OpenQA.Selenium;
using CocoonAutomation.Core;

namespace CocoonAutomationTest
{
    [TestFixture]
    public class ProductTests
    {
        private SeleniumHelper app;

        [SetUp]
        public void Setup() { app = new SeleniumHelper(); }

        [TearDown]
        public void TearDown() { app.Dispose(); }

        [Test]
        public void TC_SP_01_SearchProductNotExist()
        {
            app.GoToUrl("https://localhost:44368/Product");
            app.InputByName("search", "xyz123@@");
            app.ClickByXPath("//button[contains(text(), 'ÁP DỤNG')]");
            app.Wait(3000);
            Assert.That(app.Driver.PageSource.Contains("Không tìm thấy") || app.Driver.PageSource.Contains("0 sản phẩm"), Is.True);
        }

        [Test]
        public void TC_SP_07_OpenProductPage()
        {
            app.GoToUrl("https://localhost:44368/Product");
            Assert.That(app.Driver.Url, Does.Contain("/Product"));
            Assert.That(app.Driver.PageSource.Contains("Tìm tên sản phẩm"), Is.True);
        }

        [Test]
        public void TC_SP_08_ViewProductList()
        {
            app.GoToUrl("https://localhost:44368/Product");
            var productElements = app.Driver.FindElements(By.CssSelector(".col-md-9 .col-md-4, .col-md-9 a"));
            Assert.That(productElements.Count, Is.GreaterThan(0));
            Assert.That(app.Driver.PageSource.Contains("Bưởi") || app.Driver.PageSource.Contains("Cà phê"), Is.True);
        }
    }
}