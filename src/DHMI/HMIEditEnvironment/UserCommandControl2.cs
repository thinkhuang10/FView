using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class UserCommandControl2 : UserControl
{
    private static readonly Regex regex = new("^[a-zA-Z_][a-zA-Z0-9_]*$");

    public CGlobal theglobal;

    private Button button_绘制椭圆;

    private Button button_绘制矩形;

    private Button button_绘制折线;

    private Button button_绘制多边形;

    private Button button_绘制文字;

    private Button button_水平等间距;

    private Button button_竖直等间距;

    private Button button_顶层;

    private Button button_底层;

    private Button button_左对齐;

    private Button button_居中对齐;

    private Button button_右对齐;

    private Button button_底端对齐;

    private Button button_中间对齐;

    private Button button_顶端对齐;

    private Button button_水平镜像;

    private Button button_竖直镜像;

    private Button button_组合;

    private Button button_拆解;

    public Button button_撤消;

    private Button button_保存;

    private Button button_读取;

    private OpenFileDialog openFileDialog1;

    private SaveFileDialog saveFileDialog1;

    private Button button_大小相等;

    public Button button_重复;

    private Button button_绘制圆角矩形;

    private Button button_导入图片;

    private Button button_绘制直线;

    public event EventHandler OnButtonEnableChanged;

    public UserCommandControl2()
    {
        InitializeComponent();
    }

    public static bool GiveName(CShape s)
    {
        int num = 1;
        string text = s.GetType().Name;
        if (text == "CControl")
        {
            _ = (CControl)s;
            string type = ((CControl)s).type;
            if (type.Trim().Length > 0)
            {
                string[] array = type.Split('.');
                text = array[0];
                if (text == "ShapeRuntime" || !UserCommandControl2.regex.IsMatch(text))
                {
                    text = array[array.Length - 1];
                }
            }
        }
        Regex regex = new("^" + text + "\\d+$");
        DataTable dataTable = new();
        dataTable.Columns.Add("ID", Type.GetType("System.Int32"));
        dataTable.Columns.Add("CShapeName", Type.GetType("System.String"));
        foreach (CShape item in CEditEnvironmentGlobal.childform.theglobal.g_ListAllShowCShape)
        {
            if (regex.IsMatch(item.ShapeName ?? ""))
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = Convert.ToInt32(item.ShapeName.Substring(text.Length, item.ShapeName.Length - text.Length));
                dataRow["CShapeName"] = item.ShapeName;
                dataTable.Rows.Add(dataRow);
            }
        }
        DataTable dataTable2 = dataTable.Copy();
        DataView defaultView = dataTable.DefaultView;
        defaultView.Sort = "ID";
        dataTable2 = defaultView.ToTable();
        foreach (DataRow row in dataTable2.Rows)
        {
            if (Convert.ToInt32(row["ID"]) == num)
            {
                num++;
                continue;
            }
            break;
        }
        s.Name = text + num;
        return true;
    }

    public void LockShape()
    {
        if (CEditEnvironmentGlobal.mdiparent.barCheckItem10.Checked)
        {
            foreach (CShape selectedShape in theglobal.SelectedShapeList)
            {
                selectedShape.locked = true;
            }
            CEditEnvironmentGlobal.mdiparent.barCheckItem10.Checked = true;
            return;
        }
        foreach (CShape selectedShape2 in theglobal.SelectedShapeList)
        {
            selectedShape2.locked = false;
        }
        CEditEnvironmentGlobal.mdiparent.barCheckItem10.Checked = false;
    }

    public void button_绘制直线_Click(object sender, EventArgs e)
    {
        CLine item = new();
        theglobal.g_ListAllShowCShape.Add(item);
        theglobal.str_IMDoingWhat = "BeginDrawShape";
    }

    public void button_绘制折线_Click(object sender, EventArgs e)
    {
        CLines item = new();
        theglobal.g_ListAllShowCShape.Add(item);
        theglobal.str_IMDoingWhat = "BeginDrawShape";
    }

    public void button_绘制椭圆_Click(object sender, EventArgs e)
    {
        CEllipse item = new();
        theglobal.g_ListAllShowCShape.Add(item);
        theglobal.str_IMDoingWhat = "BeginDrawShape";
    }

    public void button_绘制矩形_Click(object sender, EventArgs e)
    {
        CRectangle item = new();
        theglobal.g_ListAllShowCShape.Add(item);
        theglobal.str_IMDoingWhat = "BeginDrawShape";
    }

    public void button_绘制多边形_Click(object sender, EventArgs e)
    {
        CCloseLines item = new();
        theglobal.g_ListAllShowCShape.Add(item);
        theglobal.str_IMDoingWhat = "BeginDrawShape";
    }

    public void button_绘制圆角矩形_Click(object sender, EventArgs e)
    {
        CCircleRect item = new();
        theglobal.g_ListAllShowCShape.Add(item);
        theglobal.str_IMDoingWhat = "BeginDrawShape";
    }

    public void button_绘制文字_Click(object sender, EventArgs e)
    {
        CString item = new("#####");
        theglobal.g_ListAllShowCShape.Add(item);
        theglobal.str_IMDoingWhat = "BeginDrawShape";
    }

    public void button_导入图片_Click(object sender, EventArgs e)
    {
        try
        {
            ImageManage imageManage = new();
            if (imageManage.ShowDialog() == DialogResult.OK && imageManage.OutImage != null)
            {
                CPicture cPicture = new();
                GiveName(cPicture);
                cPicture.Cimage = imageManage.OutImage;
                cPicture.AddPoint(Point.Empty);
                theglobal.g_ListAllShowCShape.Add(cPicture);
                theglobal.SelectedShapeList.Clear();
                theglobal.SelectedShapeList.Add(cPicture);
                CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cPicture.ShapeID.ToString());
                CShape item = cPicture.Copy();
                List<CShape> list = new()
                {
                    item
                };
                theglobal.ForUndo(list, null);
                theglobal.uc2.RefreshGraphics();
                CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Source + "\n" + ex.Message + "\n" + ex.StackTrace);
        }
    }

    public static int CompareByMidX(CShape x, CShape y)
    {
        if (x == null)
        {
            if (y == null)
            {
                return 0;
            }
            return -1;
        }
        if (y == null)
        {
            return 1;
        }
        if (x.ImportantPoints.Length == 0)
        {
            if (y.ImportantPoints.Length == 0)
            {
                return 0;
            }
            return -1;
        }
        if (x.ImportantPoints[0].X + x.ImportantPoints[1].X >= y.ImportantPoints[0].X + y.ImportantPoints[1].X)
        {
            return 1;
        }
        return -1;
    }

    public void button7_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count < 3)
        {
            return;
        }
        List<CShape> list = new();
        list = theglobal.SelectedShapeList;
        list.Sort(CompareByMidX);
        List<CShape> list2 = new();
        List<CShape> list3 = new();
        float num = 0f;
        for (int i = 0; i < list.Count - 1; i++)
        {
            float num2 = float.MaxValue;
            float num3 = float.MaxValue;
            float num4 = float.MinValue;
            float num5 = float.MinValue;
            for (int j = 0; j < 8; j++)
            {
                if (num2 > list[i].ImportantPoints[j].X)
                {
                    num2 = list[i].ImportantPoints[j].X;
                }
                if (num3 > list[i + 1].ImportantPoints[j].X)
                {
                    num3 = list[i + 1].ImportantPoints[j].X;
                }
                if (num4 < list[i].ImportantPoints[j].X)
                {
                    num4 = list[i].ImportantPoints[j].X;
                }
                if (num5 < list[i + 1].ImportantPoints[j].X)
                {
                    num5 = list[i + 1].ImportantPoints[j].X;
                }
            }
            num += num3 - num4;
        }
        float num6 = num / (float)(list.Count - 1);
        for (int k = 0; k < list.Count - 1; k++)
        {
            float num7 = float.MaxValue;
            float num8 = float.MaxValue;
            float num9 = float.MinValue;
            float num10 = float.MinValue;
            int num11 = 0;
            int num12 = 0;
            for (int l = 0; l < 8; l++)
            {
                if (num7 > list[k].ImportantPoints[l].X)
                {
                    num7 = list[k].ImportantPoints[l].X;
                }
                if (num8 > list[k + 1].ImportantPoints[l].X)
                {
                    num8 = list[k + 1].ImportantPoints[l].X;
                    num11 = l;
                }
                if (num9 < list[k].ImportantPoints[l].X)
                {
                    num9 = list[k].ImportantPoints[l].X;
                    num12 = l;
                }
                if (num10 < list[k + 1].ImportantPoints[l].X)
                {
                    num10 = list[k + 1].ImportantPoints[l].X;
                }
            }
            list2.Add(list[k + 1].Copy());
            list[k + 1].EditLocationByPoint(list[k + 1].ImportantPoints[num11], new PointF(list[k].ImportantPoints[num12].X + num6, list[k + 1].ImportantPoints[num11].Y));
            list3.Add(list[k + 1].Copy());
        }
        theglobal.ForUndo(list3, list2);
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public static int CompareByMidY(CShape x, CShape y)
    {
        if (x == null)
        {
            if (y == null)
            {
                return 0;
            }
            return -1;
        }
        if (y == null)
        {
            return 1;
        }
        if (x.ImportantPoints.Length == 0)
        {
            if (y.ImportantPoints.Length == 0)
            {
                return 0;
            }
            return -1;
        }
        if (x.ImportantPoints[0].Y + x.ImportantPoints[1].Y >= y.ImportantPoints[0].Y + y.ImportantPoints[1].Y)
        {
            return 1;
        }
        return -1;
    }

    public void button8_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count < 3)
        {
            return;
        }
        List<CShape> list = new();
        list = theglobal.SelectedShapeList;
        list.Sort(CompareByMidY);
        List<CShape> list2 = new();
        List<CShape> list3 = new();
        float num = 0f;
        for (int i = 0; i < list.Count - 1; i++)
        {
            float num2 = float.MaxValue;
            float num3 = float.MaxValue;
            float num4 = float.MinValue;
            float num5 = float.MinValue;
            for (int j = 0; j < 8; j++)
            {
                if (num2 > list[i].ImportantPoints[j].Y)
                {
                    num2 = list[i].ImportantPoints[j].Y;
                }
                if (num3 > list[i + 1].ImportantPoints[j].Y)
                {
                    num3 = list[i + 1].ImportantPoints[j].Y;
                }
                if (num4 < list[i].ImportantPoints[j].Y)
                {
                    num4 = list[i].ImportantPoints[j].Y;
                }
                if (num5 < list[i + 1].ImportantPoints[j].Y)
                {
                    num5 = list[i + 1].ImportantPoints[j].Y;
                }
            }
            num += num3 - num4;
        }
        float num6 = num / (float)(list.Count - 1);
        for (int k = 0; k < list.Count - 1; k++)
        {
            float num7 = float.MaxValue;
            float num8 = float.MaxValue;
            float num9 = float.MinValue;
            float num10 = float.MinValue;
            int num11 = 0;
            int num12 = 0;
            for (int l = 0; l < 8; l++)
            {
                if (num7 > list[k].ImportantPoints[l].Y)
                {
                    num7 = list[k].ImportantPoints[l].Y;
                }
                if (num8 > list[k + 1].ImportantPoints[l].Y)
                {
                    num8 = list[k + 1].ImportantPoints[l].Y;
                    num11 = l;
                }
                if (num9 < list[k].ImportantPoints[l].Y)
                {
                    num9 = list[k].ImportantPoints[l].Y;
                    num12 = l;
                }
                if (num10 < list[k + 1].ImportantPoints[l].Y)
                {
                    num10 = list[k + 1].ImportantPoints[l].Y;
                }
            }
            list2.Add(list[k + 1].Copy());
            list[k + 1].EditLocationByPoint(list[k + 1].ImportantPoints[num11], new PointF(list[k + 1].ImportantPoints[num11].X, list[k].ImportantPoints[num12].Y + num6));
            list3.Add(list[k + 1].Copy());
        }
        theglobal.ForUndo(list3, list2);
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void button9_Click(object sender, EventArgs e)
    {
        List<CShape> list = new();
        List<CShape> list2 = new();
        if (theglobal.SelectedShapeList.Count == 0)
        {
            return;
        }
        theglobal.SelectedShapeList.Sort(CEditEnvironmentGlobal.CompareByLayer);
        for (int i = 0; i < theglobal.SelectedShapeList.Count; i++)
        {
            CShape cShape = theglobal.SelectedShapeList[i];
            list2.Add(cShape.Copy());
            cShape.Layer = CShape.SumLayer++;
            list.Add(cShape);
            try
            {
                CControl cControl = (CControl)cShape;
                cControl._c.BringToFront();
            }
            catch (Exception)
            {
            }
        }
        theglobal.ForUndo(list, list2);
        theglobal.g_ListAllShowCShape.Sort(CEditEnvironmentGlobal.CompareByLayer);
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void button10_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count == 0)
        {
            return;
        }
        List<CShape> list = new();
        List<CShape> list2 = new();
        theglobal.SelectedShapeList.Sort(CEditEnvironmentGlobal.CompareByLayer);
        for (int num = theglobal.SelectedShapeList.Count - 1; num >= 0; num--)
        {
            CShape cShape = theglobal.SelectedShapeList[num];
            list2.Add(cShape.Copy());
            cShape.Layer = -(CShape.SumLayer++);
            list.Add(cShape);
            try
            {
                CControl cControl = (CControl)cShape;
                cControl._c.SendToBack();
            }
            catch (Exception)
            {
            }
        }
        theglobal.ForUndo(list, list2);
        theglobal.g_ListAllShowCShape.Sort(CEditEnvironmentGlobal.CompareByLayer);
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void button11_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count == 0)
        {
            return;
        }
        List<CShape> list = new();
        List<CShape> list2 = new();
        float num = float.MaxValue;
        for (int i = 0; i < 8; i++)
        {
            if (num > theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].ImportantPoints[i].X)
            {
                num = theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].ImportantPoints[i].X;
            }
        }
        CShape[] array = theglobal.SelectedShapeList.ToArray();
        foreach (CShape cShape in array)
        {
            float num2 = float.MaxValue;
            int num3 = 0;
            for (int k = 0; k < 8; k++)
            {
                if (num2 > cShape.ImportantPoints[k].X)
                {
                    num2 = cShape.ImportantPoints[k].X;
                    num3 = k;
                }
            }
            list2.Add(cShape.Copy());
            cShape.EditLocationByPoint(cShape.ImportantPoints[num3], new PointF(num, cShape.ImportantPoints[num3].Y));
            list.Add(cShape);
        }
        theglobal.ForUndo(list, list2);
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void button12_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count != 0)
        {
            List<CShape> list = new();
            List<CShape> list2 = new();
            float num = theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].ImportantPoints[0].X / 2f + theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].ImportantPoints[1].X / 2f;
            CShape[] array = theglobal.SelectedShapeList.ToArray();
            foreach (CShape cShape in array)
            {
                list2.Add(cShape.Copy());
                cShape.EditLocationByPoint(new PointF(cShape.ImportantPoints[0].X / 2f + cShape.ImportantPoints[1].X / 2f, cShape.ImportantPoints[0].Y / 2f + cShape.ImportantPoints[1].Y / 2f), new PointF(num, cShape.ImportantPoints[0].Y / 2f + cShape.ImportantPoints[1].Y / 2f));
                list.Add(cShape);
            }
            theglobal.ForUndo(list, list2);
            theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
            theglobal.uc2.RefreshGraphics();
            CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
        }
    }

    public void button13_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count == 0)
        {
            return;
        }
        List<CShape> list = new();
        List<CShape> list2 = new();
        float num = float.MinValue;
        for (int i = 0; i < 8; i++)
        {
            if (num < theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].ImportantPoints[i].X)
            {
                num = theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].ImportantPoints[i].X;
            }
        }
        CShape[] array = theglobal.SelectedShapeList.ToArray();
        foreach (CShape cShape in array)
        {
            float num2 = float.MinValue;
            int num3 = 0;
            for (int k = 0; k < 8; k++)
            {
                if (num2 < cShape.ImportantPoints[k].X)
                {
                    num2 = cShape.ImportantPoints[k].X;
                    num3 = k;
                }
            }
            list2.Add(cShape.Copy());
            cShape.EditLocationByPoint(cShape.ImportantPoints[num3], new PointF(num, cShape.ImportantPoints[num3].Y));
            list.Add(cShape);
        }
        theglobal.ForUndo(list, list2);
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void button16_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count == 0)
        {
            return;
        }
        List<CShape> list = new();
        List<CShape> list2 = new();
        float num = float.MaxValue;
        for (int i = 0; i < 8; i++)
        {
            if (num > theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].ImportantPoints[i].Y)
            {
                num = theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].ImportantPoints[i].Y;
            }
        }
        CShape[] array = theglobal.SelectedShapeList.ToArray();
        foreach (CShape cShape in array)
        {
            float num2 = float.MaxValue;
            int num3 = 0;
            for (int k = 0; k < 8; k++)
            {
                if (num2 > cShape.ImportantPoints[k].Y)
                {
                    num2 = cShape.ImportantPoints[k].Y;
                    num3 = k;
                }
            }
            list2.Add(cShape.Copy());
            cShape.EditLocationByPoint(cShape.ImportantPoints[num3], new PointF(cShape.ImportantPoints[num3].X, num));
            list.Add(cShape.Copy());
        }
        theglobal.ForUndo(list, list2);
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void button15_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count != 0)
        {
            List<CShape> list = new();
            List<CShape> list2 = new();
            float num = theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].ImportantPoints[0].Y / 2f + theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].ImportantPoints[1].Y / 2f;
            CShape[] array = theglobal.SelectedShapeList.ToArray();
            foreach (CShape cShape in array)
            {
                list2.Add(cShape.Copy());
                cShape.EditLocationByPoint(new PointF(cShape.ImportantPoints[0].X / 2f + cShape.ImportantPoints[1].X / 2f, cShape.ImportantPoints[0].Y / 2f + cShape.ImportantPoints[1].Y / 2f), new PointF(cShape.ImportantPoints[0].X / 2f + cShape.ImportantPoints[1].X / 2f, num));
                list.Add(cShape);
            }
            theglobal.ForUndo(list, list2);
            theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
            theglobal.uc2.RefreshGraphics();
            CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
        }
    }

    public void button14_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count == 0)
        {
            return;
        }
        List<CShape> list = new();
        List<CShape> list2 = new();
        float num = float.MinValue;
        for (int i = 0; i < 8; i++)
        {
            if (num < theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].ImportantPoints[i].Y)
            {
                num = theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].ImportantPoints[i].Y;
            }
        }
        CShape[] array = theglobal.SelectedShapeList.ToArray();
        foreach (CShape cShape in array)
        {
            float num2 = float.MinValue;
            int num3 = 0;
            for (int k = 0; k < 8; k++)
            {
                if (num2 < cShape.ImportantPoints[k].Y)
                {
                    num2 = cShape.ImportantPoints[k].Y;
                    num3 = k;
                }
            }
            list2.Add(cShape.Copy());
            cShape.EditLocationByPoint(cShape.ImportantPoints[num3], new PointF(cShape.ImportantPoints[num3].X, num));
            list.Add(cShape);
        }
        theglobal.ForUndo(list, list2);
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void button17_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count == 0)
        {
            return;
        }
        List<CShape> list = new();
        List<CShape> list2 = new();
        CShape[] array = theglobal.SelectedShapeList.ToArray();
        foreach (CShape cShape in array)
        {
            if (cShape is CGraphicsPath)
            {
                MessageBox.Show("组合状态不允许翻转!");
                return;
            }
            list2.Add(cShape.Copy());
            cShape.Mirror(0);
            list.Add(cShape);
        }
        theglobal.ForUndo(list, list2);
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void button18_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count == 0)
        {
            return;
        }
        List<CShape> list = new();
        List<CShape> list2 = new();
        CShape[] array = theglobal.SelectedShapeList.ToArray();
        foreach (CShape cShape in array)
        {
            if (cShape is CGraphicsPath)
            {
                MessageBox.Show("组合状态不允许翻转!");
                return;
            }
            list2.Add(cShape.Copy());
            cShape.Mirror(1);
            list.Add(cShape);
        }
        theglobal.ForUndo(list, list2);
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void ReFreshEnable()
    {
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        if (theglobal.SelectedShapeList.Count < 1)
        {
            button_顶层.Enabled = false;
            button_底层.Enabled = false;
            button_水平镜像.Enabled = false;
            button_竖直镜像.Enabled = false;
        }
        else
        {
            button_顶层.Enabled = true;
            button_底层.Enabled = true;
            button_水平镜像.Enabled = true;
            button_竖直镜像.Enabled = true;
        }
        if (theglobal.SelectedShapeList.Count < 2)
        {
            button_左对齐.Enabled = false;
            button_居中对齐.Enabled = false;
            button_右对齐.Enabled = false;
            button_底端对齐.Enabled = false;
            button_中间对齐.Enabled = false;
            button_顶端对齐.Enabled = false;
        }
        else
        {
            button_左对齐.Enabled = true;
            button_居中对齐.Enabled = true;
            button_右对齐.Enabled = true;
            button_底端对齐.Enabled = true;
            button_中间对齐.Enabled = true;
            button_顶端对齐.Enabled = true;
            int num = 0;
            if (theglobal.SelectedShapeList[0].GetType() == typeof(CEllipse) || theglobal.SelectedShapeList[0].GetType() == typeof(CRectangle) || theglobal.SelectedShapeList[0].GetType() == typeof(CCloseLines))
            {
                foreach (CShape selectedShape in theglobal.SelectedShapeList)
                {
                    _ = selectedShape;
                    if (theglobal.SelectedShapeList[0].GetType() == typeof(CEllipse) || theglobal.SelectedShapeList[0].GetType() == typeof(CRectangle) || theglobal.SelectedShapeList[0].GetType() == typeof(CCloseLines))
                    {
                        num++;
                    }
                }
            }
        }
        button_组合.Enabled = false;
        if (theglobal.SelectedShapeList.Count > 1)
        {
            button_组合.Enabled = true;
        }
        if (theglobal.SelectedShapeList.Count == 1 && theglobal.SelectedShapeList[0].Shapes.Length == 0)
        {
            button_组合.Enabled = true;
        }
        button_拆解.Enabled = false;
        CShape[] array = theglobal.SelectedShapeList.ToArray();
        foreach (CShape cShape in array)
        {
            if (cShape.Shapes.Length != 0)
            {
                button_拆解.Enabled = true;
            }
        }
        if (theglobal.SelectedShapeList.Count < 3)
        {
            button_水平等间距.Enabled = false;
            button_竖直等间距.Enabled = false;
        }
        else
        {
            button_水平等间距.Enabled = true;
            button_竖直等间距.Enabled = true;
        }
        if (this.OnButtonEnableChanged != null)
        {
            this.OnButtonEnableChanged(null, null);
        }
    }

    public void button19_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count == 0)
        {
            return;
        }
        CGraphicsPath cGraphicsPath = new();
        GiveName(cGraphicsPath);
        List<CShape> list = new();
        List<CShape> list2 = new();
        bool flag = false;
        CShape[] array = theglobal.SelectedShapeList.ToArray();
        foreach (CShape cShape in array)
        {
            if (cShape is not CControl)
            {
                flag = true;
                theglobal.g_ListAllShowCShape.Remove(cShape);
                list2.Add(cShape);
                CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cShape.ShapeID.ToString());
            }
        }
        if (!flag)
        {
            return;
        }
        theglobal.SelectedShapeList.Sort(CEditEnvironmentGlobal.CompareByLayer);
        CShape[] array2 = theglobal.SelectedShapeList.ToArray();
        foreach (CShape cShape2 in array2)
        {
            if (cShape2 is not CControl)
            {
                if (cShape2 is CPixieControl)
                {
                    ((CPixieControl)cShape2).clearEvent();
                }
                cGraphicsPath.AddPath(cShape2);
            }
        }
        theglobal.SelectedShapeList.Clear();
        theglobal.g_ListAllShowCShape.Add(cGraphicsPath);
        theglobal.SelectedShapeList.Add(cGraphicsPath);
        list.Add(cGraphicsPath);
        CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cGraphicsPath.ShapeID.ToString());
        theglobal.ForUndo(list, list2);
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        ReFreshEnable();
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void button20_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count == 0)
        {
            return;
        }
        List<CShape> list = new();
        List<CShape> list2 = new();
        CShape[] array = theglobal.SelectedShapeList.ToArray();
        foreach (CShape cShape in array)
        {
            if (cShape.Shapes.Length <= 0)
            {
                continue;
            }
            _ = (CGraphicsPath)cShape;
            theglobal.g_ListAllShowCShape.Remove(cShape);
            theglobal.SelectedShapeList.Remove(cShape);
            list2.Add(cShape.Copy());
            for (int j = 0; j < cShape.Shapes.Length; j++)
            {
                CShape cShape2 = cShape.Shapes[j].Copy();
                cShape2.EditPoint(cShape2.RotateAtPoint, new PointF(cShape2.ImportantPoints[0].X / 2f + cShape2.ImportantPoints[1].X / 2f, cShape2.ImportantPoints[0].Y / 2f + cShape2.ImportantPoints[1].Y / 2f), 55);
                cShape2.TranslateMatrix.Invert();
                cShape2.TranslateMatrix.TransformPoints(cShape2.ImportantPoints);
                cShape2.RotateAngel += cShape.RotateAngel;
                cShape2.TranslateMatrix.Reset();
                cShape2.TranslateMatrix.RotateAt(cShape2.RotateAngel, cShape2.RotateAtPoint);
                cShape2.TranslateMatrix.TransformPoints(cShape2.ImportantPoints);
                cShape2.EditLocationByPoint(new PointF(cShape2.ImportantPoints[0].X / 2f + cShape2.ImportantPoints[1].X / 2f, cShape2.ImportantPoints[0].Y / 2f + cShape2.ImportantPoints[1].Y / 2f), cShape.ImportantPoints[8 + j]);
                if (cShape2 is CPixieControl)
                {
                    ((CPixieControl)cShape2).OnGetVarTable += CForDCCEControl.GetVarTableEvent;
                    ((CPixieControl)cShape2).ValidateVar += CForDCCEControl.ValidateVarEvent;
                }
                GiveName(cShape2);
                theglobal.g_ListAllShowCShape.Add(cShape2);
                theglobal.SelectedShapeList.Add(cShape2);
                list.Add(cShape2);
                CEditEnvironmentGlobal.mdiparent.objView_Page.OnFresh(cShape2.ShapeID.ToString());
            }
            theglobal.ForUndo(list, list2);
        }
        theglobal.g_ListAllShowCShape.Sort(CEditEnvironmentGlobal.CompareByLayer);
        ReFreshEnable();
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
        CEditEnvironmentGlobal.mdiparent.objView_Page.FreshAll();
    }

    public void button_撤消_Click(object sender, EventArgs e)
    {
        theglobal.UnDo();
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        theglobal.g_ListAllShowCShape.Sort(CEditEnvironmentGlobal.CompareByLayer);
        theglobal.uc2.RefreshGraphics();
        ReFreshEnable();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void button_重复_Click(object sender, EventArgs e)
    {
        theglobal.ReDo();
        theglobal.ReFreshReUnDo((Button)base.Controls.Find("button_撤消", searchAllChildren: false)[0], (Button)base.Controls.Find("button_重复", searchAllChildren: false)[0]);
        theglobal.g_ListAllShowCShape.Sort(CEditEnvironmentGlobal.CompareByLayer);
        theglobal.uc2.RefreshGraphics();
        ReFreshEnable();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void button25_Click(object sender, EventArgs e)
    {
        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
            DataFile dataFile = new()
            {
                ListAllShowCShape = theglobal.g_ListAllShowCShape,
                color = theglobal.pageProp.PageColor,
                pageimage = theglobal.pageProp.PageImage.img,
                pageImageLayout = theglobal.pageProp.PageImageLayout,
                location = theglobal.pageProp.PageLocation,
                name = theglobal.pageProp.PageName,
                size = theglobal.pageProp.PageSize
            };
            dataFile.PageOldSize = dataFile.size;
            dataFile.visable = theglobal.pageProp.PageVisible;
            Operation.BinarySaveFile(saveFileDialog1.FileName, dataFile);
            MessageBox.Show("保存成功，到" + saveFileDialog1.FileName);
        }
    }

    public void button26_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        theglobal.g_ListAllShowCShape.Clear();
        theglobal.SelectedShapeList.Clear();
        DataFile dataFile = Operation.BinaryLoadFile(openFileDialog1.FileName);
        theglobal.g_ListAllShowCShape = dataFile.ListAllShowCShape;
        CEditEnvironmentGlobal.mdiparent.objView_Page.OnClear();
        CEditEnvironmentGlobal.mdiparent.objView_Page.m_ObjGbl = theglobal;
        CEditEnvironmentGlobal.mdiparent.objView_Page.OnShow();
        foreach (CShape item in theglobal.g_ListAllShowCShape)
        {
            if (item.GetType() == typeof(CControl))
            {
                ((CControl)item).ReLifeMe();
                ((CControl)item).initLocationErr = true;
                theglobal.uc2.Controls.Add(((CControl)item)._c);
                ((CControl)item).initLocationErr = false;
                ((CControl)item)._c.Enabled = false;
            }
        }
        theglobal.uc2.RefreshGraphics();
        MessageBox.Show("读取成功，从" + openFileDialog1.FileName);
    }

    public void button27_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count < 2)
        {
            return;
        }
        SizeF size = theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].Size;
        List<CShape> list = new();
        List<CShape> list2 = new();
        foreach (CShape selectedShape in theglobal.SelectedShapeList)
        {
            list2.Add(selectedShape.Copy());
            selectedShape.Size = size;
            list.Add(selectedShape);
        }
        theglobal.ForUndo(list, list2);
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void SameWidth()
    {
        if (theglobal.SelectedShapeList.Count < 2)
        {
            return;
        }
        SizeF size = theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].Size;
        List<CShape> list = new();
        List<CShape> list2 = new();
        foreach (CShape selectedShape in theglobal.SelectedShapeList)
        {
            list2.Add(selectedShape.Copy());
            selectedShape.Width = size.Width;
            list.Add(selectedShape);
        }
        theglobal.ForUndo(list, list2);
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    public void SameHeight()
    {
        if (theglobal.SelectedShapeList.Count < 2)
        {
            return;
        }
        SizeF size = theglobal.SelectedShapeList[theglobal.SelectedShapeList.Count - 1].Size;
        List<CShape> list = new();
        List<CShape> list2 = new();
        foreach (CShape selectedShape in theglobal.SelectedShapeList)
        {
            list2.Add(selectedShape.Copy());
            selectedShape.Height = size.Height;
            list.Add(selectedShape);
        }
        theglobal.ForUndo(list, list2);
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
    }

    private void InitializeComponent()
    {
        this.button_绘制椭圆 = new System.Windows.Forms.Button();
        this.button_绘制矩形 = new System.Windows.Forms.Button();
        this.button_绘制折线 = new System.Windows.Forms.Button();
        this.button_绘制多边形 = new System.Windows.Forms.Button();
        this.button_绘制文字 = new System.Windows.Forms.Button();
        this.button_水平等间距 = new System.Windows.Forms.Button();
        this.button_竖直等间距 = new System.Windows.Forms.Button();
        this.button_顶层 = new System.Windows.Forms.Button();
        this.button_底层 = new System.Windows.Forms.Button();
        this.button_左对齐 = new System.Windows.Forms.Button();
        this.button_居中对齐 = new System.Windows.Forms.Button();
        this.button_右对齐 = new System.Windows.Forms.Button();
        this.button_底端对齐 = new System.Windows.Forms.Button();
        this.button_中间对齐 = new System.Windows.Forms.Button();
        this.button_顶端对齐 = new System.Windows.Forms.Button();
        this.button_水平镜像 = new System.Windows.Forms.Button();
        this.button_竖直镜像 = new System.Windows.Forms.Button();
        this.button_组合 = new System.Windows.Forms.Button();
        this.button_拆解 = new System.Windows.Forms.Button();
        this.button_撤消 = new System.Windows.Forms.Button();
        this.button_重复 = new System.Windows.Forms.Button();
        this.button_保存 = new System.Windows.Forms.Button();
        this.button_读取 = new System.Windows.Forms.Button();
        this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        this.button_大小相等 = new System.Windows.Forms.Button();
        this.button_绘制直线 = new System.Windows.Forms.Button();
        this.button_绘制圆角矩形 = new System.Windows.Forms.Button();
        this.button_导入图片 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        this.button_绘制椭圆.Location = new System.Drawing.Point(82, 34);
        this.button_绘制椭圆.Name = "button_绘制椭圆";
        this.button_绘制椭圆.Size = new System.Drawing.Size(75, 23);
        this.button_绘制椭圆.TabIndex = 0;
        this.button_绘制椭圆.Text = "椭圆";
        this.button_绘制椭圆.UseVisualStyleBackColor = true;
        this.button_绘制椭圆.Click += new System.EventHandler(button_绘制椭圆_Click);
        this.button_绘制矩形.Location = new System.Drawing.Point(163, 34);
        this.button_绘制矩形.Name = "button_绘制矩形";
        this.button_绘制矩形.Size = new System.Drawing.Size(75, 23);
        this.button_绘制矩形.TabIndex = 1;
        this.button_绘制矩形.Text = "矩形";
        this.button_绘制矩形.UseVisualStyleBackColor = true;
        this.button_绘制矩形.Click += new System.EventHandler(button_绘制矩形_Click);
        this.button_绘制折线.Location = new System.Drawing.Point(163, 5);
        this.button_绘制折线.Name = "button_绘制折线";
        this.button_绘制折线.Size = new System.Drawing.Size(75, 23);
        this.button_绘制折线.TabIndex = 2;
        this.button_绘制折线.Text = "折线";
        this.button_绘制折线.UseVisualStyleBackColor = true;
        this.button_绘制折线.Click += new System.EventHandler(button_绘制折线_Click);
        this.button_绘制多边形.Location = new System.Drawing.Point(82, 63);
        this.button_绘制多边形.Name = "button_绘制多边形";
        this.button_绘制多边形.Size = new System.Drawing.Size(75, 23);
        this.button_绘制多边形.TabIndex = 3;
        this.button_绘制多边形.Text = "多边形";
        this.button_绘制多边形.UseVisualStyleBackColor = true;
        this.button_绘制多边形.Click += new System.EventHandler(button_绘制多边形_Click);
        this.button_绘制文字.Location = new System.Drawing.Point(82, 92);
        this.button_绘制文字.Name = "button_绘制文字";
        this.button_绘制文字.Size = new System.Drawing.Size(75, 23);
        this.button_绘制文字.TabIndex = 4;
        this.button_绘制文字.Text = "文字";
        this.button_绘制文字.UseVisualStyleBackColor = true;
        this.button_绘制文字.Click += new System.EventHandler(button_绘制文字_Click);
        this.button_水平等间距.Location = new System.Drawing.Point(82, 150);
        this.button_水平等间距.Name = "button_水平等间距";
        this.button_水平等间距.Size = new System.Drawing.Size(75, 23);
        this.button_水平等间距.TabIndex = 6;
        this.button_水平等间距.Text = "水平等间距";
        this.button_水平等间距.UseVisualStyleBackColor = true;
        this.button_水平等间距.Click += new System.EventHandler(button7_Click);
        this.button_竖直等间距.Location = new System.Drawing.Point(163, 150);
        this.button_竖直等间距.Name = "button_竖直等间距";
        this.button_竖直等间距.Size = new System.Drawing.Size(75, 23);
        this.button_竖直等间距.TabIndex = 7;
        this.button_竖直等间距.Text = "竖直等间距";
        this.button_竖直等间距.UseVisualStyleBackColor = true;
        this.button_竖直等间距.Click += new System.EventHandler(button8_Click);
        this.button_顶层.Location = new System.Drawing.Point(0, 150);
        this.button_顶层.Name = "button_顶层";
        this.button_顶层.Size = new System.Drawing.Size(37, 23);
        this.button_顶层.TabIndex = 8;
        this.button_顶层.Text = "顶层";
        this.button_顶层.UseVisualStyleBackColor = true;
        this.button_顶层.Click += new System.EventHandler(button9_Click);
        this.button_底层.Location = new System.Drawing.Point(38, 150);
        this.button_底层.Name = "button_底层";
        this.button_底层.Size = new System.Drawing.Size(37, 23);
        this.button_底层.TabIndex = 9;
        this.button_底层.Text = "底层";
        this.button_底层.UseVisualStyleBackColor = true;
        this.button_底层.Click += new System.EventHandler(button10_Click);
        this.button_左对齐.Location = new System.Drawing.Point(3, 5);
        this.button_左对齐.Name = "button_左对齐";
        this.button_左对齐.Size = new System.Drawing.Size(21, 23);
        this.button_左对齐.TabIndex = 10;
        this.button_左对齐.Text = "左";
        this.button_左对齐.UseVisualStyleBackColor = true;
        this.button_左对齐.Click += new System.EventHandler(button11_Click);
        this.button_居中对齐.Location = new System.Drawing.Point(29, 5);
        this.button_居中对齐.Name = "button_居中对齐";
        this.button_居中对齐.Size = new System.Drawing.Size(21, 23);
        this.button_居中对齐.TabIndex = 11;
        this.button_居中对齐.Text = "中";
        this.button_居中对齐.UseVisualStyleBackColor = true;
        this.button_居中对齐.Click += new System.EventHandler(button12_Click);
        this.button_右对齐.Location = new System.Drawing.Point(55, 5);
        this.button_右对齐.Name = "button_右对齐";
        this.button_右对齐.Size = new System.Drawing.Size(21, 23);
        this.button_右对齐.TabIndex = 12;
        this.button_右对齐.Text = "右";
        this.button_右对齐.UseVisualStyleBackColor = true;
        this.button_右对齐.Click += new System.EventHandler(button13_Click);
        this.button_底端对齐.Location = new System.Drawing.Point(55, 34);
        this.button_底端对齐.Name = "button_底端对齐";
        this.button_底端对齐.Size = new System.Drawing.Size(21, 23);
        this.button_底端对齐.TabIndex = 15;
        this.button_底端对齐.Text = "下";
        this.button_底端对齐.UseVisualStyleBackColor = true;
        this.button_底端对齐.Click += new System.EventHandler(button14_Click);
        this.button_中间对齐.Location = new System.Drawing.Point(29, 34);
        this.button_中间对齐.Name = "button_中间对齐";
        this.button_中间对齐.Size = new System.Drawing.Size(21, 23);
        this.button_中间对齐.TabIndex = 14;
        this.button_中间对齐.Text = "中";
        this.button_中间对齐.UseVisualStyleBackColor = true;
        this.button_中间对齐.Click += new System.EventHandler(button15_Click);
        this.button_顶端对齐.Location = new System.Drawing.Point(3, 34);
        this.button_顶端对齐.Name = "button_顶端对齐";
        this.button_顶端对齐.Size = new System.Drawing.Size(21, 23);
        this.button_顶端对齐.TabIndex = 13;
        this.button_顶端对齐.Text = "上";
        this.button_顶端对齐.UseVisualStyleBackColor = true;
        this.button_顶端对齐.Click += new System.EventHandler(button16_Click);
        this.button_水平镜像.Location = new System.Drawing.Point(82, 179);
        this.button_水平镜像.Name = "button_水平镜像";
        this.button_水平镜像.Size = new System.Drawing.Size(75, 23);
        this.button_水平镜像.TabIndex = 16;
        this.button_水平镜像.Text = "水平镜像";
        this.button_水平镜像.UseVisualStyleBackColor = true;
        this.button_水平镜像.Click += new System.EventHandler(button17_Click);
        this.button_竖直镜像.Location = new System.Drawing.Point(163, 179);
        this.button_竖直镜像.Name = "button_竖直镜像";
        this.button_竖直镜像.Size = new System.Drawing.Size(75, 23);
        this.button_竖直镜像.TabIndex = 17;
        this.button_竖直镜像.Text = "竖直镜像";
        this.button_竖直镜像.UseVisualStyleBackColor = true;
        this.button_竖直镜像.Click += new System.EventHandler(button18_Click);
        this.button_组合.Location = new System.Drawing.Point(82, 121);
        this.button_组合.Name = "button_组合";
        this.button_组合.Size = new System.Drawing.Size(75, 23);
        this.button_组合.TabIndex = 18;
        this.button_组合.Text = "组合";
        this.button_组合.UseVisualStyleBackColor = true;
        this.button_组合.Click += new System.EventHandler(button19_Click);
        this.button_拆解.Location = new System.Drawing.Point(163, 121);
        this.button_拆解.Name = "button_拆解";
        this.button_拆解.Size = new System.Drawing.Size(75, 23);
        this.button_拆解.TabIndex = 19;
        this.button_拆解.Text = "拆解";
        this.button_拆解.UseVisualStyleBackColor = true;
        this.button_拆解.Click += new System.EventHandler(button20_Click);
        this.button_撤消.Location = new System.Drawing.Point(0, 63);
        this.button_撤消.Name = "button_撤消";
        this.button_撤消.Size = new System.Drawing.Size(75, 23);
        this.button_撤消.TabIndex = 20;
        this.button_撤消.Text = "撤销";
        this.button_撤消.UseVisualStyleBackColor = true;
        this.button_撤消.Click += new System.EventHandler(button_撤消_Click);
        this.button_重复.Location = new System.Drawing.Point(0, 92);
        this.button_重复.Name = "button_重复";
        this.button_重复.Size = new System.Drawing.Size(75, 23);
        this.button_重复.TabIndex = 21;
        this.button_重复.Text = "重复";
        this.button_重复.UseVisualStyleBackColor = true;
        this.button_重复.Click += new System.EventHandler(button_重复_Click);
        this.button_保存.Location = new System.Drawing.Point(0, 121);
        this.button_保存.Name = "button_保存";
        this.button_保存.Size = new System.Drawing.Size(37, 23);
        this.button_保存.TabIndex = 24;
        this.button_保存.Text = "保存";
        this.button_保存.UseVisualStyleBackColor = true;
        this.button_保存.Click += new System.EventHandler(button25_Click);
        this.button_读取.Location = new System.Drawing.Point(38, 121);
        this.button_读取.Name = "button_读取";
        this.button_读取.Size = new System.Drawing.Size(37, 23);
        this.button_读取.TabIndex = 25;
        this.button_读取.Text = "读取";
        this.button_读取.UseVisualStyleBackColor = true;
        this.button_读取.Click += new System.EventHandler(button26_Click);
        this.openFileDialog1.FileName = "openFileDialog1";
        this.button_大小相等.Location = new System.Drawing.Point(1, 179);
        this.button_大小相等.Name = "button_大小相等";
        this.button_大小相等.Size = new System.Drawing.Size(75, 23);
        this.button_大小相等.TabIndex = 26;
        this.button_大小相等.Text = "大小相等";
        this.button_大小相等.UseVisualStyleBackColor = true;
        this.button_大小相等.Click += new System.EventHandler(button27_Click);
        this.button_绘制直线.Location = new System.Drawing.Point(82, 5);
        this.button_绘制直线.Name = "button_绘制直线";
        this.button_绘制直线.Size = new System.Drawing.Size(75, 23);
        this.button_绘制直线.TabIndex = 27;
        this.button_绘制直线.Text = "直线";
        this.button_绘制直线.UseVisualStyleBackColor = true;
        this.button_绘制直线.Click += new System.EventHandler(button_绘制直线_Click);
        this.button_绘制圆角矩形.Location = new System.Drawing.Point(163, 63);
        this.button_绘制圆角矩形.Name = "button_绘制圆角矩形";
        this.button_绘制圆角矩形.Size = new System.Drawing.Size(75, 23);
        this.button_绘制圆角矩形.TabIndex = 28;
        this.button_绘制圆角矩形.Text = "圆角矩形";
        this.button_绘制圆角矩形.UseVisualStyleBackColor = true;
        this.button_绘制圆角矩形.Click += new System.EventHandler(button_绘制圆角矩形_Click);
        this.button_导入图片.Location = new System.Drawing.Point(163, 92);
        this.button_导入图片.Name = "button_导入图片";
        this.button_导入图片.Size = new System.Drawing.Size(75, 23);
        this.button_导入图片.TabIndex = 29;
        this.button_导入图片.Text = "图片";
        this.button_导入图片.UseVisualStyleBackColor = true;
        this.button_导入图片.Click += new System.EventHandler(button_导入图片_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.Controls.Add(this.button_导入图片);
        base.Controls.Add(this.button_绘制圆角矩形);
        base.Controls.Add(this.button_绘制直线);
        base.Controls.Add(this.button_大小相等);
        base.Controls.Add(this.button_读取);
        base.Controls.Add(this.button_保存);
        base.Controls.Add(this.button_重复);
        base.Controls.Add(this.button_撤消);
        base.Controls.Add(this.button_拆解);
        base.Controls.Add(this.button_组合);
        base.Controls.Add(this.button_竖直镜像);
        base.Controls.Add(this.button_水平镜像);
        base.Controls.Add(this.button_底端对齐);
        base.Controls.Add(this.button_中间对齐);
        base.Controls.Add(this.button_顶端对齐);
        base.Controls.Add(this.button_右对齐);
        base.Controls.Add(this.button_居中对齐);
        base.Controls.Add(this.button_左对齐);
        base.Controls.Add(this.button_底层);
        base.Controls.Add(this.button_顶层);
        base.Controls.Add(this.button_竖直等间距);
        base.Controls.Add(this.button_水平等间距);
        base.Controls.Add(this.button_绘制文字);
        base.Controls.Add(this.button_绘制多边形);
        base.Controls.Add(this.button_绘制折线);
        base.Controls.Add(this.button_绘制矩形);
        base.Controls.Add(this.button_绘制椭圆);
        base.Name = "UserCommandControl2";
        base.Size = new System.Drawing.Size(239, 208);
        base.ResumeLayout(false);
    }
}
