using DevApp.ViewModels;
using MoellonToolkit.CommonDlgs.Impl;
using System.Windows;
using System.Windows.Controls;

namespace DevApp.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _prevText;

        public MainWindow()
        {
            InitializeComponent();

            // for closing the Closeable tabItems
            this.AddHandler(CloseableTabItem.CloseTabEvent, new RoutedEventHandler(this.CloseTab));
        }

        /// <summary>
        /// Request the close of the window (and the app because its the main window).
        /// Coming from 2 sources: action (clic) on the "X" of the window, or from the AppController. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // exec request close the window (and the app)
            ((MainVM)this.DataContext).RequestCloseWindowCmd.Execute(null);

            // if the user cancel the close of the window, cancel the operation : IsWindowClosing= False -> Cancel= True
            e.Cancel = !((MainVM)this.DataContext).IsWindowClosing;
        }

        /// <summary>
        /// Close the tabItem, by clic on the X.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        private void CloseTab(object source, RoutedEventArgs args)
        {
            TabItem tabItem = args.Source as TabItem;
            if (tabItem != null)
            {
                TabControl tabControl = tabItem.Parent as TabControl;
                if (tabControl != null)
                    tabControl.Items.Remove(tabItem);
            }
        }

        /// <summary>
        /// The user put a char in the textbox (text matching a code).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // get the data context
            //MainVM mainVM = (MainVM)this.DataContext;

            //string textBefore = ((TextBox)sender).Text;
            //mainVM.StringCodeToEditKeyDown(textBefore);
        }

        private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            _prevText = ((TextBox)sender).Text;

            // get the data context
            //MainVM mainVM = (MainVM)this.DataContext;

            //string textBefore = ((TextBox)sender).Text;
            //mainVM.StringCodeToEditKeyDown(textBefore);

        }

        /// <summary>
        /// preview to cathc specials keys like: 
        /// space, backspace (not catched by KeyDown or KeyUp).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // get the data context
            MainVM mainVM = (MainVM)this.DataContext;

            string textBefore = ((TextBox)sender).Text;

            if (_prevText == textBefore)
                // text is not modified (Up, Down,..)
                return;

            mainVM.StringCodeToEditKeyDown(textBefore);

        }
    }
}
