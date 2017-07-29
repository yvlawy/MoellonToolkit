using DevApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.Mgr
{
    /// <summary>
    /// Manager of Specifics dialogs of the application
    /// </summary>
    public class AppDlg : IAppDlg
    {
        MainWindow _mainWindow;

        public void SetMainWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        // TODO: define calls to dialogBox
    }
}
