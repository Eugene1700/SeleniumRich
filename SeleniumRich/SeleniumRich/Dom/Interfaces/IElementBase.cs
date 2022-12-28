using SeleniumRich.Services;

namespace SeleniumRich.Dom.Interfaces
{
    public interface IElementBase
    {
        IElementBase WaitPresent(int timeOutInSeconds = SeleniumExtensions.DefaultTimeoutInSeconds);

        IElementBase WaitPresentOrNull(int timeOutInSeconds = SeleniumExtensions.DefaultTimeoutInSeconds);

        void WaitEnabled(int timeOutInSeconds = SeleniumExtensions.DefaultTimeoutInSeconds);

        bool IsPresent();

        bool IsEnabled();
        
    }
}