using CommonSnappableTypes;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using HMIEditEnvironment.CodeCompletion;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Actions;
using ICSharpCode.TextEditor.Document;
using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace HMIEditEnvironment;

public partial class ScriptUnit : XtraForm
{
    private TempTag currentTempTag;

    private TreeNode rootNode;

    private TreeNode iot;

    public static TreeNode ClickNode;

    public static CShape ShapeCtrl;

    private string CtrlName;

    private string EventName;

    private ScriptUnitAddUserDefineEvent eventScriptFrm;

    private Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> objectitems;

    private Dictionary<string, List<string>> apiitems;

    private readonly List<TreeNode> dirtyNodes = new();

    private bool usereditit = true;

    private static readonly Font RegularFont = new("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point);

    private static readonly Font BoldFont = new("Tahoma", 9f, FontStyle.Bold, GraphicsUnit.Point);

    private readonly ScriptUnitFindAndReplaceFrm FindAndReplaceFrm = new();

    private readonly ScriptUnitMoveLineFrm MoveLineFrm = new();

    private TextEditorControl ActiveTextEditor => textEditorControl1;

    public ScriptUnit()
    {
        InitializeComponent();
        HighlightingManager.Manager.AddSyntaxModeFileProvider(new FileSyntaxModeProvider(AppDomain.CurrentDomain.BaseDirectory + "Config"));
        textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("VBSCRIPT");
        CodeCompletionKeyHandler.Attach(this, textEditorControl1);
        Init();
    }

    private void DeleteEventNode(TreeNode node)
    {
        if (node.Tag != null && ((TempTag)node.Tag).obj is CShape && MessageBox.Show("您确认将要删除当前事件脚本?", "删除警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            ((TempTag)node.Tag).isDeleted = true;
            node.Remove();
        }
    }

    private bool CheckAllScript()
    {
        ClassForScript.old = true;
        objectitems = CheckScript.MakeObjectDict();
        apiitems = CheckScript.MakeSystemApiDict();
        foreach (TreeNode dirtyNode in dirtyNodes)
        {
            if (!iterativeCheckScript(dirtyNode))
            {
                return false;
            }
        }
        return true;
    }

    private bool iterativeCheckScript(TreeNode tn)
    {
        foreach (TreeNode node in tn.Nodes)
        {
            if (!iterativeCheckScript(node))
            {
                return false;
            }
        }
        if (tn.Tag == null || ((TempTag)tn.Tag).temp == null)
        {
            return true;
        }
        string text = "";
        string script = (string)((TempTag)tn.Tag).temp;
        while (tn != null)
        {
            text = tn.Text + "." + text;
            tn = tn.Parent;
        }
        text = text.Substring(0, text.Length - 1);
        if (!checkScript(text, script))
        {
            return false;
        }
        return true;
    }

    private bool checkScript(string scriptName, string script)
    {
        ScriptEngine scriptEngine = new(ScriptLanguage.VBscript);
        try
        {
            scriptEngine.AddCode("If false Then \n\r" + script + "\n\r End If");
        }
        catch (Exception ex)
        {
            MessageBox.Show("脚本:" + scriptName + Environment.NewLine + "行" + (scriptEngine.Error.Line - 2) + ":" + ex.Message.Replace("_____", "."), "错误");
            return false;
        }
        Dictionary<int, string> dictionary = CheckScript.CheckScriptItems(script, objectitems, apiitems);
        if (dictionary.Count != 0)
        {
            using Dictionary<int, string>.KeyCollection.Enumerator enumerator = dictionary.Keys.GetEnumerator();
            if (enumerator.MoveNext())
            {
                int current = enumerator.Current;
                MessageBox.Show("脚本:" + scriptName + Environment.NewLine + "行" + (current - 1) + ":" + dictionary[current].Trim(), "错误");
                return false;
            }
        }
        return true;
    }

    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        AfterSelectHandler(e.Node);
    }

    private void AfterSelectHandler(TreeNode selectedNode)
    {
        if (!dirtyNodes.Contains(selectedNode))
        {
            dirtyNodes.Add(selectedNode);
        }
        try
        {
            Text = "脚本管理>" + selectedNode.Text;
        }
        catch
        {
        }
        if (currentTempTag != null)
        {
            currentTempTag.temp = textEditorControl1.Text;
        }
        if (selectedNode.Tag != null)
        {
            currentTempTag = (TempTag)selectedNode.Tag;
        }
        else
        {
            currentTempTag = null;
        }
        try
        {
            if (selectedNode.Tag == null || selectedNode.Name == "CreateMailRule.png")
            {
                textEditorControl1.Text = "";
                textEditorControl1.Refresh();
                textEditorControl1.Enabled = false;
                panelControl1.Visible = false;
                splitterControl2.Visible = false;
                return;
            }
            textEditorControl1.Enabled = true;
            if ((string)((TempTag)selectedNode.Tag).temp == null)
            {
                if ((string)((TempTag)selectedNode.Tag).value != null)
                {
                    ((TempTag)selectedNode.Tag).temp = (string)((TempTag)selectedNode.Tag).value;
                }
                else
                {
                    ((TempTag)selectedNode.Tag).temp = "";
                }
            }
            textEditorControl1.Text = (string)((TempTag)selectedNode.Tag).temp;
            textEditorControl1.Refresh();
            panelControl1.Visible = false;
            splitterControl2.Visible = false;
            labelControl1.Visible = false;
            spinEdit1.Visible = false;
            checkEdit1.Visible = false;
            if (((TempTag)selectedNode.Tag).t != null)
            {
                panelControl1.Visible = true;
                splitterControl2.Visible = true;
                spinEdit1.EditValue = ((TempTag)selectedNode.Tag).t;
                labelControl1.Visible = true;
                spinEdit1.Visible = true;
            }
            if (((TempTag)selectedNode.Tag).WhenOutThenDo != null)
            {
                panelControl1.Visible = true;
                splitterControl2.Visible = true;
                checkEdit1.Visible = true;
                usereditit = false;
                checkEdit1.Checked = ((ObjectBoolean)((TempTag)selectedNode.Tag).WhenOutThenDo).value;
                usereditit = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddEventScriptTreeNode()
    {
        TreeNode treeNode;
        if (eventScriptFrm.ExistedCtrlEventDic.ContainsKey(CtrlName))
        {
            treeNode = GetCtrlTreeNode();
            treeNode.Nodes.Add(eventScriptFrm.EventNode);
        }
        else
        {
            treeNode = ClickNode.Nodes.Add("shape.png", CtrlName);
            treeNode.Nodes.Add(eventScriptFrm.EventNode);
        }
        treeNode.Expand();
        treeView1.SelectedNode = treeNode.LastNode;
    }

    private TreeNode GetCtrlTreeNode()
    {
        TreeNode result = new();
        if (ClickNode.Parent.Parent.Text == "页面相关")
        {
            result = ClickNode;
        }
        else
        {
            foreach (TreeNode node in ClickNode.Nodes)
            {
                if (node.Text == CtrlName)
                {
                    result = node;
                }
            }
        }
        return result;
    }

    internal void ShowEventScriptForm(string pageName, CShape csSel)
    {
        ClickNode = GetPageNode(pageName);
        eventScriptFrm = new ScriptUnitAddUserDefineEvent
        {
            Owner = this
        };
        eventScriptFrm.ShowDialog();
    }

    internal void ShowEventScriptForm(DataFile pageData, CShape shapeCtrl)
    {
        TreeNode clickNode = null;
        ShapeCtrl = shapeCtrl;
        string pageName = pageData.pageName;
        string name = shapeCtrl.Name;
        bool flag = false;
        TreeNode pageNode = GetPageNode(pageName);
        pageNode.Expand();
        foreach (TreeNode node in pageNode.Nodes)
        {
            if (!(node.Text != name))
            {
                flag = true;
                clickNode = node;
            }
        }
        if (flag)
        {
            ClickNode = clickNode;
        }
        else
        {
            ClickNode = pageNode;
        }
        eventScriptFrm = new ScriptUnitAddUserDefineEvent
        {
            Owner = this
        };
        DialogResult dialogResult = eventScriptFrm.ShowDialog();
        if (dialogResult == DialogResult.OK)
        {
            CtrlName = eventScriptFrm.CtrlName;
            EventName = eventScriptFrm.EventName;
            AddEventScriptTreeNode();
        }
    }

    private TreeNode GetPageNode(string pageName)
    {
        TreeNode result = null;
        if (treeView1.Nodes.Count > 0)
        {
            treeView1.Nodes[0].Expand();
            foreach (TreeNode node in treeView1.Nodes[0].Nodes)
            {
                if (!(node.Text == "页面相关"))
                {
                    continue;
                }
                node.Expand();
                foreach (TreeNode node2 in node.Nodes)
                {
                    if (node2.Text == pageName)
                    {
                        result = node2;
                    }
                }
            }
        }
        return result;
    }

    private void makeRedoAndUndo()
    {
        List<CShape> list = new();
        List<CShape> list2 = new();
        Form[] mdiChildren = CEditEnvironmentGlobal.mdiparent.MdiChildren;
        foreach (Form form in mdiChildren)
        {
            if (form is not ChildForm)
            {
                continue;
            }
            ChildForm childForm = (ChildForm)form;
            foreach (object key in TempTag.newOldDict.Keys)
            {
                if (key is CShape && childForm.theglobal.g_ListAllShowCShape.Contains((CShape)key))
                {
                    CShape item = ((CShape)TempTag.newOldDict[key]).Copy();
                    list2.Add(item);
                    list.Add((CShape)key);
                }
            }
            childForm.theglobal.ForUndo(list, list2);
        }
        TempTag.newOldDict.Clear();
    }

    private int ShapeNameCompare(CShape x, CShape y)
    {
        return string.Compare(x.Name, y.Name);
    }

    private int PageNameCompare(DataFile x, DataFile y)
    {
        return string.Compare(x.pageName, y.pageName);
    }

    public void Init()
    {
        Init(privateMode: false);
    }

    private TreeNode AddNode(string strPng, string strTitle, string strScript, bool bBold, ref TreeNode tnParent, object obj)
    {
        TreeNode treeNode = tnParent.Nodes.Add(strPng, strTitle, strPng);
        treeNode.Tag = new TempTag(null, strScript, obj);
        if (bBold)
        {
            treeNode.NodeFont = BoldFont;
        }
        return treeNode;
    }

    public bool NodeProjectExecute()
    {
        try
        {
            rootNode = treeView1.Nodes.Add("project.png", "工程相关");
            return true;
        }
        catch
        {
            MessageBox.Show("添加工程相关父节点出现异常！", "提示");
            return false;
        }
    }

    public bool NodeWholeSituation()
    {
        try
        {
            TreeNode treeNode = rootNode.Nodes.Add("l.png", "全局脚本");
            treeNode.Tag = new TempTag(null, CEditEnvironmentGlobal.dhp.SrcGlobalLogic, CEditEnvironmentGlobal.dhp);
            rootNode.Expand();
            if (CEditEnvironmentGlobal.dhp.SrcGlobalLogic != string.Empty)
            {
                treeNode.NodeFont = BoldFont;
            }
            return true;
        }
        catch
        {
            MessageBox.Show("添加全局脚本出现异常！", "提示");
            return false;
        }
    }

    public bool NodeDevOnLine()
    {
        try
        {
            TreeNode treeNode = rootNode.Nodes.Add("l.png", "设备上下线事件脚本");
            treeNode.Tag = new TempTag(null, CEditEnvironmentGlobal.dhp.devjiaoben, CEditEnvironmentGlobal.dhp);
            if (CEditEnvironmentGlobal.dhp.devjiaoben != string.Empty)
            {
                treeNode.NodeFont = BoldFont;
            }
            return true;
        }
        catch
        {
            MessageBox.Show("设备上下线事件脚本出现异常！", "提示");
            return false;
        }
    }

    public bool NodeProjectStart()
    {
        try
        {
            TreeNode treeNode = rootNode.Nodes.Add("l.png", "程序启动脚本");
            treeNode.Tag = new TempTag(null, CEditEnvironmentGlobal.dhp.cxdzshijiaoben[0], CEditEnvironmentGlobal.dhp);
            if (CEditEnvironmentGlobal.dhp.cxdzshijiaoben[0] != "")
            {
                treeNode.NodeFont = BoldFont;
            }
            return true;
        }
        catch
        {
            MessageBox.Show("程序启动脚本出现异常！", "提示");
            return false;
        }
    }

    public bool NodeProjectRun()
    {
        try
        {
            TreeNode treeNode = rootNode.Nodes.Add("l.png", "程序运行脚本");
            treeNode.Tag = new TempTag(null, CEditEnvironmentGlobal.dhp.cxdzshijiaoben[1], CEditEnvironmentGlobal.dhp);
            if (CEditEnvironmentGlobal.dhp.cxdzshijiaoben[1] != string.Empty)
            {
                treeNode.NodeFont = BoldFont;
            }
            ((TempTag)treeNode.Tag).t = CEditEnvironmentGlobal.dhp.LogicTime;
            return true;
        }
        catch
        {
            MessageBox.Show("程序启动脚本出现异常！", "提示");
            return false;
        }
    }

    public bool NodeProjectClose()
    {
        try
        {
            TreeNode treeNode = rootNode.Nodes.Add("l.png", "程序关闭脚本");
            treeNode.Tag = new TempTag(null, CEditEnvironmentGlobal.dhp.cxdzshijiaoben[2], CEditEnvironmentGlobal.dhp);
            if (CEditEnvironmentGlobal.dhp.cxdzshijiaoben[2] != string.Empty)
            {
                treeNode.NodeFont = BoldFont;
            }
            return true;
        }
        catch
        {
            MessageBox.Show("程序关闭脚本出现异常！", "提示");
            return false;
        }
    }

    public bool NodeVarAlarm()
    {
        try
        {
            iot = rootNode.Nodes.Add("io.png", "变量报警脚本");
            return true;
        }
        catch
        {
            MessageBox.Show("变量报警脚本添加出现异常！", "提示");
            return false;
        }
    }

    public bool NodePageRelate()
    {
        try
        {
            TreeNode treeNode = rootNode.Nodes.Add("page.png", "页面相关");
            List<DataFile> list = new(CEditEnvironmentGlobal.dfs);
            list.Sort(PageNameCompare);
            treeNode.Expand();
            foreach (DataFile item in list)
            {
                TreeNode tnParent = treeNode.Nodes.Add("page.png", item.pageName);
                TreeNode treeNode2 = AddNode("l.png", "页面显示脚本", item.pagedzshijiaoben[0], item.pagedzshijiaoben[0] != string.Empty, ref tnParent, item);
                treeNode2 = AddNode("l.png", "页面运行脚本", item.pagedzshijiaoben[1], item.pagedzshijiaoben[1] != string.Empty, ref tnParent, item);
                ((TempTag)treeNode2.Tag).t = item.LogicTime;
                treeNode2 = AddNode("l.png", "页面隐藏脚本", item.pagedzshijiaoben[2], item.pagedzshijiaoben[2] != string.Empty, ref tnParent, item);
                List<CShape> list2 = new(item.ListAllShowCShape);
                list2.Sort(ShapeNameCompare);
                foreach (CShape item2 in list2)
                {
                    TreeNode tnParent2 = tnParent.Nodes.Add("shape.png", item2.Name);
                    int num = 0;
                    if (item2 is not CControl)
                    {
                        object whenOutThenDo = new ObjectBoolean(item2.sbsjWhenOutThenDo);
                        if (item2.UserLogic[1] != null)
                        {
                            treeNode2 = AddNode("l.png", "鼠标左键按下", item2.UserLogic[1], item2.UserLogic[1] != "", ref tnParent2, item2);
                            ((TempTag)treeNode2.Tag).WhenOutThenDo = whenOutThenDo;
                            whenOutThenDo = ((TempTag)treeNode2.Tag).WhenOutThenDo;
                            num++;
                        }
                        if (item2.UserLogic[6] != null)
                        {
                            treeNode2 = AddNode("l.png", "鼠标左键抬起", item2.UserLogic[6], item2.UserLogic[6] != "", ref tnParent2, item2);
                            ((TempTag)treeNode2.Tag).WhenOutThenDo = whenOutThenDo;
                            num++;
                        }
                        if (item2.UserLogic[2] != null)
                        {
                            treeNode2 = AddNode("l.png", "鼠标左键双击", item2.UserLogic[2], item2.UserLogic[2] != "", ref tnParent2, item2);
                            num++;
                        }
                        if (item2.UserLogic[3] != null)
                        {
                            treeNode2 = AddNode("l.png", "鼠标右键按下", item2.UserLogic[3], item2.UserLogic[3] != "", ref tnParent2, item2);
                            ((TempTag)treeNode2.Tag).WhenOutThenDo = whenOutThenDo;
                            num++;
                        }
                        if (item2.UserLogic[7] != null)
                        {
                            treeNode2 = AddNode("l.png", "鼠标右键抬起", item2.UserLogic[7], item2.UserLogic[7] != "", ref tnParent2, item2);
                            ((TempTag)treeNode2.Tag).WhenOutThenDo = whenOutThenDo;
                            num++;
                        }
                        if (item2.UserLogic[4] != null)
                        {
                            treeNode2 = AddNode("l.png", "鼠标右键双击", item2.UserLogic[4], item2.UserLogic[4] != "", ref tnParent2, item2);
                            num++;
                        }
                        if (item2.UserLogic[5] != null)
                        {
                            treeNode2 = AddNode("l.png", "鼠标滚轮滚动", item2.UserLogic[5], item2.UserLogic[5] != "", ref tnParent2, item2);
                            num++;
                        }
                        if (item2.DBOKScriptUser != null && item2.DBOKScriptUser != "")
                        {
                            treeNode2 = AddNode("l.png", "数据库操作成功", item2.DBOKScriptUser, item2.DBOKScriptUser != "", ref tnParent2, item2);
                            num++;
                        }
                        if (item2.DBErrScriptUser != null && item2.DBOKScriptUser != "")
                        {
                            treeNode2 = AddNode("l.png", "数据库操作失败", item2.DBErrScriptUser, item2.DBErrScriptUser != "", ref tnParent2, item2);
                            num++;
                        }
                    }
                    else
                    {
                        Type type = ((CControl)item2)._c.GetType();
                        if (((CControl)item2)._c is AxHost || ((CControl)item2)._c is IDCCEControl)
                        {
                            EventInfo[] array = type.GetEvents();
                            if (item2.ysdzshijianmingcheng == null)
                            {
                                item2.ysdzshijianmingcheng = new string[array.Length];
                                item2.ysdzshijianbiaodashi = new string[array.Length];
                                item2.ysdzshijianLogic = new string[array.Length];
                                for (int i = 0; i < array.Length; i++)
                                {
                                    if (item2.ysdzshijianbiaodashi[i] != null)
                                    {
                                        treeNode2 = tnParent2.Nodes.Add("l.png", array[i].Name.ToString());
                                        treeNode2.Tag = new TempTag(null, item2.ysdzshijianbiaodashi[i], item2);
                                        item2.ysdzshijianmingcheng[i] = array[i].ToString();
                                        if (item2.ysdzshijianbiaodashi[i] != "")
                                        {
                                            treeNode2.NodeFont = BoldFont;
                                        }
                                        num++;
                                    }
                                }
                            }
                            else if (item2.ysdzshijianmingcheng.Length != array.Length)
                            {
                                string[] ysdzshijianmingcheng = item2.ysdzshijianmingcheng;
                                string[] ysdzshijianbiaodashi = item2.ysdzshijianbiaodashi;
                                string[] ysdzshijianLogic = item2.ysdzshijianLogic;
                                item2.ysdzshijianmingcheng = new string[array.Length];
                                item2.ysdzshijianbiaodashi = new string[array.Length];
                                item2.ysdzshijianLogic = new string[array.Length];
                                for (int j = 0; j < array.Length; j++)
                                {
                                    if (ysdzshijianmingcheng.Length == 0)
                                    {
                                        for (int k = 0; k < array.Length; k++)
                                        {
                                            item2.ysdzshijianmingcheng[k] = array[k].ToString();
                                            item2.ysdzshijianbiaodashi[k] = null;
                                            item2.ysdzshijianLogic[j] = null;
                                        }
                                    }
                                    else
                                    {
                                        for (int l = 0; l < ysdzshijianmingcheng.Length; l++)
                                        {
                                            if (array[j].ToString() == ysdzshijianmingcheng[l])
                                            {
                                                item2.ysdzshijianbiaodashi[j] = ysdzshijianbiaodashi[l];
                                                item2.ysdzshijianLogic[j] = ysdzshijianLogic[l];
                                            }
                                        }
                                    }
                                    if (item2.ysdzshijianbiaodashi[j] != null)
                                    {
                                        treeNode2 = tnParent2.Nodes.Add("l.png", array[j].Name.ToString());
                                        treeNode2.Tag = new TempTag(null, item2.ysdzshijianbiaodashi[j], item2);
                                        item2.ysdzshijianmingcheng[j] = array[j].ToString();
                                        if (item2.ysdzshijianbiaodashi[j] != "")
                                        {
                                            treeNode2.NodeFont = BoldFont;
                                        }
                                        num++;
                                    }
                                }
                            }
                            else
                            {
                                for (int m = 0; m < array.Length; m++)
                                {
                                    if (item2.ysdzshijianbiaodashi[m] != null)
                                    {
                                        treeNode2 = tnParent2.Nodes.Add("l.png", array[m].Name.ToString());
                                        treeNode2.Tag = new TempTag(null, item2.ysdzshijianbiaodashi[m], item2);
                                        if (item2.ysdzshijianbiaodashi[m] != "")
                                        {
                                            treeNode2.NodeFont = BoldFont;
                                        }
                                        num++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            EventInfo[] array2 = type.GetEvents();
                            if (item2.ysdzshijianmingcheng == null || item2.ysdzshijianmingcheng.Length != array2.Length)
                            {
                                item2.ysdzshijianmingcheng = new string[array2.Length];
                                item2.ysdzshijianbiaodashi = new string[array2.Length];
                                item2.ysdzshijianLogic = new string[array2.Length];
                                for (int n = 0; n < array2.Length; n++)
                                {
                                    if (item2.ysdzshijianbiaodashi[n] != null)
                                    {
                                        treeNode2 = tnParent2.Nodes.Add("l.png", array2[n].Name.ToString());
                                        treeNode2.Tag = new TempTag(null, item2.ysdzshijianbiaodashi[n], item2);
                                        item2.ysdzshijianmingcheng[n] = array2[n].ToString();
                                        if (item2.ysdzshijianbiaodashi[n] != "")
                                        {
                                            treeNode2.NodeFont = BoldFont;
                                        }
                                        num++;
                                    }
                                }
                            }
                            else
                            {
                                for (int num2 = 0; num2 < array2.Length; num2++)
                                {
                                    item2.ysdzshijianmingcheng[num2] = array2[num2].ToString();
                                    if (item2.ysdzshijianbiaodashi[num2] != null)
                                    {
                                        treeNode2 = tnParent2.Nodes.Add("l.png", array2[num2].Name.ToString());
                                        treeNode2.Tag = new TempTag(null, item2.ysdzshijianbiaodashi[num2], item2);
                                        if (item2.ysdzshijianbiaodashi[num2] != "")
                                        {
                                            treeNode2.NodeFont = BoldFont;
                                        }
                                        num++;
                                    }
                                }
                            }
                        }
                    }
                    if (num == 0)
                    {
                        tnParent2.Remove();
                    }
                }
            }
            return true;
        }
        catch
        {
            MessageBox.Show("页面添加关联脚本出现异常！", "提示");
            return false;
        }
    }

    public bool NodeVariable()
    {
        try
        {
            rootNode = treeView2.Nodes.Add("iog", "变量");
            iot = rootNode.Nodes.Add("0", "设备变量");
            TreeNode treeNode2 = (TreeNode)CEditEnvironmentGlobal.dhp.ProjectIOTreeRoot.Clone();
            SetProjectIONodes(treeNode2);
            rootNode.Nodes.Add(treeNode2);
            return true;
        }
        catch
        {
            MessageBox.Show("添加变量脚本出现异常！", "提示");
            return false;
        }
    }

    public bool NodeAPI()
    {
        try
        {
            rootNode = treeView2.Nodes.Add("api.png", "系统API");
            TreeNode treeNode = rootNode.Nodes.Add("api", "KeyValue");
            treeNode.Tag = "键盘事件中获取按下键代码 System.KeyValue";
            treeNode = rootNode.Nodes.Add("api", "Exec");
            treeNode.Tag = "启动外部程序 System.Exec String,String (String:启动外部程序路径,String:启动外部程序参数)";
            treeNode = rootNode.Nodes.Add("api", "ExecWait");
            treeNode.Tag = "启动外部程序并等待结束 System.ExecWait String,String (String:启动外部程序路径,String:启动外部程序参数)";
            treeNode = rootNode.Nodes.Add("api", "SetPageVisible");
            treeNode.Tag = "设置页面的显示隐藏 System.SetPageVisible String,Boolean (String:页面脚本名称,Boolean:是否可见)";
            treeNode = rootNode.Nodes.Add("api", "FullScreen");
            treeNode.Tag = "全屏显示并返回布尔值 System.FullScreen";
            treeNode = rootNode.Nodes.Add("api", "Exit");
            treeNode.Tag = "退出运行环境 System.Exit";
            treeNode = rootNode.Nodes.Add("api", "SetDBConn");
            treeNode.Tag = "设置数据库连接字符串 System.SetDBConn String";
            treeNode = rootNode.Nodes.Add("api", "ExecuteSql");
            treeNode.Tag = "执行SQL指令 System.ExecuteSql String (String:要被执行的SQL语句)";
            treeNode = rootNode.Nodes.Add("api", "ExecuteSqlReturnArray");
            treeNode.Tag = "执行SQL指令并返回数组 System.ExecuteSqlReturnArray String (String:要被执行的SQL语句)";
            treeNode = rootNode.Nodes.Add("api", "DBSelect");
            treeNode.Tag = "读取数据表 System.DBSelect String (String:读取数据使用的Select语句字符串)";
            treeNode = rootNode.Nodes.Add("api", "GetAppFullDir");
            treeNode.Tag = "获取当前程序集路径 System. GetAppFullDir";
            return true;
        }
        catch
        {
            MessageBox.Show("添加系统API脚本节点出现异常！", "提示");
            return false;
        }
    }

    public bool NodeAttribute()
    {
        try
        {
            rootNode = treeView2.Nodes.Add("project.png", "工程属性相关");
            List<DataFile> list = new(CEditEnvironmentGlobal.dfs);
            foreach (DataFile item in list)
            {
                TreeNode treeNode = rootNode.Nodes.Add("page.png", item.pageName + "(" + item.name + ")");
                treeNode.Name = item.name;
                List<CShape> list2 = new(item.ListAllShowCShape);
                list2.Sort(ShapeNameCompare);
                foreach (CShape item2 in list2)
                {
                    TreeNode treeNode2 = treeNode.Nodes.Add("shape.png", item2.Name);
                    if (item2 is CControl)
                    {
                        object c = (item2 as CControl)._c;
                        PropertyInfo[] properties = c.GetType().GetProperties();
                        CScriptPropertyShow cScriptPropertyShow = new();
                        cScriptPropertyShow.AddControlPropertyShow();
                        PropertyInfo[] array = properties;
                        foreach (PropertyInfo propertyInfo in array)
                        {
                            try
                            {
                                if (!cScriptPropertyShow.ltProperty.Contains(propertyInfo.Name))
                                {
                                    if (c is AxHost)
                                    {
                                        treeNode2.Nodes.Add("property.png", propertyInfo.Name);
                                    }
                                    else if (!Attribute.IsDefined(propertyInfo, typeof(DHMIHidePropertyAttribute)))
                                    {
                                        treeNode2.Nodes.Add("property.png", propertyInfo.Name);
                                    }
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        continue;
                    }
                    Type type = item2.GetType();
                    PropertyInfo[] properties2 = type.GetProperties();
                    CScriptPropertyShow cScriptPropertyShow2 = new();
                    cScriptPropertyShow2.AddShapePropertyShow();
                    PropertyInfo[] array2 = properties2;
                    foreach (PropertyInfo propertyInfo2 in array2)
                    {
                        try
                        {
                            if (!Attribute.IsDefined(propertyInfo2, typeof(DHMIHidePropertyAttribute)) && !cScriptPropertyShow2.ltProperty.Contains(propertyInfo2.Name))
                            {
                                treeNode2.Nodes.Add("property.png", propertyInfo2.Name);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            return true;
        }
        catch
        {
            MessageBox.Show("添加工程属性节点出现异常！", "提示");
            return false;
        }
    }

    public bool NodeFunction()
    {
        try
        {
            rootNode = treeView2.Nodes.Add("project.png", "工程相关方法");
            List<DataFile> list = new(CEditEnvironmentGlobal.dfs);
            foreach (DataFile item in list)
            {
                TreeNode treeNode = rootNode.Nodes.Add("page.png", item.pageName + "(" + item.name + ")");
                treeNode.Name = item.name;
                List<CShape> list2 = new(item.ListAllShowCShape);
                list2.Sort(ShapeNameCompare);
                foreach (CShape item2 in list2)
                {
                    TreeNode treeNode2 = treeNode.Nodes.Add("shape.png", item2.Name);
                    if (item2 is CControl)
                    {
                        object c = (item2 as CControl)._c;
                        MethodInfo[] methods = c.GetType().GetMethods();
                        CScriptFuncShow cScriptFuncShow = new();
                        cScriptFuncShow.AddControlFuncShow();
                        MethodInfo[] array = methods;
                        foreach (MethodInfo methodInfo in array)
                        {
                            try
                            {
                                int num = methodInfo.Name.IndexOf('_');
                                switch (methodInfo.Name.Substring(0, num + 1))
                                {
                                    case "remove_":
                                        continue;
                                    case "set_":
                                        continue;
                                    case "get_":
                                        continue;
                                }
                                if (!cScriptFuncShow.ltFunc.Contains(methodInfo.Name))
                                {
                                    if (c is AxHost)
                                    {
                                        treeNode2.Nodes.Add("property.png", methodInfo.Name);
                                    }
                                    else if (!Attribute.IsDefined(methodInfo, typeof(DHMIHidePropertyAttribute)))
                                    {
                                        treeNode2.Nodes.Add("property.png", methodInfo.Name);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                        continue;
                    }
                    Type type = item2.GetType();
                    MethodInfo[] methods2 = type.GetMethods();
                    CScriptFuncShow cScriptFuncShow2 = new();
                    cScriptFuncShow2.AddShapeFuncShow();
                    MethodInfo[] array2 = methods2;
                    foreach (MethodInfo methodInfo2 in array2)
                    {
                        try
                        {
                            if (Attribute.IsDefined(methodInfo2, typeof(DHMIHidePropertyAttribute)))
                            {
                                continue;
                            }
                            int num2 = methodInfo2.Name.IndexOf('_');
                            switch (methodInfo2.Name.Substring(0, num2 + 1))
                            {
                                case "remove_":
                                    continue;
                                case "set_":
                                    continue;
                                case "get_":
                                    continue;
                            }
                            if (!cScriptFuncShow2.ltFunc.Contains(methodInfo2.Name))
                            {
                                treeNode2.Nodes.Add("property.png", methodInfo2.Name);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            return true;
        }
        catch
        {
            MessageBox.Show("添加工程方法节点出现异常！", "提示");
            return false;
        }
    }

    public bool NodeAddInit()
    {
        try
        {
            treeView1.Nodes.Clear();
            treeView2.Nodes.Clear();
            textEditorControl1.Text = "";
            textEditorControl1.Refresh();
            dirtyNodes.Clear();
            currentTempTag = null;
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void Init(bool privateMode)
    {
        if ((!base.Visible || privateMode) && NodeAddInit() && NodeProjectExecute() && NodeWholeSituation() && NodeDevOnLine() && NodeProjectStart() && NodeProjectRun() && NodeProjectClose() && NodeVarAlarm() && NodePageRelate() && NodeVariable() && NodeAPI() && NodeAttribute())
        {
            NodeFunction();
        }
    }

    private void SetProjectIONodes(TreeNode tn)
    {
        tn.ImageKey = "io.png";
        foreach (TreeNode node in tn.Nodes)
        {
            SetProjectIONodes(node);
        }
        foreach (ProjectIO projectIO in CEditEnvironmentGlobal.dhp.ProjectIOs)
        {
            if (!(projectIO.GroupName != tn.Text))
            {
                tn.Nodes.Add("io", projectIO.name, "io.png");
            }
        }
    }

    public void SelectScript(string scpritName)
    {
        base.TopMost = true;
        base.TopMost = false;
        string[] array = scpritName.Split('>');
        TreeNode selectedNode = new();
        TreeNodeCollection nodes = treeView1.Nodes;
        int num = 0;
        while (true)
        {
            if (num < array.Length)
            {
                foreach (TreeNode item in nodes)
                {
                    if (!(item.Text == array[num]))
                    {
                        continue;
                    }
                    nodes = item.Nodes;
                    selectedNode = item;
                    goto IL_0091;
                }
                break;
            }
            treeView1.SelectedNode = selectedNode;
            return;
        IL_0091:
            num++;
        }
        Console.WriteLine("ERR:用户所申请的脚本路径没找到." + Environment.NewLine + "MSG:" + scpritName);
    }

    private bool Save(TreeNode tn)
    {
        if (tn.Text == "全局脚本")
        {
            CEditEnvironmentGlobal.dhp.SrcGlobalLogic = (string)((TempTag)tn.Tag).temp;
        }
        else if (tn.Text == "设备上下线事件脚本")
        {
            CEditEnvironmentGlobal.dhp.devjiaoben = (string)((TempTag)tn.Tag).temp;
        }
        else if (tn.Text == "程序启动脚本")
        {
            CEditEnvironmentGlobal.dhp.cxdzshijiaoben[0] = (string)((TempTag)tn.Tag).temp;
        }
        else if (tn.Text == "程序运行脚本")
        {
            CEditEnvironmentGlobal.dhp.LogicTime = Convert.ToInt32(((TempTag)tn.Tag).t);
            CEditEnvironmentGlobal.dhp.cxdzshijiaoben[1] = (string)((TempTag)tn.Tag).temp;
        }
        else if (tn.Text == "程序关闭脚本")
        {
            CEditEnvironmentGlobal.dhp.cxdzshijiaoben[2] = (string)((TempTag)tn.Tag).temp;
        }
        else if (tn.Text == "页面显示脚本")
        {
            DataFile dataFile = (DataFile)((TempTag)tn.Tag).obj;
            dataFile.pagedzshijiaoben[0] = (string)((TempTag)tn.Tag).temp;
            CEditEnvironmentGlobal.dhp.dirtyPageAdd(dataFile.name);
        }
        else if (tn.Text == "页面运行脚本")
        {
            DataFile dataFile2 = (DataFile)((TempTag)tn.Tag).obj;
            dataFile2.pagedzshijiaoben[1] = (string)((TempTag)tn.Tag).temp;
            dataFile2.LogicTime = Convert.ToInt32(((TempTag)tn.Tag).t);
            CEditEnvironmentGlobal.dhp.dirtyPageAdd(dataFile2.name);
        }
        else if (tn.Text == "页面隐藏脚本")
        {
            DataFile dataFile3 = (DataFile)((TempTag)tn.Tag).obj;
            dataFile3.pagedzshijiaoben[2] = (string)((TempTag)tn.Tag).temp;
            CEditEnvironmentGlobal.dhp.dirtyPageAdd(dataFile3.name);
        }
        else if (tn.Text == "鼠标左键按下")
        {
            if (((TempTag)tn.Tag).isDeleted)
            {
                CShape cShape = (CShape)((TempTag)tn.Tag).obj;
                cShape.UserLogic[1] = null;
                return true;
            }
            CShape cShape2 = (CShape)((TempTag)tn.Tag).obj;
            CShape value = cShape2.Copy();
            cShape2.UserLogic[1] = (string)((TempTag)tn.Tag).temp;
            cShape2.sbsjWhenOutThenDo = ((ObjectBoolean)((TempTag)tn.Tag).WhenOutThenDo).value;
            if (TempTag.newOldDict.ContainsKey(cShape2))
            {
                object value2 = TempTag.newOldDict[cShape2];
                TempTag.newOldDict.Remove(cShape2);
                TempTag.newOldDict.Add(cShape2, value2);
            }
            else
            {
                TempTag.newOldDict.Add(cShape2, value);
            }
            foreach (DataFile df in CEditEnvironmentGlobal.dfs)
            {
                if (df.ListAllShowCShape.Contains(cShape2))
                {
                    CEditEnvironmentGlobal.dhp.dirtyPageAdd(df.name);
                }
            }
        }
        else if (tn.Text == "鼠标左键双击")
        {
            if (((TempTag)tn.Tag).isDeleted)
            {
                CShape cShape3 = (CShape)((TempTag)tn.Tag).obj;
                cShape3.UserLogic[2] = null;
                return true;
            }
            CShape cShape4 = (CShape)((TempTag)tn.Tag).obj;
            CShape value3 = cShape4.Copy();
            cShape4.UserLogic[2] = (string)((TempTag)tn.Tag).temp;
            if (TempTag.newOldDict.ContainsKey(cShape4))
            {
                object value4 = TempTag.newOldDict[cShape4];
                TempTag.newOldDict.Remove(cShape4);
                TempTag.newOldDict.Add(cShape4, value4);
            }
            else
            {
                TempTag.newOldDict.Add(cShape4, value3);
            }
            foreach (DataFile df2 in CEditEnvironmentGlobal.dfs)
            {
                if (df2.ListAllShowCShape.Contains(cShape4))
                {
                    CEditEnvironmentGlobal.dhp.dirtyPageAdd(df2.name);
                }
            }
        }
        else if (tn.Text == "鼠标右键按下")
        {
            if (((TempTag)tn.Tag).isDeleted)
            {
                CShape cShape5 = (CShape)((TempTag)tn.Tag).obj;
                cShape5.UserLogic[3] = null;
                return true;
            }
            CShape cShape6 = (CShape)((TempTag)tn.Tag).obj;
            CShape value5 = cShape6.Copy();
            cShape6.UserLogic[3] = (string)((TempTag)tn.Tag).temp;
            cShape6.sbsjWhenOutThenDo = ((ObjectBoolean)((TempTag)tn.Tag).WhenOutThenDo).value;
            if (TempTag.newOldDict.ContainsKey(cShape6))
            {
                object value6 = TempTag.newOldDict[cShape6];
                TempTag.newOldDict.Remove(cShape6);
                TempTag.newOldDict.Add(cShape6, value6);
            }
            else
            {
                TempTag.newOldDict.Add(cShape6, value5);
            }
            foreach (DataFile df3 in CEditEnvironmentGlobal.dfs)
            {
                if (df3.ListAllShowCShape.Contains(cShape6))
                {
                    CEditEnvironmentGlobal.dhp.dirtyPageAdd(df3.name);
                }
            }
        }
        else if (tn.Text == "鼠标右键双击")
        {
            if (((TempTag)tn.Tag).isDeleted)
            {
                CShape cShape7 = (CShape)((TempTag)tn.Tag).obj;
                cShape7.UserLogic[4] = null;
                return true;
            }
            CShape cShape8 = (CShape)((TempTag)tn.Tag).obj;
            CShape value7 = cShape8.Copy();
            cShape8.UserLogic[4] = (string)((TempTag)tn.Tag).temp;
            if (TempTag.newOldDict.ContainsKey(cShape8))
            {
                object value8 = TempTag.newOldDict[cShape8];
                TempTag.newOldDict.Remove(cShape8);
                TempTag.newOldDict.Add(cShape8, value8);
            }
            else
            {
                TempTag.newOldDict.Add(cShape8, value7);
            }
            foreach (DataFile df4 in CEditEnvironmentGlobal.dfs)
            {
                if (df4.ListAllShowCShape.Contains(cShape8))
                {
                    CEditEnvironmentGlobal.dhp.dirtyPageAdd(df4.name);
                }
            }
        }
        else if (tn.Text == "鼠标滚轮滚动")
        {
            if (((TempTag)tn.Tag).isDeleted)
            {
                CShape cShape9 = (CShape)((TempTag)tn.Tag).obj;
                cShape9.UserLogic[5] = null;
                return true;
            }
            CShape cShape10 = (CShape)((TempTag)tn.Tag).obj;
            CShape value9 = cShape10.Copy();
            cShape10.UserLogic[5] = (string)((TempTag)tn.Tag).temp;
            if (TempTag.newOldDict.ContainsKey(cShape10))
            {
                object value10 = TempTag.newOldDict[cShape10];
                TempTag.newOldDict.Remove(cShape10);
                TempTag.newOldDict.Add(cShape10, value10);
            }
            else
            {
                TempTag.newOldDict.Add(cShape10, value9);
            }
            foreach (DataFile df5 in CEditEnvironmentGlobal.dfs)
            {
                if (df5.ListAllShowCShape.Contains(cShape10))
                {
                    CEditEnvironmentGlobal.dhp.dirtyPageAdd(df5.name);
                }
            }
        }
        else if (tn.Text == "鼠标左键抬起")
        {
            if (((TempTag)tn.Tag).isDeleted)
            {
                CShape cShape11 = (CShape)((TempTag)tn.Tag).obj;
                cShape11.UserLogic[6] = null;
                return true;
            }
            CShape cShape12 = (CShape)((TempTag)tn.Tag).obj;
            CShape value11 = cShape12.Copy();
            cShape12.UserLogic[6] = (string)((TempTag)tn.Tag).temp;
            cShape12.sbsjWhenOutThenDo = ((ObjectBoolean)((TempTag)tn.Tag).WhenOutThenDo).value;
            if (TempTag.newOldDict.ContainsKey(cShape12))
            {
                object value12 = TempTag.newOldDict[cShape12];
                TempTag.newOldDict.Remove(cShape12);
                TempTag.newOldDict.Add(cShape12, value12);
            }
            else
            {
                TempTag.newOldDict.Add(cShape12, value11);
            }
            foreach (DataFile df6 in CEditEnvironmentGlobal.dfs)
            {
                if (df6.ListAllShowCShape.Contains(cShape12))
                {
                    CEditEnvironmentGlobal.dhp.dirtyPageAdd(df6.name);
                }
            }
        }
        else if (tn.Text == "鼠标右键抬起")
        {
            if (((TempTag)tn.Tag).isDeleted)
            {
                CShape cShape13 = (CShape)((TempTag)tn.Tag).obj;
                cShape13.UserLogic[7] = null;
                return true;
            }
            CShape cShape14 = (CShape)((TempTag)tn.Tag).obj;
            CShape value13 = cShape14.Copy();
            cShape14.UserLogic[7] = (string)((TempTag)tn.Tag).temp;
            cShape14.sbsjWhenOutThenDo = ((ObjectBoolean)((TempTag)tn.Tag).WhenOutThenDo).value;
            if (TempTag.newOldDict.ContainsKey(cShape14))
            {
                object value14 = TempTag.newOldDict[cShape14];
                TempTag.newOldDict.Remove(cShape14);
                TempTag.newOldDict.Add(cShape14, value14);
            }
            else
            {
                TempTag.newOldDict.Add(cShape14, value13);
            }
            foreach (DataFile df7 in CEditEnvironmentGlobal.dfs)
            {
                if (df7.ListAllShowCShape.Contains(cShape14))
                {
                    CEditEnvironmentGlobal.dhp.dirtyPageAdd(df7.name);
                }
            }
        }
        else if (tn.Text == "数据库操作成功")
        {
            if (((TempTag)tn.Tag).isDeleted)
            {
                CShape cShape15 = (CShape)((TempTag)tn.Tag).obj;
                cShape15.DBOKScriptUser = null;
                return true;
            }
            CShape cShape16 = (CShape)((TempTag)tn.Tag).obj;
            CShape value15 = cShape16.Copy();
            cShape16.DBOKScriptUser = (string)((TempTag)tn.Tag).temp;
            cShape16.DBOKScript = cShape16.DBOKScriptUser;
            if (TempTag.newOldDict.ContainsKey(cShape16))
            {
                object value16 = TempTag.newOldDict[cShape16];
                TempTag.newOldDict.Remove(cShape16);
                TempTag.newOldDict.Add(cShape16, value16);
            }
            else
            {
                TempTag.newOldDict.Add(cShape16, value15);
            }
            foreach (DataFile df8 in CEditEnvironmentGlobal.dfs)
            {
                if (df8.ListAllShowCShape.Contains(cShape16))
                {
                    CEditEnvironmentGlobal.dhp.dirtyPageAdd(df8.name);
                }
            }
        }
        else if (tn.Text == "数据库操作失败")
        {
            if (((TempTag)tn.Tag).isDeleted)
            {
                CShape cShape17 = (CShape)((TempTag)tn.Tag).obj;
                cShape17.DBErrScriptUser = null;
                return true;
            }
            CShape cShape18 = (CShape)((TempTag)tn.Tag).obj;
            CShape value17 = cShape18.Copy();
            cShape18.DBErrScriptUser = (string)((TempTag)tn.Tag).temp;
            cShape18.DBErrScript = cShape18.DBErrScriptUser;
            if (TempTag.newOldDict.ContainsKey(cShape18))
            {
                object value18 = TempTag.newOldDict[cShape18];
                TempTag.newOldDict.Remove(cShape18);
                TempTag.newOldDict.Add(cShape18, value18);
            }
            else
            {
                TempTag.newOldDict.Add(cShape18, value17);
            }
            foreach (DataFile df9 in CEditEnvironmentGlobal.dfs)
            {
                if (df9.ListAllShowCShape.Contains(cShape18))
                {
                    CEditEnvironmentGlobal.dhp.dirtyPageAdd(df9.name);
                }
            }
        }
        else if (tn.Text == "位变量开")
        {
            if ((CIOAlarm)((TempTag)tn.Tag).obj == null)
            {
                CIOAlarm cIOAlarm = new()
                {
                    name = "[" + tn.Parent.Text + "]"
                };
                cIOAlarm.boolAlarmScript[5] = (string)((TempTag)tn.Tag).temp;
                CEditEnvironmentGlobal.dhp.IOAlarms.Add(cIOAlarm);
                ((TempTag)tn.Tag).obj = cIOAlarm;
            }
            else
            {
                CIOAlarm cIOAlarm2 = (CIOAlarm)((TempTag)tn.Tag).obj;
                cIOAlarm2.boolAlarmScript[5] = (string)((TempTag)tn.Tag).temp;
            }
        }
        else if (tn.Text == "位变量关")
        {
            if ((CIOAlarm)((TempTag)tn.Tag).obj == null)
            {
                CIOAlarm cIOAlarm3 = new()
                {
                    name = "[" + tn.Parent.Text + "]"
                };
                cIOAlarm3.boolAlarmScript[6] = (string)((TempTag)tn.Tag).temp;
                CEditEnvironmentGlobal.dhp.IOAlarms.Add(cIOAlarm3);
                ((TempTag)tn.Tag).obj = cIOAlarm3;
            }
            else
            {
                CIOAlarm cIOAlarm4 = (CIOAlarm)((TempTag)tn.Tag).obj;
                cIOAlarm4.boolAlarmScript[6] = (string)((TempTag)tn.Tag).temp;
            }
        }
        else if (tn.Text == "位变量由开到关")
        {
            if ((CIOAlarm)((TempTag)tn.Tag).obj == null)
            {
                CIOAlarm cIOAlarm5 = new()
                {
                    name = "[" + tn.Parent.Text + "]"
                };
                cIOAlarm5.boolAlarmScript[7] = (string)((TempTag)tn.Tag).temp;
                CEditEnvironmentGlobal.dhp.IOAlarms.Add(cIOAlarm5);
                ((TempTag)tn.Tag).obj = cIOAlarm5;
            }
            else
            {
                CIOAlarm cIOAlarm6 = (CIOAlarm)((TempTag)tn.Tag).obj;
                cIOAlarm6.boolAlarmScript[7] = (string)((TempTag)tn.Tag).temp;
            }
        }
        else if (tn.Text == "位变量由关到开")
        {
            if ((CIOAlarm)((TempTag)tn.Tag).obj == null)
            {
                CIOAlarm cIOAlarm7 = new()
                {
                    name = "[" + tn.Parent.Text + "]"
                };
                cIOAlarm7.boolAlarmScript[8] = (string)((TempTag)tn.Tag).temp;
                CEditEnvironmentGlobal.dhp.IOAlarms.Add(cIOAlarm7);
                ((TempTag)tn.Tag).obj = cIOAlarm7;
            }
            else
            {
                CIOAlarm cIOAlarm8 = (CIOAlarm)((TempTag)tn.Tag).obj;
                cIOAlarm8.boolAlarmScript[8] = (string)((TempTag)tn.Tag).temp;
            }
        }
        else if (tn.Text == "位变量变化")
        {
            if ((CIOAlarm)((TempTag)tn.Tag).obj == null)
            {
                CIOAlarm cIOAlarm9 = new()
                {
                    name = "[" + tn.Parent.Text + "]"
                };
                cIOAlarm9.boolAlarmScript[9] = (string)((TempTag)tn.Tag).temp;
                CEditEnvironmentGlobal.dhp.IOAlarms.Add(cIOAlarm9);
                ((TempTag)tn.Tag).obj = cIOAlarm9;
            }
            else
            {
                CIOAlarm cIOAlarm10 = (CIOAlarm)((TempTag)tn.Tag).obj;
                cIOAlarm10.boolAlarmScript[9] = (string)((TempTag)tn.Tag).temp;
            }
        }
        else if (tn.Text == "下下限报警")
        {
            if ((CIOAlarm)((TempTag)tn.Tag).obj == null)
            {
                CIOAlarm cIOAlarm11 = new()
                {
                    name = "[" + tn.Parent.Text + "]"
                };
                cIOAlarm11.script[6] = (string)((TempTag)tn.Tag).temp;
                CEditEnvironmentGlobal.dhp.IOAlarms.Add(cIOAlarm11);
                ((TempTag)tn.Tag).obj = cIOAlarm11;
            }
            else
            {
                CIOAlarm cIOAlarm12 = (CIOAlarm)((TempTag)tn.Tag).obj;
                cIOAlarm12.script[6] = (string)((TempTag)tn.Tag).temp;
            }
        }
        else if (tn.Text == "下限报警")
        {
            if ((CIOAlarm)((TempTag)tn.Tag).obj == null)
            {
                CIOAlarm cIOAlarm13 = new()
                {
                    name = "[" + tn.Parent.Text + "]"
                };
                cIOAlarm13.script[7] = (string)((TempTag)tn.Tag).temp;
                CEditEnvironmentGlobal.dhp.IOAlarms.Add(cIOAlarm13);
                ((TempTag)tn.Tag).obj = cIOAlarm13;
            }
            else
            {
                CIOAlarm cIOAlarm14 = (CIOAlarm)((TempTag)tn.Tag).obj;
                cIOAlarm14.script[7] = (string)((TempTag)tn.Tag).temp;
            }
        }
        else if (tn.Text == "上限报警")
        {
            if ((CIOAlarm)((TempTag)tn.Tag).obj == null)
            {
                CIOAlarm cIOAlarm15 = new()
                {
                    name = "[" + tn.Parent.Text + "]"
                };
                cIOAlarm15.script[8] = (string)((TempTag)tn.Tag).temp;
                CEditEnvironmentGlobal.dhp.IOAlarms.Add(cIOAlarm15);
                ((TempTag)tn.Tag).obj = cIOAlarm15;
            }
            else
            {
                CIOAlarm cIOAlarm16 = (CIOAlarm)((TempTag)tn.Tag).obj;
                cIOAlarm16.script[8] = (string)((TempTag)tn.Tag).temp;
            }
        }
        else if (tn.Text == "上上限报警")
        {
            if ((CIOAlarm)((TempTag)tn.Tag).obj == null)
            {
                CIOAlarm cIOAlarm17 = new()
                {
                    name = "[" + tn.Parent.Text + "]"
                };
                cIOAlarm17.script[9] = (string)((TempTag)tn.Tag).temp;
                CEditEnvironmentGlobal.dhp.IOAlarms.Add(cIOAlarm17);
                ((TempTag)tn.Tag).obj = cIOAlarm17;
            }
            else
            {
                CIOAlarm cIOAlarm18 = (CIOAlarm)((TempTag)tn.Tag).obj;
                cIOAlarm18.script[9] = (string)((TempTag)tn.Tag).temp;
            }
        }
        else if (tn.Text == "目标值报警")
        {
            if ((CIOAlarm)((TempTag)tn.Tag).obj == null)
            {
                CIOAlarm cIOAlarm19 = new()
                {
                    name = "[" + tn.Parent.Text + "]"
                };
                cIOAlarm19.script[10] = (string)((TempTag)tn.Tag).temp;
                CEditEnvironmentGlobal.dhp.IOAlarms.Add(cIOAlarm19);
                ((TempTag)tn.Tag).obj = cIOAlarm19;
            }
            else
            {
                CIOAlarm cIOAlarm20 = (CIOAlarm)((TempTag)tn.Tag).obj;
                cIOAlarm20.script[10] = (string)((TempTag)tn.Tag).temp;
            }
        }
        else if (tn.Text == "变化率报警")
        {
            if ((CIOAlarm)((TempTag)tn.Tag).obj == null)
            {
                CIOAlarm cIOAlarm21 = new()
                {
                    name = "[" + tn.Parent.Text + "]"
                };
                cIOAlarm21.script[11] = (string)((TempTag)tn.Tag).temp;
                CEditEnvironmentGlobal.dhp.IOAlarms.Add(cIOAlarm21);
                ((TempTag)tn.Tag).obj = cIOAlarm21;
            }
            else
            {
                CIOAlarm cIOAlarm22 = (CIOAlarm)((TempTag)tn.Tag).obj;
                cIOAlarm22.script[11] = (string)((TempTag)tn.Tag).temp;
            }
        }
        else if (tn.Text == "Click" || tn.Text == "DoubleClick" || tn.Text == "MouseDown" || tn.Text == "MouseUp" || tn.Text == "MouseMove" || tn.Text == "MouseEnter" || tn.Text == "MouseLeave" || tn.Text == "MouseClick" || tn.Text == "CheckedChanged" || tn.Text == "TextChange" || tn.Text == "TextChanged" || tn.Text == "SelectedIndexChanged" || tn.Text == "KeyUp" || tn.Text == "Tick")
        {
            CShape cShape19 = (CShape)((TempTag)tn.Tag).obj;
            CShape value19 = cShape19.Copy();
            for (int i = 0; i < cShape19.ysdzshijianmingcheng.Length; i++)
            {
                string[] array = cShape19.ysdzshijianmingcheng[i].Split(' ');
                if (array[1] == tn.Text)
                {
                    if (((TempTag)tn.Tag).temp != null)
                    {
                        cShape19.ysdzshijianbiaodashi[i] = (string)((TempTag)tn.Tag).temp;
                    }
                    break;
                }
                if (i == cShape19.ysdzshijianmingcheng.Length - 1)
                {
                    MessageBox.Show(cShape19.Name + "控件原有的事件方法发生改变.");
                }
            }
            if (TempTag.newOldDict.ContainsKey(cShape19))
            {
                object value20 = TempTag.newOldDict[cShape19];
                TempTag.newOldDict.Remove(cShape19);
                TempTag.newOldDict.Add(cShape19, value20);
            }
            else
            {
                TempTag.newOldDict.Add(cShape19, value19);
            }
            foreach (DataFile df10 in CEditEnvironmentGlobal.dfs)
            {
                if (df10.ListAllShowCShape.Contains(cShape19))
                {
                    CEditEnvironmentGlobal.dhp.dirtyPageAdd(df10.name);
                }
            }
        }
        return true;
    }

    private TreeNode FindNode(string id, TreeNode startNode)
    {
        if (startNode.Name == id)
        {
            return startNode;
        }
        foreach (TreeNode node in startNode.Nodes)
        {
            if (FindNode(id, node) != null)
                return FindNode(id, node);
        }
        return null;
    }

    private void TreeView2_AfterSelect(object sender, TreeViewEventArgs e)
    {
        TreeNode node = e.Node;
        if (node.Tag != null)
        {
            barStaticItem1.Caption = (string)node.Tag;
        }
        else
        {
            barStaticItem1.Caption = "";
        }
    }

    private void BarButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (currentTempTag != null)
        {
            currentTempTag.temp = textEditorControl1.Text;
            if (panelControl1.Visible)
            {
                currentTempTag.t = spinEdit1.EditValue;
            }
        }
        if (!CheckAllScript())
        {
            return;
        }
        foreach (TreeNode dirtyNode in dirtyNodes)
        {
            Save(dirtyNode);
        }
        dirtyNodes.Clear();
        makeRedoAndUndo();
    }

    private void CheckEdit1_CheckedChanged(object sender, EventArgs e)
    {
        if (usereditit)
        {
            ((ObjectBoolean)currentTempTag.WhenOutThenDo).value = checkEdit1.Checked;
        }
    }

    private void TextEditorControl1_TextChanged(object sender, EventArgs e)
    {
        if (treeView1.SelectedNode != null && !dirtyNodes.Contains(treeView1.SelectedNode))
        {
            dirtyNodes.Add(treeView1.SelectedNode);
        }
        if (treeView1.SelectedNode != null)
        {
            if (textEditorControl1.Text == string.Empty)
            {
                treeView1.BeginUpdate();
                treeView1.SelectedNode.NodeFont = RegularFont;
                treeView1.EndUpdate();
            }
            else
            {
                treeView1.BeginUpdate();
                treeView1.SelectedNode.NodeFont = BoldFont;
                treeView1.EndUpdate();
            }
        }
    }

    private void TreeView2_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        TreeNode nodeAt = treeView2.GetNodeAt(e.Location);
        treeView2.SelectedNode = nodeAt;
        string text = "";
        if (nodeAt.Name == "io" && nodeAt.ImageKey == "io.png")
        {
            text = text + "[" + nodeAt.Text + "]";
        }
        else if (nodeAt.Name == "api" && nodeAt.ImageKey == "api.png")
        {
            if (nodeAt.Parent != null)
            {
                text = text + "System." + nodeAt.Text;
            }
        }
        else if (nodeAt.ImageKey == "property.png" && nodeAt.Parent != null)
        {
            string text2 = text;
            text = text2 + nodeAt.Parent.Parent.Name + "." + nodeAt.Parent.Text + "." + nodeAt.Text;
        }
        textEditorControl1.ActiveTextAreaControl.TextArea.InsertString(text);
        textEditorControl1.Focus();
    }

    private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (currentTempTag != null)
        {
            currentTempTag.temp = textEditorControl1.Text;
        }
        if (CheckAllScript())
        {
            MessageBox.Show("脚本检查完成");
        }
    }

    private void ScriptUnit_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        barButtonItem5_ItemClick(null, null);
    }

    private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                File.WriteAllText(saveFileDialog1.FileName, textEditorControl1.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (currentTempTag != null)
        {
            currentTempTag.temp = textEditorControl1.Text;
            if (panelControl1.Visible)
            {
                currentTempTag.t = spinEdit1.EditValue;
            }
        }
        CheckAllScript();
        switch (MessageBox.Show("保存脚本?", "提示", MessageBoxButtons.YesNoCancel))
        {
            case DialogResult.Yes:
                if (CheckAllScript())
                {
                    foreach (TreeNode dirtyNode in dirtyNodes)
                    {
                        Save(dirtyNode);
                    }
                    dirtyNodes.Clear();
                    makeRedoAndUndo();
                    CEditEnvironmentGlobal.mdiparent.Focus();
                    CEditEnvironmentGlobal.mdiparent.Select();
                    Hide();
                }
                else if (MessageBox.Show("放弃脚本并退出?", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CEditEnvironmentGlobal.mdiparent.Focus();
                    CEditEnvironmentGlobal.mdiparent.Select();
                    Hide();
                }
                break;
            case DialogResult.No:
                CEditEnvironmentGlobal.mdiparent.Focus();
                CEditEnvironmentGlobal.mdiparent.Select();
                Hide();
                break;
        }
    }

    public void treeView1_MouseClick(object sender, MouseEventArgs e)
    {
        TreeNode nodeAt;
        if (e.Button == MouseButtons.Right && (nodeAt = treeView1.GetNodeAt(e.Location)) != null)
        {
            if ((nodeAt.Parent != null && nodeAt.Parent.Text == "页面相关") || (nodeAt.Parent != null && nodeAt.Parent.Parent != null && nodeAt.Parent.Parent.Text == "页面相关" && nodeAt.Text != "页面显示脚本" && nodeAt.Text != "页面运行脚本" && nodeAt.Text != "页面隐藏脚本"))
            {
                ClickNode = nodeAt;
                addEventScriptPopMenu.ShowPopup(Cursor.Position);
            }
            else if (nodeAt.Tag != null && ((TempTag)nodeAt.Tag).obj is CShape)
            {
                deleteEventPopMenu.ShowPopup(Cursor.Position);
            }
            treeView1.SelectedNode = nodeAt;
            AfterSelectHandler(nodeAt);
        }
    }

    private void deleteEventBtnItem_ItemClick(object sender, ItemClickEventArgs e)
    {
        DeleteEventNode(treeView1.SelectedNode);
    }

    private void addEventScriptBtn_ItemClick(object sender, ItemClickEventArgs e)
    {
        eventScriptFrm = new ScriptUnitAddUserDefineEvent
        {
            Owner = this
        };
        DialogResult dialogResult = eventScriptFrm.ShowDialog();
        if (dialogResult == DialogResult.OK)
        {
            CtrlName = eventScriptFrm.CtrlName;
            EventName = eventScriptFrm.EventName;
            AddEventScriptTreeNode();
        }
    }

    private bool HaveSelection()
    {
        return ActiveTextEditor?.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected ?? false;
    }

    private void DoEditAction(TextEditorControl editor, IEditAction action)
    {
        if (editor == null || action == null)
        {
            return;
        }
        TextArea textArea = editor.ActiveTextAreaControl.TextArea;
        editor.BeginUpdate();
        try
        {
            lock (editor.Document)
            {
                action.Execute(textArea);
                if (textArea.SelectionManager.HasSomethingSelected && textArea.AutoClearSelection && textArea.Document.TextEditorProperties.DocumentSelectionMode == DocumentSelectionMode.Normal)
                {
                    textArea.SelectionManager.ClearSelection();
                }
            }
        }
        finally
        {
            editor.EndUpdate();
            textArea.Caret.UpdateCaretPosition();
        }
    }

    private void UndoBarBtn_ItemClick(object sender, EventArgs e)
    {
        DoEditAction(ActiveTextEditor, new Undo());
    }

    private void redoBarBtn_ItemClick(object sender, EventArgs e)
    {
        DoEditAction(ActiveTextEditor, new Redo());
    }

    private void cutBarBtn_ItemClick(object sender, EventArgs e)
    {
        if (HaveSelection())
        {
            DoEditAction(ActiveTextEditor, new Cut());
        }
    }

    private void copyBarBtn_ItemClick(object sender, EventArgs e)
    {
        if (HaveSelection())
        {
            DoEditAction(ActiveTextEditor, new Copy());
        }
    }

    private void pasteBarBtn_ItemClick(object sender, EventArgs e)
    {
        DoEditAction(ActiveTextEditor, new Paste());
    }

    private void delBarBtn_ItemClick(object sender, EventArgs e)
    {
        if (HaveSelection())
        {
            DoEditAction(ActiveTextEditor, new Delete());
        }
    }

    private void RepBarBtn_ItemClick(object sender, EventArgs e)
    {
        TextEditorControl activeTextEditor = ActiveTextEditor;
        if (activeTextEditor != null)
        {
            FindAndReplaceFrm.ShowFor(activeTextEditor, replaceMode: true);
        }
    }

    private void moveBarBtn_ItemClick(object sender, EventArgs e)
    {
        TextEditorControl activeTextEditor = ActiveTextEditor;
        if (activeTextEditor != null)
        {
            MoveLineFrm.ShowFor(activeTextEditor);
        }
    }

    private void selAllBarBtn_ItemClick(object sender, EventArgs e)
    {
        DoEditAction(ActiveTextEditor, new SelectWholeDocument());
    }

    private void treeView1_KeyDown(object sender, KeyEventArgs e)
    {
        if (treeView1.SelectedNode != null)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (e.KeyCode == Keys.Delete)
            {
                DeleteEventNode(selectedNode);
            }
        }
    }

    private void copyBarBtn_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (HaveSelection())
        {
            DoEditAction(ActiveTextEditor, new Copy());
        }
    }

    private void cutBarBtn_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (HaveSelection())
        {
            DoEditAction(ActiveTextEditor, new Cut());
        }
    }

    private void findBarBtn_ItemClick(object sender, ItemClickEventArgs e)
    {
        TextEditorControl activeTextEditor = ActiveTextEditor;
        if (activeTextEditor != null)
        {
            FindAndReplaceFrm.ShowFor(activeTextEditor, replaceMode: false);
        }
    }

    private void moveBarBtn_ItemClick(object sender, ItemClickEventArgs e)
    {
        TextEditorControl activeTextEditor = ActiveTextEditor;
        if (activeTextEditor != null)
        {
            MoveLineFrm.ShowFor(activeTextEditor);
        }
    }

    private void pasteBarBtn_ItemClick(object sender, ItemClickEventArgs e)
    {
        DoEditAction(ActiveTextEditor, new Paste());
    }

    private void UndoBarBtn_ItemClick(object sender, ItemClickEventArgs e)
    {
        DoEditAction(ActiveTextEditor, new Undo());
    }

    private void redoBarBtn_ItemClick(object sender, ItemClickEventArgs e)
    {
        DoEditAction(ActiveTextEditor, new Redo());
    }

    private void delBarBtn_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (HaveSelection())
        {
            DoEditAction(ActiveTextEditor, new Delete());
        }
    }

    private void barButtonItem_Copy_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (HaveSelection())
        {
            DoEditAction(ActiveTextEditor, new Copy());
        }
    }

    private void barButtonItem_Paste_ItemClick(object sender, ItemClickEventArgs e)
    {
        DoEditAction(ActiveTextEditor, new Paste());
    }

    private void barButtonItem_Cut_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (HaveSelection())
        {
            DoEditAction(ActiveTextEditor, new Cut());
        }
    }

    private void barButtonItem_Delete_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (HaveSelection())
        {
            DoEditAction(ActiveTextEditor, new Delete());
        }
    }

    private void barButtonItem_Undo_ItemClick(object sender, ItemClickEventArgs e)
    {
        DoEditAction(ActiveTextEditor, new Undo());
    }

    private void barButtonItem_Redo_ItemClick(object sender, ItemClickEventArgs e)
    {
        DoEditAction(ActiveTextEditor, new Redo());
    }

    private void barButtonItem_Find_ItemClick(object sender, ItemClickEventArgs e)
    {
        TextEditorControl activeTextEditor = ActiveTextEditor;
        if (activeTextEditor != null)
        {
            FindAndReplaceFrm.ShowFor(activeTextEditor, replaceMode: false);
        }
    }

    private void barButtonItem_Replace_ItemClick(object sender, ItemClickEventArgs e)
    {
        TextEditorControl activeTextEditor = ActiveTextEditor;
        if (activeTextEditor != null)
        {
            FindAndReplaceFrm.ShowFor(activeTextEditor, replaceMode: true);
        }
    }

    private void barButtonItem_Goto_ItemClick(object sender, ItemClickEventArgs e)
    {
        TextEditorControl activeTextEditor = ActiveTextEditor;
        if (activeTextEditor != null)
        {
            MoveLineFrm.ShowFor(activeTextEditor);
        }
    }

    private void ScriptUnit_Load(object sender, EventArgs e)
    {
    }
}
