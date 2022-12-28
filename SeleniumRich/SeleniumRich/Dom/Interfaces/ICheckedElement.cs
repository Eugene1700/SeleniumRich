namespace SeleniumRich.Dom.Interfaces
{
    public interface ICheckedElement
    {
        bool IsChecked { get; }
        void Check(bool state);
    }
}