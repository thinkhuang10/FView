using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class BSet : Form
{
    public int pt2 = 2;

    private string errorstr;

    public float MinValue;

    public float MaxValue = 300f;

    public string VarName = "";

    public int MainMarkCount = 10;

    public int OtherMarkCount = 1;

    public Color Bgcolor = Color.LightGray;

    public Color nmlcolor = Color.Green;

    public Color BiaoqianColor = Color.Blue;

    public Color warncolor = Color.Yellow;

    public Color Errorcolor = Color.Red;

    public Color TxtColor = Color.Black;

    public Color MarkColor = Color.Black;

    public float nmlsta = 120f;

    public float nmlend = 180f;

    public float warnsta1 = 60f;

    public float warnsta2 = 180f;

    public float warnend1 = 120f;

    public float warnend2 = 240f;

    public float errorsta1;

    public float errorsta2 = 240f;

    public float errorend1 = 60f;

    public float errorend2 = 300f;

    public bool lbHide;

    public bool markHide;

    public string Mark = "仪表";

    private int closeflag = 1;

    private TextBox txt_var;

    private Button btn_view;

    private GroupBox groupBox1;

    private Label label1;

    private Label label4;

    private TextBox txt_varjd1;

    private Label label3;

    private TextBox txt_bq;

    private Label lbl_bgcolor;

    private Label lbl_markcolor;

    private Label label5;

    private GroupBox groupBox2;

    private TextBox txt_minmark;

    private TextBox txt_maxmark;

    private Label label8;

    private Label label7;

    private GroupBox groupBox3;

    private TextBox txt_nmlend;

    private Label label11;

    private TextBox txt_nmlsta;

    private Label label10;

    private Label label9;

    private GroupBox groupBox4;

    private Label lbl_warncolor;

    private Label label17;

    private TextBox txt_warnend1;

    private Label label18;

    private TextBox txt_warnsta2;

    private TextBox txt_warnsta1;

    private Label label15;

    private Label label14;

    private CheckBox ckb_warn2;

    private CheckBox ckb_warn1;

    private Label lbl_nmlcolor;

    private Label label12;

    private TextBox txt_warnend2;

    private Label label19;

    private GroupBox groupBox5;

    private TextBox txt_errorend2;

    private Label label13;

    private Label lbl_errorcolor;

    private Label label20;

    private TextBox txt_errorend1;

    private Label label21;

    private TextBox txt_errorsta2;

    private Label label22;

    private Label label23;

    private CheckBox ckb_error2;

    private CheckBox ckb_error1;

    private Button btn_OK;

    private Button btn_cancel;

    private GroupBox groupBox6;

    private TextBox txt_varjd2;

    private Label label25;

    private Label lbl_txtcolor;

    private Label label16;

    private Label label27;

    private Label label26;

    private TextBox txt_othercount;

    private TextBox txt_maincount;

    private TextBox txt_errorsta1;

    private ColorDialog colorDialog1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public BSet()
    {
        InitializeComponent();
    }

    public new DialogResult ShowDialog()
    {
        return base.ShowDialog();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ckvarevent(txt_var.Text))
            {
                pt2 = Convert.ToInt32(txt_varjd2.Text);
                MaxValue = Convert.ToSingle(txt_maxmark.Text);
                MinValue = Convert.ToSingle(txt_minmark.Text);
                MainMarkCount = Convert.ToInt32(txt_maincount.Text);
                OtherMarkCount = Convert.ToInt32(txt_othercount.Text);
                VarName = "[" + txt_var.Text + "]";
                Bgcolor = lbl_bgcolor.BackColor;
                nmlcolor = lbl_nmlcolor.BackColor;
                warncolor = lbl_warncolor.BackColor;
                Errorcolor = lbl_errorcolor.BackColor;
                nmlsta = Convert.ToSingle(txt_nmlsta.Text);
                nmlend = Convert.ToSingle(txt_nmlend.Text);
                warnsta1 = Convert.ToSingle(txt_warnsta1.Text);
                warnsta2 = Convert.ToSingle(txt_warnsta2.Text);
                warnend1 = Convert.ToSingle(txt_warnend1.Text);
                warnend2 = Convert.ToSingle(txt_warnend2.Text);
                errorsta1 = Convert.ToSingle(txt_errorsta1.Text);
                errorsta2 = Convert.ToSingle(txt_errorsta2.Text);
                errorend1 = Convert.ToSingle(txt_errorend1.Text);
                errorend2 = Convert.ToSingle(txt_errorend2.Text);
                Mark = txt_bq.Text;
                BiaoqianColor = lbl_markcolor.BackColor;
                TxtColor = lbl_txtcolor.BackColor;
                checkinput();
                if (!string.IsNullOrEmpty(errorstr) && errorstr != "区间有重叠！")
                {
                    MessageBox.Show(errorstr);
                    errorstr = "";
                    closeflag = 0;
                }
                else if (errorstr == "区间有重叠！")
                {
                    MessageBox.Show(errorstr);
                    errorstr = "";
                    closeflag = 1;
                }
                else
                {
                    closeflag = 1;
                }
                if (closeflag == 1)
                {
                    base.DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("变量名称错误！");
            }
        }
        catch
        {
            MessageBox.Show("出现异常，可能是您填写的文本有问题！");
        }
    }

    private void checkinput()
    {
        try
        {
            if (Convert.ToInt32(txt_varjd2.Text) > 10 || Convert.ToInt32(txt_varjd2.Text) < 0)
            {
                errorstr = "小数位数不能超过10或小于0";
            }
            else if (txt_var.Text == "")
            {
                errorstr = "绑定变量值不能为空！";
            }
            else if (ckb_warn1.Checked && (txt_warnend1.Text == "" || txt_warnsta1.Text == ""))
            {
                errorstr = "警告区间1不完整！";
            }
            else if (ckb_warn2.Checked && (txt_warnend2.Text == "" || txt_warnsta2.Text == ""))
            {
                errorstr = "警告区间1不完整！";
            }
            else if (ckb_error1.Checked && (txt_errorend1.Text == "" || txt_errorsta1.Text == ""))
            {
                errorstr = "异常区间1不完整！";
            }
            else if (ckb_error2.Checked && (txt_errorend2.Text == "" || txt_errorsta2.Text == ""))
            {
                errorstr = "异常区间2不完整！";
            }
            else if (txt_nmlsta.Text == "" || txt_nmlend.Text == "")
            {
                errorstr = "正常区间不完整！";
            }
            else if (txt_othercount.Text == "" || txt_maincount.Text == "")
            {
                errorstr = "主刻度数或副刻度数不能为空！";
            }
            else if (txt_maxmark.Text == "" || txt_minmark.Text == "")
            {
                errorstr = "最大值或最小值不能为空！";
            }
            else if (Convert.ToSingle(txt_errorsta2.Text) > Convert.ToSingle(txt_errorend2.Text) || Convert.ToSingle(txt_errorsta1.Text) > Convert.ToSingle(txt_errorend1.Text) || Convert.ToSingle(txt_warnsta1.Text) > Convert.ToSingle(txt_warnend1.Text) || Convert.ToSingle(txt_warnsta2.Text) > Convert.ToSingle(txt_warnend2.Text) || Convert.ToSingle(txt_nmlsta.Text) > Convert.ToSingle(txt_nmlend.Text))
            {
                errorstr = "区间的最小值不能大于区间最大值！";
            }
            else if (Convert.ToSingle(txt_nmlsta.Text) < MinValue || Convert.ToSingle(txt_nmlend.Text) > MaxValue || Convert.ToSingle(txt_errorsta1.Text) < MinValue || Convert.ToSingle(txt_errorsta2.Text) < MinValue || Convert.ToSingle(txt_warnsta1.Text) < MinValue || Convert.ToSingle(txt_warnsta2.Text) < MinValue || Convert.ToSingle(txt_warnend1.Text) > MaxValue || Convert.ToSingle(txt_warnend2.Text) > MaxValue || Convert.ToSingle(txt_errorend1.Text) > MaxValue || Convert.ToSingle(txt_errorend2.Text) > MaxValue)
            {
                errorstr = "区间数值有错误！";
            }
            else if (MainMarkCount < 0 || OtherMarkCount < 0)
            {
                errorstr = "刻度数不能小于零！";
            }
            else if ((Convert.ToSingle(txt_nmlsta.Text) > Convert.ToSingle(txt_warnsta1.Text) && Convert.ToSingle(txt_nmlsta.Text) < Convert.ToSingle(txt_warnend1.Text)) || (Convert.ToSingle(txt_nmlsta.Text) > Convert.ToSingle(txt_warnsta2.Text) && Convert.ToSingle(txt_nmlsta.Text) < Convert.ToSingle(txt_warnend2.Text)) || (Convert.ToSingle(txt_nmlsta.Text) > Convert.ToSingle(txt_errorsta1.Text) && Convert.ToSingle(txt_nmlsta.Text) < Convert.ToSingle(txt_errorend1.Text)) || (Convert.ToSingle(txt_nmlsta.Text) > Convert.ToSingle(txt_errorsta2.Text) && Convert.ToSingle(txt_nmlsta.Text) < Convert.ToSingle(txt_errorend2.Text)))
            {
                errorstr = "区间有重叠！";
            }
            else if ((Convert.ToSingle(txt_nmlend.Text) > Convert.ToSingle(txt_warnsta1.Text) && Convert.ToSingle(txt_nmlend.Text) < Convert.ToSingle(txt_warnend1.Text)) || (Convert.ToSingle(txt_nmlend.Text) > Convert.ToSingle(txt_warnsta2.Text) && Convert.ToSingle(txt_nmlend.Text) < Convert.ToSingle(txt_warnend2.Text)) || (Convert.ToSingle(txt_nmlend.Text) > Convert.ToSingle(txt_errorsta1.Text) && Convert.ToSingle(txt_nmlend.Text) < Convert.ToSingle(txt_errorend1.Text)) || (Convert.ToSingle(txt_nmlend.Text) > Convert.ToSingle(txt_errorsta2.Text) && Convert.ToSingle(txt_nmlend.Text) < Convert.ToSingle(txt_errorend2.Text)))
            {
                errorstr = "区间有重叠！";
            }
            else if ((Convert.ToSingle(txt_warnsta1.Text) > Convert.ToSingle(txt_nmlsta.Text) && Convert.ToSingle(txt_warnsta1.Text) < Convert.ToSingle(txt_nmlend.Text)) || (Convert.ToSingle(txt_warnsta1.Text) > Convert.ToSingle(txt_warnsta2.Text) && Convert.ToSingle(txt_warnsta1.Text) < Convert.ToSingle(txt_warnend2.Text)) || (Convert.ToSingle(txt_warnsta1.Text) > Convert.ToSingle(txt_errorsta1.Text) && Convert.ToSingle(txt_warnsta1.Text) < Convert.ToSingle(txt_errorend1.Text)) || (Convert.ToSingle(txt_warnsta1.Text) > Convert.ToSingle(txt_errorsta2.Text) && Convert.ToSingle(txt_warnsta1.Text) < Convert.ToSingle(txt_errorend2.Text)))
            {
                errorstr = "区间有重叠！";
            }
            else if ((Convert.ToSingle(txt_warnend1.Text) > Convert.ToSingle(txt_nmlsta.Text) && Convert.ToSingle(txt_warnend1.Text) < Convert.ToSingle(txt_nmlend.Text)) || (Convert.ToSingle(txt_warnend1.Text) > Convert.ToSingle(txt_warnsta2.Text) && Convert.ToSingle(txt_warnend1.Text) < Convert.ToSingle(txt_warnend2.Text)) || (Convert.ToSingle(txt_warnend1.Text) > Convert.ToSingle(txt_errorsta1.Text) && Convert.ToSingle(txt_warnend1.Text) < Convert.ToSingle(txt_errorend1.Text)) || (Convert.ToSingle(txt_warnend1.Text) > Convert.ToSingle(txt_errorsta2.Text) && Convert.ToSingle(txt_warnend1.Text) < Convert.ToSingle(txt_errorend2.Text)))
            {
                errorstr = "区间有重叠！";
            }
            else if ((Convert.ToSingle(txt_warnsta2.Text) > Convert.ToSingle(txt_nmlsta.Text) && Convert.ToSingle(txt_warnsta2.Text) < Convert.ToSingle(txt_nmlend.Text)) || (Convert.ToSingle(txt_warnsta2.Text) > Convert.ToSingle(txt_warnsta1.Text) && Convert.ToSingle(txt_warnsta2.Text) < Convert.ToSingle(txt_warnend1.Text)) || (Convert.ToSingle(txt_warnsta2.Text) > Convert.ToSingle(txt_errorsta1.Text) && Convert.ToSingle(txt_warnsta2.Text) < Convert.ToSingle(txt_errorend1.Text)) || (Convert.ToSingle(txt_warnsta2.Text) > Convert.ToSingle(txt_errorsta2.Text) && Convert.ToSingle(txt_warnsta2.Text) < Convert.ToSingle(txt_errorend2.Text)))
            {
                errorstr = "区间有重叠！";
            }
            else if ((Convert.ToSingle(txt_warnend2.Text) > Convert.ToSingle(txt_nmlsta.Text) && Convert.ToSingle(txt_warnend2.Text) < Convert.ToSingle(txt_nmlend.Text)) || (Convert.ToSingle(txt_warnend2.Text) > Convert.ToSingle(txt_warnsta1.Text) && Convert.ToSingle(txt_warnend2.Text) < Convert.ToSingle(txt_warnend1.Text)) || (Convert.ToSingle(txt_warnend2.Text) > Convert.ToSingle(txt_errorsta1.Text) && Convert.ToSingle(txt_warnend2.Text) < Convert.ToSingle(txt_errorend1.Text)) || (Convert.ToSingle(txt_warnend2.Text) > Convert.ToSingle(txt_errorsta2.Text) && Convert.ToSingle(txt_warnend2.Text) < Convert.ToSingle(txt_errorend2.Text)))
            {
                errorstr = "区间有重叠！";
            }
            else if ((Convert.ToSingle(txt_errorsta1.Text) > Convert.ToSingle(txt_nmlsta.Text) && Convert.ToSingle(txt_errorsta1.Text) < Convert.ToSingle(txt_nmlend.Text)) || (Convert.ToSingle(txt_errorsta1.Text) > Convert.ToSingle(txt_warnsta1.Text) && Convert.ToSingle(txt_errorsta1.Text) < Convert.ToSingle(txt_warnend1.Text)) || (Convert.ToSingle(txt_errorsta1.Text) > Convert.ToSingle(txt_warnsta2.Text) && Convert.ToSingle(txt_errorsta1.Text) < Convert.ToSingle(txt_warnend2.Text)) || (Convert.ToSingle(txt_errorsta1.Text) > Convert.ToSingle(txt_errorsta2.Text) && Convert.ToSingle(txt_errorsta1.Text) < Convert.ToSingle(txt_errorend2.Text)))
            {
                errorstr = "区间有重叠！";
            }
            else if ((Convert.ToSingle(txt_errorend1.Text) > Convert.ToSingle(txt_nmlsta.Text) && Convert.ToSingle(txt_errorend1.Text) < Convert.ToSingle(txt_nmlend.Text)) || (Convert.ToSingle(txt_errorend1.Text) > Convert.ToSingle(txt_warnsta1.Text) && Convert.ToSingle(txt_errorend1.Text) < Convert.ToSingle(txt_warnend1.Text)) || (Convert.ToSingle(txt_errorend1.Text) > Convert.ToSingle(txt_warnsta2.Text) && Convert.ToSingle(txt_errorend1.Text) < Convert.ToSingle(txt_warnend2.Text)) || (Convert.ToSingle(txt_errorend1.Text) > Convert.ToSingle(txt_errorsta2.Text) && Convert.ToSingle(txt_errorend1.Text) < Convert.ToSingle(txt_errorend2.Text)))
            {
                errorstr = "区间有重叠！";
            }
            else if ((Convert.ToSingle(txt_errorsta2.Text) > Convert.ToSingle(txt_nmlsta.Text) && Convert.ToSingle(txt_errorsta2.Text) < Convert.ToSingle(txt_nmlend.Text)) || (Convert.ToSingle(txt_errorsta2.Text) > Convert.ToSingle(txt_warnsta1.Text) && Convert.ToSingle(txt_errorsta2.Text) < Convert.ToSingle(txt_warnend1.Text)) || (Convert.ToSingle(txt_errorsta2.Text) > Convert.ToSingle(txt_warnsta2.Text) && Convert.ToSingle(txt_errorsta2.Text) < Convert.ToSingle(txt_warnend2.Text)) || (Convert.ToSingle(txt_errorsta2.Text) > Convert.ToSingle(txt_errorsta1.Text) && Convert.ToSingle(txt_errorsta2.Text) < Convert.ToSingle(txt_errorend1.Text)))
            {
                errorstr = "区间有重叠！";
            }
            else if ((Convert.ToSingle(txt_errorend2.Text) > Convert.ToSingle(txt_nmlsta.Text) && Convert.ToSingle(txt_errorend2.Text) < Convert.ToSingle(txt_nmlend.Text)) || (Convert.ToSingle(txt_errorend2.Text) > Convert.ToSingle(txt_warnsta1.Text) && Convert.ToSingle(txt_errorend2.Text) < Convert.ToSingle(txt_warnend1.Text)) || (Convert.ToSingle(txt_errorend2.Text) > Convert.ToSingle(txt_warnsta2.Text) && Convert.ToSingle(txt_errorend2.Text) < Convert.ToSingle(txt_warnend2.Text)) || (Convert.ToSingle(txt_errorend2.Text) > Convert.ToSingle(txt_errorsta1.Text) && Convert.ToSingle(txt_errorend2.Text) < Convert.ToSingle(txt_errorend1.Text)))
            {
                errorstr = "区间有重叠！";
            }
        }
        catch
        {
            MessageBox.Show("发生异常！");
        }
    }

    private void lbl_bgcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bgcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_markcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_markcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_txtcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_txtcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_nmlcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_nmlcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_warncolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_warncolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_errorcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_errorcolor.BackColor = colorDialog1.Color;
        }
    }

    private void btn_view_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void ckb_warn1_CheckedChanged(object sender, EventArgs e)
    {
        if (!ckb_warn1.Checked)
        {
            txt_warnsta1.Text = MinValue.ToString();
            txt_warnsta1.Enabled = false;
            txt_warnend1.Text = MinValue.ToString();
            txt_warnend1.Enabled = false;
        }
        else
        {
            txt_warnsta1.Enabled = true;
            txt_warnend1.Enabled = true;
        }
    }

    private void ckb_warn2_CheckedChanged(object sender, EventArgs e)
    {
        if (!ckb_warn2.Checked)
        {
            txt_warnsta2.Text = MinValue.ToString();
            txt_warnsta2.Enabled = false;
            txt_warnend2.Text = MinValue.ToString();
            txt_warnend2.Enabled = false;
        }
        else
        {
            txt_warnsta2.Enabled = true;
            txt_warnend2.Enabled = true;
        }
    }

    private void ckb_error1_CheckedChanged(object sender, EventArgs e)
    {
        if (!ckb_error1.Checked)
        {
            txt_errorsta1.Text = MinValue.ToString();
            txt_errorsta1.Enabled = false;
            txt_errorend1.Text = MinValue.ToString();
            txt_errorend1.Enabled = false;
        }
        else
        {
            txt_errorsta1.Enabled = true;
            txt_errorend1.Enabled = true;
        }
    }

    private void ckb_error2_CheckedChanged(object sender, EventArgs e)
    {
        if (!ckb_error2.Checked)
        {
            txt_errorsta2.Text = MinValue.ToString();
            txt_errorsta2.Enabled = false;
            txt_errorend2.Text = MinValue.ToString();
            txt_errorend2.Enabled = false;
        }
        else
        {
            txt_errorsta2.Enabled = true;
            txt_errorend2.Enabled = true;
        }
    }

    private void BSet_Load(object sender, EventArgs e)
    {
        txt_varjd2.Text = pt2.ToString();
        ckb_warn1.Checked = true;
        ckb_warn2.Checked = true;
        ckb_error1.Checked = true;
        ckb_error2.Checked = true;
        txt_maxmark.Text = MaxValue.ToString();
        txt_minmark.Text = MinValue.ToString();
        txt_maincount.Text = MainMarkCount.ToString();
        txt_othercount.Text = OtherMarkCount.ToString();
        if (!string.IsNullOrEmpty(VarName))
        {
            txt_var.Text = VarName.Substring(1, VarName.Length - 2);
        }
        lbl_bgcolor.BackColor = Bgcolor;
        lbl_nmlcolor.BackColor = nmlcolor;
        lbl_warncolor.BackColor = warncolor;
        lbl_errorcolor.BackColor = Errorcolor;
        lbl_markcolor.BackColor = BiaoqianColor;
        lbl_txtcolor.BackColor = TxtColor;
        txt_nmlsta.Text = nmlsta.ToString();
        txt_nmlend.Text = nmlend.ToString();
        txt_warnsta1.Text = warnsta1.ToString();
        txt_warnsta2.Text = warnsta2.ToString();
        txt_warnend1.Text = warnend1.ToString();
        txt_warnend2.Text = warnend2.ToString();
        txt_errorsta1.Text = errorsta1.ToString();
        txt_errorsta2.Text = errorsta2.ToString();
        txt_errorend1.Text = errorend1.ToString();
        txt_errorend2.Text = errorend2.ToString();
        txt_bq.Text = Mark;
        if (txt_warnsta1.Text == MinValue.ToString() && txt_warnend1.Text == MinValue.ToString())
        {
            ckb_warn1.Checked = false;
        }
        if (txt_warnsta2.Text == MinValue.ToString() && txt_warnend2.Text == MinValue.ToString())
        {
            ckb_warn2.Checked = false;
        }
        if (lbHide)
        {
            label1.Visible = false;
            txt_bq.Visible = false;
        }
        if (markHide)
        {
            label27.Visible = false;
            txt_othercount.Visible = false;
        }
    }

    private void btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void txt_minmark_TextChanged(object sender, EventArgs e)
    {
        if (!ckb_warn1.Checked)
        {
            txt_warnsta1.Text = txt_minmark.Text;
            txt_warnend1.Text = txt_minmark.Text;
        }
        if (!ckb_warn2.Checked)
        {
            txt_warnsta2.Text = txt_minmark.Text;
            txt_warnend2.Text = txt_minmark.Text;
        }
    }

    private void InitializeComponent()
    {
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.lbl_markcolor = new System.Windows.Forms.Label();
        this.txt_varjd1 = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.txt_bq = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.lbl_bgcolor = new System.Windows.Forms.Label();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.txt_minmark = new System.Windows.Forms.TextBox();
        this.txt_maxmark = new System.Windows.Forms.TextBox();
        this.label8 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.groupBox5 = new System.Windows.Forms.GroupBox();
        this.txt_errorsta1 = new System.Windows.Forms.TextBox();
        this.txt_errorend2 = new System.Windows.Forms.TextBox();
        this.label13 = new System.Windows.Forms.Label();
        this.lbl_errorcolor = new System.Windows.Forms.Label();
        this.label20 = new System.Windows.Forms.Label();
        this.txt_errorend1 = new System.Windows.Forms.TextBox();
        this.label21 = new System.Windows.Forms.Label();
        this.txt_errorsta2 = new System.Windows.Forms.TextBox();
        this.label22 = new System.Windows.Forms.Label();
        this.label23 = new System.Windows.Forms.Label();
        this.ckb_error2 = new System.Windows.Forms.CheckBox();
        this.ckb_error1 = new System.Windows.Forms.CheckBox();
        this.groupBox4 = new System.Windows.Forms.GroupBox();
        this.txt_warnend2 = new System.Windows.Forms.TextBox();
        this.label19 = new System.Windows.Forms.Label();
        this.lbl_warncolor = new System.Windows.Forms.Label();
        this.label17 = new System.Windows.Forms.Label();
        this.txt_warnend1 = new System.Windows.Forms.TextBox();
        this.label18 = new System.Windows.Forms.Label();
        this.txt_warnsta2 = new System.Windows.Forms.TextBox();
        this.txt_warnsta1 = new System.Windows.Forms.TextBox();
        this.label15 = new System.Windows.Forms.Label();
        this.label14 = new System.Windows.Forms.Label();
        this.ckb_warn2 = new System.Windows.Forms.CheckBox();
        this.ckb_warn1 = new System.Windows.Forms.CheckBox();
        this.lbl_nmlcolor = new System.Windows.Forms.Label();
        this.label12 = new System.Windows.Forms.Label();
        this.txt_nmlend = new System.Windows.Forms.TextBox();
        this.label11 = new System.Windows.Forms.Label();
        this.txt_nmlsta = new System.Windows.Forms.TextBox();
        this.label10 = new System.Windows.Forms.Label();
        this.label9 = new System.Windows.Forms.Label();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.groupBox6 = new System.Windows.Forms.GroupBox();
        this.txt_varjd2 = new System.Windows.Forms.TextBox();
        this.label25 = new System.Windows.Forms.Label();
        this.lbl_txtcolor = new System.Windows.Forms.Label();
        this.label16 = new System.Windows.Forms.Label();
        this.txt_othercount = new System.Windows.Forms.TextBox();
        this.txt_maincount = new System.Windows.Forms.TextBox();
        this.label27 = new System.Windows.Forms.Label();
        this.label26 = new System.Windows.Forms.Label();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.groupBox3.SuspendLayout();
        this.groupBox5.SuspendLayout();
        this.groupBox4.SuspendLayout();
        this.groupBox6.SuspendLayout();
        base.SuspendLayout();
        this.txt_var.Location = new System.Drawing.Point(24, 29);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(400, 21);
        this.txt_var.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(431, 29);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.groupBox1.Controls.Add(this.lbl_markcolor);
        this.groupBox1.Controls.Add(this.txt_varjd1);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label5);
        this.groupBox1.Controls.Add(this.txt_bq);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.lbl_bgcolor);
        this.groupBox1.Location = new System.Drawing.Point(17, 68);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(322, 82);
        this.groupBox1.TabIndex = 2;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "仪表表盘";
        this.lbl_markcolor.AutoSize = true;
        this.lbl_markcolor.BackColor = System.Drawing.SystemColors.ControlText;
        this.lbl_markcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_markcolor.Location = new System.Drawing.Point(244, 27);
        this.lbl_markcolor.Name = "lbl_markcolor";
        this.lbl_markcolor.Size = new System.Drawing.Size(49, 14);
        this.lbl_markcolor.TabIndex = 5;
        this.lbl_markcolor.Text = "       ";
        this.lbl_markcolor.Click += new System.EventHandler(lbl_markcolor_Click);
        this.txt_varjd1.Location = new System.Drawing.Point(244, 49);
        this.txt_varjd1.Name = "txt_varjd1";
        this.txt_varjd1.Size = new System.Drawing.Size(42, 21);
        this.txt_varjd1.TabIndex = 3;
        this.txt_varjd1.Visible = false;
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(173, 55);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(65, 12);
        this.label3.TabIndex = 3;
        this.label3.Text = "小数位数：";
        this.label3.Visible = false;
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(173, 27);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(65, 12);
        this.label5.TabIndex = 6;
        this.label5.Text = "标签颜色：";
        this.txt_bq.Location = new System.Drawing.Point(91, 49);
        this.txt_bq.Name = "txt_bq";
        this.txt_bq.Size = new System.Drawing.Size(66, 21);
        this.txt_bq.TabIndex = 2;
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(19, 27);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(65, 12);
        this.label4.TabIndex = 5;
        this.label4.Text = "表盘颜色：";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(19, 54);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "标签文字：";
        this.lbl_bgcolor.AutoSize = true;
        this.lbl_bgcolor.BackColor = System.Drawing.SystemColors.ActiveBorder;
        this.lbl_bgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_bgcolor.Location = new System.Drawing.Point(91, 27);
        this.lbl_bgcolor.Name = "lbl_bgcolor";
        this.lbl_bgcolor.Size = new System.Drawing.Size(49, 14);
        this.lbl_bgcolor.TabIndex = 4;
        this.lbl_bgcolor.Text = "       ";
        this.lbl_bgcolor.Click += new System.EventHandler(lbl_bgcolor_Click);
        this.groupBox2.Controls.Add(this.txt_minmark);
        this.groupBox2.Controls.Add(this.txt_maxmark);
        this.groupBox2.Controls.Add(this.label8);
        this.groupBox2.Controls.Add(this.label7);
        this.groupBox2.Location = new System.Drawing.Point(346, 68);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(200, 82);
        this.groupBox2.TabIndex = 3;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "量程范围";
        this.txt_minmark.Location = new System.Drawing.Point(100, 46);
        this.txt_minmark.Name = "txt_minmark";
        this.txt_minmark.Size = new System.Drawing.Size(81, 21);
        this.txt_minmark.TabIndex = 7;
        this.txt_minmark.TextChanged += new System.EventHandler(txt_minmark_TextChanged);
        this.txt_maxmark.Location = new System.Drawing.Point(100, 19);
        this.txt_maxmark.Name = "txt_maxmark";
        this.txt_maxmark.Size = new System.Drawing.Size(81, 21);
        this.txt_maxmark.TabIndex = 6;
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(22, 52);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(65, 12);
        this.label8.TabIndex = 1;
        this.label8.Text = "最小刻度：";
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(22, 27);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(65, 12);
        this.label7.TabIndex = 0;
        this.label7.Text = "最大刻度：";
        this.groupBox3.Controls.Add(this.groupBox5);
        this.groupBox3.Controls.Add(this.groupBox4);
        this.groupBox3.Controls.Add(this.lbl_nmlcolor);
        this.groupBox3.Controls.Add(this.label12);
        this.groupBox3.Controls.Add(this.txt_nmlend);
        this.groupBox3.Controls.Add(this.label11);
        this.groupBox3.Controls.Add(this.txt_nmlsta);
        this.groupBox3.Controls.Add(this.label10);
        this.groupBox3.Controls.Add(this.label9);
        this.groupBox3.Location = new System.Drawing.Point(17, 238);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(529, 235);
        this.groupBox3.TabIndex = 4;
        this.groupBox3.TabStop = false;
        this.groupBox3.Text = "提醒标志";
        this.groupBox5.Controls.Add(this.txt_errorsta1);
        this.groupBox5.Controls.Add(this.txt_errorend2);
        this.groupBox5.Controls.Add(this.label13);
        this.groupBox5.Controls.Add(this.lbl_errorcolor);
        this.groupBox5.Controls.Add(this.label20);
        this.groupBox5.Controls.Add(this.txt_errorend1);
        this.groupBox5.Controls.Add(this.label21);
        this.groupBox5.Controls.Add(this.txt_errorsta2);
        this.groupBox5.Controls.Add(this.label22);
        this.groupBox5.Controls.Add(this.label23);
        this.groupBox5.Controls.Add(this.ckb_error2);
        this.groupBox5.Controls.Add(this.ckb_error1);
        this.groupBox5.Location = new System.Drawing.Point(16, 143);
        this.groupBox5.Name = "groupBox5";
        this.groupBox5.Size = new System.Drawing.Size(493, 77);
        this.groupBox5.TabIndex = 10;
        this.groupBox5.TabStop = false;
        this.groupBox5.Text = "异常区间";
        this.txt_errorsta1.Location = new System.Drawing.Point(104, 23);
        this.txt_errorsta1.Name = "txt_errorsta1";
        this.txt_errorsta1.Size = new System.Drawing.Size(72, 21);
        this.txt_errorsta1.TabIndex = 23;
        this.txt_errorend2.Location = new System.Drawing.Point(250, 48);
        this.txt_errorend2.Name = "txt_errorend2";
        this.txt_errorend2.Size = new System.Drawing.Size(67, 21);
        this.txt_errorend2.TabIndex = 27;
        this.label13.AutoSize = true;
        this.label13.Location = new System.Drawing.Point(212, 52);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(29, 12);
        this.label13.TabIndex = 14;
        this.label13.Text = "到：";
        this.lbl_errorcolor.AutoSize = true;
        this.lbl_errorcolor.BackColor = System.Drawing.Color.Red;
        this.lbl_errorcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_errorcolor.Location = new System.Drawing.Point(430, 33);
        this.lbl_errorcolor.Name = "lbl_errorcolor";
        this.lbl_errorcolor.Size = new System.Drawing.Size(43, 14);
        this.lbl_errorcolor.TabIndex = 28;
        this.lbl_errorcolor.Text = "      ";
        this.lbl_errorcolor.Click += new System.EventHandler(lbl_errorcolor_Click);
        this.label20.AutoSize = true;
        this.label20.Location = new System.Drawing.Point(355, 35);
        this.label20.Name = "label20";
        this.label20.Size = new System.Drawing.Size(65, 12);
        this.label20.TabIndex = 12;
        this.label20.Text = "警告颜色：";
        this.txt_errorend1.Location = new System.Drawing.Point(250, 23);
        this.txt_errorend1.Name = "txt_errorend1";
        this.txt_errorend1.Size = new System.Drawing.Size(67, 21);
        this.txt_errorend1.TabIndex = 24;
        this.label21.AutoSize = true;
        this.label21.Location = new System.Drawing.Point(212, 26);
        this.label21.Name = "label21";
        this.label21.Size = new System.Drawing.Size(29, 12);
        this.label21.TabIndex = 9;
        this.label21.Text = "到：";
        this.txt_errorsta2.Location = new System.Drawing.Point(104, 49);
        this.txt_errorsta2.Name = "txt_errorsta2";
        this.txt_errorsta2.Size = new System.Drawing.Size(72, 21);
        this.txt_errorsta2.TabIndex = 26;
        this.label22.AutoSize = true;
        this.label22.Location = new System.Drawing.Point(73, 51);
        this.label22.Name = "label22";
        this.label22.Size = new System.Drawing.Size(29, 12);
        this.label22.TabIndex = 3;
        this.label22.Text = "从：";
        this.label23.AutoSize = true;
        this.label23.Location = new System.Drawing.Point(72, 26);
        this.label23.Name = "label23";
        this.label23.Size = new System.Drawing.Size(29, 12);
        this.label23.TabIndex = 2;
        this.label23.Text = "从：";
        this.ckb_error2.AutoSize = true;
        this.ckb_error2.Location = new System.Drawing.Point(16, 52);
        this.ckb_error2.Name = "ckb_error2";
        this.ckb_error2.Size = new System.Drawing.Size(54, 16);
        this.ckb_error2.TabIndex = 25;
        this.ckb_error2.Text = "区间2";
        this.ckb_error2.UseVisualStyleBackColor = true;
        this.ckb_error2.CheckedChanged += new System.EventHandler(ckb_error2_CheckedChanged);
        this.ckb_error1.AutoSize = true;
        this.ckb_error1.Location = new System.Drawing.Point(16, 25);
        this.ckb_error1.Name = "ckb_error1";
        this.ckb_error1.Size = new System.Drawing.Size(54, 16);
        this.ckb_error1.TabIndex = 22;
        this.ckb_error1.Text = "区间1";
        this.ckb_error1.UseVisualStyleBackColor = true;
        this.ckb_error1.CheckedChanged += new System.EventHandler(ckb_error1_CheckedChanged);
        this.groupBox4.Controls.Add(this.txt_warnend2);
        this.groupBox4.Controls.Add(this.label19);
        this.groupBox4.Controls.Add(this.lbl_warncolor);
        this.groupBox4.Controls.Add(this.label17);
        this.groupBox4.Controls.Add(this.txt_warnend1);
        this.groupBox4.Controls.Add(this.label18);
        this.groupBox4.Controls.Add(this.txt_warnsta2);
        this.groupBox4.Controls.Add(this.txt_warnsta1);
        this.groupBox4.Controls.Add(this.label15);
        this.groupBox4.Controls.Add(this.label14);
        this.groupBox4.Controls.Add(this.ckb_warn2);
        this.groupBox4.Controls.Add(this.ckb_warn1);
        this.groupBox4.Location = new System.Drawing.Point(17, 54);
        this.groupBox4.Name = "groupBox4";
        this.groupBox4.Size = new System.Drawing.Size(492, 83);
        this.groupBox4.TabIndex = 9;
        this.groupBox4.TabStop = false;
        this.groupBox4.Text = "警告区间";
        this.txt_warnend2.Location = new System.Drawing.Point(249, 46);
        this.txt_warnend2.Name = "txt_warnend2";
        this.txt_warnend2.Size = new System.Drawing.Size(67, 21);
        this.txt_warnend2.TabIndex = 20;
        this.label19.AutoSize = true;
        this.label19.Location = new System.Drawing.Point(211, 51);
        this.label19.Name = "label19";
        this.label19.Size = new System.Drawing.Size(29, 12);
        this.label19.TabIndex = 14;
        this.label19.Text = "到：";
        this.lbl_warncolor.AutoSize = true;
        this.lbl_warncolor.BackColor = System.Drawing.Color.Yellow;
        this.lbl_warncolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_warncolor.Location = new System.Drawing.Point(429, 38);
        this.lbl_warncolor.Name = "lbl_warncolor";
        this.lbl_warncolor.Size = new System.Drawing.Size(43, 14);
        this.lbl_warncolor.TabIndex = 21;
        this.lbl_warncolor.Text = "      ";
        this.lbl_warncolor.Click += new System.EventHandler(lbl_warncolor_Click);
        this.label17.AutoSize = true;
        this.label17.Location = new System.Drawing.Point(354, 38);
        this.label17.Name = "label17";
        this.label17.Size = new System.Drawing.Size(65, 12);
        this.label17.TabIndex = 12;
        this.label17.Text = "警告颜色：";
        this.txt_warnend1.Location = new System.Drawing.Point(249, 20);
        this.txt_warnend1.Name = "txt_warnend1";
        this.txt_warnend1.Size = new System.Drawing.Size(67, 21);
        this.txt_warnend1.TabIndex = 17;
        this.label18.AutoSize = true;
        this.label18.Location = new System.Drawing.Point(211, 24);
        this.label18.Name = "label18";
        this.label18.Size = new System.Drawing.Size(29, 12);
        this.label18.TabIndex = 9;
        this.label18.Text = "到：";
        this.txt_warnsta2.Location = new System.Drawing.Point(103, 46);
        this.txt_warnsta2.Name = "txt_warnsta2";
        this.txt_warnsta2.Size = new System.Drawing.Size(72, 21);
        this.txt_warnsta2.TabIndex = 19;
        this.txt_warnsta1.Location = new System.Drawing.Point(103, 19);
        this.txt_warnsta1.Name = "txt_warnsta1";
        this.txt_warnsta1.Size = new System.Drawing.Size(72, 21);
        this.txt_warnsta1.TabIndex = 16;
        this.label15.AutoSize = true;
        this.label15.Location = new System.Drawing.Point(72, 52);
        this.label15.Name = "label15";
        this.label15.Size = new System.Drawing.Size(29, 12);
        this.label15.TabIndex = 3;
        this.label15.Text = "从：";
        this.label14.AutoSize = true;
        this.label14.Location = new System.Drawing.Point(72, 22);
        this.label14.Name = "label14";
        this.label14.Size = new System.Drawing.Size(29, 12);
        this.label14.TabIndex = 2;
        this.label14.Text = "从：";
        this.ckb_warn2.AutoSize = true;
        this.ckb_warn2.Location = new System.Drawing.Point(15, 50);
        this.ckb_warn2.Name = "ckb_warn2";
        this.ckb_warn2.Size = new System.Drawing.Size(54, 16);
        this.ckb_warn2.TabIndex = 18;
        this.ckb_warn2.Text = "区间2";
        this.ckb_warn2.UseVisualStyleBackColor = true;
        this.ckb_warn2.CheckedChanged += new System.EventHandler(ckb_warn2_CheckedChanged);
        this.ckb_warn1.AutoSize = true;
        this.ckb_warn1.Location = new System.Drawing.Point(15, 22);
        this.ckb_warn1.Name = "ckb_warn1";
        this.ckb_warn1.Size = new System.Drawing.Size(54, 16);
        this.ckb_warn1.TabIndex = 15;
        this.ckb_warn1.Text = "区间1";
        this.ckb_warn1.UseVisualStyleBackColor = true;
        this.ckb_warn1.CheckedChanged += new System.EventHandler(ckb_warn1_CheckedChanged);
        this.lbl_nmlcolor.AutoSize = true;
        this.lbl_nmlcolor.BackColor = System.Drawing.Color.Lime;
        this.lbl_nmlcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_nmlcolor.Location = new System.Drawing.Point(446, 33);
        this.lbl_nmlcolor.Name = "lbl_nmlcolor";
        this.lbl_nmlcolor.Size = new System.Drawing.Size(43, 14);
        this.lbl_nmlcolor.TabIndex = 14;
        this.lbl_nmlcolor.Text = "      ";
        this.lbl_nmlcolor.Click += new System.EventHandler(lbl_nmlcolor_Click);
        this.label12.AutoSize = true;
        this.label12.Location = new System.Drawing.Point(374, 33);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(65, 12);
        this.label12.TabIndex = 7;
        this.label12.Text = "正常颜色：";
        this.txt_nmlend.Location = new System.Drawing.Point(266, 24);
        this.txt_nmlend.Name = "txt_nmlend";
        this.txt_nmlend.Size = new System.Drawing.Size(67, 21);
        this.txt_nmlend.TabIndex = 13;
        this.label11.AutoSize = true;
        this.label11.Location = new System.Drawing.Point(228, 27);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(29, 12);
        this.label11.TabIndex = 3;
        this.label11.Text = "到：";
        this.txt_nmlsta.Location = new System.Drawing.Point(120, 24);
        this.txt_nmlsta.Name = "txt_nmlsta";
        this.txt_nmlsta.Size = new System.Drawing.Size(72, 21);
        this.txt_nmlsta.TabIndex = 12;
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(89, 27);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(29, 12);
        this.label10.TabIndex = 1;
        this.label10.Text = "从：";
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(21, 27);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(65, 12);
        this.label9.TabIndex = 0;
        this.label9.Text = "正常区间：";
        this.btn_OK.Location = new System.Drawing.Point(370, 479);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 29;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(462, 479);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 30;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        this.groupBox6.Controls.Add(this.txt_othercount);
        this.groupBox6.Controls.Add(this.txt_varjd2);
        this.groupBox6.Controls.Add(this.txt_maincount);
        this.groupBox6.Controls.Add(this.label25);
        this.groupBox6.Controls.Add(this.label27);
        this.groupBox6.Controls.Add(this.lbl_txtcolor);
        this.groupBox6.Controls.Add(this.label26);
        this.groupBox6.Controls.Add(this.label16);
        this.groupBox6.Location = new System.Drawing.Point(17, 156);
        this.groupBox6.Name = "groupBox6";
        this.groupBox6.Size = new System.Drawing.Size(529, 76);
        this.groupBox6.TabIndex = 7;
        this.groupBox6.TabStop = false;
        this.groupBox6.Text = "刻度标签";
        this.txt_varjd2.Location = new System.Drawing.Point(483, 54);
        this.txt_varjd2.Name = "txt_varjd2";
        this.txt_varjd2.Size = new System.Drawing.Size(42, 21);
        this.txt_varjd2.TabIndex = 9;
        this.txt_varjd2.Visible = false;
        this.label25.AutoSize = true;
        this.label25.Location = new System.Drawing.Point(412, 61);
        this.label25.Name = "label25";
        this.label25.Size = new System.Drawing.Size(65, 12);
        this.label25.TabIndex = 3;
        this.label25.Text = "小数位数：";
        this.label25.Visible = false;
        this.lbl_txtcolor.AutoSize = true;
        this.lbl_txtcolor.BackColor = System.Drawing.SystemColors.ActiveCaption;
        this.lbl_txtcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_txtcolor.Location = new System.Drawing.Point(90, 34);
        this.lbl_txtcolor.Name = "lbl_txtcolor";
        this.lbl_txtcolor.Size = new System.Drawing.Size(49, 14);
        this.lbl_txtcolor.TabIndex = 8;
        this.lbl_txtcolor.Text = "       ";
        this.lbl_txtcolor.Click += new System.EventHandler(lbl_txtcolor_Click);
        this.label16.AutoSize = true;
        this.label16.Location = new System.Drawing.Point(17, 36);
        this.label16.Name = "label16";
        this.label16.Size = new System.Drawing.Size(77, 12);
        this.label16.TabIndex = 1;
        this.label16.Text = "刻度文本色：";
        this.txt_othercount.Location = new System.Drawing.Point(429, 31);
        this.txt_othercount.Name = "txt_othercount";
        this.txt_othercount.Size = new System.Drawing.Size(81, 21);
        this.txt_othercount.TabIndex = 11;
        this.txt_maincount.Location = new System.Drawing.Point(244, 31);
        this.txt_maincount.Name = "txt_maincount";
        this.txt_maincount.Size = new System.Drawing.Size(81, 21);
        this.txt_maincount.TabIndex = 10;
        this.label27.AutoSize = true;
        this.label27.Location = new System.Drawing.Point(351, 36);
        this.label27.Name = "label27";
        this.label27.Size = new System.Drawing.Size(65, 12);
        this.label27.TabIndex = 3;
        this.label27.Text = "副刻度数：";
        this.label26.AutoSize = true;
        this.label26.Location = new System.Drawing.Point(173, 36);
        this.label26.Name = "label26";
        this.label26.Size = new System.Drawing.Size(65, 12);
        this.label26.TabIndex = 2;
        this.label26.Text = "主刻度数：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(570, 513);
        base.Controls.Add(this.groupBox6);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.groupBox3);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btn_view);
        base.Controls.Add(this.txt_var);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "BSet";
        this.Text = "仪表";
        base.Load += new System.EventHandler(BSet_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox3.ResumeLayout(false);
        this.groupBox3.PerformLayout();
        this.groupBox5.ResumeLayout(false);
        this.groupBox5.PerformLayout();
        this.groupBox4.ResumeLayout(false);
        this.groupBox4.PerformLayout();
        this.groupBox6.ResumeLayout(false);
        this.groupBox6.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
