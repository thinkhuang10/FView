using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class EventEditSelectMethod : Form
{
    private Label label3;

    private Label label4;

    private Label label1;

    private ComboBox comboBox3;

    private ComboBox comboBox2;

    private ComboBox comboBox1;

    private Button button1;

    private Button button2;

    public EventEditSelectMethod()
    {
        InitializeComponent();
    }

    private void EventEditSelectMethod_Load(object sender, EventArgs e)
    {
        comboBox3.Items.Add(new KVPart<string, DataFile>("运行环境", null));
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            comboBox3.Items.Add(new KVPart<string, DataFile>(df.pageName, df));
        }
        comboBox3.Text = CEditEnvironmentGlobal.childform.theglobal.df.pageName;
        if (comboBox3.SelectedIndex == -1 && comboBox3.Items.Count > 0)
        {
            comboBox3.SelectedIndex = 0;
        }
    }

    private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataFile value = ((KVPart<string, DataFile>)comboBox3.SelectedItem).Value;
        if (value != null)
        {
            comboBox2.Items.Clear();
            foreach (CShape item in value.ListAllShowCShape)
            {
                comboBox2.Items.Add(new KVPart<string, CShape>(item.Name, item));
            }
            if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = 0;
            }
        }
        else
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add(new KVPart<string, CShape>("全局", null));
        }
    }

    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        CShape value = ((KVPart<string, CShape>)comboBox2.SelectedItem).Value;
        object obj = ((value == null) ? new ClassForScript() : ((value is not CControl) ? ((object)value) : ((object)(value as CControl)._c)));
        Type type = obj.GetType();
        MethodInfo[] methods = type.GetMethods();
        List<string> list = new();
        DataTable dataTable = null;
        if (value != null)
        {
            dataTable = ((value is not CControl) ? CEditEnvironmentGlobal.controlMembersSetting.Tables[type.Name] : CEditEnvironmentGlobal.controlMembersSetting.Tables[type.Name]);
        }
        bool flag = true;
        if (dataTable == null)
        {
            flag = false;
        }
        else
        {
            DataRow[] array = dataTable.Select("Type='方法'");
            for (int i = 0; i < array.Length; i++)
            {
                if (Convert.ToBoolean(array[i]["Visible"]))
                {
                    list.Add(array[i]["Name"].ToString());
                }
            }
        }
        comboBox1.Items.Clear();
        MethodInfo[] array2 = methods;
        foreach (MethodInfo methodInfo in array2)
        {
            if ((!flag || list.Contains(methodInfo.Name)) && !methodInfo.Name.StartsWith("get_") && !methodInfo.Name.StartsWith("set_") && !methodInfo.Name.StartsWith("remove_") && !methodInfo.Name.StartsWith("add_"))
            {
                string text = "";
                ParameterInfo[] parameters = methodInfo.GetParameters();
                ParameterInfo[] array3 = parameters;
                foreach (ParameterInfo parameterInfo in array3)
                {
                    string text2 = text;
                    text = text2 + parameterInfo.ParameterType.Name + " " + parameterInfo.Name + ",";
                }
                if (text.EndsWith(","))
                {
                    text = text.Substring(0, text.Length - 1);
                }
                comboBox1.Items.Add(new KVPart<string, MethodInfo>(methodInfo.ReturnType.Name + " " + methodInfo.Name + "(" + text + ")", methodInfo));
            }
        }
        if (comboBox1.Items.Count > 0)
        {
            comboBox1.SelectedIndex = 0;
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Cancel;
        Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
    }

    private void InitializeComponent()
    {
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.comboBox3 = new System.Windows.Forms.ComboBox();
        this.comboBox2 = new System.Windows.Forms.ComboBox();
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(12, 9);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(29, 12);
        this.label3.TabIndex = 13;
        this.label3.Text = "页面";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(211, 9);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(29, 12);
        this.label4.TabIndex = 14;
        this.label4.Text = "控件";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(12, 36);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(29, 12);
        this.label1.TabIndex = 15;
        this.label1.Text = "方法";
        this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox3.FormattingEnabled = true;
        this.comboBox3.Location = new System.Drawing.Point(59, 6);
        this.comboBox3.Name = "comboBox3";
        this.comboBox3.Size = new System.Drawing.Size(146, 20);
        this.comboBox3.TabIndex = 10;
        this.comboBox3.SelectedIndexChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
        this.comboBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox2.FormattingEnabled = true;
        this.comboBox2.Location = new System.Drawing.Point(246, 6);
        this.comboBox2.Name = "comboBox2";
        this.comboBox2.Size = new System.Drawing.Size(222, 20);
        this.comboBox2.TabIndex = 11;
        this.comboBox2.SelectedIndexChanged += new System.EventHandler(comboBox2_SelectedIndexChanged);
        this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Location = new System.Drawing.Point(59, 33);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new System.Drawing.Size(409, 20);
        this.comboBox1.TabIndex = 12;
        this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button1.Location = new System.Drawing.Point(305, 65);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 16;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button2.Location = new System.Drawing.Point(393, 65);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 16;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(480, 100);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.comboBox3);
        base.Controls.Add(this.comboBox2);
        base.Controls.Add(this.comboBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "EventEditSelectMethod";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "调用控件方法";
        base.Load += new System.EventHandler(EventEditSelectMethod_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
