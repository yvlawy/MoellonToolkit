using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public abstract class DynDataGridFactoryBase : IDynDataGridFactory
    {
        /// <summary>
        /// Create a cell (for a row), depending on the column type.
        /// TODO: comme ca ou directement da le row? (a fournir alors)
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public IGridCell CreateCell(IGridColumn column)
        {
            //IGridCell cell;

            // create a cell matching the type of the column
            IGridColumnString colString = column as IGridColumnString;
            if (colString != null)
            {
                // set a default value
                return new GridCellString(colString, "");
            }

            //IGridColumnInt colInt = column as GridColumnInt;
            //if (colInt != null)
            //{
            //    // set a default value
            //    return new GridCellInt(colInt, 0);
            //}

            IGridColumnCheckBox colCheckBox = column as IGridColumnCheckBox;
            if(colCheckBox!=null)
            {
                return new GridCellCheckBox(colCheckBox, false);
            }

            // other type
            // TODO:

            throw new Exception("Factory.CreateCell(): type not yet implemented.");
            //return null;

        }

        /// <summary>
        /// Create a cellVM, can be a cell value or a cell component (button, comobox,...).
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public IGridCellVM CreateCellVM(IGridCell cell)
        {
            // is the cell a value? 
            IGridCellValue gridCellValue = cell as IGridCellValue;
            if(gridCellValue!=null)
            {
                IGridCellVM cellVM = new GridCellValueVM(gridCellValue);
                return cellVM;
            }

            // is the cell a checkbox?
            // $TASK-003
            IGridCellCheckBox gridCellCheckBox = cell as IGridCellCheckBox;
            if(gridCellCheckBox != null)
            {

                IGridCellVM cellVM = new GridCellCheckBoxVM(gridCellCheckBox);
                return cellVM;
            }

            //IGridCellComponent gridCellComponent = cell as IGridCellComponent;
            //if (gridCellComponent != null)
            //{
            //    IGridCellVM cellVM = new GridCellValueVM(cell);
            //    return cellVM;
            //}

            // error or not managed
            return null;
        }
    }
}
