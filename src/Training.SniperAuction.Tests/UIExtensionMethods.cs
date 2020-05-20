using System.Windows;

namespace Training.SniperAuction.Tests
{
    public static class UIExtensionMethods
    {
        public static T FindElement<T>(this FrameworkElement parentElement, string elementName)
        {
            object elementControl = parentElement.FindName(elementName);
            if (elementControl == null) throw new System.Exception($"elementName {elementName} not found");
            return (T)elementControl;
        }
    }
}
