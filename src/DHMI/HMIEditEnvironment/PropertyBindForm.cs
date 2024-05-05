using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CommonSnappableTypes;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class PropertyBindForm : Form
{
    private CShape selectST;

    private readonly List<ListViewItem> saveitems = new();

    private IContainer components;

    private ListView listView3;

    private ColumnHeader columnHeader5;

    private ColumnHeader columnHeader6;

    private TextBox textBox1;

    private Button button5;

    private Button button6;

    private Button button7;

    private ContextMenuStrip contextMenuStrip1;

    private ToolStripMenuItem 配置控件常用属性PToolStripMenuItem;

    public CShape SelectST
    {
        get
        {
            return selectST;
        }
        set
        {
            selectST = value;
        }
    }

    public PropertyBindForm(CShape st)
    {
        InitializeComponent();
        selectST = st;
    }

    private void PropertyBindForm_Load(object sender, EventArgs e)
    {
        saveitems.Clear();
        listView3.Items.Clear();
        DataTable dataTable = null;
        object c;
        Type type;
        if (selectST is CControl)
        {
            c = (selectST as CControl)._c;
            type = c.GetType();
            dataTable = CEditEnvironmentGlobal.controlMembersSetting.Tables[type.Name];
        }
        else
        {
            c = selectST;
            type = c.GetType();
        }
        if (type == null && dataTable == null)
        {
            return;
        }
        PropertyInfo[] properties = type.GetProperties();
        List<string> list = new();
        bool flag = true;
        if (dataTable == null)
        {
            flag = false;
        }
        else
        {
            DataRow[] array = dataTable.Select("Type='属性'");
            for (int i = 0; i < array.Length; i++)
            {
                if (Convert.ToBoolean(array[i]["Visible"]))
                {
                    list.Add(array[i]["Name"].ToString());
                }
            }
        }
        List<string> list2 = new();
        PropertyInfo[] array2 = properties;
        foreach (PropertyInfo propertyInfo in array2)
        {
            if (list2.Contains(propertyInfo.Name))
            {
                continue;
            }
            try
            {
                if (Attribute.IsDefined(propertyInfo, typeof(DHMIHidePropertyAttribute)))
                {
                    continue;
                }
                ListViewItem listViewItem = new(new string[2] { propertyInfo.Name, "" }, 0);
                if (propertyInfo.DeclaringType == type)
                {
                    listViewItem.SubItems[0].Font = new Font(listViewItem.SubItems[0].Font, FontStyle.Bold);
                }
                listViewItem.ToolTipText = propertyInfo.Name;
                try
                {
                    if (Attribute.IsDefined(propertyInfo, typeof(DisplayNameAttribute)))
                    {
                        listViewItem.ToolTipText = ((DisplayNameAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DisplayNameAttribute))).DisplayName;
                    }
                }
                catch
                {
                }
                try
                {
                    if (Attribute.IsDefined(propertyInfo, typeof(DescriptionAttribute)))
                    {
                        listViewItem.ToolTipText = listViewItem.ToolTipText + ":" + ((DescriptionAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DescriptionAttribute))).Description;
                    }
                }
                catch
                {
                }
                object value = propertyInfo.GetValue(c, null);
                listViewItem.ForeColor = Color.Silver;
                listViewItem.SubItems[1].Tag = ((value == null) ? "null" : value.ToString());
                listViewItem.SubItems[1].Text = (string)listViewItem.SubItems[1].Tag;
                string text = "杂项";
                try
                {
                    if (Attribute.IsDefined(propertyInfo, typeof(CategoryAttribute)))
                    {
                        text = ((CategoryAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(CategoryAttribute))).Category;
                    }
                }
                catch (Exception)
                {
                }
                ListViewGroup listViewGroup = new();
                foreach (ListViewGroup group in listView3.Groups)
                {
                    if (group.Name == text)
                    {
                        listViewGroup = group;
                    }
                }
                if (listViewGroup.Name != text)
                {
                    listViewGroup.Name = text;
                    listViewGroup.Header = text;
                    listView3.Groups.Add(listViewGroup);
                }
                listViewItem.Group = listViewGroup;
                listViewItem.Tag = propertyInfo.PropertyType.AssemblyQualifiedName;
                saveitems.Add(listViewItem);
                if (!flag || list.Contains(propertyInfo.Name))
                {
                    listView3.Items.Add(listViewItem);
                    list2.Add(propertyInfo.Name);
                }
            }
            catch (Exception)
            {
            }
        }
        if (selectST.propertyBindDT == null)
        {
            DataTable dataTable2 = new();
            dataTable2.Columns.Add("PropertyName");
            dataTable2.Columns.Add("Type");
            dataTable2.Columns.Add("Bind");
            selectST.propertyBindDT = dataTable2;
        }
        foreach (DataRow row in selectST.propertyBindDT.Rows)
        {
            foreach (ListViewItem saveitem in saveitems)
            {
                if (row["PropertyName"].ToString() == saveitem.Text && row["Bind"] != null)
                {
                    saveitem.SubItems[1].Text = row["Bind"].ToString();
                    saveitem.ForeColor = Color.Black;
                }
            }
        }
    }

    private void button7_Click(object sender, EventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent != "")
        {
            textBox1.Text = varTableEvent;
        }
    }

    private void button5_Click(object sender, EventArgs e)
    {
        DataTable dataTable = new();
        dataTable.Columns.Add("PropertyName");
        dataTable.Columns.Add("Type");
        dataTable.Columns.Add("Bind");
        foreach (ListViewItem saveitem in saveitems)
        {
            if (saveitem.SubItems[1].Text != null && saveitem.ForeColor == Color.Black && saveitem.SubItems[1].Text.Trim() != "")
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["PropertyName"] = saveitem.Text;
                dataRow["Type"] = saveitem.Tag;
                dataRow["Bind"] = saveitem.SubItems[1].Text;
                dataTable.Rows.Add(dataRow);
            }
        }
        selectST.propertyBindDT = dataTable;
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void button6_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void listView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        textBox1.Hide();
        button7.Hide();
    }

    private void listView3_DoubleClick(object sender, EventArgs e)
    {
        if (listView3.SelectedItems.Count != 0)
        {
            if (listView3.SelectedItems[0].ForeColor == Color.Black)
            {
                textBox1.Text = listView3.SelectedItems[0].SubItems[1].Text;
            }
            else
            {
                textBox1.Text = "";
            }
            textBox1.Location = new Point(listView3.Location.X + listView3.SelectedItems[0].Position.X + listView3.Columns[0].Width + 1, listView3.Location.Y + listView3.SelectedItems[0].Position.Y);
            textBox1.Size = new Size(listView3.SelectedItems[0].GetBounds(ItemBoundsPortion.Entire).Width - listView3.Columns[0].Width - 2 - 25, listView3.SelectedItems[0].GetBounds(ItemBoundsPortion.Entire).Height);
            textBox1.Font = listView3.SelectedItems[0].Font;
            button7.Location = new Point(textBox1.Location.X + listView3.SelectedItems[0].GetBounds(ItemBoundsPortion.Entire).Width - listView3.Columns[0].Width - 2 - 25, textBox1.Location.Y);
            button7.Size = new Size(25, textBox1.Height);
            button7.Font = textBox1.Font;
            textBox1.Show();
            button7.Show();
        }
    }

    private void textBox1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            textBox1.Hide();
            button7.Hide();
        }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        if (listView3.SelectedItems.Count != 0)
        {
            if (textBox1.Text != "")
            {
                listView3.SelectedItems[0].ForeColor = Color.Black;
                listView3.SelectedItems[0].SubItems[1].Text = textBox1.Text;
            }
            else
            {
                listView3.SelectedItems[0].ForeColor = Color.Silver;
                listView3.SelectedItems[0].SubItems[1].Text = (string)listView3.SelectedItems[0].SubItems[1].Tag;
            }
        }
    }

    private void listView3_KeyDown(object sender, KeyEventArgs e)
    {
        if (listView3.SelectedItems.Count != 0 && e.KeyCode == Keys.Delete)
        {
            listView3.SelectedItems[0].SubItems[1].Text = "";
        }
    }

    private void listView3_MouseClick(object sender, MouseEventArgs e)
    {
        textBox1.Hide();
        button7.Hide();
    }

    private void 配置控件常用属性PToolStripMenuItem_Click(object sender, EventArgs e)
    {
        object obj = ((selectST is not CControl) ? ((object)selectST) : ((object)(selectST as CControl)._c));
        Type type = obj.GetType();
        ControlInfoSetting controlInfoSetting = new(type.Name);
        controlInfoSetting.ShowDialog();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.listView3 = new System.Windows.Forms.ListView();
        this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.配置控件常用属性PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.button5 = new System.Windows.Forms.Button();
        this.button6 = new System.Windows.Forms.Button();
        this.button7 = new System.Windows.Forms.Button();
        this.contextMenuStrip1.SuspendLayout();
        base.SuspendLayout();
        this.listView3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[2] { this.columnHeader5, this.columnHeader6 });
        this.listView3.ContextMenuStrip = this.contextMenuStrip1;
        this.listView3.FullRowSelect = true;
        this.listView3.GridLines = true;
        this.listView3.HideSelection = false;
        this.listView3.Location = new System.Drawing.Point(12, 16);
        this.listView3.MultiSelect = false;
        this.listView3.Name = "listView3";
        this.listView3.ShowItemToolTips = true;
        this.listView3.Size = new System.Drawing.Size(415, 398);
        this.listView3.TabIndex = 2;
        this.listView3.UseCompatibleStateImageBehavior = false;
        this.listView3.View = System.Windows.Forms.View.Details;
        this.listView3.SelectedIndexChanged += new System.EventHandler(listView3_SelectedIndexChanged);
        this.listView3.DoubleClick += new System.EventHandler(listView3_DoubleClick);
        this.listView3.KeyDown += new System.Windows.Forms.KeyEventHandler(listView3_KeyDown);
        this.listView3.MouseClick += new System.Windows.Forms.MouseEventHandler(listView3_MouseClick);
        this.columnHeader5.Text = "属性名";
        this.columnHeader5.Width = 120;
        this.columnHeader6.Text = "绑定";
        this.columnHeader6.Width = 281;
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.配置控件常用属性PToolStripMenuItem });
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new System.Drawing.Size(188, 26);
        this.配置控件常用属性PToolStripMenuItem.Name = "配置控件常用属性PToolStripMenuItem";
        this.配置控件常用属性PToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
        this.配置控件常用属性PToolStripMenuItem.Text = "配置控件常用属性(&P)";
        this.配置控件常用属性PToolStripMenuItem.Click += new System.EventHandler(配置控件常用属性PToolStripMenuItem_Click);
        this.textBox1.ContextMenuStrip = this.contextMenuStrip1;
        this.textBox1.Location = new System.Drawing.Point(42, 106);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(266, 21);
        this.textBox1.TabIndex = 4;
        this.textBox1.Visible = false;
        this.textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
        this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
        this.button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.button5.ContextMenuStrip = this.contextMenuStrip1;
        this.button5.Location = new System.Drawing.Point(83, 420);
        this.button5.Name = "button5";
        this.button5.Size = new System.Drawing.Size(75, 23);
        this.button5.TabIndex = 5;
        this.button5.Text = "保存";
        this.button5.UseVisualStyleBackColor = true;
        this.button5.Click += new System.EventHandler(button5_Click);
        this.button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button6.ContextMenuStrip = this.contextMenuStrip1;
        this.button6.Location = new System.Drawing.Point(280, 420);
        this.button6.Name = "button6";
        this.button6.Size = new System.Drawing.Size(75, 23);
        this.button6.TabIndex = 5;
        this.button6.Text = "关闭";
        this.button6.UseVisualStyleBackColor = true;
        this.button6.Click += new System.EventHandler(button6_Click);
        this.button7.ContextMenuStrip = this.contextMenuStrip1;
        this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.button7.Location = new System.Drawing.Point(324, 105);
        this.button7.Name = "button7";
        this.button7.Size = new System.Drawing.Size(41, 20);
        this.button7.TabIndex = 3;
        this.button7.Text = "...";
        this.button7.UseVisualStyleBackColor = true;
        this.button7.Visible = false;
        this.button7.Click += new System.EventHandler(button7_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(439, 452);
        base.Controls.Add(this.button6);
        base.Controls.Add(this.button5);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.button7);
        base.Controls.Add(this.listView3);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "PropertyBindForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "属性绑定";
        base.Load += new System.EventHandler(PropertyBindForm_Load);
        this.contextMenuStrip1.ResumeLayout(false);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
