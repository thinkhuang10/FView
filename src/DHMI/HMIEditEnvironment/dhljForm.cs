using DevExpress.XtraEditors;
using HMIEditEnvironment.Animation;
using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class dhljForm : XtraForm
{
    public CGlobal theglobal;

    public List<DataFile> dfs;

    private CShape oldshape;

    private bool dirty;

    private Button button_Close;

    private GroupBox groupBox1;

    private GroupBox groupBox2;

    private CheckBox checkBox_Move_R;

    private CheckBox checkBox_Move_H;

    private CheckBox checkBox_Move_V;

    private Button button_Move_R;

    private Button button_Move_H;

    private Button button_Move_V;

    private CheckBox checkBox_Height;

    private Button button_Height;

    private CheckBox checkBox_Width;

    private Button button_Width;

    private GroupBox groupBox3;

    private CheckBox checkBox_Color1;

    private Button button_Color1;

    private CheckBox checkBox_Line;

    private Button button_Line;

    private CheckBox checkBox_Color2;

    private Button button_Color2;

    private GroupBox groupBox4;

    private CheckBox checkBox_Fill_H;

    private Button button_Fill_H;

    private CheckBox checkBox_Fill_V;

    private Button button_Fill_V;

    private GroupBox groupBox5;

    private CheckBox checkBox_Out_S;

    private Button button_Out_S;

    private CheckBox checkBox_Out_D;

    private Button button_Out_D;

    private CheckBox checkBox_Out_A;

    private Button button_Out_A;

    private CheckBox checkBox_In_S;

    private Button button_In_S;

    private CheckBox checkBox_In_D;

    private Button button_In_D;

    private CheckBox checkBox_In_A;

    private Button button_In_A;

    private CheckBox checkBox_Change_Page;

    private Button button_Change_Page;

    private TextBox textBox1;

    private GroupBox groupBox6;

    private Button button_Drag_V;

    private CheckBox checkBox_Drag_V;

    private Button button_Drag_H;

    private CheckBox checkBox_Drag_H;

    private Button button_Enent_Mouse;

    private CheckBox checkBox_Enent_Mouse;

    private CheckBox checkBox_Visable;

    private Button button_Visable;

    private Button buttonJinglingPand;

    private GroupBox groupBox7;

    private CheckBox checkBox_DB_Update;

    private CheckBox checkBox_DB_Insert;

    private CheckBox checkBox_DB_Select;

    private Button button_DB_Update;

    private Button button_DB_Insert;

    private Button button_DB_Select;

    private CheckBox checkBox_DB_Delete;

    private Button button_DB_Delete;

    private CheckBox checkBox_DB_MultiOperate;

    private Button button_DB_MultiOperate;

    private GroupBox groupBox8;

    private Button buttonEventBind;

    private Button button_PropertyBind;

    private Button button_DB_Creat;

    private CheckBox checkBox_DB_Creat;

    public dhljForm()
    {
        InitializeComponent();
    }

    private void ControlEnableInitSetting()
    {
        foreach (object control in base.Controls)
        {
            if (!(control is GroupBox))
            {
                continue;
            }
            foreach (object control2 in ((Control)control).Controls)
            {
                if (control2 is CheckBox || control2 is Button)
                {
                    ((Control)control2).Enabled = false;
                }
            }
        }
    }

    private void Form2_Load(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count == 0)
        {
            MessageBox.Show("请选择图元后再执行该操作.");
            Close();
            return;
        }
        if (theglobal.SelectedShapeList[0] is CControl)
        {
            ControlEnableInitSetting();
            button_Close.Enabled = true;
        }
        oldshape = theglobal.SelectedShapeList[0].Copy();
        textBox1.Text = theglobal.SelectedShapeList[0].UserLogic[0];
        if (!(theglobal.SelectedShapeList[0] is CString))
        {
            CheckBox checkBox = checkBox_In_A;
            CheckBox checkBox2 = checkBox_In_D;
            bool flag2 = (checkBox_In_S.Enabled = false);
            bool enabled = (checkBox2.Enabled = flag2);
            checkBox.Enabled = enabled;
            CheckBox checkBox3 = checkBox_Out_A;
            CheckBox checkBox4 = checkBox_Out_D;
            bool flag5 = (checkBox_Out_S.Enabled = false);
            bool enabled2 = (checkBox4.Enabled = flag5);
            checkBox3.Enabled = enabled2;
            Button button = button_In_A;
            Button button2 = button_In_D;
            bool flag8 = (button_In_S.Enabled = false);
            bool enabled3 = (button2.Enabled = flag8);
            button.Enabled = enabled3;
            Button button3 = button_Out_A;
            Button button4 = button_Out_D;
            bool flag11 = (button_Out_S.Enabled = false);
            bool enabled4 = (button4.Enabled = flag11);
            button3.Enabled = enabled4;
        }
        if (theglobal.SelectedShapeList[0] is CPixieControl)
        {
            ControlEnableInitSetting();
            buttonJinglingPand.Enabled = true;
            button_PropertyBind.Enabled = false;
            buttonEventBind.Enabled = false;
            checkBox_Enent_Mouse.Enabled = true;
            button_Enent_Mouse.Enabled = true;
            checkBox_Change_Page.Enabled = true;
            button_Change_Page.Enabled = true;
            checkBox_Drag_H.Enabled = true;
            button_Drag_H.Enabled = true;
            checkBox_Drag_V.Enabled = true;
            button_Drag_V.Enabled = true;
            checkBox_Move_V.Enabled = true;
            button_Move_V.Enabled = true;
            checkBox_Move_H.Enabled = true;
            button_Move_H.Enabled = true;
            checkBox_Move_R.Enabled = true;
            button_Move_R.Enabled = true;
            checkBox_Width.Enabled = true;
            button_Width.Enabled = true;
            checkBox_Height.Enabled = true;
            button_Height.Enabled = true;
            checkBox_Visable.Enabled = true;
            button_Visable.Enabled = true;
            RefreshCheckBoxs();
            return;
        }
        buttonJinglingPand.Enabled = false;
        if (DBOperationGlobal.effect && ((theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation) || (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))))
        {
            button_DB_Update.Enabled = true;
            button_DB_Insert.Enabled = true;
            button_DB_Select.Enabled = true;
            button_DB_Delete.Enabled = true;
            button_DB_MultiOperate.Enabled = true;
            button_DB_Creat.Enabled = true;
            checkBox_DB_Update.Enabled = true;
            checkBox_DB_Insert.Enabled = true;
            checkBox_DB_Select.Enabled = true;
            checkBox_DB_Delete.Enabled = true;
            checkBox_DB_MultiOperate.Enabled = true;
            checkBox_DB_Creat.Enabled = true;
        }
        else
        {
            button_DB_Update.Enabled = false;
            button_DB_Insert.Enabled = false;
            button_DB_Select.Enabled = false;
            button_DB_Delete.Enabled = false;
            button_DB_MultiOperate.Enabled = false;
            button_DB_Creat.Enabled = false;
            checkBox_DB_Update.Enabled = false;
            checkBox_DB_Insert.Enabled = false;
            checkBox_DB_Select.Enabled = false;
            checkBox_DB_Delete.Enabled = false;
            checkBox_DB_MultiOperate.Enabled = false;
            checkBox_DB_Creat.Enabled = false;
        }
        button_PropertyBind.Enabled = true;
        buttonEventBind.Enabled = true;

        RefreshCheckBoxs();
    }

    private void RefreshCheckBoxs()
    {
        checkBox_In_A.Checked = theglobal.SelectedShapeList[0].ai;
        checkBox_In_D.Checked = theglobal.SelectedShapeList[0].di;
        checkBox_In_S.Checked = theglobal.SelectedShapeList[0].zfcsr;
        checkBox_Out_A.Checked = theglobal.SelectedShapeList[0].ao;
        checkBox_Out_D.Checked = theglobal.SelectedShapeList[0].doo;
        checkBox_Out_S.Checked = theglobal.SelectedShapeList[0].zfcsc;
        checkBox_Change_Page.Checked = theglobal.SelectedShapeList[0].ymqh;
        checkBox_Drag_V.Checked = theglobal.SelectedShapeList[0].cztz;
        checkBox_Drag_H.Checked = theglobal.SelectedShapeList[0].sptz;
        checkBox_Enent_Mouse.Checked = theglobal.SelectedShapeList[0].sbsj;
        checkBox_Fill_V.Checked = theglobal.SelectedShapeList[0].czbfb;
        checkBox_Fill_H.Checked = theglobal.SelectedShapeList[0].spbfb;
        checkBox_Move_V.Checked = theglobal.SelectedShapeList[0].czyd;
        checkBox_Move_H.Checked = theglobal.SelectedShapeList[0].spyd;
        checkBox_Move_R.Checked = theglobal.SelectedShapeList[0].mbxz;
        checkBox_Width.Checked = theglobal.SelectedShapeList[0].kdbh;
        checkBox_Height.Checked = theglobal.SelectedShapeList[0].gdbh;
        checkBox_Visable.Checked = theglobal.SelectedShapeList[0].txyc;
        checkBox_Line.Checked = theglobal.SelectedShapeList[0].bxysbh;
        checkBox_Color1.Checked = theglobal.SelectedShapeList[0].tcs1ysbh;
        checkBox_Color2.Checked = theglobal.SelectedShapeList[0].tcs2ysbh;
        if (theglobal.SelectedShapeList[0].boolysbh)
        {
            checkBox_Color1.Checked = theglobal.SelectedShapeList[0].boolysbh;
        }
        if (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))
        {
            CShape cShape = theglobal.SelectedShapeList[0];
            checkBox_DB_Creat.Checked = cShape.newtable;
            checkBox_DB_Select.Checked = cShape.dbselect;
            checkBox_DB_Delete.Checked = cShape.dbdelete;
            checkBox_DB_Insert.Checked = cShape.dbinsert;
            checkBox_DB_Update.Checked = cShape.dbupdate;
            checkBox_DB_MultiOperate.Checked = cShape.dbmultoperation;
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            checkBox_DB_Creat.Checked = iDBAnimation.Newtable;
            checkBox_DB_Select.Checked = iDBAnimation.Dbselect;
            checkBox_DB_Delete.Checked = iDBAnimation.Dbdelete;
            checkBox_DB_Insert.Checked = iDBAnimation.Dbinsert;
            checkBox_DB_Update.Checked = iDBAnimation.Dbupdate;
            checkBox_DB_MultiOperate.Checked = iDBAnimation.Dbmultoperation;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        theglobal.uc2.RefreshGraphics();
        Close();
    }

    private void checkBox5_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].gdbh = checkBox_Height.Checked;
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].czyd = checkBox_Move_V.Checked;
    }

    private void checkBox2_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].spyd = checkBox_Move_H.Checked;
    }

    private void checkBox3_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].mbxz = checkBox_Move_R.Checked;
    }

    private void button4_Click(object sender, EventArgs e)
    {
        xzForm xzForm2 = new(theglobal);
        if (xzForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }

    private void checkBox4_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].kdbh = checkBox_Width.Checked;
    }

    private void button5_Click(object sender, EventArgs e)
    {
        kdbhForm kdbhForm2 = new(theglobal);
        if (kdbhForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void button6_Click(object sender, EventArgs e)
    {
        gdbhForm gdbhForm2 = new(theglobal);
        if (gdbhForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        czydForm czydForm2 = new(theglobal);
        if (czydForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        spydForm spydForm2 = new(theglobal);
        if (spydForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void button8_Click(object sender, EventArgs e)
    {
        ysForm ysForm2 = new(theglobal, 1);
        if (ysForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void button7_Click(object sender, EventArgs e)
    {
        ysForm ysForm2 = new(theglobal, 2);
        if (ysForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void button9_Click(object sender, EventArgs e)
    {
        ysForm ysForm2 = new(theglobal, 3);
        if (ysForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void checkBox7_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].bxysbh = checkBox_Line.Checked;
    }

    private void checkBox6_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].tcs1ysbh = checkBox_Color1.Checked;
    }

    private void checkBox8_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].tcs2ysbh = checkBox_Color2.Checked;
    }

    private void checkBox10_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].czbfb = checkBox_Fill_V.Checked;
    }

    private void button13_Click(object sender, EventArgs e)
    {
        aiForm aiForm2 = new(theglobal);
        if (aiForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void checkBox_In_A_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].ai = checkBox_In_A.Checked;
        if (theglobal.SelectedShapeList[0].aibianliang == "")
        {
            theglobal.SelectedShapeList[0].ai = checkBox_In_A.Checked = false;
        }
        else
        {
            theglobal.SelectedShapeList[0].ai = checkBox_In_A.Checked;
        }
    }

    private void checkBox_In_D_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].di = checkBox_In_D.Checked;
        if (theglobal.SelectedShapeList[0].dibianlaing == "")
        {
            theglobal.SelectedShapeList[0].di = checkBox_In_D.Checked = false;
        }
        else
        {
            theglobal.SelectedShapeList[0].di = checkBox_In_D.Checked;
        }
    }

    private void checkBox_In_S_CheckedChanged(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0].zfcsrbianliang == "")
        {
            theglobal.SelectedShapeList[0].zfcsr = checkBox_In_S.Checked = false;
        }
        else
        {
            theglobal.SelectedShapeList[0].zfcsr = checkBox_In_S.Checked;
        }
    }

    private void button12_Click(object sender, EventArgs e)
    {
        diForm diForm2 = new(theglobal);
        if (diForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void button17_Click(object sender, EventArgs e)
    {
        zfcsrForm form = new(theglobal);
        if (form.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void checkBox15_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].ao = checkBox_Out_A.Checked;
    }

    private void checkBox14_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].doo = checkBox_Out_D.Checked;
    }

    private void checkBox17_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].zfcsc = checkBox_Out_S.Checked;
    }

    private void button16_Click(object sender, EventArgs e)
    {
        aoForm aoForm2 = new(theglobal);
        if (aoForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void button15_Click(object sender, EventArgs e)
    {
        doForm doForm2 = new(theglobal);
        if (doForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void button18_Click(object sender, EventArgs e)
    {
        zfcscForm zfcscForm2 = new(theglobal);
        if (zfcscForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void checkBox13_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].ymqh = checkBox_Change_Page.Checked;
    }

    private void button14_Click(object sender, EventArgs e)
    {
        pageForm pageForm2 = new(theglobal, dfs);
        if (pageForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void button19_Click(object sender, EventArgs e)
    {
        sptzForm sptzForm2 = new(theglobal);
        if (sptzForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void checkBox18_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].sptz = checkBox_Drag_H.Checked;
        if (theglobal.SelectedShapeList[0].sptzbianliang == "")
        {
            theglobal.SelectedShapeList[0].sptz = false;
        }
        else if (theglobal.SelectedShapeList[0].sptzyidongmax == theglobal.SelectedShapeList[0].sptzyidongmin)
        {
            theglobal.SelectedShapeList[0].sptz = false;
        }
        else
        {
            foreach (string iOItem in CheckIOExists.IOItemList)
            {
                if (!(iOItem == theglobal.SelectedShapeList[0].sptzbianliang))
                {
                    continue;
                }
                goto IL_010b;
            }
            theglobal.SelectedShapeList[0].sptz = false;
        }
        goto IL_010b;
    IL_010b:
        checkBox_Drag_H.Checked = theglobal.SelectedShapeList[0].sptz;
    }

    private void button20_Click(object sender, EventArgs e)
    {
        cztzForm cztzForm2 = new(theglobal);
        if (cztzForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void checkBox19_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].cztz = checkBox_Drag_V.Checked;
        if (theglobal.SelectedShapeList[0].cztzbianliang == "")
        {
            theglobal.SelectedShapeList[0].cztz = false;
        }
        else if (theglobal.SelectedShapeList[0].cztzyidongmax == theglobal.SelectedShapeList[0].cztzyidongmin)
        {
            theglobal.SelectedShapeList[0].cztz = false;
        }
        else
        {
            foreach (string iOItem in CheckIOExists.IOItemList)
            {
                if (!(iOItem == theglobal.SelectedShapeList[0].cztzbianliang))
                {
                    continue;
                }
                goto IL_010b;
            }
            theglobal.SelectedShapeList[0].cztz = false;
        }
        goto IL_010b;
    IL_010b:
        checkBox_Drag_V.Checked = theglobal.SelectedShapeList[0].cztz;
    }

    private void button11_Click(object sender, EventArgs e)
    {
        czbfbForm czbfbForm2 = new(theglobal);
        if (czbfbForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void button10_Click(object sender, EventArgs e)
    {
        spbfbForm spbfbForm2 = new(theglobal);
        if (spbfbForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void checkBox9_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].spbfb = checkBox_Fill_H.Checked;
    }

    private void button21_Click(object sender, EventArgs e)
    {
        CEditEnvironmentGlobal.scriptUnitForm.Init();
        CEditEnvironmentGlobal.scriptUnitForm.Show();
        CEditEnvironmentGlobal.scriptUnitForm.BringToFront();
        CEditEnvironmentGlobal.scriptUnitForm.WindowState = FormWindowState.Normal;
        CEditEnvironmentGlobal.scriptUnitForm.SelectScript(string.Concat("工程相关>页面相关>", theglobal.df.pageName, ">", theglobal.SelectedShapeList[0], ">鼠标左键按下"));
        theglobal.SelectedShapeList[0].sbsj = true;
        RefreshCheckBoxs();
    }

    private void checkBox20_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].sbsj = checkBox_Enent_Mouse.Checked;
    }

    private void button22_Click(object sender, EventArgs e)
    {
        txycForm txycForm2 = new(theglobal);
        if (txycForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void checkBox21_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].txyc = checkBox_Visable.Checked;
    }

    private void label2_Click(object sender, EventArgs e)
    {
    }

    private void label3_Click(object sender, EventArgs e)
    {
    }

    private void label4_Click(object sender, EventArgs e)
    {
    }

    private void button24_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] is CPixieControl)
        {
            CPixieControl cPixieControl = (CPixieControl)theglobal.SelectedShapeList[0];
            cPixieControl.ShowPropertyDialog();
            Invalidate(invalidateChildren: false);
            Update();
            dirty = true;
        }
    }

    private void dhljForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        oldshape = theglobal.SelectedShapeList[0].Copy();
        if (oldshape.czyd != theglobal.SelectedShapeList[0].czyd || oldshape.spyd != theglobal.SelectedShapeList[0].spyd || oldshape.mbxz != theglobal.SelectedShapeList[0].mbxz || oldshape.kdbh != theglobal.SelectedShapeList[0].kdbh || oldshape.gdbh != theglobal.SelectedShapeList[0].gdbh || oldshape.tcs1ysbh != theglobal.SelectedShapeList[0].tcs1ysbh || oldshape.bxysbh != theglobal.SelectedShapeList[0].bxysbh || oldshape.tcs2ysbh != theglobal.SelectedShapeList[0].tcs2ysbh || oldshape.spbfb != theglobal.SelectedShapeList[0].spbfb || oldshape.czbfb != theglobal.SelectedShapeList[0].czbfb || oldshape.di != theglobal.SelectedShapeList[0].di || oldshape.ai != theglobal.SelectedShapeList[0].ai || oldshape.ymqh != theglobal.SelectedShapeList[0].ymqh || oldshape.doo != theglobal.SelectedShapeList[0].doo || oldshape.ao != theglobal.SelectedShapeList[0].ao || oldshape.zfcsr != theglobal.SelectedShapeList[0].zfcsr || oldshape.zfcsc != theglobal.SelectedShapeList[0].zfcsc || oldshape.sptz != theglobal.SelectedShapeList[0].sptz || oldshape.cztz != theglobal.SelectedShapeList[0].cztz || oldshape.sbsj != theglobal.SelectedShapeList[0].sbsj || oldshape.txyc != theglobal.SelectedShapeList[0].txyc || oldshape.Dbselect != theglobal.SelectedShapeList[0].Dbselect || oldshape.Dbinsert != theglobal.SelectedShapeList[0].Dbinsert || oldshape.Dbupdate != theglobal.SelectedShapeList[0].Dbupdate || oldshape.Dbdelete != theglobal.SelectedShapeList[0].Dbdelete)
        {
            List<CShape> list = new();
            List<CShape> list2 = new();
            list.Add(theglobal.SelectedShapeList[0]);
            list2.Add(oldshape);
            theglobal.ForUndo(list, list2);
            dirty = true;
        }
        if (oldshape is CControl && ((CControl)oldshape)._c is IDBAnimation && ((((CControl)oldshape)._c as IDBAnimation).Dbselect != (((CControl)theglobal.SelectedShapeList[0])._c as IDBAnimation).Dbselect || (((CControl)oldshape)._c as IDBAnimation).Dbinsert != (((CControl)theglobal.SelectedShapeList[0])._c as IDBAnimation).Dbinsert || (((CControl)oldshape)._c as IDBAnimation).Dbupdate != (((CControl)theglobal.SelectedShapeList[0])._c as IDBAnimation).Dbupdate || (((CControl)oldshape)._c as IDBAnimation).Dbdelete != (((CControl)theglobal.SelectedShapeList[0])._c as IDBAnimation).Dbdelete))
        {
            List<CShape> list3 = new();
            List<CShape> list4 = new();
            list3.Add(theglobal.SelectedShapeList[0]);
            list4.Add(oldshape);
            theglobal.ForUndo(list3, list4);
            dirty = true;
        }
        if (dirty)
        {
            CEditEnvironmentGlobal.dhp.dirtyPageAdd(theglobal.df.name);
        }
    }

    private void button27_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))
        {
            CShape cShape = theglobal.SelectedShapeList[0];
            DBSelectForm dBSelectForm = new();
            dBSelectForm.label4.Text = theglobal.SelectedShapeList[0].Name;
            dBSelectForm.ResultSQL = cShape.dbselectSQL;
            dBSelectForm.ResultTo = cShape.dbselectTO;
            dBSelectForm.Ansync = cShape.ansyncselect;
            dBSelectForm.OtherData = cShape.dbselectOtherData;
            if (dBSelectForm.ShowDialog() == DialogResult.OK)
            {
                cShape.dbselectSQL = dBSelectForm.ResultSQL;
                cShape.dbselectTO = dBSelectForm.ResultTo;
                cShape.ansyncselect = dBSelectForm.Ansync;
                cShape.dbselectOtherData = dBSelectForm.OtherData;
                cShape.dbselect = true;
                checkBox_DB_Select.Checked = true;
                dirty = true;
            }
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            DBSelectForm dBSelectForm2 = new();
            dBSelectForm2.label4.Text = theglobal.SelectedShapeList[0].Name;
            dBSelectForm2.ResultSQL = iDBAnimation.DbselectSQL;
            if (iDBAnimation.DbselectTO == null || iDBAnimation.DbselectTO == "")
            {
                dBSelectForm2.ResultTo = "{" + theglobal.df.name + "." + theglobal.SelectedShapeList[0].Name + "}";
            }
            else
            {
                dBSelectForm2.ResultTo = iDBAnimation.DbselectTO;
            }
            dBSelectForm2.Ansync = iDBAnimation.Ansyncselect;
            dBSelectForm2.OtherData = iDBAnimation.DbselectOtherData;
            if (dBSelectForm2.ShowDialog() == DialogResult.OK)
            {
                iDBAnimation.DbselectSQL = dBSelectForm2.ResultSQL;
                iDBAnimation.DbselectTO = dBSelectForm2.ResultTo;
                iDBAnimation.Ansyncselect = dBSelectForm2.Ansync;
                iDBAnimation.DbselectOtherData = dBSelectForm2.OtherData;
                iDBAnimation.Dbselect = true;
                checkBox_DB_Select.Checked = true;
                dirty = true;
            }
        }
    }

    private void checkBox28_CheckedChanged(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))
        {
            theglobal.SelectedShapeList[0].newtable = checkBox_DB_Creat.Checked;
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            iDBAnimation.Newtable = checkBox_DB_Creat.Checked;
        }
    }

    private void button32_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))
        {
            CShape cShape = theglobal.SelectedShapeList[0];
            DBNewForm dBNewForm = new();
            dBNewForm.label4.Text = theglobal.SelectedShapeList[0].Name;
            dBNewForm.Ansync = cShape.ansyncnewtable;
            dBNewForm.resultSQL = cShape.newtableSQL;
            dBNewForm.OtherData = cShape.newtableOtherData;
            if (dBNewForm.ShowDialog() == DialogResult.OK)
            {
                cShape.ansyncnewtable = dBNewForm.Ansync;
                cShape.newtableOtherData = dBNewForm.OtherData;
                cShape.newtableSQL = dBNewForm.resultSQL;
                cShape.newtable = true;
                checkBox_DB_Creat.Checked = true;
                dirty = true;
            }
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            DBNewForm dBNewForm2 = new();
            dBNewForm2.label4.Text = theglobal.SelectedShapeList[0].Name;
            dBNewForm2.Ansync = iDBAnimation.Ansyncnewtable;
            dBNewForm2.resultSQL = iDBAnimation.NewtableSQL;
            dBNewForm2.OtherData = iDBAnimation.NewtableOtherData;
            if (dBNewForm2.ShowDialog() == DialogResult.OK)
            {
                iDBAnimation.NewtableSQL = dBNewForm2.resultSQL;
                iDBAnimation.Ansyncnewtable = dBNewForm2.Ansync;
                iDBAnimation.NewtableOtherData = dBNewForm2.OtherData;
                iDBAnimation.Newtable = true;
                checkBox_DB_Creat.Checked = true;
                dirty = true;
            }
        }
    }

    private void checkBox25_CheckedChanged(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))
        {
            theglobal.SelectedShapeList[0].dbselect = checkBox_DB_Select.Checked;
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            iDBAnimation.Dbselect = checkBox_DB_Select.Checked;
        }
    }

    private void button26_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))
        {
            CShape cShape = theglobal.SelectedShapeList[0];
            DBInsertForm dBInsertForm = new();
            dBInsertForm.label4.Text = theglobal.SelectedShapeList[0].Name;
            dBInsertForm.ResultSQL = cShape.dbinsertSQL;
            dBInsertForm.Ansync = cShape.ansyncinsert;
            dBInsertForm.OtherData = cShape.dbinsertOtherData;
            if (dBInsertForm.ShowDialog() == DialogResult.OK)
            {
                cShape.dbinsertSQL = dBInsertForm.ResultSQL;
                cShape.ansyncinsert = dBInsertForm.Ansync;
                cShape.dbinsertOtherData = dBInsertForm.OtherData;
                cShape.dbinsert = true;
                checkBox_DB_Insert.Checked = true;
                dirty = true;
            }
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            DBInsertForm dBInsertForm2 = new();
            dBInsertForm2.label4.Text = theglobal.SelectedShapeList[0].Name;
            dBInsertForm2.ResultSQL = iDBAnimation.DbinsertSQL;
            dBInsertForm2.Ansync = iDBAnimation.Ansyncinsert;
            dBInsertForm2.OtherData = iDBAnimation.DbinsertOtherData;
            if (dBInsertForm2.ShowDialog() == DialogResult.OK)
            {
                iDBAnimation.DbinsertSQL = dBInsertForm2.ResultSQL;
                iDBAnimation.Ansyncinsert = dBInsertForm2.Ansync;
                iDBAnimation.DbinsertOtherData = dBInsertForm2.OtherData;
                iDBAnimation.Dbinsert = true;
                checkBox_DB_Insert.Checked = true;
                dirty = true;
            }
        }
    }

    private void checkBox24_CheckedChanged(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))
        {
            theglobal.SelectedShapeList[0].dbinsert = checkBox_DB_Insert.Checked;
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            iDBAnimation.Dbinsert = checkBox_DB_Insert.Checked;
        }
    }

    private void button28_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))
        {
            CShape cShape = theglobal.SelectedShapeList[0];
            DBDeleteForm dBDeleteForm = new();
            dBDeleteForm.label4.Text = theglobal.SelectedShapeList[0].Name;
            dBDeleteForm.ResultSQL = cShape.dbdeleteSQL;
            dBDeleteForm.Ansync = cShape.ansyncdelete;
            dBDeleteForm.OtherData = cShape.dbdeleteOtherData;
            if (dBDeleteForm.ShowDialog() == DialogResult.OK)
            {
                cShape.dbdeleteSQL = dBDeleteForm.ResultSQL;
                cShape.ansyncdelete = dBDeleteForm.Ansync;
                cShape.dbdeleteOtherData = dBDeleteForm.OtherData;
                cShape.dbdelete = true;
                checkBox_DB_Delete.Checked = true;
                dirty = true;
            }
        }
        if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            DBDeleteForm dBDeleteForm2 = new();
            dBDeleteForm2.label4.Text = theglobal.SelectedShapeList[0].Name;
            dBDeleteForm2.ResultSQL = iDBAnimation.DbdeleteSQL;
            dBDeleteForm2.Ansync = iDBAnimation.Ansyncdelete;
            dBDeleteForm2.OtherData = iDBAnimation.DbdeleteOtherData;
            if (dBDeleteForm2.ShowDialog() == DialogResult.OK)
            {
                iDBAnimation.DbdeleteSQL = dBDeleteForm2.ResultSQL;
                iDBAnimation.Ansyncdelete = dBDeleteForm2.Ansync;
                iDBAnimation.DbdeleteOtherData = dBDeleteForm2.OtherData;
                iDBAnimation.Dbdelete = true;
                checkBox_DB_Delete.Checked = true;
                dirty = true;
            }
        }
    }

    private void checkBox26_CheckedChanged(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))
        {
            theglobal.SelectedShapeList[0].dbdelete = checkBox_DB_Delete.Checked;
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            iDBAnimation.Dbdelete = checkBox_DB_Delete.Checked;
        }
    }

    private void checkBox23_CheckedChanged(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))
        {
            theglobal.SelectedShapeList[0].dbupdate = checkBox_DB_Update.Checked;
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            iDBAnimation.Dbupdate = checkBox_DB_Update.Checked;
        }
    }

    private void button25_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))
        {
            CShape cShape = theglobal.SelectedShapeList[0];
            DBUpdateForm dBUpdateForm = new();
            dBUpdateForm.label4.Text = theglobal.SelectedShapeList[0].Name;
            dBUpdateForm.ResultSQL = cShape.dbupdateSQL;
            dBUpdateForm.Ansync = cShape.Ansyncupdate;
            dBUpdateForm.OtherData = cShape.dbupdateOtherData;
            if (dBUpdateForm.ShowDialog() == DialogResult.OK)
            {
                cShape.dbupdateSQL = dBUpdateForm.ResultSQL;
                cShape.Ansyncupdate = dBUpdateForm.Ansync;
                cShape.dbupdateOtherData = dBUpdateForm.OtherData;
                cShape.dbupdate = true;
                checkBox_DB_Update.Checked = true;
                dirty = true;
            }
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            DBUpdateForm dBUpdateForm2 = new();
            dBUpdateForm2.label4.Text = theglobal.SelectedShapeList[0].Name;
            dBUpdateForm2.ResultSQL = iDBAnimation.DbupdateSQL;
            dBUpdateForm2.Ansync = iDBAnimation.Ansyncupdate;
            dBUpdateForm2.OtherData = iDBAnimation.DbupdateOtherData;
            if (dBUpdateForm2.ShowDialog() == DialogResult.OK)
            {
                iDBAnimation.DbupdateSQL = dBUpdateForm2.ResultSQL;
                iDBAnimation.Ansyncupdate = dBUpdateForm2.Ansync;
                iDBAnimation.DbupdateOtherData = dBUpdateForm2.OtherData;
                iDBAnimation.Dbupdate = true;
                checkBox_DB_Update.Checked = true;
                dirty = true;
            }
        }
    }

    private void button29_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))
        {
            CShape cShape = theglobal.SelectedShapeList[0];
            DBMultOperation dBMultOperation = new()
            {
                DBAnimations = cShape.DBAnimations
            };
            if (dBMultOperation.ShowDialog() == DialogResult.OK)
            {
                cShape.DBAnimations = dBMultOperation.DBAnimations;
                cShape.dbmultoperation = true;
                checkBox_DB_MultiOperate.Checked = true;
                dirty = true;
            }
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            DBMultOperation dBMultOperation2 = new()
            {
                DBAnimations = iDBAnimation.DBAnimationList
            };
            if (dBMultOperation2.ShowDialog() == DialogResult.OK)
            {
                iDBAnimation.DBAnimationList = dBMultOperation2.DBAnimations;
                iDBAnimation.Dbmultoperation = true;
                checkBox_DB_MultiOperate.Checked = true;
                dirty = true;
            }
        }
    }

    private void checkBox27_CheckedChanged(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && !(theglobal.SelectedShapeList[0] is CControl))
        {
            theglobal.SelectedShapeList[0].dbmultoperation = checkBox_DB_MultiOperate.Checked;
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            iDBAnimation.Dbmultoperation = checkBox_DB_MultiOperate.Checked;
        }
    }

    private void button30_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count > 0 && theglobal.SelectedShapeList[0] != null)
        {
            PropertyBindForm propertyBindForm = new(theglobal.SelectedShapeList[0]);
            if (propertyBindForm.ShowDialog() == DialogResult.OK)
            {
                dirty = true;
            }
        }
    }

    private void button31_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList.Count > 0 && theglobal.SelectedShapeList[0] != null)
        {
            CShape cShape = theglobal.SelectedShapeList[0];
            EventBindForm eventBindForm = new(theglobal.SelectedShapeList[0])
            {
                SafeRegion = cShape.m_EventBindRegionList
            };
            if (eventBindForm.ShowDialog() == DialogResult.OK)
            {
                cShape.m_EventBindRegionList = eventBindForm.SafeRegion;
                dirty = true;
            }
        }
    }

    private void InitializeComponent()
    {
            this.button_Close = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_Move_R = new System.Windows.Forms.CheckBox();
            this.checkBox_Move_H = new System.Windows.Forms.CheckBox();
            this.checkBox_Move_V = new System.Windows.Forms.CheckBox();
            this.button_Move_R = new System.Windows.Forms.Button();
            this.button_Move_H = new System.Windows.Forms.Button();
            this.button_Move_V = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_Visable = new System.Windows.Forms.CheckBox();
            this.button_Visable = new System.Windows.Forms.Button();
            this.checkBox_Height = new System.Windows.Forms.CheckBox();
            this.button_Height = new System.Windows.Forms.Button();
            this.checkBox_Width = new System.Windows.Forms.CheckBox();
            this.button_Width = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox_Color2 = new System.Windows.Forms.CheckBox();
            this.button_Color2 = new System.Windows.Forms.Button();
            this.checkBox_Color1 = new System.Windows.Forms.CheckBox();
            this.button_Color1 = new System.Windows.Forms.Button();
            this.checkBox_Line = new System.Windows.Forms.CheckBox();
            this.button_Line = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBox_Fill_H = new System.Windows.Forms.CheckBox();
            this.button_Fill_H = new System.Windows.Forms.Button();
            this.checkBox_Fill_V = new System.Windows.Forms.CheckBox();
            this.button_Fill_V = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox_Out_S = new System.Windows.Forms.CheckBox();
            this.button_Out_S = new System.Windows.Forms.Button();
            this.checkBox_Out_D = new System.Windows.Forms.CheckBox();
            this.button_Out_D = new System.Windows.Forms.Button();
            this.checkBox_Out_A = new System.Windows.Forms.CheckBox();
            this.button_Out_A = new System.Windows.Forms.Button();
            this.checkBox_In_S = new System.Windows.Forms.CheckBox();
            this.button_In_S = new System.Windows.Forms.Button();
            this.checkBox_In_D = new System.Windows.Forms.CheckBox();
            this.button_In_D = new System.Windows.Forms.Button();
            this.checkBox_In_A = new System.Windows.Forms.CheckBox();
            this.button_In_A = new System.Windows.Forms.Button();
            this.checkBox_Change_Page = new System.Windows.Forms.CheckBox();
            this.button_Change_Page = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button_Enent_Mouse = new System.Windows.Forms.Button();
            this.checkBox_Enent_Mouse = new System.Windows.Forms.CheckBox();
            this.button_Drag_V = new System.Windows.Forms.Button();
            this.checkBox_Drag_V = new System.Windows.Forms.CheckBox();
            this.button_Drag_H = new System.Windows.Forms.Button();
            this.checkBox_Drag_H = new System.Windows.Forms.CheckBox();
            this.buttonJinglingPand = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.checkBox_DB_Creat = new System.Windows.Forms.CheckBox();
            this.button_DB_Creat = new System.Windows.Forms.Button();
            this.checkBox_DB_MultiOperate = new System.Windows.Forms.CheckBox();
            this.button_DB_MultiOperate = new System.Windows.Forms.Button();
            this.checkBox_DB_Delete = new System.Windows.Forms.CheckBox();
            this.checkBox_DB_Update = new System.Windows.Forms.CheckBox();
            this.checkBox_DB_Insert = new System.Windows.Forms.CheckBox();
            this.button_DB_Delete = new System.Windows.Forms.Button();
            this.checkBox_DB_Select = new System.Windows.Forms.CheckBox();
            this.button_DB_Update = new System.Windows.Forms.Button();
            this.button_DB_Insert = new System.Windows.Forms.Button();
            this.button_DB_Select = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.buttonEventBind = new System.Windows.Forms.Button();
            this.button_PropertyBind = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(675, 253);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(87, 27);
            this.button_Close.TabIndex = 55;
            this.button_Close.Text = "退出";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_Move_R);
            this.groupBox1.Controls.Add(this.checkBox_Move_H);
            this.groupBox1.Controls.Add(this.checkBox_Move_V);
            this.groupBox1.Controls.Add(this.button_Move_R);
            this.groupBox1.Controls.Add(this.button_Move_H);
            this.groupBox1.Controls.Add(this.button_Move_V);
            this.groupBox1.Location = new System.Drawing.Point(331, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(142, 125);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "目标移动";
            // 
            // checkBox_Move_R
            // 
            this.checkBox_Move_R.AutoSize = true;
            this.checkBox_Move_R.Location = new System.Drawing.Point(15, 93);
            this.checkBox_Move_R.Name = "checkBox_Move_R";
            this.checkBox_Move_R.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Move_R.TabIndex = 28;
            this.checkBox_Move_R.UseVisualStyleBackColor = true;
            this.checkBox_Move_R.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox_Move_H
            // 
            this.checkBox_Move_H.AutoSize = true;
            this.checkBox_Move_H.Location = new System.Drawing.Point(15, 58);
            this.checkBox_Move_H.Name = "checkBox_Move_H";
            this.checkBox_Move_H.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Move_H.TabIndex = 26;
            this.checkBox_Move_H.UseVisualStyleBackColor = true;
            this.checkBox_Move_H.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox_Move_V
            // 
            this.checkBox_Move_V.AutoSize = true;
            this.checkBox_Move_V.Location = new System.Drawing.Point(15, 23);
            this.checkBox_Move_V.Name = "checkBox_Move_V";
            this.checkBox_Move_V.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Move_V.TabIndex = 24;
            this.checkBox_Move_V.UseVisualStyleBackColor = true;
            this.checkBox_Move_V.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button_Move_R
            // 
            this.button_Move_R.Location = new System.Drawing.Point(40, 87);
            this.button_Move_R.Name = "button_Move_R";
            this.button_Move_R.Size = new System.Drawing.Size(87, 27);
            this.button_Move_R.TabIndex = 29;
            this.button_Move_R.Text = "旋转";
            this.button_Move_R.UseVisualStyleBackColor = true;
            this.button_Move_R.Click += new System.EventHandler(this.button4_Click);
            // 
            // button_Move_H
            // 
            this.button_Move_H.Location = new System.Drawing.Point(40, 52);
            this.button_Move_H.Name = "button_Move_H";
            this.button_Move_H.Size = new System.Drawing.Size(87, 27);
            this.button_Move_H.TabIndex = 27;
            this.button_Move_H.Text = "水平";
            this.button_Move_H.UseVisualStyleBackColor = true;
            this.button_Move_H.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_Move_V
            // 
            this.button_Move_V.Location = new System.Drawing.Point(40, 17);
            this.button_Move_V.Name = "button_Move_V";
            this.button_Move_V.Size = new System.Drawing.Size(87, 27);
            this.button_Move_V.TabIndex = 25;
            this.button_Move_V.Text = "垂直";
            this.button_Move_V.UseVisualStyleBackColor = true;
            this.button_Move_V.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox_Visable);
            this.groupBox2.Controls.Add(this.button_Visable);
            this.groupBox2.Controls.Add(this.checkBox_Height);
            this.groupBox2.Controls.Add(this.button_Height);
            this.groupBox2.Controls.Add(this.checkBox_Width);
            this.groupBox2.Controls.Add(this.button_Width);
            this.groupBox2.Location = new System.Drawing.Point(331, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(142, 125);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "尺寸";
            // 
            // checkBox_Visable
            // 
            this.checkBox_Visable.AutoSize = true;
            this.checkBox_Visable.Location = new System.Drawing.Point(15, 93);
            this.checkBox_Visable.Name = "checkBox_Visable";
            this.checkBox_Visable.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Visable.TabIndex = 34;
            this.checkBox_Visable.UseVisualStyleBackColor = true;
            this.checkBox_Visable.CheckedChanged += new System.EventHandler(this.checkBox21_CheckedChanged);
            // 
            // button_Visable
            // 
            this.button_Visable.Location = new System.Drawing.Point(40, 87);
            this.button_Visable.Name = "button_Visable";
            this.button_Visable.Size = new System.Drawing.Size(87, 27);
            this.button_Visable.TabIndex = 35;
            this.button_Visable.Text = "隐藏";
            this.button_Visable.UseVisualStyleBackColor = true;
            this.button_Visable.Click += new System.EventHandler(this.button22_Click);
            // 
            // checkBox_Height
            // 
            this.checkBox_Height.AutoSize = true;
            this.checkBox_Height.Location = new System.Drawing.Point(15, 58);
            this.checkBox_Height.Name = "checkBox_Height";
            this.checkBox_Height.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Height.TabIndex = 32;
            this.checkBox_Height.UseVisualStyleBackColor = true;
            this.checkBox_Height.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // button_Height
            // 
            this.button_Height.Location = new System.Drawing.Point(40, 52);
            this.button_Height.Name = "button_Height";
            this.button_Height.Size = new System.Drawing.Size(87, 27);
            this.button_Height.TabIndex = 33;
            this.button_Height.Text = "高度";
            this.button_Height.UseVisualStyleBackColor = true;
            this.button_Height.Click += new System.EventHandler(this.button6_Click);
            // 
            // checkBox_Width
            // 
            this.checkBox_Width.AutoSize = true;
            this.checkBox_Width.Location = new System.Drawing.Point(15, 23);
            this.checkBox_Width.Name = "checkBox_Width";
            this.checkBox_Width.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Width.TabIndex = 30;
            this.checkBox_Width.UseVisualStyleBackColor = true;
            this.checkBox_Width.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // button_Width
            // 
            this.button_Width.Location = new System.Drawing.Point(40, 17);
            this.button_Width.Name = "button_Width";
            this.button_Width.Size = new System.Drawing.Size(87, 27);
            this.button_Width.TabIndex = 31;
            this.button_Width.Text = "宽度";
            this.button_Width.UseVisualStyleBackColor = true;
            this.button_Width.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox_Color2);
            this.groupBox3.Controls.Add(this.button_Color2);
            this.groupBox3.Controls.Add(this.checkBox_Color1);
            this.groupBox3.Controls.Add(this.button_Color1);
            this.groupBox3.Controls.Add(this.checkBox_Line);
            this.groupBox3.Controls.Add(this.button_Line);
            this.groupBox3.Location = new System.Drawing.Point(489, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(142, 125);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "颜色变化";
            // 
            // checkBox_Color2
            // 
            this.checkBox_Color2.AutoSize = true;
            this.checkBox_Color2.Location = new System.Drawing.Point(15, 93);
            this.checkBox_Color2.Name = "checkBox_Color2";
            this.checkBox_Color2.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Color2.TabIndex = 40;
            this.checkBox_Color2.UseVisualStyleBackColor = true;
            this.checkBox_Color2.CheckedChanged += new System.EventHandler(this.checkBox8_CheckedChanged);
            // 
            // button_Color2
            // 
            this.button_Color2.Location = new System.Drawing.Point(40, 87);
            this.button_Color2.Name = "button_Color2";
            this.button_Color2.Size = new System.Drawing.Size(87, 27);
            this.button_Color2.TabIndex = 41;
            this.button_Color2.Text = "填充色2";
            this.button_Color2.UseVisualStyleBackColor = true;
            this.button_Color2.Click += new System.EventHandler(this.button9_Click);
            // 
            // checkBox_Color1
            // 
            this.checkBox_Color1.AutoSize = true;
            this.checkBox_Color1.Location = new System.Drawing.Point(15, 58);
            this.checkBox_Color1.Name = "checkBox_Color1";
            this.checkBox_Color1.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Color1.TabIndex = 38;
            this.checkBox_Color1.UseVisualStyleBackColor = true;
            this.checkBox_Color1.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // button_Color1
            // 
            this.button_Color1.Location = new System.Drawing.Point(40, 52);
            this.button_Color1.Name = "button_Color1";
            this.button_Color1.Size = new System.Drawing.Size(87, 27);
            this.button_Color1.TabIndex = 39;
            this.button_Color1.Text = "填充色1";
            this.button_Color1.UseVisualStyleBackColor = true;
            this.button_Color1.Click += new System.EventHandler(this.button7_Click);
            // 
            // checkBox_Line
            // 
            this.checkBox_Line.AutoSize = true;
            this.checkBox_Line.Location = new System.Drawing.Point(15, 23);
            this.checkBox_Line.Name = "checkBox_Line";
            this.checkBox_Line.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Line.TabIndex = 36;
            this.checkBox_Line.UseVisualStyleBackColor = true;
            this.checkBox_Line.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
            // 
            // button_Line
            // 
            this.button_Line.Location = new System.Drawing.Point(40, 17);
            this.button_Line.Name = "button_Line";
            this.button_Line.Size = new System.Drawing.Size(87, 27);
            this.button_Line.TabIndex = 37;
            this.button_Line.Text = "边线";
            this.button_Line.UseVisualStyleBackColor = true;
            this.button_Line.Click += new System.EventHandler(this.button8_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox_Fill_H);
            this.groupBox4.Controls.Add(this.button_Fill_H);
            this.groupBox4.Controls.Add(this.checkBox_Fill_V);
            this.groupBox4.Controls.Add(this.button_Fill_V);
            this.groupBox4.Location = new System.Drawing.Point(169, 178);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(142, 94);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "百分比填充";
            // 
            // checkBox_Fill_H
            // 
            this.checkBox_Fill_H.AutoSize = true;
            this.checkBox_Fill_H.Location = new System.Drawing.Point(15, 61);
            this.checkBox_Fill_H.Name = "checkBox_Fill_H";
            this.checkBox_Fill_H.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Fill_H.TabIndex = 22;
            this.checkBox_Fill_H.UseVisualStyleBackColor = true;
            this.checkBox_Fill_H.CheckedChanged += new System.EventHandler(this.checkBox9_CheckedChanged);
            // 
            // button_Fill_H
            // 
            this.button_Fill_H.Location = new System.Drawing.Point(40, 56);
            this.button_Fill_H.Name = "button_Fill_H";
            this.button_Fill_H.Size = new System.Drawing.Size(87, 27);
            this.button_Fill_H.TabIndex = 23;
            this.button_Fill_H.Text = "水平";
            this.button_Fill_H.UseVisualStyleBackColor = true;
            this.button_Fill_H.Click += new System.EventHandler(this.button10_Click);
            // 
            // checkBox_Fill_V
            // 
            this.checkBox_Fill_V.AutoSize = true;
            this.checkBox_Fill_V.Location = new System.Drawing.Point(15, 23);
            this.checkBox_Fill_V.Name = "checkBox_Fill_V";
            this.checkBox_Fill_V.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Fill_V.TabIndex = 20;
            this.checkBox_Fill_V.UseVisualStyleBackColor = true;
            this.checkBox_Fill_V.CheckedChanged += new System.EventHandler(this.checkBox10_CheckedChanged);
            // 
            // button_Fill_V
            // 
            this.button_Fill_V.Location = new System.Drawing.Point(40, 17);
            this.button_Fill_V.Name = "button_Fill_V";
            this.button_Fill_V.Size = new System.Drawing.Size(87, 27);
            this.button_Fill_V.TabIndex = 21;
            this.button_Fill_V.Text = "垂直";
            this.button_Fill_V.UseVisualStyleBackColor = true;
            this.button_Fill_V.Click += new System.EventHandler(this.button11_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBox_Out_S);
            this.groupBox5.Controls.Add(this.button_Out_S);
            this.groupBox5.Controls.Add(this.checkBox_Out_D);
            this.groupBox5.Controls.Add(this.button_Out_D);
            this.groupBox5.Controls.Add(this.checkBox_Out_A);
            this.groupBox5.Controls.Add(this.button_Out_A);
            this.groupBox5.Controls.Add(this.checkBox_In_S);
            this.groupBox5.Controls.Add(this.button_In_S);
            this.groupBox5.Controls.Add(this.checkBox_In_D);
            this.groupBox5.Controls.Add(this.button_In_D);
            this.groupBox5.Controls.Add(this.checkBox_In_A);
            this.groupBox5.Controls.Add(this.button_In_A);
            this.groupBox5.Location = new System.Drawing.Point(20, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(142, 260);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "输入输出";
            // 
            // checkBox_Out_S
            // 
            this.checkBox_Out_S.AutoSize = true;
            this.checkBox_Out_S.Location = new System.Drawing.Point(15, 228);
            this.checkBox_Out_S.Name = "checkBox_Out_S";
            this.checkBox_Out_S.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Out_S.TabIndex = 10;
            this.checkBox_Out_S.UseVisualStyleBackColor = true;
            this.checkBox_Out_S.CheckedChanged += new System.EventHandler(this.checkBox17_CheckedChanged);
            // 
            // button_Out_S
            // 
            this.button_Out_S.Location = new System.Drawing.Point(40, 222);
            this.button_Out_S.Name = "button_Out_S";
            this.button_Out_S.Size = new System.Drawing.Size(87, 27);
            this.button_Out_S.TabIndex = 11;
            this.button_Out_S.Text = "字符串输出";
            this.button_Out_S.UseVisualStyleBackColor = true;
            this.button_Out_S.Click += new System.EventHandler(this.button18_Click);
            // 
            // checkBox_Out_D
            // 
            this.checkBox_Out_D.AutoSize = true;
            this.checkBox_Out_D.Location = new System.Drawing.Point(15, 190);
            this.checkBox_Out_D.Name = "checkBox_Out_D";
            this.checkBox_Out_D.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Out_D.TabIndex = 8;
            this.checkBox_Out_D.UseVisualStyleBackColor = true;
            this.checkBox_Out_D.CheckedChanged += new System.EventHandler(this.checkBox14_CheckedChanged);
            // 
            // button_Out_D
            // 
            this.button_Out_D.Location = new System.Drawing.Point(40, 184);
            this.button_Out_D.Name = "button_Out_D";
            this.button_Out_D.Size = new System.Drawing.Size(87, 27);
            this.button_Out_D.TabIndex = 9;
            this.button_Out_D.Text = "数字量输出";
            this.button_Out_D.UseVisualStyleBackColor = true;
            this.button_Out_D.Click += new System.EventHandler(this.button15_Click);
            // 
            // checkBox_Out_A
            // 
            this.checkBox_Out_A.AutoSize = true;
            this.checkBox_Out_A.Location = new System.Drawing.Point(15, 152);
            this.checkBox_Out_A.Name = "checkBox_Out_A";
            this.checkBox_Out_A.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Out_A.TabIndex = 6;
            this.checkBox_Out_A.UseVisualStyleBackColor = true;
            this.checkBox_Out_A.CheckedChanged += new System.EventHandler(this.checkBox15_CheckedChanged);
            // 
            // button_Out_A
            // 
            this.button_Out_A.Location = new System.Drawing.Point(40, 146);
            this.button_Out_A.Name = "button_Out_A";
            this.button_Out_A.Size = new System.Drawing.Size(87, 27);
            this.button_Out_A.TabIndex = 7;
            this.button_Out_A.Text = "模拟量输出";
            this.button_Out_A.UseVisualStyleBackColor = true;
            this.button_Out_A.Click += new System.EventHandler(this.button16_Click);
            // 
            // checkBox_In_S
            // 
            this.checkBox_In_S.AutoSize = true;
            this.checkBox_In_S.Location = new System.Drawing.Point(15, 99);
            this.checkBox_In_S.Name = "checkBox_In_S";
            this.checkBox_In_S.Size = new System.Drawing.Size(15, 14);
            this.checkBox_In_S.TabIndex = 4;
            this.checkBox_In_S.UseVisualStyleBackColor = true;
            this.checkBox_In_S.CheckedChanged += new System.EventHandler(this.checkBox_In_S_CheckedChanged);
            // 
            // button_In_S
            // 
            this.button_In_S.Location = new System.Drawing.Point(40, 93);
            this.button_In_S.Name = "button_In_S";
            this.button_In_S.Size = new System.Drawing.Size(87, 27);
            this.button_In_S.TabIndex = 5;
            this.button_In_S.Text = "字符串输入";
            this.button_In_S.UseVisualStyleBackColor = true;
            this.button_In_S.Click += new System.EventHandler(this.button17_Click);
            // 
            // checkBox_In_D
            // 
            this.checkBox_In_D.AutoSize = true;
            this.checkBox_In_D.Location = new System.Drawing.Point(15, 61);
            this.checkBox_In_D.Name = "checkBox_In_D";
            this.checkBox_In_D.Size = new System.Drawing.Size(15, 14);
            this.checkBox_In_D.TabIndex = 2;
            this.checkBox_In_D.UseVisualStyleBackColor = true;
            this.checkBox_In_D.CheckedChanged += new System.EventHandler(this.checkBox_In_D_CheckedChanged);
            // 
            // button_In_D
            // 
            this.button_In_D.Location = new System.Drawing.Point(40, 55);
            this.button_In_D.Name = "button_In_D";
            this.button_In_D.Size = new System.Drawing.Size(87, 27);
            this.button_In_D.TabIndex = 3;
            this.button_In_D.Text = "数字量输入";
            this.button_In_D.UseVisualStyleBackColor = true;
            this.button_In_D.Click += new System.EventHandler(this.button12_Click);
            // 
            // checkBox_In_A
            // 
            this.checkBox_In_A.AutoSize = true;
            this.checkBox_In_A.Location = new System.Drawing.Point(15, 23);
            this.checkBox_In_A.Name = "checkBox_In_A";
            this.checkBox_In_A.Size = new System.Drawing.Size(15, 14);
            this.checkBox_In_A.TabIndex = 0;
            this.checkBox_In_A.UseVisualStyleBackColor = true;
            this.checkBox_In_A.CheckedChanged += new System.EventHandler(this.checkBox_In_A_CheckedChanged);
            // 
            // button_In_A
            // 
            this.button_In_A.Location = new System.Drawing.Point(40, 17);
            this.button_In_A.Name = "button_In_A";
            this.button_In_A.Size = new System.Drawing.Size(87, 27);
            this.button_In_A.TabIndex = 1;
            this.button_In_A.Text = "模拟量输入";
            this.button_In_A.UseVisualStyleBackColor = true;
            this.button_In_A.Click += new System.EventHandler(this.button13_Click);
            // 
            // checkBox_Change_Page
            // 
            this.checkBox_Change_Page.AutoSize = true;
            this.checkBox_Change_Page.Location = new System.Drawing.Point(15, 23);
            this.checkBox_Change_Page.Name = "checkBox_Change_Page";
            this.checkBox_Change_Page.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Change_Page.TabIndex = 12;
            this.checkBox_Change_Page.UseVisualStyleBackColor = true;
            this.checkBox_Change_Page.CheckedChanged += new System.EventHandler(this.checkBox13_CheckedChanged);
            // 
            // button_Change_Page
            // 
            this.button_Change_Page.Location = new System.Drawing.Point(40, 17);
            this.button_Change_Page.Name = "button_Change_Page";
            this.button_Change_Page.Size = new System.Drawing.Size(87, 27);
            this.button_Change_Page.TabIndex = 13;
            this.button_Change_Page.Text = "页面切换";
            this.button_Change_Page.UseVisualStyleBackColor = true;
            this.button_Change_Page.Click += new System.EventHandler(this.button14_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(710, 582);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(476, 290);
            this.textBox1.TabIndex = 19;
            this.textBox1.Visible = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button_Enent_Mouse);
            this.groupBox6.Controls.Add(this.checkBox_Enent_Mouse);
            this.groupBox6.Controls.Add(this.button_Drag_V);
            this.groupBox6.Controls.Add(this.checkBox_Drag_V);
            this.groupBox6.Controls.Add(this.button_Drag_H);
            this.groupBox6.Controls.Add(this.checkBox_Drag_H);
            this.groupBox6.Controls.Add(this.button_Change_Page);
            this.groupBox6.Controls.Add(this.checkBox_Change_Page);
            this.groupBox6.Location = new System.Drawing.Point(169, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(142, 159);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "鼠标动作";
            // 
            // button_Enent_Mouse
            // 
            this.button_Enent_Mouse.Location = new System.Drawing.Point(40, 122);
            this.button_Enent_Mouse.Name = "button_Enent_Mouse";
            this.button_Enent_Mouse.Size = new System.Drawing.Size(87, 27);
            this.button_Enent_Mouse.TabIndex = 19;
            this.button_Enent_Mouse.Text = "鼠标事件";
            this.button_Enent_Mouse.UseVisualStyleBackColor = true;
            this.button_Enent_Mouse.Click += new System.EventHandler(this.button21_Click);
            // 
            // checkBox_Enent_Mouse
            // 
            this.checkBox_Enent_Mouse.AutoSize = true;
            this.checkBox_Enent_Mouse.Location = new System.Drawing.Point(15, 128);
            this.checkBox_Enent_Mouse.Name = "checkBox_Enent_Mouse";
            this.checkBox_Enent_Mouse.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Enent_Mouse.TabIndex = 18;
            this.checkBox_Enent_Mouse.UseVisualStyleBackColor = true;
            this.checkBox_Enent_Mouse.CheckedChanged += new System.EventHandler(this.checkBox20_CheckedChanged);
            // 
            // button_Drag_V
            // 
            this.button_Drag_V.Location = new System.Drawing.Point(40, 87);
            this.button_Drag_V.Name = "button_Drag_V";
            this.button_Drag_V.Size = new System.Drawing.Size(87, 27);
            this.button_Drag_V.TabIndex = 17;
            this.button_Drag_V.Text = "垂直拖拽";
            this.button_Drag_V.UseVisualStyleBackColor = true;
            this.button_Drag_V.Click += new System.EventHandler(this.button20_Click);
            // 
            // checkBox_Drag_V
            // 
            this.checkBox_Drag_V.AutoSize = true;
            this.checkBox_Drag_V.Location = new System.Drawing.Point(15, 93);
            this.checkBox_Drag_V.Name = "checkBox_Drag_V";
            this.checkBox_Drag_V.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Drag_V.TabIndex = 16;
            this.checkBox_Drag_V.UseVisualStyleBackColor = true;
            this.checkBox_Drag_V.CheckedChanged += new System.EventHandler(this.checkBox19_CheckedChanged);
            // 
            // button_Drag_H
            // 
            this.button_Drag_H.Location = new System.Drawing.Point(40, 52);
            this.button_Drag_H.Name = "button_Drag_H";
            this.button_Drag_H.Size = new System.Drawing.Size(87, 27);
            this.button_Drag_H.TabIndex = 15;
            this.button_Drag_H.Text = "水平拖拽";
            this.button_Drag_H.UseVisualStyleBackColor = true;
            this.button_Drag_H.Click += new System.EventHandler(this.button19_Click);
            // 
            // checkBox_Drag_H
            // 
            this.checkBox_Drag_H.AutoSize = true;
            this.checkBox_Drag_H.Location = new System.Drawing.Point(15, 58);
            this.checkBox_Drag_H.Name = "checkBox_Drag_H";
            this.checkBox_Drag_H.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Drag_H.TabIndex = 14;
            this.checkBox_Drag_H.UseVisualStyleBackColor = true;
            this.checkBox_Drag_H.CheckedChanged += new System.EventHandler(this.checkBox18_CheckedChanged);
            // 
            // buttonJinglingPand
            // 
            this.buttonJinglingPand.Location = new System.Drawing.Point(15, 87);
            this.buttonJinglingPand.Name = "buttonJinglingPand";
            this.buttonJinglingPand.Size = new System.Drawing.Size(112, 27);
            this.buttonJinglingPand.TabIndex = 44;
            this.buttonJinglingPand.Text = "精灵面板";
            this.buttonJinglingPand.UseVisualStyleBackColor = true;
            this.buttonJinglingPand.Click += new System.EventHandler(this.button24_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.checkBox_DB_Creat);
            this.groupBox7.Controls.Add(this.button_DB_Creat);
            this.groupBox7.Controls.Add(this.checkBox_DB_MultiOperate);
            this.groupBox7.Controls.Add(this.button_DB_MultiOperate);
            this.groupBox7.Controls.Add(this.checkBox_DB_Delete);
            this.groupBox7.Controls.Add(this.checkBox_DB_Update);
            this.groupBox7.Controls.Add(this.checkBox_DB_Insert);
            this.groupBox7.Controls.Add(this.button_DB_Delete);
            this.groupBox7.Controls.Add(this.checkBox_DB_Select);
            this.groupBox7.Controls.Add(this.button_DB_Update);
            this.groupBox7.Controls.Add(this.button_DB_Insert);
            this.groupBox7.Controls.Add(this.button_DB_Select);
            this.groupBox7.Location = new System.Drawing.Point(647, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(142, 235);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "数据库操作";
            // 
            // checkBox_DB_Creat
            // 
            this.checkBox_DB_Creat.AutoSize = true;
            this.checkBox_DB_Creat.Location = new System.Drawing.Point(15, 23);
            this.checkBox_DB_Creat.Name = "checkBox_DB_Creat";
            this.checkBox_DB_Creat.Size = new System.Drawing.Size(15, 14);
            this.checkBox_DB_Creat.TabIndex = 58;
            this.checkBox_DB_Creat.UseVisualStyleBackColor = true;
            this.checkBox_DB_Creat.CheckedChanged += new System.EventHandler(this.checkBox28_CheckedChanged);
            // 
            // button_DB_Creat
            // 
            this.button_DB_Creat.Location = new System.Drawing.Point(40, 17);
            this.button_DB_Creat.Name = "button_DB_Creat";
            this.button_DB_Creat.Size = new System.Drawing.Size(87, 27);
            this.button_DB_Creat.TabIndex = 57;
            this.button_DB_Creat.Text = "新建表";
            this.button_DB_Creat.UseVisualStyleBackColor = true;
            this.button_DB_Creat.Click += new System.EventHandler(this.button32_Click);
            // 
            // checkBox_DB_MultiOperate
            // 
            this.checkBox_DB_MultiOperate.AutoSize = true;
            this.checkBox_DB_MultiOperate.Location = new System.Drawing.Point(15, 203);
            this.checkBox_DB_MultiOperate.Name = "checkBox_DB_MultiOperate";
            this.checkBox_DB_MultiOperate.Size = new System.Drawing.Size(15, 14);
            this.checkBox_DB_MultiOperate.TabIndex = 53;
            this.checkBox_DB_MultiOperate.UseVisualStyleBackColor = true;
            this.checkBox_DB_MultiOperate.CheckedChanged += new System.EventHandler(this.checkBox27_CheckedChanged);
            // 
            // button_DB_MultiOperate
            // 
            this.button_DB_MultiOperate.Location = new System.Drawing.Point(40, 197);
            this.button_DB_MultiOperate.Name = "button_DB_MultiOperate";
            this.button_DB_MultiOperate.Size = new System.Drawing.Size(87, 27);
            this.button_DB_MultiOperate.TabIndex = 54;
            this.button_DB_MultiOperate.Text = "复合操作";
            this.button_DB_MultiOperate.UseVisualStyleBackColor = true;
            this.button_DB_MultiOperate.Click += new System.EventHandler(this.button29_Click);
            // 
            // checkBox_DB_Delete
            // 
            this.checkBox_DB_Delete.AutoSize = true;
            this.checkBox_DB_Delete.Location = new System.Drawing.Point(15, 167);
            this.checkBox_DB_Delete.Name = "checkBox_DB_Delete";
            this.checkBox_DB_Delete.Size = new System.Drawing.Size(15, 14);
            this.checkBox_DB_Delete.TabIndex = 51;
            this.checkBox_DB_Delete.UseVisualStyleBackColor = true;
            this.checkBox_DB_Delete.CheckedChanged += new System.EventHandler(this.checkBox26_CheckedChanged);
            // 
            // checkBox_DB_Update
            // 
            this.checkBox_DB_Update.AutoSize = true;
            this.checkBox_DB_Update.Location = new System.Drawing.Point(15, 131);
            this.checkBox_DB_Update.Name = "checkBox_DB_Update";
            this.checkBox_DB_Update.Size = new System.Drawing.Size(15, 14);
            this.checkBox_DB_Update.TabIndex = 49;
            this.checkBox_DB_Update.UseVisualStyleBackColor = true;
            this.checkBox_DB_Update.CheckedChanged += new System.EventHandler(this.checkBox23_CheckedChanged);
            // 
            // checkBox_DB_Insert
            // 
            this.checkBox_DB_Insert.AutoSize = true;
            this.checkBox_DB_Insert.Location = new System.Drawing.Point(15, 95);
            this.checkBox_DB_Insert.Name = "checkBox_DB_Insert";
            this.checkBox_DB_Insert.Size = new System.Drawing.Size(15, 14);
            this.checkBox_DB_Insert.TabIndex = 47;
            this.checkBox_DB_Insert.UseVisualStyleBackColor = true;
            this.checkBox_DB_Insert.CheckedChanged += new System.EventHandler(this.checkBox24_CheckedChanged);
            // 
            // button_DB_Delete
            // 
            this.button_DB_Delete.Location = new System.Drawing.Point(40, 161);
            this.button_DB_Delete.Name = "button_DB_Delete";
            this.button_DB_Delete.Size = new System.Drawing.Size(87, 27);
            this.button_DB_Delete.TabIndex = 52;
            this.button_DB_Delete.Text = "删除数据";
            this.button_DB_Delete.UseVisualStyleBackColor = true;
            this.button_DB_Delete.Click += new System.EventHandler(this.button28_Click);
            // 
            // checkBox_DB_Select
            // 
            this.checkBox_DB_Select.AutoSize = true;
            this.checkBox_DB_Select.Location = new System.Drawing.Point(15, 59);
            this.checkBox_DB_Select.Name = "checkBox_DB_Select";
            this.checkBox_DB_Select.Size = new System.Drawing.Size(15, 14);
            this.checkBox_DB_Select.TabIndex = 45;
            this.checkBox_DB_Select.UseVisualStyleBackColor = true;
            this.checkBox_DB_Select.CheckedChanged += new System.EventHandler(this.checkBox25_CheckedChanged);
            // 
            // button_DB_Update
            // 
            this.button_DB_Update.Location = new System.Drawing.Point(40, 125);
            this.button_DB_Update.Name = "button_DB_Update";
            this.button_DB_Update.Size = new System.Drawing.Size(87, 27);
            this.button_DB_Update.TabIndex = 50;
            this.button_DB_Update.Text = "更新数据";
            this.button_DB_Update.UseVisualStyleBackColor = true;
            this.button_DB_Update.Click += new System.EventHandler(this.button25_Click);
            // 
            // button_DB_Insert
            // 
            this.button_DB_Insert.Location = new System.Drawing.Point(40, 89);
            this.button_DB_Insert.Name = "button_DB_Insert";
            this.button_DB_Insert.Size = new System.Drawing.Size(87, 27);
            this.button_DB_Insert.TabIndex = 48;
            this.button_DB_Insert.Text = "添加数据";
            this.button_DB_Insert.UseVisualStyleBackColor = true;
            this.button_DB_Insert.Click += new System.EventHandler(this.button26_Click);
            // 
            // button_DB_Select
            // 
            this.button_DB_Select.Location = new System.Drawing.Point(40, 53);
            this.button_DB_Select.Name = "button_DB_Select";
            this.button_DB_Select.Size = new System.Drawing.Size(87, 27);
            this.button_DB_Select.TabIndex = 46;
            this.button_DB_Select.Text = "查询数据";
            this.button_DB_Select.UseVisualStyleBackColor = true;
            this.button_DB_Select.Click += new System.EventHandler(this.button27_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.buttonEventBind);
            this.groupBox8.Controls.Add(this.buttonJinglingPand);
            this.groupBox8.Controls.Add(this.button_PropertyBind);
            this.groupBox8.Location = new System.Drawing.Point(489, 147);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(142, 125);
            this.groupBox8.TabIndex = 9;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "控件绑定";
            // 
            // buttonEventBind
            // 
            this.buttonEventBind.Location = new System.Drawing.Point(15, 52);
            this.buttonEventBind.Name = "buttonEventBind";
            this.buttonEventBind.Size = new System.Drawing.Size(112, 27);
            this.buttonEventBind.TabIndex = 43;
            this.buttonEventBind.Text = "事件绑定";
            this.buttonEventBind.UseVisualStyleBackColor = true;
            this.buttonEventBind.Click += new System.EventHandler(this.button31_Click);
            // 
            // button_PropertyBind
            // 
            this.button_PropertyBind.Location = new System.Drawing.Point(15, 17);
            this.button_PropertyBind.Name = "button_PropertyBind";
            this.button_PropertyBind.Size = new System.Drawing.Size(112, 27);
            this.button_PropertyBind.TabIndex = 42;
            this.button_PropertyBind.Text = "属性绑定";
            this.button_PropertyBind.UseVisualStyleBackColor = true;
            this.button_PropertyBind.Click += new System.EventHandler(this.button30_Click);
            // 
            // dhljForm
            // 
            this.AcceptButton = this.button_Close;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 293);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_Close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "dhljForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "动画连接";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.dhljForm_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
}
