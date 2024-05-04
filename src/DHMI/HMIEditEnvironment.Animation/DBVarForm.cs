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
        this.listBox1 = new System.Windows.Forms.ListBox();
        this.listBox2 = new System.Windows.Forms.ListBox();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.button4 = new System.Windows.Forms.Button();
        this.button5 = new System.Windows.Forms.Button();
        this.button6 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        this.listBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.listBox1.FormattingEnabled = true;
        this.listBox1.HorizontalScrollbar = true;
        this.listBox1.ItemHeight = 12;
        this.listBox1.Location = new System.Drawing.Point(12, 195);
        this.listBox1.Name = "listBox1";
        this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        this.listBox1.Size = new System.Drawing.Size(318, 148);
        this.listBox1.TabIndex = 5;
        this.listBox1.DoubleClick += new System.EventHandler(listBox1_DoubleClick);
        this.listBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.listBox2.FormattingEnabled = true;
        this.listBox2.HorizontalScrollbar = true;
        this.listBox2.ItemHeight = 12;
        this.listBox2.Location = new System.Drawing.Point(12, 12);
        this.listBox2.Name = "listBox2";
        this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        this.listBox2.Size = new System.Drawing.Size(318, 148);
        this.listBox2.TabIndex = 0;
        this.listBox2.DoubleClick += new System.EventHandler(listBox2_DoubleClick);
        this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.button1.Location = new System.Drawing.Point(12, 166);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 1;
        this.button1.Text = "添加项";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.button2.Location = new System.Drawing.Point(93, 166);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 2;
        this.button2.Text = "移除项";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.button3.Location = new System.Drawing.Point(174, 166);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(75, 23);
        this.button3.TabIndex = 3;
        this.button3.Text = "全选项";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.button4.Location = new System.Drawing.Point(255, 166);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(75, 23);
        this.button4.TabIndex = 4;
        this.button4.Text = "全移除";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(button4_Click);
        this.button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.button5.Location = new System.Drawing.Point(59, 366);
        this.button5.Name = "button5";
        this.button5.Size = new System.Drawing.Size(75, 23);
        this.button5.TabIndex = 6;
        this.button5.Text = "确定";
        this.button5.UseVisualStyleBackColor = true;
        this.button5.Click += new System.EventHandler(button5_Click);
        this.button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button6.Location = new System.Drawing.Point(202, 366);
        this.button6.Name = "button6";
        this.button6.Size = new System.Drawing.Size(75, 23);
        this.button6.TabIndex = 7;
        this.button6.Text = "取消";
        this.button6.UseVisualStyleBackColor = true;
        this.button6.Click += new System.EventHandler(button6_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(344, 406);
        base.Controls.Add(this.button4);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button6);
        base.Controls.Add(this.button5);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.listBox2);
        base.Controls.Add(this.listBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        base.Name = "DBVarForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "绑定设置";
        base.Load += new System.EventHandler(DBVarFrom_Load);
        base.ResumeLayout(false);
    }
}
