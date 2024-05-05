using ShapeRuntime;
using System;
using System.Windows.Forms;
using System.Xml;

namespace HMIEditEnvironment.Animation;

public class DBVarForm : Form
{
    public string toVarStr = "";

    private ListBox listBox1;

    private ListBox listBox2;

    private Button button1;

    private Button button2;

    private Button button3;

    private Button button4;

    private Button button5;

    private Button button6;

    public DBVarForm()
    {
        InitializeComponent();
    }

    private void DBVarFrom_Load(object sender, EventArgs e)
    {
        listBox2.Items.Add("{权限管理字段}");
        foreach (ProjectIO projectIO in CEditEnvironmentGlobal.dhp.ProjectIOs)
        {
            listBox2.Items.Add("{[" + projectIO.name + "]}");
        }
        XmlNodeList xmlNodeList = CEditEnvironmentGlobal.xmldoc.SelectNodes("/DocumentRoot/Item");
        foreach (XmlNode item in xmlNodeList)
        {
            listBox2.Items.Add("{[" + item.Attributes["Name"].Value + "]}");
        }
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            foreach (CShape item2 in df.ListAllShowCShape)
            {
                if (item2 is CControl && (((CControl)item2)._c is CDataGridView || ((CControl)item2)._c is CButton || ((CControl)item2)._c is CDateTimePicker || ((CControl)item2)._c is CTextBox || ((CControl)item2)._c is CLabel || ((CControl)item2)._c is CComboBox || ((CControl)item2)._c is CListBox))
                {
                    listBox2.Items.Add("{" + df.name + "." + item2.Name + "}");
                    listBox2.Items.Add("{" + df.name + "." + item2.Name + ".Tag}");
                }
                else if (item2 is CControl && ((CControl)item2)._c is CCheckBox)
                {
                    listBox2.Items.Add("{" + df.name + "." + item2.Name + "}");
                    listBox2.Items.Add("{" + df.name + "." + item2.Name + ".Value}");
                    listBox2.Items.Add("{" + df.name + "." + item2.Name + ".Tag}");
                }
                else if (item2 is CString)
                {
                    listBox2.Items.Add("{" + df.name + "." + item2.Name + "}");
                    listBox2.Items.Add("{" + df.name + "." + item2.Name + ".Tag}");
                }
            }
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (listBox2.SelectedItems != null)
        {
            object[] array = new object[listBox2.SelectedItems.Count];
            listBox2.SelectedItems.CopyTo(array, 0);
            object[] array2 = array;
            foreach (object obj in array2)
            {
                listBox1.Items.Add(obj);
                listBox2.Items.Remove(obj);
            }
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (listBox1.SelectedItems != null)
        {
            object[] array = new object[listBox1.SelectedItems.Count];
            listBox1.SelectedItems.CopyTo(array, 0);
            object[] array2 = array;
            foreach (object obj in array2)
            {
                listBox2.Items.Add(obj);
                listBox1.Items.Remove(obj);
            }
        }
    }

    private void button3_Click(object sender, EventArgs e)
    {
        if (listBox2.Items != null)
        {
            listBox1.Items.AddRange(listBox2.Items);
            listBox2.Items.Clear();
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        if (listBox1.Items != null)
        {
            listBox2.Items.AddRange(listBox1.Items);
            listBox1.Items.Clear();
        }
    }

    private void button5_Click(object sender, EventArgs e)
    {
        toVarStr = "";
        foreach (object item in listBox1.Items)
        {
            toVarStr = toVarStr + item.ToString() + ",";
        }
        toVarStr = toVarStr.Substring(0, toVarStr.Length - 1);
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void button6_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void listBox2_DoubleClick(object sender, EventArgs e)
    {
        if (listBox2.SelectedItems != null)
        {
            object[] array = new object[listBox2.SelectedItems.Count];
            listBox2.SelectedItems.CopyTo(array, 0);
            object[] array2 = array;
            foreach (object obj in array2)
            {
                listBox1.Items.Add(obj);
                listBox2.Items.Remove(obj);
            }
        }
    }

    private void listBox1_DoubleClick(object sender, EventArgs e)
    {
        if (listBox1.SelectedItems != null)
        {
            object[] array = new object[listBox1.SelectedItems.Count];
            listBox1.SelectedItems.CopyTo(array, 0);
            object[] array2 = array;
            foreach (object obj in array2)
            {
                listBox2.Items.Add(obj);
                listBox1.Items.Remove(obj);
            }
        }
    }

    private void InitializeComponent()
    {
        listBox1 = new System.Windows.Forms.ListBox();
        listBox2 = new System.Windows.Forms.ListBox();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        button4 = new System.Windows.Forms.Button();
        button5 = new System.Windows.Forms.Button();
        button6 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        listBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        listBox1.FormattingEnabled = true;
        listBox1.HorizontalScrollbar = true;
        listBox1.ItemHeight = 12;
        listBox1.Location = new System.Drawing.Point(12, 195);
        listBox1.Name = "listBox1";
        listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        listBox1.Size = new System.Drawing.Size(318, 148);
        listBox1.TabIndex = 5;
        listBox1.DoubleClick += new System.EventHandler(listBox1_DoubleClick);
        listBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        listBox2.FormattingEnabled = true;
        listBox2.HorizontalScrollbar = true;
        listBox2.ItemHeight = 12;
        listBox2.Location = new System.Drawing.Point(12, 12);
        listBox2.Name = "listBox2";
        listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        listBox2.Size = new System.Drawing.Size(318, 148);
        listBox2.TabIndex = 0;
        listBox2.DoubleClick += new System.EventHandler(listBox2_DoubleClick);
        button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button1.Location = new System.Drawing.Point(12, 166);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 1;
        button1.Text = "添加项";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button2.Location = new System.Drawing.Point(93, 166);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 2;
        button2.Text = "移除项";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button3.Location = new System.Drawing.Point(174, 166);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(75, 23);
        button3.TabIndex = 3;
        button3.Text = "全选项";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button4.Location = new System.Drawing.Point(255, 166);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(75, 23);
        button4.TabIndex = 4;
        button4.Text = "全移除";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(button4_Click);
        button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        button5.Location = new System.Drawing.Point(59, 366);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(75, 23);
        button5.TabIndex = 6;
        button5.Text = "确定";
        button5.UseVisualStyleBackColor = true;
        button5.Click += new System.EventHandler(button5_Click);
        button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        button6.Location = new System.Drawing.Point(202, 366);
        button6.Name = "button6";
        button6.Size = new System.Drawing.Size(75, 23);
        button6.TabIndex = 7;
        button6.Text = "取消";
        button6.UseVisualStyleBackColor = true;
        button6.Click += new System.EventHandler(button6_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(344, 406);
        base.Controls.Add(button4);
        base.Controls.Add(button3);
        base.Controls.Add(button2);
        base.Controls.Add(button6);
        base.Controls.Add(button5);
        base.Controls.Add(button1);
        base.Controls.Add(listBox2);
        base.Controls.Add(listBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        base.Name = "DBVarForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "绑定设置";
        base.Load += new System.EventHandler(DBVarFrom_Load);
        base.ResumeLayout(false);
    }
}
