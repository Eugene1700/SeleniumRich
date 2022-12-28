using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumRich.Dom.Interfaces;
using SeleniumRich.Services;

namespace PepsicoCatcherPure.TmsPageModel.PageModel.Common
{
    public abstract class ElementBase : IElementBase
    {
        protected IWebDriver Driver { get; }

        private readonly ElementBase _parent;
        protected readonly By ByCriteria;
        private readonly int? _index;

        public IWebElement Element => _index == null ? SearchScope.FindElement(ByCriteria) : SearchScope.FindElements(ByCriteria)[_index.Value];

        private ISearchContext SearchScope => _parent != null ? (ISearchContext) _parent.Element : Driver;
        
        protected ElementBase(IWebDriver webDriver, ElementBase parent, By by, int? index)
        {
            _parent = parent;
            ByCriteria = @by;
            _index = index;
            Driver = webDriver;
        }

        public IElementBase WaitPresent(int timeOutInSeconds = SeleniumExtensions.DefaultTimeoutInSeconds)
        {
            if (_index == null)
            {
                Driver.WaitElement(ByCriteria, timeOutInSeconds);
                return this;
            }
            else
            {
                Driver.WaitElements(ByCriteria, timeOutInSeconds);
                return this;
            }
        }
        
        public IElementBase WaitPresentOrNull(int timeOutInSeconds = SeleniumExtensions.DefaultTimeoutInSeconds)
        {
            var res = Driver.WaitElementOrNull(ByCriteria, timeOutInSeconds);
            return res == null ? null : this;
        }
        
        public void WaitEnabled(int timeOutInSeconds = SeleniumExtensions.DefaultTimeoutInSeconds)
        {
            Element.WaitEnabled(timeOutInSeconds);
        }

        public bool IsPresent()
        {
            return Driver.TryFindElement(ByCriteria, out _);
        }

        public bool IsEnabled()
        {
            return Element.Enabled;
        }

        public bool HasAttr(string attr)
        {
            var attrN = Element.GetAttribute(attr);
            return attrN != null;
        }
        
        public string GetAttrValue(string attr)
        {
            var attrN = Element.GetAttribute(attr);
            return attrN;
        }

        public int? GetIndex()
        {
            return _index;
        }
        
        public void ScrollToElement()
        {
            Actions actions = new Actions(Driver);
            actions.MoveToElement(Element);
            actions.Perform();
        }
    }
}