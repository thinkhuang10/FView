using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using ShapeRuntime;

namespace HMIEditEnvironment;

internal class CheckIOExists
{
    public static List<string> IOItemList = new();

    public static List<string> OnlyIOItemList = new();

    private static bool ioTableOld = true;

    public static bool IOTableOld
    {
        get
        {
            return ioTableOld;
        }
        set
        {
            if (value)
            {
                Renew();
            }
        }
    }

    private static void Renew()
    {
        IOItemList.Clear();
        OnlyIOItemList.Clear();
        TreeView treeView = new();
        TreeNode tn = treeView.Nodes.Add("0", "变量");
        treeView.Nodes.Add("Page", "页面");
        if (CEditEnvironmentGlobal.ioitemroot.subitem.Count != 0)
        {
            PushToTree(tn, CEditEnvironmentGlobal.ioitemroot);
        }
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            TreeNode treeNode = treeView.Nodes["Page"].Nodes.Add("Page", df.name);
            foreach (CShape item in df.ListAllShowCShape)
            {
                TreeNode treeNode2 = treeNode.Nodes.Add("PageItem", item.Name);
                Type type = item.GetType();
                if (type == typeof(CControl))
                {
                    type = ((CControl)item)._c.GetType();
                    PropertyInfo[] properties = type.GetProperties();
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        TreeNode treeNode3 = treeNode2.Nodes.Add("PageShapeItem", propertyInfo.Name);
                        IOItemList.Add(treeNode3.Parent.Parent.Text + "." + treeNode3.Parent.Text + "." + treeNode3.Text);
                    }
                }
                else if (type == typeof(CString))
                {
                    TreeNode treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "DisplayStr");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "X");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Y");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Width");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Height");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Angel");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "BrushAngel");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Lay");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "RotatePointX");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "RotatePointY");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Visible");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "PenWidth");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "PenColorA");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "PenColorR");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "PenColorG");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "PenColorB");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Color1A");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Color1R");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Color1G");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Color1B");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Color2A");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Color2R");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Color2G");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                    treeNode4 = treeNode2.Nodes.Add("PageShapeItem", "Color2B");
                    IOItemList.Add(treeNode4.Parent.Parent.Text + "." + treeNode4.Parent.Text + "." + treeNode4.Text);
                }
                else
                {
                    TreeNode treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "X");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Y");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Width");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Height");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Angel");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "BrushAngel");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Lay");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "RotatePointX");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "RotatePointY");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Visible");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "PenWidth");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "PenColorA");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "PenColorR");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "PenColorG");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "PenColorB");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Color1A");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Color1R");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Color1G");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Color1B");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Color2A");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Color2R");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Color2G");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                    treeNode5 = treeNode2.Nodes.Add("PageShapeItem", "Color2B");
                    IOItemList.Add(treeNode5.Parent.Parent.Text + "." + treeNode5.Parent.Text + "." + treeNode5.Text);
                }
            }
        }
        TreeNode treeNode6 = (TreeNode)CEditEnvironmentGlobal.dhp.ProjectIOTreeRoot.Clone();
        PorjectIOPushToTree(treeNode6, CEditEnvironmentGlobal.dhp.ProjectIOs);
        treeNode6.Name = "ProjectIO";
        treeView.Nodes["0"].Nodes.Add(treeNode6);

        ioTableOld = false;
    }

    public static void PushToTree(TreeNode tn, CIOItem root)
    {
        TreeNode treeNode = tn.Nodes.Add(root.id, root.name);
        if (root.IsLeaf)
        {
            OnlyIOItemList.Add("[" + treeNode.Text + "]");
            IOItemList.Add("[" + treeNode.Text + "]");
        }
        foreach (CIOItem item in root.subitem)
        {
            PushToTree(treeNode, item);
        }
    }

    public static void PorjectIOPushToTree(TreeNode tn, List<ProjectIO> ProjectIOs)
    {
        foreach (TreeNode node in tn.Nodes)
        {
            PorjectIOPushToTree(node, ProjectIOs);
        }
        foreach (ProjectIO ProjectIO in ProjectIOs)
        {
            if (!(ProjectIO.GroupName != tn.Text))
            {
                TreeNode treeNode = tn.Nodes.Add("theProjectIO", ProjectIO.name);
                IOItemList.Add("[" + treeNode.Text + "]");
                OnlyIOItemList.Add("[" + treeNode.Text + "]");
            }
        }
    }

    public static List<string> PageInUse(string pageName)
    {
        Regex regex = new("[\\W]" + pageName + "[\\W|\\n]");
        List<string> list = new();
        if (CEditEnvironmentGlobal.dhp.cxdzshijiaoben != null)
        {
            if (CEditEnvironmentGlobal.dhp.cxdzshijiaoben[0] != null && regex.IsMatch(" " + CEditEnvironmentGlobal.dhp.cxdzshijiaoben[0]))
            {
                list.Add("程序启动脚本");
            }
            if (CEditEnvironmentGlobal.dhp.cxdzshijiaoben[1] != null && regex.IsMatch(" " + CEditEnvironmentGlobal.dhp.cxdzshijiaoben[1]))
            {
                list.Add("程序运行脚本");
            }
            if (CEditEnvironmentGlobal.dhp.cxdzshijiaoben[2] != null && regex.IsMatch(" " + CEditEnvironmentGlobal.dhp.cxdzshijiaoben[2]))
            {
                list.Add("程序关闭脚本");
            }
        }
        if (CEditEnvironmentGlobal.dhp.IOAlarms != null)
        {
            foreach (CIOAlarm iOAlarm in CEditEnvironmentGlobal.dhp.IOAlarms)
            {
                if (iOAlarm.script[6] != null && regex.IsMatch(" " + iOAlarm.script[6]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.script[7] != null && regex.IsMatch(" " + iOAlarm.script[7]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.script[8] != null && regex.IsMatch(" " + iOAlarm.script[8]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.script[9] != null && regex.IsMatch(" " + iOAlarm.script[9]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.script[10] != null && regex.IsMatch(" " + iOAlarm.script[10]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.script[11] != null && regex.IsMatch(" " + iOAlarm.script[11]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.boolAlarmScript[5] != null && regex.IsMatch(" " + iOAlarm.boolAlarmScript[5]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.boolAlarmScript[6] != null && regex.IsMatch(" " + iOAlarm.boolAlarmScript[6]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.boolAlarmScript[7] != null && regex.IsMatch(" " + iOAlarm.boolAlarmScript[7]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.boolAlarmScript[8] != null && regex.IsMatch(" " + iOAlarm.boolAlarmScript[8]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.boolAlarmScript[9] != null && regex.IsMatch(" " + iOAlarm.boolAlarmScript[9]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
            }
        }
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            if (df.name == pageName)
            {
                continue;
            }
            if (df.pagedzshijiaoben != null)
            {
                if (df.pagedzshijiaoben[0] != null && regex.IsMatch(" " + df.pagedzshijiaoben[0]))
                {
                    list.Add("页面" + df.name + "启动脚本");
                }
                if (df.pagedzshijiaoben[1] != null && regex.IsMatch(" " + df.pagedzshijiaoben[1]))
                {
                    list.Add("页面" + df.name + "运行脚本");
                }
                if (df.pagedzshijiaoben[2] != null && regex.IsMatch(" " + df.pagedzshijiaoben[2]))
                {
                    list.Add("页面" + df.name + "关闭脚本");
                }
            }
            foreach (CShape item in df.ListAllShowCShape)
            {
                if (item.ymqhxianshi != null)
                {
                    string[] ymqhxianshi = item.ymqhxianshi;
                    foreach (string text in ymqhxianshi)
                    {
                        if (text != null && text.Contains(pageName))
                        {
                            list.Add("页面" + df.name + "中图元" + item.ShapeName + "动画页面切换");
                            break;
                        }
                    }
                }
                if (item.ymqhyincang != null)
                {
                    string[] ymqhyincang = item.ymqhyincang;
                    foreach (string text2 in ymqhyincang)
                    {
                        if (text2 != null && text2.Contains(pageName))
                        {
                            list.Add("页面" + df.name + "中图元" + item.ShapeName + "动画页面切换");
                            break;
                        }
                    }
                }
                if (item.UserLogic != null)
                {
                    string[] userLogic = item.UserLogic;
                    foreach (string text3 in userLogic)
                    {
                        if (text3 != null && text3.Contains(pageName))
                        {
                            list.Add("页面" + df.name + "中图元" + item.ShapeName + "动画鼠标事件高级脚本");
                            break;
                        }
                    }
                }
                if (item.DBOKScriptUser != null && item.DBOKScriptUser.Contains(pageName))
                {
                    list.Add("页面" + df.name + "中图元" + item.ShapeName + "数据库操作成功脚本");
                    break;
                }
                if (item.DBErrScriptUser != null && item.DBOKScriptUser.Contains(pageName))
                {
                    list.Add("页面" + df.name + "中图元" + item.ShapeName + "数据库操作失败脚本");
                    break;
                }
                if (item.ysdzshijianbiaodashi == null)
                {
                    continue;
                }
                for (int l = 0; l < item.ysdzshijianbiaodashi.Length; l++)
                {
                    string text4 = item.ysdzshijianbiaodashi[l];
                    if (text4 != null && text4.Contains(pageName))
                    {
                        list.Add("页面" + df.name + "中图元" + item.ShapeName + "事件" + item.ysdzshijianmingcheng[l] + "脚本");
                        break;
                    }
                }
            }
        }
        return list;
    }

    public static List<string> ShapeInUse(string shapeName)
    {
        Regex regex = new("[\\W]" + shapeName + "[\\W|\\n]");
        List<string> list = new();
        if (CEditEnvironmentGlobal.dhp.cxdzshijiaoben != null)
        {
            if (CEditEnvironmentGlobal.dhp.cxdzshijiaoben[0] != null && regex.IsMatch(" " + CEditEnvironmentGlobal.dhp.cxdzshijiaoben[0]))
            {
                list.Add("程序启动脚本");
            }
            if (CEditEnvironmentGlobal.dhp.cxdzshijiaoben[1] != null && regex.IsMatch(" " + CEditEnvironmentGlobal.dhp.cxdzshijiaoben[1]))
            {
                list.Add("程序运行脚本");
            }
            if (CEditEnvironmentGlobal.dhp.cxdzshijiaoben[2] != null && regex.IsMatch(" " + CEditEnvironmentGlobal.dhp.cxdzshijiaoben[2]))
            {
                list.Add("程序关闭脚本");
            }
        }
        if (CEditEnvironmentGlobal.dhp.IOAlarms != null)
        {
            foreach (CIOAlarm iOAlarm in CEditEnvironmentGlobal.dhp.IOAlarms)
            {
                if (iOAlarm.script[6] != null && regex.IsMatch(" " + iOAlarm.script[6]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.script[7] != null && regex.IsMatch(" " + iOAlarm.script[7]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.script[8] != null && regex.IsMatch(" " + iOAlarm.script[8]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.script[9] != null && regex.IsMatch(" " + iOAlarm.script[9]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.script[10] != null && regex.IsMatch(" " + iOAlarm.script[10]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.script[11] != null && regex.IsMatch(" " + iOAlarm.script[11]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.boolAlarmScript[5] != null && regex.IsMatch(" " + iOAlarm.boolAlarmScript[5]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.boolAlarmScript[6] != null && regex.IsMatch(" " + iOAlarm.boolAlarmScript[6]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.boolAlarmScript[7] != null && regex.IsMatch(" " + iOAlarm.boolAlarmScript[7]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.boolAlarmScript[8] != null && regex.IsMatch(" " + iOAlarm.boolAlarmScript[8]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
                if (iOAlarm.boolAlarmScript[9] != null && regex.IsMatch(" " + iOAlarm.boolAlarmScript[9]))
                {
                    list.Add("变量" + iOAlarm.name + "报警脚本");
                }
            }
        }
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            if (df.pagedzshijiaoben != null)
            {
                if (df.pagedzshijiaoben[0] != null && regex.IsMatch(" " + df.pagedzshijiaoben[0]))
                {
                    list.Add("页面" + df.name + "启动脚本");
                }
                if (df.pagedzshijiaoben[1] != null && regex.IsMatch(" " + df.pagedzshijiaoben[1]))
                {
                    list.Add("页面" + df.name + "运行脚本");
                }
                if (df.pagedzshijiaoben[2] != null && regex.IsMatch(" " + df.pagedzshijiaoben[2]))
                {
                    list.Add("页面" + df.name + "关闭脚本");
                }
            }
            foreach (CShape item in df.ListAllShowCShape)
            {
                if (item.ShapeName == shapeName)
                {
                    continue;
                }
                if (item.UserLogic != null)
                {
                    string[] userLogic = item.UserLogic;
                    foreach (string text in userLogic)
                    {
                        if (text != null && regex.IsMatch(" " + text))
                        {
                            list.Add("页面" + df.name + "中图元" + item.ShapeName + "动画鼠标事件高级脚本");
                            break;
                        }
                    }
                }
                if (item.DBOKScriptUser != null && regex.IsMatch(" " + item.DBOKScriptUser))
                {
                    list.Add("页面" + df.name + "中图元" + item.ShapeName + "数据库操作成功脚本");
                    break;
                }
                if (item.DBErrScriptUser != null && regex.IsMatch(" " + item.DBOKScriptUser))
                {
                    list.Add("页面" + df.name + "中图元" + item.ShapeName + "数据库操作失败脚本");
                    break;
                }
                if (item.ysdzshijianbiaodashi == null)
                {
                    continue;
                }
                for (int j = 0; j < item.ysdzshijianbiaodashi.Length; j++)
                {
                    string text2 = item.ysdzshijianbiaodashi[j];
                    if (text2 != null && regex.IsMatch(" " + text2))
                    {
                        list.Add("页面" + df.name + "中图元" + item.ShapeName + "事件" + item.ysdzshijianmingcheng[j] + "脚本");
                        break;
                    }
                }
            }
        }
        return list;
    }
}
