using System;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PepsicoCatcherPure.TmsPageModel.Services;

namespace SeleniumRich.Services
{
    public static class SeleniumExtensions
    {
        
        public static IWebElement WaitElement(this IWebDriver driver, By by, int timeoutInSeconds = 0)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            else
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultTimeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
        }

        public const int DefaultTimeoutInSeconds = 15;

        public static ReadOnlyCollection<IWebElement> WaitElements(this IWebDriver driver, By by, int timeoutInSeconds = 0)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElements(by));
            }
            else
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                return wait.Until(drv => drv.FindElements(by));
            }
        }

        public static IWebElement WaitOneElement(this IWebDriver driver, params By[] by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            return wait.Until(drv =>
            {
                IWebElement current = null;
                for (int i = 0; i < by.Length; i++)
                {
                    try
                    {
                        current = drv.FindElement(by[i]);
                        if (current != null)
                            return current;
                    }
                    catch (NoSuchElementException e)
                    {
                        continue;
                    }
                    
                }
                throw new NoSuchElementException();
            });
        }

        public static bool TryFindElement(this IWebElement element, By by, out IWebElement foundElement)
        {
            try
            {
                foundElement = element.FindElement(by);
                return true;
            }
            catch (NoSuchElementException e)
            {
                foundElement = null;
                return false;
            }
        }
        
        public static bool TryFindElements(this IWebElement element, By by, out IWebElement[] foundElements)
        {
            try
            {
                foundElements = element.FindElements(by).ToArray();
                return true;
            }
            catch (NoSuchElementException e)
            {
                foundElements = null;
                return false;
            }
        }
        
        public static bool TryFindElement(this IWebDriver element, By by, out IWebElement foundElement)
        {
            try
            {
                foundElement = element.FindElement(by);
                return true;
            }
            catch (NoSuchElementException e)
            {
                foundElement = null;
                return false;
            }
        }
        
        public static bool TryFindElements(this IWebDriver element, By by, out IWebElement[] foundElements)
        {
            try
            {
                foundElements = element.FindElements(by).ToArray();
                return true;
            }
            catch (NoSuchElementException e)
            {
                foundElements = null;
                return false;
            }
        }

        public static void WaitValue(this IWebElement element, string text)
        {
            Waiter.Wait(() => element.GetAttribute("value")== text,
                (i) => throw new InvalidOperationException($"Текст [{text}] не найден для элемента [{element.GetAttribute("class")}], был [{element.GetAttribute("value")}]"));
        }
        
        public static void WaitText(this IWebElement element, string text)
        {
            Waiter.Wait(() => element.Text== text,
                (i) => throw new InvalidOperationException($"Текст [{text}] не найден для элемента [{element.GetAttribute("class")}], был [{element.Text}]"));
        }
        
        public static void WaitTextContains(this IWebElement element, string text)
        {
            Waiter.Wait(() => element.Text.Contains(text),
                (i) => throw new InvalidOperationException($"Текст [{text}] не найден для элемента [{element.GetAttribute("class")}], был [{element.Text}]"));
        }
        
        public static bool TryWaitTextContains(this IWebElement element, string text)
        {
            //todo не консоль
            var res = false;
            Waiter.Wait(() =>
                {
                    res = element.Text.Contains(text);
                    return res;
                },
                (i) => Console.WriteLine($"Текст [{text}] не найден для элемента [{element.GetAttribute("class")}], был [{element.Text}]"));
            return res;
        }
        
        public static IWebElement WaitElementOrNull(this IWebDriver driver, By by, int timeoutInSeconds = 0)
        {
            try
            {
                return driver.WaitElement(by, timeoutInSeconds);
            }
            catch (Exception ex) when (ex is WebDriverTimeoutException ||
                                       ex is NoSuchElementException)
            {
                return null;
            }
        }
        
        public static void WaitEnabled(this IWebElement webElement, int timeoutInSeconds = 0)
        {
            Waiter.Wait(() => webElement.Enabled, timeoutInSeconds * 1000);
        }
    }
}