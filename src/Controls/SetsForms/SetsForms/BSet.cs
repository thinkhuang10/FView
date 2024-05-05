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
            if (ckvarevent(txt_var.Text))
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
        string value = viewevent();
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
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        groupBox1 = new System.Windows.Forms.GroupBox();
        lbl_markcolor = new System.Windows.Forms.Label();
        txt_varjd1 = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        txt_bq = new System.Windows.Forms.TextBox();
        label4 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        lbl_bgcolor = new System.Windows.Forms.Label();
        groupBox2 = new System.Windows.Forms.GroupBox();
        txt_minmark = new System.Windows.Forms.TextBox();
        txt_maxmark = new System.Windows.Forms.TextBox();
        label8 = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        groupBox3 = new System.Windows.Forms.GroupBox();
        groupBox5 = new System.Windows.Forms.GroupBox();
        txt_errorsta1 = new System.Windows.Forms.TextBox();
        txt_errorend2 = new System.Windows.Forms.TextBox();
        label13 = new System.Windows.Forms.Label();
        lbl_errorcolor = new System.Windows.Forms.Label();
        label20 = new System.Windows.Forms.Label();
        txt_errorend1 = new System.Windows.Forms.TextBox();
        label21 = new System.Windows.Forms.Label();
        txt_errorsta2 = new System.Windows.Forms.TextBox();
        label22 = new System.Windows.Forms.Label();
        label23 = new System.Windows.Forms.Label();
        ckb_error2 = new System.Windows.Forms.CheckBox();
        ckb_error1 = new System.Windows.Forms.CheckBox();
        groupBox4 = new System.Windows.Forms.GroupBox();
        txt_warnend2 = new System.Windows.Forms.TextBox();
        label19 = new System.Windows.Forms.Label();
        lbl_warncolor = new System.Windows.Forms.Label();
        label17 = new System.Windows.Forms.Label();
        txt_warnend1 = new System.Windows.Forms.TextBox();
        label18 = new System.Windows.Forms.Label();
        txt_warnsta2 = new System.Windows.Forms.TextBox();
        txt_warnsta1 = new System.Windows.Forms.TextBox();
        label15 = new System.Windows.Forms.Label();
        label14 = new System.Windows.Forms.Label();
        ckb_warn2 = new System.Windows.Forms.CheckBox();
        ckb_warn1 = new System.Windows.Forms.CheckBox();
        lbl_nmlcolor = new System.Windows.Forms.Label();
        label12 = new System.Windows.Forms.Label();
        txt_nmlend = new System.Windows.Forms.TextBox();
        label11 = new System.Windows.Forms.Label();
        txt_nmlsta = new System.Windows.Forms.TextBox();
        label10 = new System.Windows.Forms.Label();
        label9 = new System.Windows.Forms.Label();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        groupBox6 = new System.Windows.Forms.GroupBox();
        txt_varjd2 = new System.Windows.Forms.TextBox();
        label25 = new System.Windows.Forms.Label();
        lbl_txtcolor = new System.Windows.Forms.Label();
        label16 = new System.Windows.Forms.Label();
        txt_othercount = new System.Windows.Forms.TextBox();
        txt_maincount = new System.Windows.Forms.TextBox();
        label27 = new System.Windows.Forms.Label();
        label26 = new System.Windows.Forms.Label();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox3.SuspendLayout();
        groupBox5.SuspendLayout();
        groupBox4.SuspendLayout();
        groupBox6.SuspendLayout();
        base.SuspendLayout();
        txt_var.Location = new System.Drawing.Point(24, 29);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(400, 21);
        txt_var.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(431, 29);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        groupBox1.Controls.Add(lbl_markcolor);
        groupBox1.Controls.Add(txt_varjd1);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(txt_bq);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(lbl_bgcolor);
        groupBox1.Location = new System.Drawing.Point(17, 68);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(322, 82);
        groupBox1.TabIndex = 2;
        groupBox1.TabStop = false;
        groupBox1.Text = "仪表表盘";
        lbl_markcolor.AutoSize = true;
        lbl_markcolor.BackColor = System.Drawing.SystemColors.ControlText;
        lbl_markcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_markcolor.Location = new System.Drawing.Point(244, 27);
        lbl_markcolor.Name = "lbl_markcolor";
        lbl_markcolor.Size = new System.Drawing.Size(49, 14);
        lbl_markcolor.TabIndex = 5;
        lbl_markcolor.Text = "       ";
        lbl_markcolor.Click += new System.EventHandler(lbl_markcolor_Click);
        txt_varjd1.Location = new System.Drawing.Point(244, 49);
        txt_varjd1.Name = "txt_varjd1";
        txt_varjd1.Size = new System.Drawing.Size(42, 21);
        txt_varjd1.TabIndex = 3;
        txt_varjd1.Visible = false;
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(173, 55);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(65, 12);
        label3.TabIndex = 3;
        label3.Text = "小数位数：";
        label3.Visible = false;
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(173, 27);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(65, 12);
        label5.TabIndex = 6;
        label5.Text = "标签颜色：";
        txt_bq.Location = new System.Drawing.Point(91, 49);
        txt_bq.Name = "txt_bq";
        txt_bq.Size = new System.Drawing.Size(66, 21);
        txt_bq.TabIndex = 2;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(19, 27);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(65, 12);
        label4.TabIndex = 5;
        label4.Text = "表盘颜色：";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(19, 54);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 0;
        label1.Text = "标签文字：";
        lbl_bgcolor.AutoSize = true;
        lbl_bgcolor.BackColor = System.Drawing.SystemColors.ActiveBorder;
        lbl_bgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_bgcolor.Location = new System.Drawing.Point(91, 27);
        lbl_bgcolor.Name = "lbl_bgcolor";
        lbl_bgcolor.Size = new System.Drawing.Size(49, 14);
        lbl_bgcolor.TabIndex = 4;
        lbl_bgcolor.Text = "       ";
        lbl_bgcolor.Click += new System.EventHandler(lbl_bgcolor_Click);
        groupBox2.Controls.Add(txt_minmark);
        groupBox2.Controls.Add(txt_maxmark);
        groupBox2.Controls.Add(label8);
        groupBox2.Controls.Add(label7);
        groupBox2.Location = new System.Drawing.Point(346, 68);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(200, 82);
        groupBox2.TabIndex = 3;
        groupBox2.TabStop = false;
        groupBox2.Text = "量程范围";
        txt_minmark.Location = new System.Drawing.Point(100, 46);
        txt_minmark.Name = "txt_minmark";
        txt_minmark.Size = new System.Drawing.Size(81, 21);
        txt_minmark.TabIndex = 7;
        txt_minmark.TextChanged += new System.EventHandler(txt_minmark_TextChanged);
        txt_maxmark.Location = new System.Drawing.Point(100, 19);
        txt_maxmark.Name = "txt_maxmark";
        txt_maxmark.Size = new System.Drawing.Size(81, 21);
        txt_maxmark.TabIndex = 6;
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(22, 52);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(65, 12);
        label8.TabIndex = 1;
        label8.Text = "最小刻度：";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(22, 27);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(65, 12);
        label7.TabIndex = 0;
        label7.Text = "最大刻度：";
        groupBox3.Controls.Add(groupBox5);
        groupBox3.Controls.Add(groupBox4);
        groupBox3.Controls.Add(lbl_nmlcolor);
        groupBox3.Controls.Add(label12);
        groupBox3.Controls.Add(txt_nmlend);
        groupBox3.Controls.Add(label11);
        groupBox3.Controls.Add(txt_nmlsta);
        groupBox3.Controls.Add(label10);
        groupBox3.Controls.Add(label9);
        groupBox3.Location = new System.Drawing.Point(17, 238);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new System.Drawing.Size(529, 235);
        groupBox3.TabIndex = 4;
        groupBox3.TabStop = false;
        groupBox3.Text = "提醒标志";
        groupBox5.Controls.Add(txt_errorsta1);
        groupBox5.Controls.Add(txt_errorend2);
        groupBox5.Controls.Add(label13);
        groupBox5.Controls.Add(lbl_errorcolor);
        groupBox5.Controls.Add(label20);
        groupBox5.Controls.Add(txt_errorend1);
        groupBox5.Controls.Add(label21);
        groupBox5.Controls.Add(txt_errorsta2);
        groupBox5.Controls.Add(label22);
        groupBox5.Controls.Add(label23);
        groupBox5.Controls.Add(ckb_error2);
        groupBox5.Controls.Add(ckb_error1);
        groupBox5.Location = new System.Drawing.Point(16, 143);
        groupBox5.Name = "groupBox5";
        groupBox5.Size = new System.Drawing.Size(493, 77);
        groupBox5.TabIndex = 10;
        groupBox5.TabStop = false;
        groupBox5.Text = "异常区间";
        txt_errorsta1.Location = new System.Drawing.Point(104, 23);
        txt_errorsta1.Name = "txt_errorsta1";
        txt_errorsta1.Size = new System.Drawing.Size(72, 21);
        txt_errorsta1.TabIndex = 23;
        txt_errorend2.Location = new System.Drawing.Point(250, 48);
        txt_errorend2.Name = "txt_errorend2";
        txt_errorend2.Size = new System.Drawing.Size(67, 21);
        txt_errorend2.TabIndex = 27;
        label13.AutoSize = true;
        label13.Location = new System.Drawing.Point(212, 52);
        label13.Name = "label13";
        label13.Size = new System.Drawing.Size(29, 12);
        label13.TabIndex = 14;
        label13.Text = "到：";
        lbl_errorcolor.AutoSize = true;
        lbl_errorcolor.BackColor = System.Drawing.Color.Red;
        lbl_errorcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_errorcolor.Location = new System.Drawing.Point(430, 33);
        lbl_errorcolor.Name = "lbl_errorcolor";
        lbl_errorcolor.Size = new System.Drawing.Size(43, 14);
        lbl_errorcolor.TabIndex = 28;
        lbl_errorcolor.Text = "      ";
        lbl_errorcolor.Click += new System.EventHandler(lbl_errorcolor_Click);
        label20.AutoSize = true;
        label20.Location = new System.Drawing.Point(355, 35);
        label20.Name = "label20";
        label20.Size = new System.Drawing.Size(65, 12);
        label20.TabIndex = 12;
        label20.Text = "警告颜色：";
        txt_errorend1.Location = new System.Drawing.Point(250, 23);
        txt_errorend1.Name = "txt_errorend1";
        txt_errorend1.Size = new System.Drawing.Size(67, 21);
        txt_errorend1.TabIndex = 24;
        label21.AutoSize = true;
        label21.Location = new System.Drawing.Point(212, 26);
        label21.Name = "label21";
        label21.Size = new System.Drawing.Size(29, 12);
        label21.TabIndex = 9;
        label21.Text = "到：";
        txt_errorsta2.Location = new System.Drawing.Point(104, 49);
        txt_errorsta2.Name = "txt_errorsta2";
        txt_errorsta2.Size = new System.Drawing.Size(72, 21);
        txt_errorsta2.TabIndex = 26;
        label22.AutoSize = true;
        label22.Location = new System.Drawing.Point(73, 51);
        label22.Name = "label22";
        label22.Size = new System.Drawing.Size(29, 12);
        label22.TabIndex = 3;
        label22.Text = "从：";
        label23.AutoSize = true;
        label23.Location = new System.Drawing.Point(72, 26);
        label23.Name = "label23";
        label23.Size = new System.Drawing.Size(29, 12);
        label23.TabIndex = 2;
        label23.Text = "从：";
        ckb_error2.AutoSize = true;
        ckb_error2.Location = new System.Drawing.Point(16, 52);
        ckb_error2.Name = "ckb_error2";
        ckb_error2.Size = new System.Drawing.Size(54, 16);
        ckb_error2.TabIndex = 25;
        ckb_error2.Text = "区间2";
        ckb_error2.UseVisualStyleBackColor = true;
        ckb_error2.CheckedChanged += new System.EventHandler(ckb_error2_CheckedChanged);
        ckb_error1.AutoSize = true;
        ckb_error1.Location = new System.Drawing.Point(16, 25);
        ckb_error1.Name = "ckb_error1";
        ckb_error1.Size = new System.Drawing.Size(54, 16);
        ckb_error1.TabIndex = 22;
        ckb_error1.Text = "区间1";
        ckb_error1.UseVisualStyleBackColor = true;
        ckb_error1.CheckedChanged += new System.EventHandler(ckb_error1_CheckedChanged);
        groupBox4.Controls.Add(txt_warnend2);
        groupBox4.Controls.Add(label19);
        groupBox4.Controls.Add(lbl_warncolor);
        groupBox4.Controls.Add(label17);
        groupBox4.Controls.Add(txt_warnend1);
        groupBox4.Controls.Add(label18);
        groupBox4.Controls.Add(txt_warnsta2);
        groupBox4.Controls.Add(txt_warnsta1);
        groupBox4.Controls.Add(label15);
        groupBox4.Controls.Add(label14);
        groupBox4.Controls.Add(ckb_warn2);
        groupBox4.Controls.Add(ckb_warn1);
        groupBox4.Location = new System.Drawing.Point(17, 54);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new System.Drawing.Size(492, 83);
        groupBox4.TabIndex = 9;
        groupBox4.TabStop = false;
        groupBox4.Text = "警告区间";
        txt_warnend2.Location = new System.Drawing.Point(249, 46);
        txt_warnend2.Name = "txt_warnend2";
        txt_warnend2.Size = new System.Drawing.Size(67, 21);
        txt_warnend2.TabIndex = 20;
        label19.AutoSize = true;
        label19.Location = new System.Drawing.Point(211, 51);
        label19.Name = "label19";
        label19.Size = new System.Drawing.Size(29, 12);
        label19.TabIndex = 14;
        label19.Text = "到：";
        lbl_warncolor.AutoSize = true;
        lbl_warncolor.BackColor = System.Drawing.Color.Yellow;
        lbl_warncolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_warncolor.Location = new System.Drawing.Point(429, 38);
        lbl_warncolor.Name = "lbl_warncolor";
        lbl_warncolor.Size = new System.Drawing.Size(43, 14);
        lbl_warncolor.TabIndex = 21;
        lbl_warncolor.Text = "      ";
        lbl_warncolor.Click += new System.EventHandler(lbl_warncolor_Click);
        label17.AutoSize = true;
        label17.Location = new System.Drawing.Point(354, 38);
        label17.Name = "label17";
        label17.Size = new System.Drawing.Size(65, 12);
        label17.TabIndex = 12;
        label17.Text = "警告颜色：";
        txt_warnend1.Location = new System.Drawing.Point(249, 20);
        txt_warnend1.Name = "txt_warnend1";
        txt_warnend1.Size = new System.Drawing.Size(67, 21);
        txt_warnend1.TabIndex = 17;
        label18.AutoSize = true;
        label18.Location = new System.Drawing.Point(211, 24);
        label18.Name = "label18";
        label18.Size = new System.Drawing.Size(29, 12);
        label18.TabIndex = 9;
        label18.Text = "到：";
        txt_warnsta2.Location = new System.Drawing.Point(103, 46);
        txt_warnsta2.Name = "txt_warnsta2";
        txt_warnsta2.Size = new System.Drawing.Size(72, 21);
        txt_warnsta2.TabIndex = 19;
        txt_warnsta1.Location = new System.Drawing.Point(103, 19);
        txt_warnsta1.Name = "txt_warnsta1";
        txt_warnsta1.Size = new System.Drawing.Size(72, 21);
        txt_warnsta1.TabIndex = 16;
        label15.AutoSize = true;
        label15.Location = new System.Drawing.Point(72, 52);
        label15.Name = "label15";
        label15.Size = new System.Drawing.Size(29, 12);
        label15.TabIndex = 3;
        label15.Text = "从：";
        label14.AutoSize = true;
        label14.Location = new System.Drawing.Point(72, 22);
        label14.Name = "label14";
        label14.Size = new System.Drawing.Size(29, 12);
        label14.TabIndex = 2;
        label14.Text = "从：";
        ckb_warn2.AutoSize = true;
        ckb_warn2.Location = new System.Drawing.Point(15, 50);
        ckb_warn2.Name = "ckb_warn2";
        ckb_warn2.Size = new System.Drawing.Size(54, 16);
        ckb_warn2.TabIndex = 18;
        ckb_warn2.Text = "区间2";
        ckb_warn2.UseVisualStyleBackColor = true;
        ckb_warn2.CheckedChanged += new System.EventHandler(ckb_warn2_CheckedChanged);
        ckb_warn1.AutoSize = true;
        ckb_warn1.Location = new System.Drawing.Point(15, 22);
        ckb_warn1.Name = "ckb_warn1";
        ckb_warn1.Size = new System.Drawing.Size(54, 16);
        ckb_warn1.TabIndex = 15;
        ckb_warn1.Text = "区间1";
        ckb_warn1.UseVisualStyleBackColor = true;
        ckb_warn1.CheckedChanged += new System.EventHandler(ckb_warn1_CheckedChanged);
        lbl_nmlcolor.AutoSize = true;
        lbl_nmlcolor.BackColor = System.Drawing.Color.Lime;
        lbl_nmlcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_nmlcolor.Location = new System.Drawing.Point(446, 33);
        lbl_nmlcolor.Name = "lbl_nmlcolor";
        lbl_nmlcolor.Size = new System.Drawing.Size(43, 14);
        lbl_nmlcolor.TabIndex = 14;
        lbl_nmlcolor.Text = "      ";
        lbl_nmlcolor.Click += new System.EventHandler(lbl_nmlcolor_Click);
        label12.AutoSize = true;
        label12.Location = new System.Drawing.Point(374, 33);
        label12.Name = "label12";
        label12.Size = new System.Drawing.Size(65, 12);
        label12.TabIndex = 7;
        label12.Text = "正常颜色：";
        txt_nmlend.Location = new System.Drawing.Point(266, 24);
        txt_nmlend.Name = "txt_nmlend";
        txt_nmlend.Size = new System.Drawing.Size(67, 21);
        txt_nmlend.TabIndex = 13;
        label11.AutoSize = true;
        label11.Location = new System.Drawing.Point(228, 27);
        label11.Name = "label11";
        label11.Size = new System.Drawing.Size(29, 12);
        label11.TabIndex = 3;
        label11.Text = "到：";
        txt_nmlsta.Location = new System.Drawing.Point(120, 24);
        txt_nmlsta.Name = "txt_nmlsta";
        txt_nmlsta.Size = new System.Drawing.Size(72, 21);
        txt_nmlsta.TabIndex = 12;
        label10.AutoSize = true;
        label10.Location = new System.Drawing.Point(89, 27);
        label10.Name = "label10";
        label10.Size = new System.Drawing.Size(29, 12);
        label10.TabIndex = 1;
        label10.Text = "从：";
        label9.AutoSize = true;
        label9.Location = new System.Drawing.Point(21, 27);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(65, 12);
        label9.TabIndex = 0;
        label9.Text = "正常区间：";
        btn_OK.Location = new System.Drawing.Point(370, 479);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 29;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(462, 479);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 30;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        groupBox6.Controls.Add(txt_othercount);
        groupBox6.Controls.Add(txt_varjd2);
        groupBox6.Controls.Add(txt_maincount);
        groupBox6.Controls.Add(label25);
        groupBox6.Controls.Add(label27);
        groupBox6.Controls.Add(lbl_txtcolor);
        groupBox6.Controls.Add(label26);
        groupBox6.Controls.Add(label16);
        groupBox6.Location = new System.Drawing.Point(17, 156);
        groupBox6.Name = "groupBox6";
        groupBox6.Size = new System.Drawing.Size(529, 76);
        groupBox6.TabIndex = 7;
        groupBox6.TabStop = false;
        groupBox6.Text = "刻度标签";
        txt_varjd2.Location = new System.Drawing.Point(483, 54);
        txt_varjd2.Name = "txt_varjd2";
        txt_varjd2.Size = new System.Drawing.Size(42, 21);
        txt_varjd2.TabIndex = 9;
        txt_varjd2.Visible = false;
        label25.AutoSize = true;
        label25.Location = new System.Drawing.Point(412, 61);
        label25.Name = "label25";
        label25.Size = new System.Drawing.Size(65, 12);
        label25.TabIndex = 3;
        label25.Text = "小数位数：";
        label25.Visible = false;
        lbl_txtcolor.AutoSize = true;
        lbl_txtcolor.BackColor = System.Drawing.SystemColors.ActiveCaption;
        lbl_txtcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_txtcolor.Location = new System.Drawing.Point(90, 34);
        lbl_txtcolor.Name = "lbl_txtcolor";
        lbl_txtcolor.Size = new System.Drawing.Size(49, 14);
        lbl_txtcolor.TabIndex = 8;
        lbl_txtcolor.Text = "       ";
        lbl_txtcolor.Click += new System.EventHandler(lbl_txtcolor_Click);
        label16.AutoSize = true;
        label16.Location = new System.Drawing.Point(17, 36);
        label16.Name = "label16";
        label16.Size = new System.Drawing.Size(77, 12);
        label16.TabIndex = 1;
        label16.Text = "刻度文本色：";
        txt_othercount.Location = new System.Drawing.Point(429, 31);
        txt_othercount.Name = "txt_othercount";
        txt_othercount.Size = new System.Drawing.Size(81, 21);
        txt_othercount.TabIndex = 11;
        txt_maincount.Location = new System.Drawing.Point(244, 31);
        txt_maincount.Name = "txt_maincount";
        txt_maincount.Size = new System.Drawing.Size(81, 21);
        txt_maincount.TabIndex = 10;
        label27.AutoSize = true;
        label27.Location = new System.Drawing.Point(351, 36);
        label27.Name = "label27";
        label27.Size = new System.Drawing.Size(65, 12);
        label27.TabIndex = 3;
        label27.Text = "副刻度数：";
        label26.AutoSize = true;
        label26.Location = new System.Drawing.Point(173, 36);
        label26.Name = "label26";
        label26.Size = new System.Drawing.Size(65, 12);
        label26.TabIndex = 2;
        label26.Text = "主刻度数：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(570, 513);
        base.Controls.Add(groupBox6);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(btn_OK);
        base.Controls.Add(groupBox3);
        base.Controls.Add(groupBox2);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btn_view);
        base.Controls.Add(txt_var);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "BSet";
        Text = "仪表";
        base.Load += new System.EventHandler(BSet_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        groupBox5.ResumeLayout(false);
        groupBox5.PerformLayout();
        groupBox4.ResumeLayout(false);
        groupBox4.PerformLayout();
        groupBox6.ResumeLayout(false);
        groupBox6.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
