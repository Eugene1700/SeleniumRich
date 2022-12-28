using System;
using System.Linq;
using OpenQA.Selenium;
using PepsicoCatcherPure.TmsPageModel.Services;
using SeleniumRich.Dom.Interfaces;

namespace PepsicoCatcherPure.TmsPageModel.PageModel.Common
{
    public class ListOfElements<TElement> : ElementBase where TElement: ElementBase
    {
        private readonly By _byElement;

        public ListOfElements(IWebDriver webDriver, ElementBase parent, By @byList, By @byElement,int? index) : base(webDriver, parent, @byList, index)
        {
            _byElement = byElement;
        }

        public TElement[] GetItems()
        {
            var index = 0;
            return Element.FindElements(_byElement).Select(x =>
                (TElement) Activator.CreateInstance(typeof(TElement), Driver, this, _byElement, index++)).ToArray();
        }
        
        public TElement WaitItemBySubstring(string subs)
        {
            return WaitItemPresent(GetTextPredicate(subs, true),
                items =>
                    $"Got [{items.Length}] items by selector [{_byElement.Criteria}]: {string.Join(", ", items.Select(x => ((ITextElement) x).GetText()))}"
            );
        }
        
        public TElement WaitItemPresent(Func<TElement, bool> predicate, Func<TElement[], string> onFail = null)
        {
            WaitPresent();
            var res = default(TElement);
            Waiter.Wait(() =>
                {
                    try
                    {
                        res = GetItems()
                            .FirstOrDefault(predicate);
                        return res != null;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("* WAITING ERROR " + e.Message);
                    }

                    return false;
                },
                (attempts) =>
                {
                    var msg = $"list of [{typeof(TElement).Name}] does not contain expected " +
                              $"element by predicate by [{attempts}] attempts";
                    if (onFail != null)
                        msg += "\r\n" + onFail(GetItems().ToArray());
                    throw new Exception(msg);
                }
            );
            return res;
        }
        
        private Func<TElement, bool> GetTextPredicate(string s, bool substring = false)
        {
            if (substring)
                return t => ((ITextElement) t).GetText().ToLower().Contains(s.ToLower());

            return t => ((ITextElement) t).GetText().ToLower() == s.ToLower();
        }
        
        public ListOfElements<TElement> WaitCount(int count, int? timeoutInMs = null)
        {
            Waiter.Wait(() => IsPresent() && GetCount() == count,
                attempts =>
                    Console.WriteLine($"Waiting for list count={count}, but " +
                                $"{(IsPresent() ? "was " + GetCount() : "list not present")}, attempts [{attempts}], itemSelector [] "),
                timeoutInMs ?? Waiter.DefaultTimeout,
                typeof(InvalidOperationException));
            return this;
        }
        
        public ListOfElements<TElement> WaitAtLeastCount(int count, int? timeoutInMs = null)
        {
            Waiter.Wait(() => IsPresent() && GetCount() >= count,
                attempts =>
                    Console.WriteLine($"Waiting for list count={count}, but " +
                                      $"{(IsPresent() ? "was " + GetCount() : "list not present")}, attempts [{attempts}], itemSelector [] "),
                timeoutInMs ?? Waiter.DefaultTimeout,
                typeof(InvalidOperationException));
            return this;
        }

        public int GetCount()
        {
            return GetItems().Length;
        }
    }
}