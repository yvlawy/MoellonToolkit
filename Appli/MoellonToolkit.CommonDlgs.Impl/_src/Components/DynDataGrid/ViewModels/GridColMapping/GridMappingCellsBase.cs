using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// For a grid: mapping of cells between row and col.
    /// 
    /// </summary>
    public abstract class GridMappingCellsBase :  ObservableCollection<IGridCellVM>
    {
        /// <summary>
        /// Find the cell or create it (the first time the cell is acceded).
        /// </summary>
        /// <param name="ColumnBinding"></param>
        /// <param name="RowBinding"></param>
        /// <returns></returns>
        public abstract IGridCellVM FindOrCreate(object ColumnBinding, object RowBinding);


        public bool Exist(object ColumnBinding, object RowBinding)
        {
            return this.Count(x => x.RowBinding == RowBinding &&
                x.ColumnBinding == ColumnBinding) > 0;
        }

        public void RemoveByColumn(object ColumnBinding)
        {
            foreach (var item in this.Where(x => x.ColumnBinding == ColumnBinding).ToList())
                this.Remove(item);
        }

        public void RemoveByRow(object RowBinding)
        {
            foreach (var item in this.Where(x => x.RowBinding == RowBinding).ToList())
                this.Remove(item);
        }

    }
}
