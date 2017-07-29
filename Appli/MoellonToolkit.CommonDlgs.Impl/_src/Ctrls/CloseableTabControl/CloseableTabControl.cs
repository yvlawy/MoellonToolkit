using System;
using System.Windows.Controls;
using System.Windows;

namespace MoellonToolkit.CommonDlgs.Impl
{
    /// <summary>
    /// A closeable TabControl.
    /// Use customized TabItems: closeable tab item.
    /// 
    /// need these libs: PresentationCore, PresentationFramework, System.Xaml, WindowsBase.
    /// </summary>
    public class CloseableTabControl : TabControl
    {
        /// <summary>
        /// Create an inherited TabItem in place of the TabItem.
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CloseableTabItem();
        }
    }
}
