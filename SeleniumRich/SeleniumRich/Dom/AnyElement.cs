using OpenQA.Selenium;
using PepsicoCatcherPure.TmsPageModel.PageModel.Common;

namespace SeleniumRich.Dom
{
    public class AnyElement : ElementBase
    {
        public AnyElement(IWebDriver webDriver, ElementBase parent, By @by, int? index) : base(webDriver, parent, @by, index)
        {
        }
    }
}