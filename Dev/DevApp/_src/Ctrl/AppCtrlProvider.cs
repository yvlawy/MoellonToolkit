using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.Ctrl
{
    /// <summary>
    /// provide the application controller, from every where in the application.
    /// </summary>
    public class AppCtrlProvider
    {
        private static AppCtrlProvider _instance = new AppCtrlProvider();

        public static AppCtrlProvider Instance
        {
            get { return _instance; }
        }

        public static IAppCtrl AppCtrl
        {
            get { return _instance._ctrl; }
        }


        private IAppCtrl _ctrl;

        /// <summary>
        /// Private constructor.
        /// </summary>
        private AppCtrlProvider()
        {

        }

        // set the ctrl
        public void SetAppCtrl(IAppCtrl ctrl)
        {
            _ctrl = ctrl;
        }

    }
}
