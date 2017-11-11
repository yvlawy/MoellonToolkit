using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// To edit a cell in grid column TextBox  direclty on F2 click.
    /// 
    /// see: http://stackoverflow.com/questions/10216524/datatemplatecolumn-requires-2-tabs-to-get-to-the-content
    /// 
    /// on textBox:
    /// customControls:FocusAttacher.Focus="True"
    /// </summary>
    public class FocusAttacher
    {
        public static readonly DependencyProperty FocusProperty =
               DependencyProperty.RegisterAttached("Focus",
               typeof(bool),
               typeof(FocusAttacher),
               new PropertyMetadata(false, FocusChanged));

        public static bool GetFocus(DependencyObject d)
        {
            return (bool)d.GetValue(FocusProperty);
        }

        public static void SetFocus(DependencyObject d, bool value)
        {
            d.SetValue(FocusProperty, value);
        }

        public static void FocusChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ((UIElement)sender).Focus();
            }
        }
    }
}
