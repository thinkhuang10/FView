using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class czydForm : XtraForm
{
    private readonly CGlobal theglobal;

    private Button button1;

    private Button button2;

    public TextBox textBox1;

    public TextBox textBox3;

    public TextBox textBox4;

    public TextBox textBox5;

    private Button button3;

    public TextBox textBox2;

    private GroupControl groupControl2;

    private Label label7;

    private Label label3;

    private GroupControl groupControl1;

    private Label label8;

    private Label label9;

    private Label label10;

    private PictureBox pictureBox1;

    public czydForm(CGlobal _theglobal)
    {
        InitializeComponent();
        theglobal = _theglobal;
    }

    private void Form4_Load(object sender, EventArgs e)
    {
        try
        {
            textBox3.Text = theglobal.SelectedShapeList[0].czydbianliang;
            textBox1.Text = (theglobal.SelectedShapeList[0].czydxiangsumax * -1f).ToString();
            textBox2.Text = (theglobal.SelectedShapeList[0].czydxiangsumin * -1f).ToString();
            textBox5.Text = theglobal.SelectedShapeList[0].czydzhimin.ToString();
            textBox4.Text = theglobal.SelectedShapeList[0].czydzhimax.ToString();
            if ("0" == textBox1.Text && "0" == textBox2.Text && "0" == textBox5.Text && "0" == textBox4.Text)
            {
                textBox1.Text = "100";
                textBox4.Text = "100";
            }
        }
        catch (Exception)
        {
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (textBox3.Text == "")
        {
            theglobal.SelectedShapeList[0].czyd = false;
            Close();
            return;
        }
        try
        {
            if (Convert.ToDouble(textBox4.Text) == Convert.ToDouble(textBox5.Text))
            {
                MessageBox.Show("上限等于下限,请从新输入");
                return;
            }
        }
        catch (Exception)
        {
            MessageBox.Show("数值输入框内存在不可识别数值,请从新输入");
            return;
        }
        try
        {
            base.DialogResult = DialogResult.OK;
            theglobal.SelectedShapeList[0].czydbianliang = textBox3.Text;
            theglobal.SelectedShapeList[0].czydxiangsumax = Convert.ToInt32(textBox1.Text) * -1;
            theglobal.SelectedShapeList[0].czydxiangsumin = Convert.ToInt32(textBox2.Text) * -1;
            theglobal.SelectedShapeList[0].czydzhimin = Convert.ToInt32(textBox5.Text);
            theglobal.SelectedShapeList[0].czydzhimax = Convert.ToInt32(textBox4.Text);
            if (!theglobal.SelectedShapeList[0].czyd && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].czyd = true;
            }
            Close();
        }
        catch (Exception)
        {
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Cancel;
        Close();
    }

    private void textBox3_Click(object sender, EventArgs e)
    {
    }

    private void button3_Click(object sender, EventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent != "")
        {
            int selectionStart = textBox3.SelectionStart;
            int selectionLength = textBox3.SelectionLength;
            string text = textBox3.Text.Remove(selectionStart, selectionLength);
            textBox3.Text = text.Insert(selectionStart, "[" + varTableEvent + "]");
        }
    }

    private void InitializeComponent()
    {
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        textBox1 = new System.Windows.Forms.TextBox();
        textBox3 = new System.Windows.Forms.TextBox();
        textBox4 = new System.Windows.Forms.TextBox();
        textBox5 = new System.Windows.Forms.TextBox();
        button3 = new System.Windows.Forms.Button();
        textBox2 = new System.Windows.Forms.TextBox();
        groupControl2 = new DevExpress.XtraEditors.GroupControl();
        label7 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        groupControl1 = new DevExpress.XtraEditors.GroupControl();
        label8 = new System.Windows.Forms.Label();
        label9 = new System.Windows.Forms.Label();
        label10 = new System.Windows.Forms.Label();
        pictureBox1 = new System.Windows.Forms.PictureBox();
        ((System.ComponentModel.ISupportInitialize)groupControl2).BeginInit();
        groupControl2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
        groupControl1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(178, 217);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 4;
        button1.Text = "确认";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(275, 217);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 5;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        textBox1.Location = new System.Drawing.Point(254, 33);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(69, 22);
        textBox1.TabIndex = 1;
        textBox1.Text = "100";
        textBox3.HideSelection = false;
        textBox3.Location = new System.Drawing.Point(76, 19);
        textBox3.Name = "textBox3";
        textBox3.Size = new System.Drawing.Size(189, 22);
        textBox3.TabIndex = 0;
        textBox4.Location = new System.Drawing.Point(254, 34);
        textBox4.Name = "textBox4";
        textBox4.Size = new System.Drawing.Size(69, 22);
        textBox4.TabIndex = 1;
        textBox4.Text = "100";
        textBox5.Location = new System.Drawing.Point(90, 34);
        textBox5.Name = "textBox5";
        textBox5.Size = new System.Drawing.Size(69, 22);
        textBox5.TabIndex = 0;
        textBox5.Text = "0";
        button3.Location = new System.Drawing.Point(273, 16);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(87, 27);
        button3.TabIndex = 1;
        button3.Text = "选择变量";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        textBox2.Location = new System.Drawing.Point(90, 33);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(69, 22);
        textBox2.TabIndex = 0;
        textBox2.Text = "0";
        groupControl2.Controls.Add(label7);
        groupControl2.Controls.Add(label3);
        groupControl2.Controls.Add(textBox2);
        groupControl2.Controls.Add(textBox1);
        groupControl2.Location = new System.Drawing.Point(18, 120);
        groupControl2.Name = "groupControl2";
        groupControl2.Size = new System.Drawing.Size(342, 65);
        groupControl2.TabIndex = 3;
        groupControl2.Text = "向上移动像素";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(204, 36);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(43, 14);
        label7.TabIndex = 15;
        label7.Text = "最大值";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(43, 36);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(43, 14);
        label3.TabIndex = 15;
        label3.Text = "最小值";
        groupControl1.Controls.Add(label8);
        groupControl1.Controls.Add(label9);
        groupControl1.Controls.Add(textBox5);
        groupControl1.Controls.Add(textBox4);
        groupControl1.Location = new System.Drawing.Point(18, 49);
        groupControl1.Name = "groupControl1";
        groupControl1.Size = new System.Drawing.Size(342, 65);
        groupControl1.TabIndex = 2;
        groupControl1.Text = "绑定变量";
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(204, 37);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(43, 14);
        label8.TabIndex = 11;
        label8.Text = "最大值";
        label9.AutoSize = true;
        label9.Location = new System.Drawing.Point(43, 37);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(43, 14);
        label9.TabIndex = 10;
        label9.Text = "最小值";
        label10.AutoSize = true;
        label10.Location = new System.Drawing.Point(15, 22);
        label10.Name = "label10";
        label10.Size = new System.Drawing.Size(55, 14);
        label10.TabIndex = 25;
        label10.Text = "绑定变量";
        pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        pictureBox1.Location = new System.Drawing.Point(17, 204);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new System.Drawing.Size(340, 1);
        pictureBox1.TabIndex = 29;
        pictureBox1.TabStop = false;
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(374, 256);
        base.Controls.Add(pictureBox1);
        base.Controls.Add(groupControl2);
        base.Controls.Add(groupControl1);
        base.Controls.Add(label10);
        base.Controls.Add(button3);
        base.Controls.Add(textBox3);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "czydForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "垂直移动";
        base.Load += new System.EventHandler(Form4_Load);
        ((System.ComponentModel.ISupportInitialize)groupControl2).EndInit();
        groupControl2.ResumeLayout(false);
        groupControl2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
        groupControl1.ResumeLayout(false);
        groupControl1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
