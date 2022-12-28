using OpenQA.Selenium;
using PepsicoCatcherPure.TmsPageModel.PageModel.Common;
using SeleniumRich.Dom.Interfaces;
using SeleniumRich.Services;

namespace SeleniumRich.Dom
{
    public class StaticText : ElementBase, ITextElement, IClickElement
    {
        public StaticText(IWebDriver webDriver, ElementBase parent, By @by, int? index) : base(webDriver, parent, @by, index)
        {
        }

        public string GetText()
        {
            return Element.Text;
        }

        public void WaitText(string text)
        {
            Element.WaitText(text);
        }
        
        public void WaitTextContains(string text)
        {
            Element.WaitTextContains(text);
        }
        
        public bool TryWaitTextContains(string text)
        {
            return Element.TryWaitTextContains(text);
        }

        public void Click()
        {
            Element.Click();
        }
    }
}