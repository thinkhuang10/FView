using System.Windows.Forms;

namespace HMIEditEnvironment.Animation;

public class DataGridViewComboEditBoxColumn : DataGridViewComboBoxColumn
{
    public DataGridViewComboEditBoxColumn()
    {
        DataGridViewComboEditBoxCell dataGridViewComboEditBoxCell = new();
        CellTemplate = dataGridViewComboEditBoxCell;
    }
}
