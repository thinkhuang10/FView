using System;
using System.Collections.Generic;
using System.Drawing;
using ShapeRuntime.DBAnimation;

namespace ShapeRuntime;

[Serializable]
public class CButtonSaveItems
{
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

    public string Text;

    public Color BackColor;

    public Color ForeColor = Color.Black;

    public Font Font;

    public int TabIndex;

    public bool hide;

    public bool disable;

    public string varBind;

    public string textVarBind;

    public string ImagePath;
}
