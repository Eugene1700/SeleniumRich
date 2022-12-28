using System;
using System.Linq;
using OpenQA.Selenium;
using PepsicoCatcherPure.TmsPageModel.Services;
using SeleniumRich.Dom;

namespace PepsicoCatcherPure.TmsPageModel.PageModel.Common
{
    public class ComboBox: ElementBase
    {
        public ComboBox(IWebDriver webDriver, ElementBase parent, By @by, int? index) : base(webDriver, parent, @by, index)
        {
            Options = new ListOfElements<StaticText>(webDriver, parent, by, By.TagName("option"), null);
        }

        public StaticText[] SelectedOptions => Options.GetItems().Where(x => x.HasAttr("selected")).ToArray();

        public ListOfElements<StaticText> Options { get; }

        public bool SelectOption(string option, bool singleMode, bool skipControl = false) 
        {
            if (!skipControl && SelectedOptions.Any(x=>x.GetText() == option))
            {
                return false;
            }
            Options.WaitPresent();
            var el = Options.WaitItemBySubstring(option);
            el.Click();
            if (!skipControl)
                Waiter.Wait(() => (!singleMode || SelectedOptions.Length == 1) && SelectedOptions.Any(x=>x.GetText() == option));
            return true;
        }
    }
}