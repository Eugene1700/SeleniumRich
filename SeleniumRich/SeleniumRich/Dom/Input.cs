using OpenQA.Selenium;
using PepsicoCatcherPure.TmsPageModel.PageModel.Common;
using SeleniumRich.Dom.Interfaces;
using SeleniumRich.Services;

namespace SeleniumRich.Dom
{
    public class Input : ElementBase, IValueElement
    {
        public Input(IWebDriver webDriver, ElementBase parent, By @by, int? index) : base(webDriver, parent, @by, index)
        {
        }
        
        public string GetValue()
        {
            return Element.GetAttribute("value");
        }

        public void SetValue(string value, string expectedValue = null)
        {
            var v = expectedValue ?? value;
            if (GetValue() == v)
                return;
            Element.SendKeys(value);
            Element.WaitValue(v);
        }
    }
}