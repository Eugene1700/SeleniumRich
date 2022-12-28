using OpenQA.Selenium;
using PepsicoCatcherPure.TmsPageModel.Services;
using SeleniumRich.Dom.Interfaces;

namespace PepsicoCatcherPure.TmsPageModel.PageModel.Common
{
    public class CheckBox : ElementBase, ICheckedElement
    {
        public CheckBox(IWebDriver webDriver, ElementBase parent, By @by, int? index) : base(webDriver, parent, @by, index)
        {
        }

        public bool IsChecked => Element.Selected;
        public void Check(bool state)
        {
            if (state == IsChecked)
                return;
            Element.Click();
            Waiter.Wait(() => IsChecked == state);
        }
        
        public bool TryCheck(bool state)
        {
            if (state == IsChecked)
                return true;
            Element.Click();
            var resultFailed = false;
            Waiter.Wait(() => IsChecked == state, (i) => resultFailed = true);
            return !resultFailed;
        }
    }
}