using System.Windows;

namespace Training.SniperAuction.Tests
{
    public static class UIExtensionMethods
    {
        public static T FindElement<T>(this FrameworkElement parentElement, string elementName) {
            return (T)parentElement.FindName(elementName);
        }
    }
}
