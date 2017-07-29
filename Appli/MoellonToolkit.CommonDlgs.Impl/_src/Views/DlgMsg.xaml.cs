using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoellonToolkit.CommonDlgs.Impl
{
    /// <summary>
    /// Logique d'interaction pour DlgEditPropertyPair.xaml
    /// </summary>
    public partial class DlgMsg : Window
    {
        public DlgMsg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Request the close of the window (and the app because its the main window).
        /// Coming from 2 sources: action (clic) on the "X" of the window, or from the AppController. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // close the dlg
            // TODO: mapper sur quel btn??? ou créer une cmd Cancel
            //((DlgMsgVM)this.DataContext).CancelCmd.Execute(true);
        }

    }
}
