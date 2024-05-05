using DevExpress.XtraEditors;
using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class pageForm : XtraForm
{
    private readonly CGlobal theglobal;

    private readonly List<DataFile> dfs;

    private ListView listView1;

    private ListView listView2;

    private ListView listView3;

    private Button button1;

    private Button button2;

    private Button button3;

    private Button button4;

    private Button button5;

    private Button button6;

    private ColumnHeader columnHeader1;

    private ColumnHeader columnHeader2;

    private ColumnHeader columnHeader3;

    public pageForm(CGlobal _theglobal, List<DataFile> _dfs)
    {
        theglobal = _theglobal;
        dfs = _dfs;
        InitializeComponent();
    }

    public pageForm()
    {
        InitializeComponent();
    }

    private void pageForm_Load(object sender, EventArgs e)
    {
        foreach (DataFile df in dfs)
        {
            listView1.Items.Add(df.name, df.pageName, "");
        }
        List<string> list = new();
        List<string> list2 = new();
        if (theglobal.SelectedShapeList[0].ymqhxianshi != null)
        {
            list = new List<string>(theglobal.SelectedShapeList[0].ymqhxianshi);
        }
        if (theglobal.SelectedShapeList[0].ymqhyincang != null)
        {
            list2 = new List<string>(theglobal.SelectedShapeList[0].ymqhyincang);
        }
        foreach (ListViewItem item in listView1.Items)
        {
            if (list.Remove(item.Name))
            {
                listView1.Items.Remove(item);
                listView2.Items.Add(item);
            }
            if (list2.Remove(item.Name))
            {
                listView1.Items.Remove(item);
                listView3.Items.Add(item);
            }
        }
    }

    private void button3_Click(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count == 0)
        {
            return;
        }
        foreach (ListViewItem selectedItem in listView1.SelectedItems)
        {
            listView1.Items.Remove(selectedItem);
            listView2.Items.Add(selectedItem);
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count == 0)
        {
            return;
        }
        foreach (ListViewItem selectedItem in listView1.SelectedItems)
        {
            listView1.Items.Remove(selectedItem);
            listView3.Items.Add(selectedItem);
        }
    }

    private void button5_Click(object sender, EventArgs e)
    {
        if (listView2.SelectedItems.Count == 0)
        {
            return;
        }
        foreach (ListViewItem selectedItem in listView2.SelectedItems)
        {
            listView2.Items.Remove(selectedItem);
            listView1.Items.Add(selectedItem);
        }
    }

    private void button6_Click(object sender, EventArgs e)
    {
        if (listView3.SelectedItems.Count == 0)
        {
            return;
        }
        foreach (ListViewItem selectedItem in listView3.SelectedItems)
        {
            listView3.Items.Remove(selectedItem);
            listView1.Items.Add(selectedItem);
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        List<string> list = new();
        foreach (ListViewItem item in listView2.Items)
        {
            list.Add(item.Name);
        }
        List<string> list2 = new();
        foreach (ListViewItem item2 in listView3.Items)
        {
            list2.Add(item2.Name);
        }
        theglobal.SelectedShapeList[0].ymqhxianshi = list.ToArray();
        theglobal.SelectedShapeList[0].ymqhyincang = list2.ToArray();
        if (!theglobal.SelectedShapeList[0].ymqh && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            theglobal.SelectedShapeList[0].ymqh = true;
        }
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void listView1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void InitializeComponent()
    {
        listView1 = new System.Windows.Forms.ListView();
        columnHeader1 = new System.Windows.Forms.ColumnHeader();
        listView2 = new System.Windows.Forms.ListView();
        columnHeader2 = new System.Windows.Forms.ColumnHeader();
        listView3 = new System.Windows.Forms.ListView();
        columnHeader3 = new System.Windows.Forms.ColumnHeader();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        button4 = new System.Windows.Forms.Button();
        button5 = new System.Windows.Forms.Button();
        button6 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { columnHeader1 });
        listView1.Location = new System.Drawing.Point(15, 14);
        listView1.Name = "listView1";
        listView1.Size = new System.Drawing.Size(210, 667);
        listView1.TabIndex = 0;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.View = System.Windows.Forms.View.Details;
        listView1.SelectedIndexChanged += new System.EventHandler(listView1_SelectedIndexChanged);
        columnHeader1.Text = "页面名称";
        columnHeader1.Width = 150;
        listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { columnHeader2 });
        listView2.Location = new System.Drawing.Point(233, 82);
        listView2.Name = "listView2";
        listView2.Size = new System.Drawing.Size(210, 262);
        listView2.TabIndex = 3;
        listView2.UseCompatibleStateImageBehavior = false;
        listView2.View = System.Windows.Forms.View.Details;
        columnHeader2.Text = "页面名称";
        columnHeader2.Width = 150;
        listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { columnHeader3 });
        listView3.Location = new System.Drawing.Point(233, 419);
        listView3.Name = "listView3";
        listView3.Size = new System.Drawing.Size(210, 262);
        listView3.TabIndex = 6;
        listView3.UseCompatibleStateImageBehavior = false;
        listView3.View = System.Windows.Forms.View.Details;
        columnHeader3.Text = "页面名称";
        columnHeader3.Width = 150;
        button1.Location = new System.Drawing.Point(471, 14);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 7;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(471, 48);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 8;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        button3.Location = new System.Drawing.Point(233, 14);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(211, 27);
        button3.TabIndex = 1;
        button3.Text = "加入点击显示";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        button4.Location = new System.Drawing.Point(233, 351);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(211, 27);
        button4.TabIndex = 4;
        button4.Text = "加入点击隐藏";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(button4_Click);
        button5.Location = new System.Drawing.Point(233, 48);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(211, 27);
        button5.TabIndex = 2;
        button5.Text = "去除点击显示";
        button5.UseVisualStyleBackColor = true;
        button5.Click += new System.EventHandler(button5_Click);
        button6.Location = new System.Drawing.Point(233, 385);
        button6.Name = "button6";
        button6.Size = new System.Drawing.Size(211, 27);
        button6.TabIndex = 5;
        button6.Text = "去除点击隐藏";
        button6.UseVisualStyleBackColor = true;
        button6.Click += new System.EventHandler(button6_Click);
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(595, 696);
        base.Controls.Add(button6);
        base.Controls.Add(button5);
        base.Controls.Add(button4);
        base.Controls.Add(button3);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.Controls.Add(listView3);
        base.Controls.Add(listView2);
        base.Controls.Add(listView1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "pageForm";
        Text = "页面切换";
        base.Load += new System.EventHandler(pageForm_Load);
        base.ResumeLayout(false);
    }
}
