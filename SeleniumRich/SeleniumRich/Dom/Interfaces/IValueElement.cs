namespace SeleniumRich.Dom.Interfaces
{
    public interface IValueElement
    {
        string GetValue();
        void SetValue(string value, string expectedValue = null);
    }
}