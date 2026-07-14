using NUnit.Framework;
using OpenQA.Selenium;
using CocoonAutomation.Core;

namespace CocoonAutomationTest
{
    [TestFixture]
    public class OrderTests
    {
        private SeleniumHelper app;

        [SetUp]
        public void Setup() { app = new SeleniumHelper(); }

        [TearDown]
        public void TearDown() { app.Dispose(); }

        [Test]
        public void TC_ORDER_01_PlaceOrderSuccess()
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

            app.InputByName("hoTen", "Nguyễn Văn A");
            app.InputByName("sdt", "0987654321");
            app.InputByName("diaChi", "123 Đường ABC, Quận 1, TP.HCM");
            app.Wait(1000);

            var paymentSelect = app.Driver.FindElement(By.Id("maPT"));
            ((IJavaScriptExecutor)app.Driver).ExecuteScript("arguments[0].selectedIndex = 0; arguments[0].dispatchEvent(new Event('change'));", paymentSelect);
            app.Wait(1000);

            var btnOrder = app.Driver.FindElement(By.XPath("//button[contains(text(), 'ĐẶT HÀNG NGAY')]"));

            ((IJavaScriptExecutor)app.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", btnOrder);
            app.Wait(1000);

            ((IJavaScriptExecutor)app.Driver).ExecuteScript("arguments[0].click();", btnOrder);
            app.Wait(6000); 

            Assert.That(
                app.Driver.PageSource,
                Does.Contain("thành công").Or.Contain("Cảm ơn").Or.Contain("đơn hàng"),
                "Lỗi: Không tìm thấy thông báo hoặc giao diện đặt hàng thành công!"
            );
        }

        [Test]
        public void TC_ORDER_02_PlaceOrderWithoutLogin()
        {
            app.GoToUrl("https://localhost:44368/Product");
            app.ClickByCss("a.btn-add-ajax[data-id='SP001']");
            app.Wait(4000);
            Assert.That(app.Driver.Url.Contains("/Account/Login") || app.Driver.Url.Contains("/Login"), Is.True);
        }
    }
}