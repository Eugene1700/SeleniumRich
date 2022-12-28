using OpenQA.Selenium;
using PepsicoCatcherPure.TmsPageModel.PageModel.Common;
using SeleniumRich.Dom.Interfaces;
using SeleniumRich.Services;

namespace SeleniumRich.Dom
{
    public class Button : ElementBase, IClickElement, ITextElement
    {
        public Button(IWebDriver webDriver, ElementBase parent, By @by, int? index) : base(webDriver, parent, @by, index)
        {
        }

        public void Click()
        {
            Element.Click();
        }

        public string GetText()
        {
            return Element.Text;
        }

        public void WaitText(string text)
        {
            Element.WaitText(text);
        }
    }
}