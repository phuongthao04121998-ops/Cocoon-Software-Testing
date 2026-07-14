using NUnit.Framework;
using OpenQA.Selenium;
using CocoonAutomation.Core;

namespace CocoonAutomationTest
{
    [TestFixture]
    public class CustomerTests
    {
        private SeleniumHelper app;

        [SetUp]
        public void Setup() { app = new SeleniumHelper(); }

        [TearDown]
        public void TearDown() { app.Dispose(); }

        [Test]
        public void TC_KH_06_CustomerLoginWrongPassword()
        {
            app.GoToUrl("https://localhost:44368/Account/Login");
            app.InputByCss("input[name='email']", "lethiyennhi02082005@gmail.com");
            app.InputByCss("input[name='matkhau']", "sai123");
            app.Wait(20000);
            app.ClickByCss("button[type='submit']");
            app.Wait(3000);
            Assert.That(app.Driver.Url, Does.Not.Contain("/Home/Index"));
            Assert.That(app.Driver.PageSource.Contains("mật khẩu") || app.Driver.PageSource.Contains("Sai"), Is.True);
        }

        [Test]
        public void TC_KH_07_LoginEmptyEmail()
        {
            app.GoToUrl("https://localhost:44368/Account/Login");
            app.InputByCss("input[name='matkhau']", "123456");
            app.ClickByCss("button[type='submit']");
            app.Wait(1000);
            var emailInput = app.Driver.FindElement(By.CssSelector("input[name='email']"));
            Assert.That(emailInput.GetAttribute("validationMessage"), Is.Not.Null.And.Not.Empty);
        }
    }
}