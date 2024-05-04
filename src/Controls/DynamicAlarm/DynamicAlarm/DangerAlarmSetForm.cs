using CommonSnappableTypes;
using System;
using System.Windows.Forms;

namespace DynamicAlarm;

public class DangerAlarmSetForm : Form
{
    private readonly SAVE_DangerAlarm saveData;

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

    private CheckBox cbTwinkle;

    public event GetVarTable GetVarTableEvent;

    public DangerAlarmSetForm(SAVE_DangerAlarm saveData)
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
        cbTwinkle.Checked = saveData.bTwinkle;
    }

    private void btSure_Click(object sender, EventArgs e)
    {
        saveData.strVar = tbVar.Text;
        saveData.bBoolValue = cbValueType.Checked;
        saveData.iMaxValue = tbUpperValue.Text;
        saveData.iMinValue = tbLowerValue.Text;
        saveData.bTwinkle = cbTwinkle.Checked;
        Close();
    }

    private void btCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btVarSel_Click(object sender, EventArgs e)
    {
        tbVar.Text = this.GetVarTableEvent("");
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
        this.btSure = new System.Windows.Forms.Button();
        this.btCancel = new System.Windows.Forms.Button();
        this.tbVar = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.btVarSel = new System.Windows.Forms.Button();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.cbTwinkle = new System.Windows.Forms.CheckBox();
        this.label3 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.tbLowerValue = new System.Windows.Forms.TextBox();
        this.tbUpperValue = new System.Windows.Forms.TextBox();
        this.cbValueType = new System.Windows.Forms.CheckBox();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.btSure.Location = new System.Drawing.Point(54, 187);
        this.btSure.Name = "btSure";
        this.btSure.Size = new System.Drawing.Size(75, 23);
        this.btSure.TabIndex = 0;
        this.btSure.Text = "确 定";
        this.btSure.UseVisualStyleBackColor = true;
        this.btSure.Click += new System.EventHandler(btSure_Click);
        this.btCancel.Location = new System.Drawing.Point(145, 187);
        this.btCancel.Name = "btCancel";
        this.btCancel.Size = new System.Drawing.Size(75, 23);
        this.btCancel.TabIndex = 1;
        this.btCancel.Text = "取 消";
        this.btCancel.UseVisualStyleBackColor = true;
        this.btCancel.Click += new System.EventHandler(btCancel_Click);
        this.tbVar.Location = new System.Drawing.Point(86, 14);
        this.tbVar.Name = "tbVar";
        this.tbVar.Size = new System.Drawing.Size(133, 21);
        this.tbVar.TabIndex = 2;
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(15, 18);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 3;
        this.label1.Text = "变量选择：";
        this.btVarSel.Location = new System.Drawing.Point(225, 13);
        this.btVarSel.Name = "btVarSel";
        this.btVarSel.Size = new System.Drawing.Size(31, 23);
        this.btVarSel.TabIndex = 4;
        this.btVarSel.Text = "...";
        this.btVarSel.UseVisualStyleBackColor = true;
        this.btVarSel.Click += new System.EventHandler(btVarSel_Click);
        this.groupBox1.Controls.Add(this.cbTwinkle);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.tbLowerValue);
        this.groupBox1.Controls.Add(this.tbUpperValue);
        this.groupBox1.Controls.Add(this.cbValueType);
        this.groupBox1.Location = new System.Drawing.Point(16, 48);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(244, 133);
        this.groupBox1.TabIndex = 5;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "值绑定";
        this.cbTwinkle.AutoSize = true;
        this.cbTwinkle.Location = new System.Drawing.Point(147, 30);
        this.cbTwinkle.Name = "cbTwinkle";
        this.cbTwinkle.Size = new System.Drawing.Size(72, 16);
        this.cbTwinkle.TabIndex = 8;
        this.cbTwinkle.Text = "是否闪烁";
        this.cbTwinkle.UseVisualStyleBackColor = true;
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(34, 103);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(77, 12);
        this.label3.TabIndex = 7;
        this.label3.Text = "报警下限值：";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(34, 65);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(77, 12);
        this.label2.TabIndex = 6;
        this.label2.Text = "报警上限值：";
        this.tbLowerValue.Location = new System.Drawing.Point(113, 98);
        this.tbLowerValue.Name = "tbLowerValue";
        this.tbLowerValue.Size = new System.Drawing.Size(100, 21);
        this.tbLowerValue.TabIndex = 2;
        this.tbLowerValue.Text = "0";
        this.tbUpperValue.Location = new System.Drawing.Point(113, 61);
        this.tbUpperValue.Name = "tbUpperValue";
        this.tbUpperValue.Size = new System.Drawing.Size(100, 21);
        this.tbUpperValue.TabIndex = 1;
        this.tbUpperValue.Text = "100";
        this.cbValueType.AutoSize = true;
        this.cbValueType.Location = new System.Drawing.Point(30, 30);
        this.cbValueType.Name = "cbValueType";
        this.cbValueType.Size = new System.Drawing.Size(108, 16);
        this.cbValueType.TabIndex = 0;
        this.cbValueType.Text = "报警值为开关量";
        this.cbValueType.UseVisualStyleBackColor = true;
        this.cbValueType.CheckStateChanged += new System.EventHandler(cbValueType_CheckStateChanged);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(276, 218);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btVarSel);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.tbVar);
        base.Controls.Add(this.btCancel);
        base.Controls.Add(this.btSure);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "CycleAlarmSetForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "设置";
        base.Load += new System.EventHandler(SetForm_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
