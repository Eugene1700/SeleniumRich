using PepsicoCatcherPure.TmsPageModel.Services;

namespace PepsicoCatcherPure.TmsPageModel.PageModel.Common
{
    public abstract class PageBase
    {
        public abstract bool IsPresent();
        
        public PageBase WaitPresent()
        {
            Waiter.Wait(IsPresent);
            return this;
        }
        
        public PageBase WaitPresentOrNull()
        {
            var fail = false;
            Waiter.Wait(IsPresent, (i) => { fail = true;});
            return fail ? null : this;
        }
    }
}