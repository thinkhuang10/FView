using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace ShapeRuntime;

[Serializable]
public class CDataGridViewSaveItems
{
    public DataGridViewCellBorderStyle CellBorderStyle = DataGridViewCellBorderStyle.Raised;

    public bool AllowUserToResizeColumns;

    public bool AllowUserToResizeRows;

    public bool HideHead = true;

    public Color GridLineColor = SystemColors.ControlDark;

    public string CellFormat;

    public Color BackColor;

    public Color ForeColor = Color.Black;

    public int TabIndex;

    public DataTable data;

    public bool fullRowSelect;

    public bool multiSelect;

    [OptionalField]
    public Font font;

    public DataGridViewContentAlignment alignment;

    public bool fullfill = true;

    public bool hide;

    public bool disable;

    public int refreshTime;

    public bool enableEdit;

    public int RowCount;

    public int ColumnCount;

    public bool newtable;

    public string newtableSQL = "";

    public bool ansyncnewtable;

    public byte[] newtableOtherData;

    public List<int> newtableSafeRegion = new();

    public bool dbselect;

    public string dbselectSQL = "";

    public string dbselectTO = "";

    public bool dbSelectAnsync;

    public byte[] dbselectOtherData;

    public List<int> dbselectSafeRegion = new();

    public bool dbinsert;

    public string dbinsertSQL = "";

    public bool dbInsertAnsync;

    public byte[] dbinsertOtherData;

    public List<int> dbinsertSafeRegion = new();

    public bool dbupdate;

    public string dbupdateSQL = "";

    public bool dbUpdateAnsync;

    public byte[] dbupdateOtherData;

    public List<int> dbupdateSafeRegion = new();

    public bool dbdelete;

    public string dbdeleteSQL = "";

    public bool dbDeleteAnsync;

    public byte[] dbdeleteOtherData;

    public List<int> dbdeleteSafeRegion = new();

    public bool dbmultoperation;

    public List<ShapeRuntime.DBAnimation.DBAnimation> DBAnimations = new();
}
