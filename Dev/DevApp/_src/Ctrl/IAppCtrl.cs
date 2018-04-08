using DevApp.Enums;
using MoellonToolkit.CommonDlgs.Impl.Components;
using MoellonToolkit.MVVMBase;
using System;

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
        /// Action, callback, called when a dataGrid cell is modified.
        /// </summary>
        //Action<IGridCell> ActionGridValueModifiedInUI { get; set; }

        /// <summary>
        /// The factory to build items for dynamic dataGrid.
        /// One factory for all dataGrid.
        /// </summary>
        IDynDataGridFactory DynDataGridFactory { get; }

        /// <summary>
        /// A datagrid to test the dynamic data grid UI.
        /// </summary>
        IDynDataGrid DataGrid { get;  }

        /// <summary>
        /// Another one datagrid to test , used in MultiComponents view.
        /// </summary>
        IDynDataGrid DataGrid2 { get; }

    }
}
