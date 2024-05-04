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
        this.listView1 = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.listView2 = new System.Windows.Forms.ListView();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.listView3 = new System.Windows.Forms.ListView();
        this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.button4 = new System.Windows.Forms.Button();
        this.button5 = new System.Windows.Forms.Button();
        this.button6 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.columnHeader1 });
        this.listView1.Location = new System.Drawing.Point(15, 14);
        this.listView1.Name = "listView1";
        this.listView1.Size = new System.Drawing.Size(210, 667);
        this.listView1.TabIndex = 0;
        this.listView1.UseCompatibleStateImageBehavior = false;
        this.listView1.View = System.Windows.Forms.View.Details;
        this.listView1.SelectedIndexChanged += new System.EventHandler(listView1_SelectedIndexChanged);
        this.columnHeader1.Text = "页面名称";
        this.columnHeader1.Width = 150;
        this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.columnHeader2 });
        this.listView2.Location = new System.Drawing.Point(233, 82);
        this.listView2.Name = "listView2";
        this.listView2.Size = new System.Drawing.Size(210, 262);
        this.listView2.TabIndex = 3;
        this.listView2.UseCompatibleStateImageBehavior = false;
        this.listView2.View = System.Windows.Forms.View.Details;
        this.columnHeader2.Text = "页面名称";
        this.columnHeader2.Width = 150;
        this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.columnHeader3 });
        this.listView3.Location = new System.Drawing.Point(233, 419);
        this.listView3.Name = "listView3";
        this.listView3.Size = new System.Drawing.Size(210, 262);
        this.listView3.TabIndex = 6;
        this.listView3.UseCompatibleStateImageBehavior = false;
        this.listView3.View = System.Windows.Forms.View.Details;
        this.columnHeader3.Text = "页面名称";
        this.columnHeader3.Width = 150;
        this.button1.Location = new System.Drawing.Point(471, 14);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 27);
        this.button1.TabIndex = 7;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(471, 48);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(87, 27);
        this.button2.TabIndex = 8;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.button3.Location = new System.Drawing.Point(233, 14);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(211, 27);
        this.button3.TabIndex = 1;
        this.button3.Text = "加入点击显示";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.button4.Location = new System.Drawing.Point(233, 351);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(211, 27);
        this.button4.TabIndex = 4;
        this.button4.Text = "加入点击隐藏";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(button4_Click);
        this.button5.Location = new System.Drawing.Point(233, 48);
        this.button5.Name = "button5";
        this.button5.Size = new System.Drawing.Size(211, 27);
        this.button5.TabIndex = 2;
        this.button5.Text = "去除点击显示";
        this.button5.UseVisualStyleBackColor = true;
        this.button5.Click += new System.EventHandler(button5_Click);
        this.button6.Location = new System.Drawing.Point(233, 385);
        this.button6.Name = "button6";
        this.button6.Size = new System.Drawing.Size(211, 27);
        this.button6.TabIndex = 5;
        this.button6.Text = "去除点击隐藏";
        this.button6.UseVisualStyleBackColor = true;
        this.button6.Click += new System.EventHandler(button6_Click);
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(595, 696);
        base.Controls.Add(this.button6);
        base.Controls.Add(this.button5);
        base.Controls.Add(this.button4);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.listView3);
        base.Controls.Add(this.listView2);
        base.Controls.Add(this.listView1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "pageForm";
        this.Text = "页面切换";
        base.Load += new System.EventHandler(pageForm_Load);
        base.ResumeLayout(false);
    }
}
