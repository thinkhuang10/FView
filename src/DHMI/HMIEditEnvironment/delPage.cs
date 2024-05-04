using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class delPage : XtraForm
{
    private ListView listView1;

    private Label label1;

    private Button button1;

    private ColumnHeader columnHeader1;

    private Button button2;

    public delPage()
    {
        InitializeComponent();
    }

    public delPage(List<string> err)
    {
        InitializeComponent();
        foreach (string item in err)
        {
            listView1.Items.Add(item);
        }
    }

    public delPage(List<string> err, string text)
    {
        InitializeComponent();
        foreach (string item in err)
        {
            listView1.Items.Add(item);
        }
        label1.Text = text;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void delPage_Load(object sender, EventArgs e)
    {
        base.AcceptButton = button1;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Yes;
        Close();
    }

    private void InitializeComponent()
    {
        this.listView1 = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.label1 = new System.Windows.Forms.Label();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.columnHeader1 });
        this.listView1.FullRowSelect = true;
        this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        this.listView1.Location = new System.Drawing.Point(14, 51);
        this.listView1.Name = "listView1";
        this.listView1.Size = new System.Drawing.Size(524, 156);
        this.listView1.TabIndex = 0;
        this.listView1.UseCompatibleStateImageBehavior = false;
        this.listView1.View = System.Windows.Forms.View.Details;
        this.columnHeader1.Width = 445;
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(26, 22);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(183, 14);
        this.label1.TabIndex = 1;
        this.label1.Text = "该页面正在被引用,是否继续操作.";
        this.button1.Location = new System.Drawing.Point(437, 14);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 27);
        this.button1.TabIndex = 2;
        this.button1.Text = "否";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.Location = new System.Drawing.Point(333, 14);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(87, 27);
        this.button2.TabIndex = 1;
        this.button2.Text = "是";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(553, 230);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.listView1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "delPage";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "警告";
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
