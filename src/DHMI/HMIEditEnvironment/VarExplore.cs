using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace HMIEditEnvironment;

public class VarExplore : UserControl
{
    private TreeNode varIO;

    private TreeNode varInner;

    private readonly List<string> ltVarIO = new();

    private readonly List<string> ltVarInner = new();

    private TreeView treeVar;

    public VarExplore()
    {
        InitializeComponent();
    }

    private void VarExplore_Load(object sender, EventArgs e)
    {
    }

    public void AddTreeNode()
    {
        varIO = treeVar.Nodes.Add("设备变量");
        varInner = treeVar.Nodes.Add("内部变量");
    }

    public void AddVarExplore()
    {
        new List<XmlNode>();
        varInner.Nodes.Clear();
        foreach (ProjectIO projectIO in CEditEnvironmentGlobal.dhp.ProjectIOs)
        {
            ltVarInner.Add(projectIO.name);
            varInner.Nodes.Add(projectIO.name);
        }
    }

    private void treeVar_MouseDown(object sender, MouseEventArgs e)
    {
        TreeView treeView = sender as TreeView;
        TreeNode nodeAt = treeView.GetNodeAt(e.X, e.Y);
        if (nodeAt == null || nodeAt.Text == "内部变量")
            return;

        if (nodeAt.Text == "设备变量")
        {
            AddVarExplore();
        }
        else if (nodeAt != null && e.Button == MouseButtons.Left)
        {
            int num = 0;
            if (num != -1)
            {
                treeView.DoDragDrop(string.Format("AddShape:{0}:{1}", num, nodeAt.Name + "变量文字:" + nodeAt.Text), DragDropEffects.Copy);
                treeView.SelectedNode = nodeAt;
            }
        }
    }

    private void InitializeComponent()
    {
        this.treeVar = new System.Windows.Forms.TreeView();
        base.SuspendLayout();
        this.treeVar.AllowDrop = true;
        this.treeVar.Dock = System.Windows.Forms.DockStyle.Fill;
        this.treeVar.Location = new System.Drawing.Point(0, 0);
        this.treeVar.Name = "treeVar";
        this.treeVar.Size = new System.Drawing.Size(253, 373);
        this.treeVar.TabIndex = 0;
        this.treeVar.MouseDown += new System.Windows.Forms.MouseEventHandler(treeVar_MouseDown);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.Controls.Add(this.treeVar);
        base.Name = "VarExplore";
        base.Size = new System.Drawing.Size(253, 373);
        base.Load += new System.EventHandler(VarExplore_Load);
        base.ResumeLayout(false);
    }
}
