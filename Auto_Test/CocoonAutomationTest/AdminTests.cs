using NUnit.Framework;
using CocoonAutomation.Core;

namespace CocoonAutomationTest
{
    [TestFixture]
    public class AdminTests
    {
        private SeleniumHelper app;

        [SetUp]
        public void Setup() { app = new SeleniumHelper(); }

        [TearDown]
        public void TearDown() { app.Dispose(); }

        [Test]
        public void TC_ADMIN_01_AdminLoginSuccess()
        {
            app.GoToUrl("https://localhost:44368/Admin/Login");
            app.InputByName("username", "hung.nguyen");
            app.InputByName("password", "hung123");
            app.ClickByXPath("//button[@type='submit']");
            app.Wait(2000);
            Assert.That(app.Driver.Url, Does.Contain("/Admin/Dashboard"));
        }

        [Test]
        public void TC_ADMIN_02_AdminLoginWrongPassword()
        {
            app.GoToUrl("https://localhost:44368/Admin/Login");
            app.InputByName("username", "hung.nguyen");
            app.InputByName("password", "sai123");
            app.ClickByXPath("//button[@type='submit']");
            app.Wait(3000);
            Assert.That(app.Driver.Url, Does.Not.Contain("/Admin/Dashboard"));
            Assert.That(app.Driver.PageSource.Contains("mật khẩu") || app.Driver.PageSource.Contains("Sai"), Is.True);
        }
    }
}