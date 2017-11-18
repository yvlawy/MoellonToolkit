using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public class GridBoundColumn : DataGridTemplateColumn
    {
        //public DataTemplate CellTemplate { get; set; }
        //public DataTemplate CellEditingTemplate { get; set; }

        /// <summary>
        /// The cells of the data grid, can be values or components.
        /// </summary>
        public GridMappingCellsBase GridMappingCells { get; set; }

        /// <summary>
        /// Find or create a grid cell.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var content = new ContentControl();
            IGridCellVM context = GridMappingCells.FindOrCreate(cell.Column.Header, dataItem);
            var binding = new Binding() { Source = context };
            content.ContentTemplate = cell.IsEditing ? CellEditingTemplate : CellTemplate;
            content.SetBinding(ContentControl.ContentProperty, binding);
            return content;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return GenerateElement(cell, dataItem);
        }
    }
}
