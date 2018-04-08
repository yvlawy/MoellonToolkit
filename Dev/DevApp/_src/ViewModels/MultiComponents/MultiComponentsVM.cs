using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevApp.Ctrl;
using DevApp.DataModel;
using MoellonToolkit.MVVMBase;

namespace DevApp.ViewModels
{
    /// <summary>
    /// A combobox  and a panel to edit components:
    /// DynDataGrid,..
    /// </summary>
    public class MultiComponentsVM : ViewModelBase
    {
        DataBase _selectedComponent;


        // list of data
        List<DataBase> _listData = new List<DataBase>();

        public MultiComponentsVM()
        {
            CollComponents = new ObservableCollection<DataBase>();

            Init();
        }

        #region Properties
        public ObservableCollection<DataBase> CollComponents { get; private set; }

        public DataBase SelectedComponent
        {
            get { return _selectedComponent; }
            set
            {
                _selectedComponent = value;
                DisplaySelectedComponent(_selectedComponent);
            }
        }

        public EditComponentBaseVM EditComponentBaseVM { get; private set; }
        #endregion

        private void Init()
        {
            DataString ds = new DataString();
            ds.Name = "a";
            ds.Value = "12";
            _listData.Add(ds);
            CollComponents.Add(ds);

            // create a dynDataGrid
            DataTable dt = new DataTable();
            dt.Name = "tt";
            _listData.Add(dt);
            CollComponents.Add(dt);

            // create a dynDataGrid
            DataTable dt2 = new DataTable();
            dt2.Name = "tt2";
            _listData.Add(dt2);
            CollComponents.Add(dt2);
        }


        private void DisplaySelectedComponent(DataBase data)
        {
            if(data.Name.Equals("a",StringComparison.InvariantCultureIgnoreCase))
            {
                EditComponentBaseVM = new EditComponentStringVM(_listData.Where(d=>d.Name.Equals("a")).FirstOrDefault() as DataString);
                RaisePropertyChanged("EditComponentBaseVM");
                return;
            }

            if(data.Name.Equals("tt", StringComparison.InvariantCultureIgnoreCase))
            {
                EditComponentBaseVM = new EditComponentDynDataGridVM(AppCtrlProvider.AppCtrl.CommonDlg, AppCtrlProvider.AppCtrl.DynDataGridFactory, AppCtrlProvider.AppCtrl.DataGrid);
                RaisePropertyChanged("EditComponentBaseVM");
                return;
            }
            if (data.Name.Equals("tt2", StringComparison.InvariantCultureIgnoreCase))
            {
                EditComponentBaseVM = new EditComponentDynDataGridVM(AppCtrlProvider.AppCtrl.CommonDlg, AppCtrlProvider.AppCtrl.DynDataGridFactory, AppCtrlProvider.AppCtrl.DataGrid2);
                RaisePropertyChanged("EditComponentBaseVM");
                return;
            }
        }



    }
}
