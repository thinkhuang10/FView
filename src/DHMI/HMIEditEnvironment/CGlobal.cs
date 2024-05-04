using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using CommonSnappableTypes;
using ShapeRuntime;

namespace HMIEditEnvironment;

[Serializable]
public class CGlobal
{
    public XmlDocument xmldoc;

    public List<DataFile> dfs;

    public CIOItem ioitemroot;

    public HMIProjectFile dhp;

    public string str_IMDoingWhat = "Select";

    public int IMContorlWhatPoint = -1;

    public List<CShape> g_ListAllShowCShape = new();

    private List<CShape> SLS = new();

    public List<CShape> CSLS = new();

    public List<CShape> OCSLS = new();

    public DataFile df = new();

    public CShape CopyShape = new();

    private readonly Stack<CReUnDoItem> UnDoStack = new();

    private readonly Stack<CReUnDoItem> ReDoStack = new();

    public UserCommandControl2 uc1 = new();

    public UserShapeEditControl uc2 = new();

    public PropertyGrid pg = new();

    public DataGridView dataGridView = new();

    public ListView listView2 = new();

    public Form theform = new();

    public CPageProperty pageProp = new();

    public bool locked;

    public List<ListViewItem> llvi;

    public List<ListViewItem> ullvi;

    public CShape OldShape;

    public List<CShape> OldShapes = new();

    public MouseEventArgs lastmouseeventargs = new(MouseButtons.None, 0, 0, 0, 0);

    public PointF mousedownp = PointF.Empty;

    public PointF mselectfirstp = default;

    public int SelectInt;

    public int Stack_Count = 50;

    public List<CShape> SelectedShapeList
    {
        get
        {
            return SLS;
        }
        set
        {
            SLS = value;
        }
    }

    public void SLS_Changed(object sender, EventArgs e)
    {
        if (SelectedShapeList.Count == 1)
        {
            if (SelectedShapeList[0] is CControl)
            {
                CControl cControl2 = (CControl)SelectedShapeList[0];
                pg.SelectedObject = cControl2._c;
            }
            else
            {
                pg.SelectedObject = SelectedShapeList[0];
            }
        }
        else if (SelectedShapeList.Count == 0)
        {
            pg.SelectedObject = pageProp;
        }
        else
        {
            List<object> list = new();
            foreach (CShape selectedShape in SelectedShapeList)
            {
                if (selectedShape is CControl)
                {
                    list.Add(((CControl)selectedShape)._c);
                }
                else
                {
                    list.Add(selectedShape);
                }
            }
            pg.SelectedObjects = list.ToArray();
        }
        listView2.Items.Clear();
        if (SelectedShapeList.Count != 1)
        {
            return;
        }
        if (SelectedShapeList[0] is CControl)
        {
            CControl cControl3 = (CControl)SelectedShapeList[0];
            if (cControl3._c is AxHost || cControl3._c is IDCCEControl)
            {
                EventInfo[] events = ((CControl)SelectedShapeList[0])._c.GetType().GetEvents();
                foreach (MemberInfo memberInfo in events)
                {
                    memberInfo.ToString();
                    listView2.Items.Add(memberInfo.ToString());
                }
            }
            else
            {
                EventInfo[] events2 = ((CControl)SelectedShapeList[0])._c.GetType().GetEvents();
                foreach (MemberInfo memberInfo2 in events2)
                {
                    memberInfo2.ToString();
                    listView2.Items.Add(memberInfo2.ToString());
                }
            }
        }
        else
        {
            EventInfo[] events3 = SelectedShapeList[0].GetType().GetEvents();
            foreach (MemberInfo memberInfo3 in events3)
            {
                memberInfo3.ToString();
                listView2.Items.Add(memberInfo3.ToString());
            }
        }
    }

    public CShape GetShapeByKey(string strGUID)
    {
        foreach (CShape item in g_ListAllShowCShape)
        {
            if (item.ShapeID.ToString() == strGUID)
            {
                return item;
            }
        }
        return null;
    }

