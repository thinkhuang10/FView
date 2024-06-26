using CommonSnappableTypes;
using System;
using System.Windows.Forms;

namespace DCCE_Button;

public class UpdownSetForm : Form
{
    private readonly SAVE_UpdownSwitch saveData;

    private Button btSure;

    private Button btCancel;

    private TextBox tbVar;

    private Label label1;

    private Button btVarSel;

    private GroupBox groupBox1;

    private CheckBox cbValueType;

    private Label label2;

    private TextBox tbLowerValue;

    private TextBox tbUpperValue;

    private Label label3;

    public event GetVarTable GetVarTableEvent;

    public UpdownSetForm(SAVE_UpdownSwitch saveData)
    {
        InitializeComponent();
        this.saveData = saveData;
    }

    private void SetForm_Load(object sender, EventArgs e)
    {
        tbVar.Text = saveData.strVar;
        cbValueType.Checked = saveData.bBoolValue;
        if (cbValueType.Checked)
        {
            tbUpperValue.Enabled = false;
            tbLowerValue.Enabled = false;
        }
        tbUpperValue.Text = saveData.iMaxValue.ToString();
        tbLowerValue.Text = saveData.iMinValue.ToString();
    }

    private void btSure_Click(object sender, EventArgs e)
    {
        saveData.strVar = tbVar.Text;
        saveData.bBoolValue = cbValueType.Checked;
        saveData.iMaxValue = tbUpperValue.Text;
        saveData.iMinValue = tbLowerValue.Text;
        Close();
    }

    private void btCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btVarSel_Click(object sender, EventArgs e)
    {
        tbVar.Text = GetVarTableEvent("");
    }

    private void cbValueType_CheckStateChanged(object sender, EventArgs e)
    {
        if (cbValueType.Checked)
        {
            tbUpperValue.Enabled = false;
            tbLowerValue.Enabled = false;
        }
        else
        {
            tbUpperValue.Enabled = true;
            tbLowerValue.Enabled = true;
        }
    }

    private void InitializeComponent()
    {
        btSure = new System.Windows.Forms.Button();
        btCancel = new System.Windows.Forms.Button();
        tbVar = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        btVarSel = new System.Windows.Forms.Button();
        groupBox1 = new System.Windows.Forms.GroupBox();
        label3 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        tbLowerValue = new System.Windows.Forms.TextBox();
        tbUpperValue = new System.Windows.Forms.TextBox();
        cbValueType = new System.Windows.Forms.CheckBox();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        btSure.Location = new System.Drawing.Point(75, 187);
        btSure.Name = "btSure";
        btSure.Size = new System.Drawing.Size(75, 23);
        btSure.TabIndex = 0;
        btSure.Text = "确 定";
        btSure.UseVisualStyleBackColor = true;
        btSure.Click += new System.EventHandler(btSure_Click);
        btCancel.Location = new System.Drawing.Point(156, 187);
        btCancel.Name = "btCancel";
        btCancel.Size = new System.Drawing.Size(75, 23);
        btCancel.TabIndex = 1;
        btCancel.Text = "取 消";
        btCancel.UseVisualStyleBackColor = true;
        btCancel.Click += new System.EventHandler(btCancel_Click);
        tbVar.Location = new System.Drawing.Point(86, 14);
        tbVar.Name = "tbVar";
        tbVar.Size = new System.Drawing.Size(100, 21);
        tbVar.TabIndex = 2;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(15, 18);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 3;
        label1.Text = "变量选择：";
        btVarSel.Location = new System.Drawing.Point(192, 13);
        btVarSel.Name = "btVarSel";
        btVarSel.Size = new System.Drawing.Size(31, 23);
        btVarSel.TabIndex = 4;
        btVarSel.Text = "...";
        btVarSel.UseVisualStyleBackColor = true;
        btVarSel.Click += new System.EventHandler(btVarSel_Click);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(tbLowerValue);
        groupBox1.Controls.Add(tbUpperValue);
        groupBox1.Controls.Add(cbValueType);
        groupBox1.Location = new System.Drawing.Point(12, 48);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(216, 133);
        groupBox1.TabIndex = 5;
        groupBox1.TabStop = false;
        groupBox1.Text = "值绑定";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(23, 102);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(53, 12);
        label3.TabIndex = 7;
        label3.Text = "关时值：";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(23, 64);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(53, 12);
        label2.TabIndex = 6;
        label2.Text = "开时值：";
        tbLowerValue.Location = new System.Drawing.Point(91, 98);
        tbLowerValue.Name = "tbLowerValue";
        tbLowerValue.Size = new System.Drawing.Size(100, 21);
        tbLowerValue.TabIndex = 2;
        tbLowerValue.Text = "0";
        tbUpperValue.Location = new System.Drawing.Point(91, 61);
        tbUpperValue.Name = "tbUpperValue";
        tbUpperValue.Size = new System.Drawing.Size(100, 21);
        tbUpperValue.TabIndex = 1;
        tbUpperValue.Text = "100";
        cbValueType.AutoSize = true;
        cbValueType.Location = new System.Drawing.Point(30, 30);
        cbValueType.Name = "cbValueType";
        cbValueType.Size = new System.Drawing.Size(108, 16);
        cbValueType.TabIndex = 0;
        cbValueType.Text = "设定值为开关量";
        cbValueType.UseVisualStyleBackColor = true;
        cbValueType.CheckStateChanged += new System.EventHandler(cbValueType_CheckStateChanged);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(240, 218);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btVarSel);
        base.Controls.Add(label1);
        base.Controls.Add(tbVar);
        base.Controls.Add(btCancel);
        base.Controls.Add(btSure);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "UpdownSetForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "设置";
        base.Load += new System.EventHandler(SetForm_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
