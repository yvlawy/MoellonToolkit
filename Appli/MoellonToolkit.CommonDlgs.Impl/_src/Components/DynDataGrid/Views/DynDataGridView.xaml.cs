using MoellonToolkit.CommonDlgs.Impl;
using System.Windows;
using System.Windows.Controls;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class DynDataGridView : UserControl
    {
        public DynDataGridView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A column is reordered, moved to a new position.
        /// 
        /// Possible to reorder colums of the dataGrid if: CanUserReorderColumns="True".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myGrid_ColumnReordered(object sender, DataGridColumnEventArgs e)
        {
            // the new position of
            int colPos = e.Column.DisplayIndex;

            // get the column View model
            GridColumnBaseVM colVM = e.Column.Header as GridColumnBaseVM;

            // TODO: notify the VM and the model!
            //colVM.DisplayIndex = colPos;
        }
    }
}
