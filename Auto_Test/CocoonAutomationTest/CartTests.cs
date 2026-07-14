using NUnit.Framework;
using OpenQA.Selenium;
using CocoonAutomation.Core;

namespace CocoonAutomationTest
{
    [TestFixture]
    public class CartTests
    {
        private SeleniumHelper app;

        [SetUp]
        public void Setup() { app = new SeleniumHelper(); }

        [TearDown]
        public void TearDown() { app.Dispose(); }

        [Test]
        public void TC_CART_01_AddToCart_WithCaptcha()
        {
            app.GoToUrl("https://localhost:44368/Account/Login");
            app.InputByCss("input[name='email']", "mainguyen@gmail.com");
            app.InputByCss("input[name='matkhau']", "123456");
            app.Wait(20000); 
            app.ClickByCss("button[type='submit']");
            app.GoToUrl("https://localhost:44368/Product");
            app.ClickByCss("a.btn-add-ajax[data-id='SP001']");
            app.Wait(2000);
        }

        [Test]
        public void TC_CART_02_RemoveProductFromCart()
        {
            app.GoToUrl("https://localhost:44368/Account/Login");
            app.InputByCss("input[name='email']", "mainguyen@gmail.com");
            app.InputByCss("input[name='matkhau']", "123456");
            app.Wait(20000); 
            app.ClickByCss("button[type='submit']");
            app.Wait(3000);

            app.GoToUrl("https://localhost:44368/Product");
            app.Wait(3000); 

            var btnAdd = app.Driver.FindElement(By.CssSelector("a.btn-add-ajax[data-id='SP001']"));
            ((IJavaScriptExecutor)app.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", btnAdd);
            app.Wait(1000);

            ((IJavaScriptExecutor)app.Driver).ExecuteScript("arguments[0].click();", btnAdd);
            app.Wait(4000); 

            app.GoToUrl("https://localhost:44368/Cart");
            app.Wait(3000); 

            var btnDelete = app.Driver.FindElement(By.CssSelector("#row-SP001 td:last-child a"));

            ((IJavaScriptExecutor)app.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", btnDelete);
            app.Wait(1000);

            ((IJavaScriptExecutor)app.Driver).ExecuteScript("arguments[0].click();", btnDelete);
            app.Wait(4000); 

            app.RefreshPage();
            app.Wait(2000);
            Assert.That(app.Driver.FindElements(By.Id("row-SP001")).Count == 0, Is.True);
        }

        [Test]
        public void TC_CART_03_UpdateQuantity_Valid()
        {
            app.GoToUrl("https://localhost:44368/Account/Login");
            app.InputByCss("input[name='email']", "mainguyen@gmail.com");
            app.InputByCss("input[name='matkhau']", "123456");
            app.Wait(20000); 
            app.ClickByCss("button[type='submit']");
            app.Wait(3000);

            app.GoToUrl("https://localhost:44368/Product");
            app.Wait(4000);

            var btnAdd = app.Driver.FindElement(By.CssSelector("a.btn-add-ajax[data-id='SP001']"));

            ((IJavaScriptExecutor)app.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", btnAdd);
            app.Wait(1500);

            ((IJavaScriptExecutor)app.Driver).ExecuteScript("arguments[0].click();", btnAdd);
            app.Wait(4000);

            app.GoToUrl("https://localhost:44368/Cart");
            app.Wait(3000);

            var qtyInput = app.Driver.FindElement(By.Id("qty-SP001"));

            qtyInput.SendKeys(Keys.Control + "a");
            app.Wait(500);

            qtyInput.SendKeys("2" + Keys.Enter);
            app.Wait(3000);
            app.RefreshPage();
            app.Wait(2000);

            var qtyInputAfterRefresh = app.Driver.FindElement(By.Id("qty-SP001"));
            string actualQuantity = qtyInputAfterRefresh.GetAttribute("value");

            Assert.That(
                actualQuantity,
                Is.EqualTo("2"),
                "Lỗi: Số lượng sản phẩm không được cập nhật thành 2 sau khi Enter và Refresh!"
            );
        }
    }
}