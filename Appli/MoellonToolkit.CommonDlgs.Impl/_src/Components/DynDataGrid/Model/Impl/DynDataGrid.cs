using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// The dataGrid model, to use with the dynamic data Grid UI component.
    /// </summary>
    public class DynDataGrid : IDynDataGrid
    {
        List<IGridColumn> _listColumn;

        List<IGridRow> _listRow;

        public DynDataGrid()
        {
            _listColumn = new List<IGridColumn>();
            _listRow = new List<IGridRow>();
        }

        public IEnumerable<IGridColumn> ListColumn
        { get { return _listColumn; } }

        public IEnumerable<IGridRow> ListRow
        { get { return _listRow; } }



        /// <summary>
        /// Add a new column.
        /// Check that the name is not already used by another column.
        /// 
        /// TODO: if row are already presents?  (and so without a cell for the new column)
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool AddColumn(IGridColumn column)
        {
            // check if the name of column is ok and not already used
            if (string.IsNullOrWhiteSpace(column.Name))
                return false;

            if (_listColumn.Where(c => c.Name.Equals(column.Name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() != null)
                return false;

            _listColumn.Add(column);
            return true;
        }

        public bool AddRow(IGridRow row)
        {
            // check if the row columns match the datagrid columns
            // TODO:

            _listRow.Add(row);
            return true;
        }

        // remove the row from the datagrid
        public bool RemoveRow(IGridRow row)
        {
            _listRow.Remove(row);
            return true;
        }

    }
}
