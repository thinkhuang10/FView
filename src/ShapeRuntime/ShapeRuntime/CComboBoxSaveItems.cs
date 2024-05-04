using System;
using System.Collections.Generic;
using System.Drawing;
using ShapeRuntime.DBAnimation;

namespace ShapeRuntime;

[Serializable]
public class CComboBoxSaveItems
{
    public string Text;

    public Font Font;

    public Color BackColor;

    public Color ForeColor = Color.Black;

    public string SelectedItem;

    public int TabIndex;

    public List<string> items;

    public object dropstyle;

    public bool hide;

    public bool disable;

    public string varBind;

    public string textVarBind;

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
