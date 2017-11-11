using DevApp.Enums;
using MoellonToolkit.CommonDlgs.Impl.Components;
using MoellonToolkit.MVVMBase;

namespace DevApp.Ctrl
{
    /// <summary>
    /// The controller of the application, provides low layered resources to viewModels and views.
    /// </summary>
    public interface IAppCtrl : IAppCtrlBase
    {
        // create and show the main view
        ViewModelBase ShowView(ViewDef view);

        bool StartApp();

        /// <summary>
        /// A datagrid to test the dynamic data grid UI.
        /// </summary>
        IDynDataGrid DataGrid { get;  }
    }
}
