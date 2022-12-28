using PepsicoCatcherPure.TmsPageModel.PageModel;
using PepsicoCatcherPure.TmsPageModel.PageModel.Common;
using SeleniumRich.Dom;
using SeleniumRich.Dom.Interfaces;

namespace PepsicoCatcherPure.TmsPageModel.Services
{
    public static class ClickElementExtensions
    {
        public static TPage Go<TPage>(this IClickElement clickElement, ApplicationNavigator applicationNavigator)
            where TPage : PageBase
        {
            clickElement.Click();
            return applicationNavigator.Enter<TPage>();
        }
        
        public static PageBase GoOr<TPage1, TPage2>(this IClickElement clickElement, ApplicationNavigator applicationNavigator)
            where TPage1 : PageBase
            where TPage2: PageBase
        {
            clickElement.Click();
            return applicationNavigator.EnterOr<TPage1, TPage2>();
        }
    }
}