    public CShape GetSelectShapeByKey(string strGUID)
    {
        foreach (CShape selectedShape in SelectedShapeList)
        {
            if (selectedShape.ShapeID.ToString() == strGUID)
            {
                return selectedShape;
            }
        }
        return null;
    }

    public void ReFreshReUnDo(Button UnDo, Button Redo)
    {
        if (UnDoStack.Count == 0)
        {
            UnDo.Enabled = false;
        }
        else
        {
            UnDo.Enabled = true;
        }
        if (ReDoStack.Count == 0)
        {
            Redo.Enabled = false;
        }
        else
        {
            Redo.Enabled = true;
        }
    }

    public void UnDo()
    {
        if (UnDoStack.Count == 0)
            return;

        var cReUnDoItem = UnDoStack.Pop();
        ReDoStack.Push(cReUnDoItem);
        if (cReUnDoItem.NewShapeList == null && cReUnDoItem.OldShapeList != null)
        {
            SelectedShapeList.Clear();
            foreach (CShape oldShape in cReUnDoItem.OldShapeList)
            {
                g_ListAllShowCShape.Add(oldShape);
                SelectedShapeList.Add(oldShape);
                CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(oldShape.ShapeID.ToString());
                if (oldShape is CControl)
                {
                    ((CControl)oldShape)._c.Enabled = false;
                    ((CControl)oldShape).initLocationErr = true;
                    uc2.Controls.Add(((CControl)oldShape)._c);
                    ((CControl)oldShape).initLocationErr = false;
                }
            }
        }
        if (cReUnDoItem.NewShapeList != null && cReUnDoItem.OldShapeList != null)
        {
            CShape[] array = g_ListAllShowCShape.ToArray();
            foreach (CShape cShape in array)
            {
                foreach (CShape newShape in cReUnDoItem.NewShapeList)
                {
                    if (cShape.ShapeID == newShape.ShapeID)
                    {
                        g_ListAllShowCShape.Remove(cShape);
                        SelectedShapeList.Clear();
                        if (cShape is CControl)
                        {
                            uc2.Controls.Remove(((CControl)cShape)._c);
                        }
                        CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cShape.ShapeID.ToString());
                    }
                }
            }
            SelectedShapeList.Clear();
            foreach (CShape oldShape2 in cReUnDoItem.OldShapeList)
            {
                g_ListAllShowCShape.Add(oldShape2);
                SelectedShapeList.Add(oldShape2);
                CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(oldShape2.ShapeID.ToString());
                if (oldShape2 is CControl)
                {
                    ((CControl)oldShape2)._c.Enabled = false;
                    ((CControl)oldShape2).initLocationErr = true;
                    uc2.Controls.Add(((CControl)oldShape2)._c);
                    ((CControl)oldShape2).initLocationErr = false;
                    ((CControl)oldShape2).UpdateControl();
                }
            }
        }
        if (cReUnDoItem.NewShapeList != null && cReUnDoItem.OldShapeList == null)
        {
            SelectedShapeList.Clear();
            CShape[] array2 = g_ListAllShowCShape.ToArray();
            foreach (CShape cShape2 in array2)
            {
                foreach (CShape newShape2 in cReUnDoItem.NewShapeList)
                {
                    if (cShape2.ShapeID == newShape2.ShapeID)
                    {
                        g_ListAllShowCShape.Remove(cShape2);
                        if (cShape2.GetType() == typeof(CControl))
                        {
                            uc2.Controls.Remove(((CControl)cShape2)._c);
                        }
                        CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cShape2.ShapeID.ToString());
                    }
                }
            }
        }
        CEditEnvironmentGlobal.mdiparent.objView_Page.FreshSelect(this);
        str_IMDoingWhat = "Select";
    }

    public void ReDo()
    {
        if (ReDoStack.Count == 0)
        {
            return;
        }
        CReUnDoItem cReUnDoItem = new();
        cReUnDoItem = ReDoStack.Pop();
        UnDoStack.Push(cReUnDoItem);
        if (cReUnDoItem.NewShapeList != null && cReUnDoItem.OldShapeList == null)
        {
            SelectedShapeList.Clear();
            foreach (CShape newShape in cReUnDoItem.NewShapeList)
            {
                g_ListAllShowCShape.Add(newShape);
                SelectedShapeList.Add(newShape);
                CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(newShape.ShapeID.ToString());
                try
                {
                    ((CControl)newShape)._c.Enabled = false;
                    ((CControl)newShape).initLocationErr = true;
                    uc2.Controls.Add(((CControl)newShape)._c);
                    ((CControl)newShape).initLocationErr = false;
                }
                catch (Exception)
                {
                }
            }
        }
        if (cReUnDoItem.NewShapeList != null && cReUnDoItem.OldShapeList != null)
        {
            CShape[] array = g_ListAllShowCShape.ToArray();
            foreach (CShape cShape in array)
            {
                foreach (CShape oldShape in cReUnDoItem.OldShapeList)
                {
                    if (cShape.ShapeID == oldShape.ShapeID)
                    {
                        g_ListAllShowCShape.Remove(cShape);
                        if (cShape.GetType() == typeof(CControl))
                        {
                            uc2.Controls.Remove(((CControl)cShape)._c);
                        }
                        CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cShape.ShapeID.ToString());
                    }
                }
            }
            SelectedShapeList.Clear();
            foreach (CShape newShape2 in cReUnDoItem.NewShapeList)
            {
                g_ListAllShowCShape.Add(newShape2);
                SelectedShapeList.Add(newShape2);
                CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(newShape2.ShapeID.ToString());
                if (newShape2.GetType() == typeof(CControl))
                {
                    ((CControl)newShape2)._c.Enabled = false;
                    ((CControl)newShape2).initLocationErr = true;
                    uc2.Controls.Add(((CControl)newShape2)._c);
                    ((CControl)newShape2).initLocationErr = false;
                    ((CControl)newShape2).UpdateControl();
                }
            }
        }
        if (cReUnDoItem.NewShapeList == null && cReUnDoItem.OldShapeList != null)
        {
            CShape[] array2 = g_ListAllShowCShape.ToArray();
            foreach (CShape cShape2 in array2)
            {
                foreach (CShape oldShape2 in cReUnDoItem.OldShapeList)
                {
                    if (cShape2.ShapeID == oldShape2.ShapeID)
                    {
                        g_ListAllShowCShape.Remove(cShape2);
                        SelectedShapeList.Clear();
                        if (cShape2.GetType() == typeof(CControl))
                        {
                            uc2.Controls.Remove(((CControl)cShape2)._c);
                        }
                        CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cShape2.ShapeID.ToString());
                    }
                }
            }
        }
        CEditEnvironmentGlobal.mdiparent.objView_Page.FreshSelect(this);
        str_IMDoingWhat = "Select";
    }

    public void ForUndo(List<CShape> newshapelist, List<CShape> oldshapelist)
    {
        ReDoStack.Clear();
        CReUnDoItem item = new(newshapelist, oldshapelist);
        UnDoStack.Push(item);
        if (UnDoStack.Count > Stack_Count)
        {
            CReUnDoItem cReUnDoItem = new();
            Stack<CReUnDoItem> stack = new(Stack_Count);
            for (int i = 0; i < Stack_Count; i++)
            {
                cReUnDoItem = UnDoStack.Pop();
                stack.Push(cReUnDoItem);
            }
            UnDoStack.Clear();
            for (int j = 0; j < Stack_Count; j++)
            {
                cReUnDoItem = stack.Pop();
                UnDoStack.Push(cReUnDoItem);
            }
            stack.Clear();
        }
    }
}
