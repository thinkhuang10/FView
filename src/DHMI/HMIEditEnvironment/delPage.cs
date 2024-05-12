using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class DelPage : XtraForm
{
    private ListView listView1;

    private Label label1;

    private Button button1;

    private ColumnHeader columnHeader1;

    private Button button2;

    public DelPage()
    {
        InitializeComponent();
    }

    public DelPage(List<string> err)
    {
        InitializeComponent();
        foreach (string item in err)
        {
            listView1.Items.Add(item);
        }
    }

    public DelPage(List<string> err, string text)
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
        listView1 = new System.Windows.Forms.ListView();
        columnHeader1 = new System.Windows.Forms.ColumnHeader();
        label1 = new System.Windows.Forms.Label();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { columnHeader1 });
        listView1.FullRowSelect = true;
        listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        listView1.Location = new System.Drawing.Point(14, 51);
        listView1.Name = "listView1";
        listView1.Size = new System.Drawing.Size(524, 156);
        listView1.TabIndex = 0;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.View = System.Windows.Forms.View.Details;
        columnHeader1.Width = 445;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(26, 22);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(183, 14);
        label1.TabIndex = 1;
        label1.Text = "该页面正在被引用,是否继续操作.";
        button1.Location = new System.Drawing.Point(437, 14);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 2;
        button1.Text = "否";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.Location = new System.Drawing.Point(333, 14);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 1;
        button2.Text = "是";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(553, 230);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.Controls.Add(label1);
        base.Controls.Add(listView1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "delPage";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "警告";
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
