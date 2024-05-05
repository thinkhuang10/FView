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
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        comboBox3 = new System.Windows.Forms.ComboBox();
        comboBox2 = new System.Windows.Forms.ComboBox();
        comboBox1 = new System.Windows.Forms.ComboBox();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(12, 9);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(29, 12);
        label3.TabIndex = 13;
        label3.Text = "页面";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(211, 9);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(29, 12);
        label4.TabIndex = 14;
        label4.Text = "控件";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(12, 36);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(29, 12);
        label1.TabIndex = 15;
        label1.Text = "方法";
        comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox3.FormattingEnabled = true;
        comboBox3.Location = new System.Drawing.Point(59, 6);
        comboBox3.Name = "comboBox3";
        comboBox3.Size = new System.Drawing.Size(146, 20);
        comboBox3.TabIndex = 10;
        comboBox3.SelectedIndexChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
        comboBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox2.FormattingEnabled = true;
        comboBox2.Location = new System.Drawing.Point(246, 6);
        comboBox2.Name = "comboBox2";
        comboBox2.Size = new System.Drawing.Size(222, 20);
        comboBox2.TabIndex = 11;
        comboBox2.SelectedIndexChanged += new System.EventHandler(comboBox2_SelectedIndexChanged);
        comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new System.Drawing.Point(59, 33);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(409, 20);
        comboBox1.TabIndex = 12;
        button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button1.Location = new System.Drawing.Point(305, 65);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 16;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button2.Location = new System.Drawing.Point(393, 65);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 16;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(480, 100);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.Controls.Add(label3);
        base.Controls.Add(label4);
        base.Controls.Add(label1);
        base.Controls.Add(comboBox3);
        base.Controls.Add(comboBox2);
        base.Controls.Add(comboBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "EventEditSelectMethod";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "调用控件方法";
        base.Load += new System.EventHandler(EventEditSelectMethod_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
