using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class CEditEnvironmentGlobal
{
    public static class PageManager
    {
        public static class DFM
        {
            public static int FindIndex(string pageName)
            {
                return dfs.FindIndex((DataFile df) => df.name.Equals(pageName));
            }

            public static DataFile Find(string pageName)
            {
                return dfs.Find((DataFile df) => df.name.Equals(pageName));
            }

            public static bool Exists(string pageName)
            {
                return dfs.Exists((DataFile item) => item.name.Equals(pageName));
            }

            public static void Prepend(DataFile df)
            {
                dfs.Insert(0, df);
            }

            public static void Append(DataFile df)
            {
                dfs.Add(df);
            }

            public static void Insert(int index, DataFile df)
            {
                dfs.Insert(index, df);
            }

            public static void Remove(DataFile df)
            {
                dfs.Remove(df);
            }
        }

        public static void MovePageGroupAfterNode(TreeNode srcNode, TreeNode dstNode)
        {
            if (dstNode == null)
            {
                return;
            }
            List<string> pageNameList = new();
            FindAllPageInGroupNode(srcNode, ref pageNameList);
            if (pageNameList.Count <= 0)
            {
                return;
            }
            string text = null;
            if (dstNode.Tag is HmiPageGroup)
            {
                TreeNode treeNode = FindLastPageNodeInGroupNode(dstNode) ?? FindPrevPageNodeFromNode(dstNode);
                if (treeNode != null)
                {
                    text = (treeNode.Tag as HmiPage).Name;
                }
            }
            else if (dstNode.Tag is HmiPage)
            {
                text = (dstNode.Tag as HmiPage).Name;
            }
            if (text != null)
            {
                pageNameList.Reverse();
                {
                    foreach (string item in pageNameList)
                    {
                        MovePageAfterPage(item, text);
                    }
                    return;
                }
            }
            pageNameList.Reverse();
            foreach (string item2 in pageNameList)
            {
                MovePageToTop(item2);
            }
        }

        public static void MovePageGroupBeforeNode(TreeNode srcNode, TreeNode dstNode)
        {
            if (dstNode == null)
            {
                return;
            }
            List<string> pageNameList = new();
            FindAllPageInGroupNode(srcNode, ref pageNameList);
            if (pageNameList.Count <= 0)
            {
                return;
            }
            string text = null;
            if (dstNode.Tag is HmiPageGroup)
            {
                TreeNode treeNode = FindFirstPageNodeInGroupNode(dstNode) ?? FindNextPageNodeFromNode(dstNode);
                if (treeNode != null)
                {
                    text = (treeNode.Tag as HmiPage).Name;
                }
            }
            else if (dstNode.Tag is HmiPage)
            {
                text = (dstNode.Tag as HmiPage).Name;
            }
            if (text != null)
            {
                foreach (string item in pageNameList)
                {
                    MovePageBeforePage(item, text);
                }
                return;
            }
            foreach (string item2 in pageNameList)
            {
                MovePageToBottom(item2);
            }
        }

        public static void MovePageAfterNode(TreeNode srcNode, TreeNode dstNode)
        {
            if (dstNode == null)
            {
                return;
            }
            string text = null;
            if (dstNode.Tag is HmiPageGroup)
            {
                TreeNode treeNode = FindLastPageNodeInGroupNode(dstNode) ?? FindPrevPageNodeFromNode(dstNode);
                if (treeNode != null)
                {
                    text = (treeNode.Tag as HmiPage).Name;
                }
            }
            else if (dstNode.Tag is HmiPage)
            {
                text = (dstNode.Tag as HmiPage).Name;
            }
            if (text != null)
            {
                MovePageAfterPage((srcNode.Tag as HmiPage).Name, text);
            }
        }

        public static void MovePageBeforeNode(TreeNode srcNode, TreeNode dstNode)
        {
            if (dstNode == null)
            {
                return;
            }
            string text = null;
            if (dstNode.Tag is HmiPageGroup)
            {
                TreeNode treeNode = FindFirstPageNodeInGroupNode(dstNode) ?? FindNextPageNodeFromNode(dstNode);
                if (treeNode != null)
                {
                    text = (treeNode.Tag as HmiPage).Name;
                }
            }
            else if (dstNode.Tag is HmiPage)
            {
                text = (dstNode.Tag as HmiPage).Name;
            }
            if (text != null)
            {
                MovePageBeforePage((srcNode.Tag as HmiPage).Name, text);
            }
        }

        public static void MovePageGroupToPageGroup(TreeNode srcNode, TreeNode dstNode)
        {
            if (dstNode == null || dstNode.Tag is not HmiPageGroup)
            {
                return;
            }
            List<string> pageNameList = new();
            FindAllPageInGroupNode(srcNode, ref pageNameList);
            if (pageNameList.Count <= 0)
            {
                return;
            }
            TreeNode treeNode = FindLastPageNodeInGroupNode(dstNode) ?? FindPrevPageNodeFromNode(dstNode);
            if (treeNode != null)
            {
                string name = (treeNode.Tag as HmiPage).Name;
                pageNameList.Reverse();
                {
                    foreach (string item in pageNameList)
                    {
                        if (!item.Equals(name))
                        {
                            MovePageAfterPage(item, name);
                            continue;
                        }
                        break;
                    }
                    return;
                }
            }
            pageNameList.Reverse();
            foreach (string item2 in pageNameList)
            {
                MovePageToTop(item2);
            }
        }

        public static void MovePageToPageGroup(TreeNode srcNode, TreeNode dstNode)
        {
            if (dstNode == null || dstNode.Tag is not HmiPageGroup)
            {
                return;
            }
            string name = (srcNode.Tag as HmiPage).Name;
            DataFile dataFile = DFM.Find(name);
            if (dataFile == null)
            {
                return;
            }
            TreeNode treeNode = FindLastPageNodeInGroupNode(dstNode);
            if (treeNode != null)
            {
                string name2 = (treeNode.Tag as HmiPage).Name;
                MovePageAfterPage(name, name2);
                return;
            }
            TreeNode treeNode2 = FindPrevPageNodeFromNode(dstNode);
            if (treeNode2 != null)
            {
                string name3 = (treeNode2.Tag as HmiPage).Name;
                MovePageAfterPage(name, name3);
            }
            else
            {
                MovePageToTop(name);
            }
        }

        public static void AddPageToPageGroup(TreeNode pageNode, TreeNode pageGroupNode)
        {
            if (pageNode == null || pageNode.Tag is not HmiPage || pageGroupNode == null || pageGroupNode.Tag is not HmiPageGroup)
            {
                return;
            }
            HmiPage hmiPage = pageNode.Tag as HmiPage;
            HmiPageGroup hmiPageGroup = pageGroupNode.Tag as HmiPageGroup;
            if (hmiPageGroup.Parent != null)
            {
                TreeNode treeNode = FindPrevPageNodeFromNode(pageNode);
                if (treeNode != null)
                {
                    int num = DFM.FindIndex((treeNode.Tag as HmiPage).Name);
                    if (num != -1)
                    {
                        DFM.Insert(num + 1, hmiPage.DataFile);
                    }
                    else
                    {
                        DFM.Append(hmiPage.DataFile);
                    }
                }
                else
                {
                    DFM.Prepend(hmiPage.DataFile);
                }
            }
            else
            {
                DFM.Append(hmiPage.DataFile);
            }
        }

        private static void FindAllPageInGroupNode(TreeNode pageGroupNode, ref List<TreeNode> pageNodeList)
        {
            if (pageGroupNode == null || pageGroupNode.Tag is not HmiPageGroup)
            {
                return;
            }
            foreach (TreeNode node in pageGroupNode.Nodes)
            {
                if (node.Tag is HmiPageGroup)
                {
                    FindAllPageInGroupNode(node, ref pageNodeList);
                }
                else if (node.Tag is HmiPage)
                {
                    pageNodeList.Add(node);
                }
            }
        }

        private static void FindAllPageInGroupNode(TreeNode pageGroupNode, ref List<string> pageNameList)
        {
            if (pageGroupNode == null || pageGroupNode.Tag is not HmiPageGroup)
            {
                return;
            }
            foreach (TreeNode node in pageGroupNode.Nodes)
            {
                if (node.Tag is HmiPageGroup)
                {
                    FindAllPageInGroupNode(node, ref pageNameList);
                }
                else if (node.Tag is HmiPage)
                {
                    pageNameList.Add((node.Tag as HmiPage).Name);
                }
            }
        }

        private static TreeNode FindFirstPageNodeInGroupNode(TreeNode pageGroupNode)
        {
            if (pageGroupNode != null && pageGroupNode.Tag is HmiPageGroup)
            {
                TreeNode firstNode = pageGroupNode.FirstNode;
                if (firstNode != null)
                {
                    if (firstNode.Tag is HmiPageGroup)
                    {
                        return FindFirstPageNodeInGroupNode(firstNode);
                    }
                    if (firstNode.Tag is HmiPage)
                    {
                        return firstNode;
                    }
                }
            }
            return null;
        }

        private static TreeNode FindLastPageNodeInGroupNode(TreeNode pageGroupNode)
        {
            if (pageGroupNode != null && pageGroupNode.Tag is HmiPageGroup)
            {
                TreeNode lastNode = pageGroupNode.LastNode;
                if (lastNode != null)
                {
                    if (lastNode.Tag is HmiPageGroup)
                    {
                        return FindLastPageNodeInGroupNode(lastNode);
                    }
                    if (lastNode.Tag is HmiPage)
                    {
                        return lastNode;
                    }
                }
            }
            return null;
        }

        private static TreeNode FindPrevPageNodeFromNode(TreeNode dstNode)
        {
            if (dstNode != null)
            {
                TreeNode prevNode = dstNode.PrevNode;
                if (prevNode == null)
                {
                    return FindPrevPageNodeFromNode(dstNode.Parent);
                }
                if (prevNode.Tag is HmiPageGroup)
                {
                    TreeNode treeNode = FindLastPageNodeInGroupNode(prevNode);
                    if (treeNode != null)
                    {
                        return treeNode;
                    }
                    return FindPrevPageNodeFromNode(prevNode);
                }
                if (prevNode.Tag is HmiPage)
                {
                    return prevNode;
                }
            }
            return null;
        }

        private static TreeNode FindNextPageNodeFromNode(TreeNode dstNode)
        {
            if (dstNode != null)
            {
                TreeNode nextNode = dstNode.NextNode;
                if (nextNode == null)
                {
                    return FindNextPageNodeFromNode(dstNode.Parent);
                }
                if (nextNode.Tag is HmiPageGroup)
                {
                    TreeNode treeNode = FindFirstPageNodeInGroupNode(nextNode);
                    if (treeNode != null)
                    {
                        return treeNode;
                    }
                    return FindNextPageNodeFromNode(nextNode);
                }
                if (nextNode.Tag is HmiPage)
                {
                    return nextNode;
                }
            }
            return null;
        }

        private static void MovePageToTop(string pageName)
        {
            DataFile dataFile = DFM.Find(pageName);
            if (dataFile != null)
            {
                DFM.Remove(dataFile);
                DFM.Prepend(dataFile);
            }
        }

        private static void MovePageToBottom(string pageName)
        {
            DataFile dataFile = DFM.Find(pageName);
            if (dataFile != null)
            {
                DFM.Remove(dataFile);
                DFM.Append(dataFile);
            }
        }

        private static void MovePageAfterPage(string srcPageName, string dstPageName)
        {
            if (!(srcPageName != dstPageName))
            {
                return;
            }
            DataFile dataFile = DFM.Find(srcPageName);
            if (dataFile != null && DFM.Exists(dstPageName))
            {
                dfs.Remove(dataFile);
                int num = DFM.FindIndex(dstPageName);
                if (num != -1)
                {
                    dfs.Insert(num + 1, dataFile);
                }
            }
        }

        private static void MovePageBeforePage(string srcPageName, string dstPageName)
        {
            if (!(srcPageName != dstPageName))
            {
                return;
            }
            DataFile dataFile = DFM.Find(srcPageName);
            if (dataFile != null && DFM.Exists(dstPageName))
            {
                dfs.Remove(dataFile);
                int num = DFM.FindIndex(dstPageName);
                if (num != -1)
                {
                    dfs.Insert(num, dataFile);
                }
            }
        }
    }

    public static DataSet systemControlMembersSetting = new();

    public static DataSet controlMembersSetting = new();

    public static List<string> dirtyPageTemp;

    public static bool varFileNeedReLoad = false;

    public static int FViewEditorHandle = 0;

    public static IOForm ioForm = new();

    public static ScriptUnit scriptUnitForm;

    public static Dictionary<string, string> ProCmdDict;

    public static MsgForm OutputMessage = new();

    public static string path = "";

    public static string ProjectPath = "";

    public static bool OnMoveMode = false;

    public static HMIProjectFile dhp = new();

    public static List<DataFile> dfs = new();

    public static Process HMIRunProcess;

    public static List<CShape> CLS = new();

    public static CIOItem ioitemroot = new();

    public static XmlDocument xmldoc = new();

    public static ChildForm childform = new();

    public static MDIParent1 mdiparent;

    public static bool NotEditValue = false;

    public static string ProjectFile = "";

    public static int CompareByLayer(CShape x, CShape y)
    {
        if (x == null)
        {
            if (y == null)
                return 0;

            return -1;
        }

        if (y == null)
            return 1;

        if (x.Layer > y.Layer)
            return 1;

        if (x.Layer == y.Layer)
            return 0;

        return -1;
    }

    public static void UnSelectAllShapesIfNotSender(object sender)
    {
       var mdiChildren = mdiparent.MdiChildren;
        foreach (var form in mdiChildren)
        {
            if (form is ChildForm && ((ChildForm)form).theglobal.uc2 != (UserShapeEditControl)sender)
            {
                ((ChildForm)form).theglobal.SelectedShapeList.Clear();
                ((ChildForm)form).theglobal.uc2.RefreshGraphics();
            }
        }
        mdiparent.objView_Page.FreshSelect(childform.theglobal);
    }
}
