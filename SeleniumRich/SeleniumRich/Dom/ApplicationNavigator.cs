using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PepsicoCatcherPure.TmsPageModel.PageModel.Common;
using PepsicoCatcherPure.TmsPageModel.Services;

namespace SeleniumRich.Dom
{
    public class ApplicationNavigator : IDisposable
    {
        private readonly IWebDriver _webDriver;

        public ApplicationNavigator()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _webDriver = new ChromeDriver(options);
        }

        public TPage Enter<TPage>(string url) where TPage : PageBase
        {
            _webDriver.Url = url;
            var page = (TPage) Activator.CreateInstance(typeof(TPage), _webDriver);
            if (page == null)
                throw new InvalidOperationException($"Page not created {nameof(TPage)}");
            page.WaitPresent();
            return page;
        }

        public TPage Enter<TPage>() where TPage : PageBase
        {
            var page = (TPage) Activator.CreateInstance(typeof(TPage), _webDriver);
            if (page == null)
                throw new InvalidOperationException($"Page not created {nameof(TPage)}");
            page.WaitPresent();
            return page;
        }
        
        public PageBase EnterOr<TPage1, TPage2>() where TPage1 : PageBase where TPage2 : PageBase
        {
            var page1 = (TPage1) Activator.CreateInstance(typeof(TPage1), _webDriver);
            var page2 = (TPage2) Activator.CreateInstance(typeof(TPage2), _webDriver);
            var fail = false;
            PageBase pageRes = null;
            Waiter.Wait(() =>
            {
                if (page1.IsPresent())
                {
                    pageRes = page1;
                    return true;
                }
                if (page2.IsPresent())
                {
                    pageRes = page2;
                    return true;
                }

                return false;
            }, (i) => { fail = true;});
            return pageRes;
        }

        private PageBase WaitPageOrNull<TPage1>() where TPage1 : PageBase 
        {
            var page = (TPage1) Activator.CreateInstance(typeof(TPage1), _webDriver);
            if (page == null)
                throw new InvalidOperationException($"Page not created {nameof(TPage1)}");
            return page.WaitPresentOrNull();
        }

        public void Dispose()
        {
            _webDriver.Quit();
            _webDriver?.Dispose();
        }
    }
}