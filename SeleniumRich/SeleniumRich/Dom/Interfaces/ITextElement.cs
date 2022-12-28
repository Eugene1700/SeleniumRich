namespace SeleniumRich.Dom.Interfaces
{
    public interface ITextElement
    {
        string GetText();

        void WaitText(string text);
    }
}