using System.ComponentModel;
using System.Windows.Forms;

namespace HMIEditEnvironment.Animation;

public class DataGridViewComboEditBoxCell : DataGridViewComboBoxCell
{
    public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
        base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
        ComboBox comboBox = (ComboBox)base.DataGridView.EditingControl;
        if (comboBox != null)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox.Validating += comboBox_Validating;
            comboBox.DropDownHeight = 98;
        }
    }

    protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
    {
        if (value != null && value.ToString().Trim() != string.Empty && Items.IndexOf(value) == -1)
        {
            Items.Add(value);
            DataGridViewComboBoxColumn dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)base.OwningColumn;
            dataGridViewComboBoxColumn.Items.Add(value);
        }
        return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
    }

    private void comboBox_Validating(object sender, CancelEventArgs e)
    {
        DataGridViewComboBoxEditingControl dataGridViewComboBoxEditingControl = (DataGridViewComboBoxEditingControl)sender;
        if (!(dataGridViewComboBoxEditingControl.Text.Trim() == string.Empty))
        {
            DataGridView editingControlDataGridView = dataGridViewComboBoxEditingControl.EditingControlDataGridView;
            object text = dataGridViewComboBoxEditingControl.Text;
            if (dataGridViewComboBoxEditingControl.Items.IndexOf(text) == -1)
            {
                DataGridViewComboBoxColumn dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)editingControlDataGridView.Columns[editingControlDataGridView.CurrentCell.ColumnIndex];
                dataGridViewComboBoxEditingControl.Items.Add(text);
                dataGridViewComboBoxColumn.Items.Add(text);
                editingControlDataGridView.CurrentCell.Value = text;
            }
        }
    }
}
