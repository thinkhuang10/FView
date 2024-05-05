using CommonSnappableTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace DataGrid;

[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("FF237F83-DE34-4088-951D-31F666437851")]
public class MainControl : UserControl, IDCCEControl
{
    private SAVE saveData = new();

    private int iCurrentRow;

    private bool bCellBeginEdit;

    private string strClickValue = "";

    private string strClickColumnName = "";

    private string strID = "";

    private DataGridView dataGridView;

    [Browsable(false)]
    public bool isRuning { get; set; }

    public event GetValue GetValueEvent;

    public event SetValue SetValueEvent;

    public event GetDataBase GetDataBaseEvent;

    public event GetVarTable GetVarTableEvent;

    public event GetValue GetSystemItemEvent;

    public event EventHandler TreeNodeClicked;

    public event EventHandler TreeNodeDoubleClicked;

    public event EventHandler TreeNodeSelectedChanged;

    public byte[] Serialize()
    {
        SAVE graph = saveData;
        BinaryFormatter binaryFormatter = new();
        MemoryStream memoryStream = new();
        binaryFormatter.Serialize(memoryStream, graph);
        byte[] result = memoryStream.ToArray();
        memoryStream.Dispose();
        return result;
    }

    public void DeSerialize(byte[] bytes)
    {
        BinaryFormatter binaryFormatter = new();
        MemoryStream memoryStream = new(bytes);
        saveData = (SAVE)binaryFormatter.Deserialize(memoryStream);
        memoryStream.Dispose();
    }

    public static Image GetLogoStatic()
    {
        ResourceManager resourceManager = new(typeof(图标));
        return (Bitmap)resourceManager.GetObject("icon");
    }

    public Bitmap GetLogo()
    {
        ResourceManager resourceManager = new(typeof(图标));
        return (Bitmap)resourceManager.GetObject("icon");
    }

    public void Stop()
    {
    }

    public MainControl()
    {
        InitializeComponent();
    }

    private void MainControl_Load(object sender, EventArgs e)
    {
        ForeColor = Color.Black;
        if (Thread.CurrentThread.CurrentCulture.Name != "zh-CN")
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("zh-CN");
        }
    }

    public void SetWidthColumn(int iColumn, int iWidth)
    {
        dataGridView.Columns[iColumn].Width = iWidth;
    }

    public void DataGrid_AddColumn(string strColumnName)
    {
        dataGridView.Columns.Add(strColumnName, strColumnName);
    }

    public void DataGrid_AddRow(int iRow)
    {
        dataGridView.Rows.Add(iRow);
    }

    public void DataGrid_Clear()
    {
        dataGridView.Rows.Clear();
        dataGridView.Columns.Clear();
    }

    public void SetCellValue(int iRow, int iColumn, object value)
    {
        dataGridView.Rows[iRow].Cells[iColumn].Value = value;
    }

    public object GetCellValue(int iRow, int iColumn)
    {
        return dataGridView.Rows[iRow].Cells[iColumn].Value;
    }

    public object GetCellColumnValue(int iColumn)
    {
        return dataGridView.Rows[iCurrentRow].Cells[iColumn].Value;
    }

    public void SQLServer_SetTableValue(object[,] OValue)
    {
        int num = OValue.GetLength(0) - 1;
        int length = OValue.GetLength(1);
        for (int i = 0; i < length; i++)
        {
            DataGrid_AddColumn(OValue[0, i].ToString());
        }
        DataGrid_AddRow(num);
        for (int j = 0; j < num; j++)
        {
            for (int k = 0; k < length; k++)
            {
                dataGridView.Rows[j].Cells[k].Value = OValue[j + 1, k];
            }
        }
        dataGridView.Refresh();
    }

    public void DataGrid_SetTableValue(object[,] OValue)
    {
        int length = OValue.GetLength(0);
        int length2 = OValue.GetLength(1);
        DataGrid_AddRow(length2);
        for (int i = 0; i < length2; i++)
        {
            for (int j = 0; j < length; j++)
            {
                dataGridView.Rows[i].Cells[j].Value = OValue[j, i];
            }
        }
        dataGridView.Refresh();
    }

    private void dataGridView_KeyUp(object sender, KeyEventArgs e)
    {
        if (bCellBeginEdit && e.KeyCode == Keys.Return)
        {
            bCellBeginEdit = false;
            OnKeyUp(e);
        }
    }

    public string GetClickCellValue()
    {
        return strClickValue;
    }

    public string GetClickColumnName()
    {
        return strClickColumnName;
    }

    public string GetClickCellID()
    {
        return strID;
    }

    private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
        bCellBeginEdit = true;
    }

    private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        strClickValue = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        strClickColumnName = dataGridView.Columns[e.ColumnIndex].Name;
        strID = dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
    }

    private void dataGridView_MouseClick(object sender, MouseEventArgs e)
    {
        OnMouseClick(e);
    }

    private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        iCurrentRow = e.RowIndex;
    }

    private void InitializeComponent()
    {
        dataGridView = new System.Windows.Forms.DataGridView();
        ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
        base.SuspendLayout();
        dataGridView.AllowUserToAddRows = false;
        dataGridView.AllowUserToDeleteRows = false;
        dataGridView.AllowUserToOrderColumns = true;
        dataGridView.AllowUserToResizeRows = false;
        dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
        dataGridView.EnableHeadersVisualStyles = false;
        dataGridView.Location = new System.Drawing.Point(0, 0);
        dataGridView.Name = "dataGridView";
        dataGridView.RowHeadersVisible = false;
        dataGridView.RowTemplate.Height = 23;
        dataGridView.Size = new System.Drawing.Size(630, 340);
        dataGridView.TabIndex = 0;
        dataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(dataGridView_CellBeginEdit);
        dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView_CellClick);
        dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView_CellEndEdit);
        dataGridView.KeyUp += new System.Windows.Forms.KeyEventHandler(dataGridView_KeyUp);
        dataGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(dataGridView_MouseClick);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.Controls.Add(dataGridView);
        base.Name = "MainControl";
        base.Size = new System.Drawing.Size(630, 340);
        base.Load += new System.EventHandler(MainControl_Load);
        ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
        base.ResumeLayout(false);
    }
}
