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
            if (control is not GroupBox)
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
        if (theglobal.SelectedShapeList[0] is not CString)
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
        if (DBOperationGlobal.effect && ((theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation) || (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)))
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
        if (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)
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

    private void CheckBox5_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].gdbh = checkBox_Height.Checked;
    }

    private void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].czyd = checkBox_Move_V.Checked;
    }

    private void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].spyd = checkBox_Move_H.Checked;
    }

    private void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].mbxz = checkBox_Move_R.Checked;
    }

    private void Button4_Click(object sender, EventArgs e)
    {
        xzForm xzForm2 = new(theglobal);
        if (xzForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].kdbh = checkBox_Width.Checked;
    }

    private void Button5_Click(object sender, EventArgs e)
    {
        kdbhForm kdbhForm2 = new(theglobal);
        if (kdbhForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void Button6_Click(object sender, EventArgs e)
    {
        gdbhForm gdbhForm2 = new(theglobal);
        if (gdbhForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void Button2_Click(object sender, EventArgs e)
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

    private void Button7_Click(object sender, EventArgs e)
    {
        ysForm ysForm2 = new(theglobal, 2);
        if (ysForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void Button9_Click(object sender, EventArgs e)
    {
        ysForm ysForm2 = new(theglobal, 3);
        if (ysForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void CheckBox7_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].bxysbh = checkBox_Line.Checked;
    }

    private void CheckBox6_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].tcs1ysbh = checkBox_Color1.Checked;
    }

    private void CheckBox8_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].tcs2ysbh = checkBox_Color2.Checked;
    }

    private void CheckBox10_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].czbfb = checkBox_Fill_V.Checked;
    }

    private void Button13_Click(object sender, EventArgs e)
    {
        aiForm aiForm2 = new(theglobal);
        if (aiForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void CheckBox_In_A_CheckedChanged(object sender, EventArgs e)
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

    private void CheckBox_In_D_CheckedChanged(object sender, EventArgs e)
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

    private void CheckBox_In_S_CheckedChanged(object sender, EventArgs e)
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

    private void Button12_Click(object sender, EventArgs e)
    {
        diForm diForm2 = new(theglobal);
        if (diForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void Button17_Click(object sender, EventArgs e)
    {
        zfcsrForm form = new(theglobal);
        if (form.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void CheckBox15_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].ao = checkBox_Out_A.Checked;
    }

    private void CheckBox14_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].doo = checkBox_Out_D.Checked;
    }

    private void CheckBox17_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].zfcsc = checkBox_Out_S.Checked;
    }

    private void Button16_Click(object sender, EventArgs e)
    {
        aoForm aoForm2 = new(theglobal);
        if (aoForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void Button15_Click(object sender, EventArgs e)
    {
        doForm doForm2 = new(theglobal);
        if (doForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void Button18_Click(object sender, EventArgs e)
    {
        zfcscForm zfcscForm2 = new(theglobal);
        if (zfcscForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void CheckBox13_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].ymqh = checkBox_Change_Page.Checked;
    }

    private void Button14_Click(object sender, EventArgs e)
    {
        pageForm pageForm2 = new(theglobal, dfs);
        if (pageForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void Button19_Click(object sender, EventArgs e)
    {
        sptzForm sptzForm2 = new(theglobal);
        if (sptzForm2.ShowDialog() == DialogResult.OK)
        {
            dirty = true;
        }
        RefreshCheckBoxs();
    }

    private void CheckBox18_CheckedChanged(object sender, EventArgs e)
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

    private void CheckBox9_CheckedChanged(object sender, EventArgs e)
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

    private void CheckBox21_CheckedChanged(object sender, EventArgs e)
    {
        theglobal.SelectedShapeList[0].txyc = checkBox_Visable.Checked;
    }

    private void Button24_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] is CPixieControl cPixieControl)
        {
            cPixieControl.ShowPropertyDialog();
            Invalidate(invalidateChildren: false);
            Update();
            dirty = true;
        }
    }

    private void DhljForm_FormClosed(object sender, FormClosedEventArgs e)
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
        if (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)
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
        if (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)
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
        if (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)
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

    private void CheckBox25_CheckedChanged(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)
        {
            theglobal.SelectedShapeList[0].dbselect = checkBox_DB_Select.Checked;
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            iDBAnimation.Dbselect = checkBox_DB_Select.Checked;
        }
    }

    private void Button26_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)
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
        if (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)
        {
            theglobal.SelectedShapeList[0].dbinsert = checkBox_DB_Insert.Checked;
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            iDBAnimation.Dbinsert = checkBox_DB_Insert.Checked;
        }
    }

    private void Button28_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)
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
        if (theglobal.SelectedShapeList[0] is CControl control && control._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)control._c;
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
        if (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)
        {
            theglobal.SelectedShapeList[0].dbdelete = checkBox_DB_Delete.Checked;
        }
        else if (theglobal.SelectedShapeList[0] is CControl && ((CControl)theglobal.SelectedShapeList[0])._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)((CControl)theglobal.SelectedShapeList[0])._c;
            iDBAnimation.Dbdelete = checkBox_DB_Delete.Checked;
        }
    }

    private void CheckBox23_CheckedChanged(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)
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
        if (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)
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

    private void Button29_Click(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)
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

    private void CheckBox27_CheckedChanged(object sender, EventArgs e)
    {
        if (theglobal.SelectedShapeList[0] != null && theglobal.SelectedShapeList[0] is not CControl)
        {
            theglobal.SelectedShapeList[0].dbmultoperation = checkBox_DB_MultiOperate.Checked;
        }
        else if (theglobal.SelectedShapeList[0] is CControl control && control._c is IDBAnimation)
        {
            IDBAnimation iDBAnimation = (IDBAnimation)control._c;
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
            button_Close = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            checkBox_Move_R = new System.Windows.Forms.CheckBox();
            checkBox_Move_H = new System.Windows.Forms.CheckBox();
            checkBox_Move_V = new System.Windows.Forms.CheckBox();
            button_Move_R = new System.Windows.Forms.Button();
            button_Move_H = new System.Windows.Forms.Button();
            button_Move_V = new System.Windows.Forms.Button();
            groupBox2 = new System.Windows.Forms.GroupBox();
            checkBox_Visable = new System.Windows.Forms.CheckBox();
            button_Visable = new System.Windows.Forms.Button();
            checkBox_Height = new System.Windows.Forms.CheckBox();
            button_Height = new System.Windows.Forms.Button();
            checkBox_Width = new System.Windows.Forms.CheckBox();
            button_Width = new System.Windows.Forms.Button();
            groupBox3 = new System.Windows.Forms.GroupBox();
            checkBox_Color2 = new System.Windows.Forms.CheckBox();
            button_Color2 = new System.Windows.Forms.Button();
            checkBox_Color1 = new System.Windows.Forms.CheckBox();
            button_Color1 = new System.Windows.Forms.Button();
            checkBox_Line = new System.Windows.Forms.CheckBox();
            button_Line = new System.Windows.Forms.Button();
            groupBox4 = new System.Windows.Forms.GroupBox();
            checkBox_Fill_H = new System.Windows.Forms.CheckBox();
            button_Fill_H = new System.Windows.Forms.Button();
            checkBox_Fill_V = new System.Windows.Forms.CheckBox();
            button_Fill_V = new System.Windows.Forms.Button();
            groupBox5 = new System.Windows.Forms.GroupBox();
            checkBox_Out_S = new System.Windows.Forms.CheckBox();
            button_Out_S = new System.Windows.Forms.Button();
            checkBox_Out_D = new System.Windows.Forms.CheckBox();
            button_Out_D = new System.Windows.Forms.Button();
            checkBox_Out_A = new System.Windows.Forms.CheckBox();
            button_Out_A = new System.Windows.Forms.Button();
            checkBox_In_S = new System.Windows.Forms.CheckBox();
            button_In_S = new System.Windows.Forms.Button();
            checkBox_In_D = new System.Windows.Forms.CheckBox();
            button_In_D = new System.Windows.Forms.Button();
            checkBox_In_A = new System.Windows.Forms.CheckBox();
            button_In_A = new System.Windows.Forms.Button();
            checkBox_Change_Page = new System.Windows.Forms.CheckBox();
            button_Change_Page = new System.Windows.Forms.Button();
            textBox1 = new System.Windows.Forms.TextBox();
            groupBox6 = new System.Windows.Forms.GroupBox();
            button_Enent_Mouse = new System.Windows.Forms.Button();
            checkBox_Enent_Mouse = new System.Windows.Forms.CheckBox();
            button_Drag_V = new System.Windows.Forms.Button();
            checkBox_Drag_V = new System.Windows.Forms.CheckBox();
            button_Drag_H = new System.Windows.Forms.Button();
            checkBox_Drag_H = new System.Windows.Forms.CheckBox();
            buttonJinglingPand = new System.Windows.Forms.Button();
            groupBox7 = new System.Windows.Forms.GroupBox();
            checkBox_DB_Creat = new System.Windows.Forms.CheckBox();
            button_DB_Creat = new System.Windows.Forms.Button();
            checkBox_DB_MultiOperate = new System.Windows.Forms.CheckBox();
            button_DB_MultiOperate = new System.Windows.Forms.Button();
            checkBox_DB_Delete = new System.Windows.Forms.CheckBox();
            checkBox_DB_Update = new System.Windows.Forms.CheckBox();
            checkBox_DB_Insert = new System.Windows.Forms.CheckBox();
            button_DB_Delete = new System.Windows.Forms.Button();
            checkBox_DB_Select = new System.Windows.Forms.CheckBox();
            button_DB_Update = new System.Windows.Forms.Button();
            button_DB_Insert = new System.Windows.Forms.Button();
            button_DB_Select = new System.Windows.Forms.Button();
            groupBox8 = new System.Windows.Forms.GroupBox();
            buttonEventBind = new System.Windows.Forms.Button();
            button_PropertyBind = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            SuspendLayout();
            // 
            // button_Close
            // 
            button_Close.Location = new System.Drawing.Point(675, 253);
            button_Close.Name = "button_Close";
            button_Close.Size = new System.Drawing.Size(87, 27);
            button_Close.TabIndex = 55;
            button_Close.Text = "退出";
            button_Close.UseVisualStyleBackColor = true;
            button_Close.Click += new System.EventHandler(button1_Click);
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox_Move_R);
            groupBox1.Controls.Add(checkBox_Move_H);
            groupBox1.Controls.Add(checkBox_Move_V);
            groupBox1.Controls.Add(button_Move_R);
            groupBox1.Controls.Add(button_Move_H);
            groupBox1.Controls.Add(button_Move_V);
            groupBox1.Location = new System.Drawing.Point(331, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(142, 125);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "目标移动";
            // 
            // checkBox_Move_R
            // 
            checkBox_Move_R.AutoSize = true;
            checkBox_Move_R.Location = new System.Drawing.Point(15, 93);
            checkBox_Move_R.Name = "checkBox_Move_R";
            checkBox_Move_R.Size = new System.Drawing.Size(15, 14);
            checkBox_Move_R.TabIndex = 28;
            checkBox_Move_R.UseVisualStyleBackColor = true;
            checkBox_Move_R.CheckedChanged += new System.EventHandler(CheckBox3_CheckedChanged);
            // 
            // checkBox_Move_H
            // 
            checkBox_Move_H.AutoSize = true;
            checkBox_Move_H.Location = new System.Drawing.Point(15, 58);
            checkBox_Move_H.Name = "checkBox_Move_H";
            checkBox_Move_H.Size = new System.Drawing.Size(15, 14);
            checkBox_Move_H.TabIndex = 26;
            checkBox_Move_H.UseVisualStyleBackColor = true;
            checkBox_Move_H.CheckedChanged += new System.EventHandler(CheckBox2_CheckedChanged);
            // 
            // checkBox_Move_V
            // 
            checkBox_Move_V.AutoSize = true;
            checkBox_Move_V.Location = new System.Drawing.Point(15, 23);
            checkBox_Move_V.Name = "checkBox_Move_V";
            checkBox_Move_V.Size = new System.Drawing.Size(15, 14);
            checkBox_Move_V.TabIndex = 24;
            checkBox_Move_V.UseVisualStyleBackColor = true;
            checkBox_Move_V.CheckedChanged += new System.EventHandler(CheckBox1_CheckedChanged);
            // 
            // button_Move_R
            // 
            button_Move_R.Location = new System.Drawing.Point(40, 87);
            button_Move_R.Name = "button_Move_R";
            button_Move_R.Size = new System.Drawing.Size(87, 27);
            button_Move_R.TabIndex = 29;
            button_Move_R.Text = "旋转";
            button_Move_R.UseVisualStyleBackColor = true;
            button_Move_R.Click += new System.EventHandler(Button4_Click);
            // 
            // button_Move_H
            // 
            button_Move_H.Location = new System.Drawing.Point(40, 52);
            button_Move_H.Name = "button_Move_H";
            button_Move_H.Size = new System.Drawing.Size(87, 27);
            button_Move_H.TabIndex = 27;
            button_Move_H.Text = "水平";
            button_Move_H.UseVisualStyleBackColor = true;
            button_Move_H.Click += new System.EventHandler(button3_Click);
            // 
            // button_Move_V
            // 
            button_Move_V.Location = new System.Drawing.Point(40, 17);
            button_Move_V.Name = "button_Move_V";
            button_Move_V.Size = new System.Drawing.Size(87, 27);
            button_Move_V.TabIndex = 25;
            button_Move_V.Text = "垂直";
            button_Move_V.UseVisualStyleBackColor = true;
            button_Move_V.Click += new System.EventHandler(Button2_Click);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(checkBox_Visable);
            groupBox2.Controls.Add(button_Visable);
            groupBox2.Controls.Add(checkBox_Height);
            groupBox2.Controls.Add(button_Height);
            groupBox2.Controls.Add(checkBox_Width);
            groupBox2.Controls.Add(button_Width);
            groupBox2.Location = new System.Drawing.Point(331, 147);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(142, 125);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "尺寸";
            // 
            // checkBox_Visable
            // 
            checkBox_Visable.AutoSize = true;
            checkBox_Visable.Location = new System.Drawing.Point(15, 93);
            checkBox_Visable.Name = "checkBox_Visable";
            checkBox_Visable.Size = new System.Drawing.Size(15, 14);
            checkBox_Visable.TabIndex = 34;
            checkBox_Visable.UseVisualStyleBackColor = true;
            checkBox_Visable.CheckedChanged += new System.EventHandler(CheckBox21_CheckedChanged);
            // 
            // button_Visable
            // 
            button_Visable.Location = new System.Drawing.Point(40, 87);
            button_Visable.Name = "button_Visable";
            button_Visable.Size = new System.Drawing.Size(87, 27);
            button_Visable.TabIndex = 35;
            button_Visable.Text = "隐藏";
            button_Visable.UseVisualStyleBackColor = true;
            button_Visable.Click += new System.EventHandler(button22_Click);
            // 
            // checkBox_Height
            // 
            checkBox_Height.AutoSize = true;
            checkBox_Height.Location = new System.Drawing.Point(15, 58);
            checkBox_Height.Name = "checkBox_Height";
            checkBox_Height.Size = new System.Drawing.Size(15, 14);
            checkBox_Height.TabIndex = 32;
            checkBox_Height.UseVisualStyleBackColor = true;
            checkBox_Height.CheckedChanged += new System.EventHandler(CheckBox5_CheckedChanged);
            // 
            // button_Height
            // 
            button_Height.Location = new System.Drawing.Point(40, 52);
            button_Height.Name = "button_Height";
            button_Height.Size = new System.Drawing.Size(87, 27);
            button_Height.TabIndex = 33;
            button_Height.Text = "高度";
            button_Height.UseVisualStyleBackColor = true;
            button_Height.Click += new System.EventHandler(Button6_Click);
            // 
            // checkBox_Width
            // 
            checkBox_Width.AutoSize = true;
            checkBox_Width.Location = new System.Drawing.Point(15, 23);
            checkBox_Width.Name = "checkBox_Width";
            checkBox_Width.Size = new System.Drawing.Size(15, 14);
            checkBox_Width.TabIndex = 30;
            checkBox_Width.UseVisualStyleBackColor = true;
            checkBox_Width.CheckedChanged += new System.EventHandler(CheckBox4_CheckedChanged);
            // 
            // button_Width
            // 
            button_Width.Location = new System.Drawing.Point(40, 17);
            button_Width.Name = "button_Width";
            button_Width.Size = new System.Drawing.Size(87, 27);
            button_Width.TabIndex = 31;
            button_Width.Text = "宽度";
            button_Width.UseVisualStyleBackColor = true;
            button_Width.Click += new System.EventHandler(Button5_Click);
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(checkBox_Color2);
            groupBox3.Controls.Add(button_Color2);
            groupBox3.Controls.Add(checkBox_Color1);
            groupBox3.Controls.Add(button_Color1);
            groupBox3.Controls.Add(checkBox_Line);
            groupBox3.Controls.Add(button_Line);
            groupBox3.Location = new System.Drawing.Point(489, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(142, 125);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "颜色变化";
            // 
            // checkBox_Color2
            // 
            checkBox_Color2.AutoSize = true;
            checkBox_Color2.Location = new System.Drawing.Point(15, 93);
            checkBox_Color2.Name = "checkBox_Color2";
            checkBox_Color2.Size = new System.Drawing.Size(15, 14);
            checkBox_Color2.TabIndex = 40;
            checkBox_Color2.UseVisualStyleBackColor = true;
            checkBox_Color2.CheckedChanged += new System.EventHandler(CheckBox8_CheckedChanged);
            // 
            // button_Color2
            // 
            button_Color2.Location = new System.Drawing.Point(40, 87);
            button_Color2.Name = "button_Color2";
            button_Color2.Size = new System.Drawing.Size(87, 27);
            button_Color2.TabIndex = 41;
            button_Color2.Text = "填充色2";
            button_Color2.UseVisualStyleBackColor = true;
            button_Color2.Click += new System.EventHandler(Button9_Click);
            // 
            // checkBox_Color1
            // 
            checkBox_Color1.AutoSize = true;
            checkBox_Color1.Location = new System.Drawing.Point(15, 58);
            checkBox_Color1.Name = "checkBox_Color1";
            checkBox_Color1.Size = new System.Drawing.Size(15, 14);
            checkBox_Color1.TabIndex = 38;
            checkBox_Color1.UseVisualStyleBackColor = true;
            checkBox_Color1.CheckedChanged += new System.EventHandler(CheckBox6_CheckedChanged);
            // 
            // button_Color1
            // 
            button_Color1.Location = new System.Drawing.Point(40, 52);
            button_Color1.Name = "button_Color1";
            button_Color1.Size = new System.Drawing.Size(87, 27);
            button_Color1.TabIndex = 39;
            button_Color1.Text = "填充色1";
            button_Color1.UseVisualStyleBackColor = true;
            button_Color1.Click += new System.EventHandler(Button7_Click);
            // 
            // checkBox_Line
            // 
            checkBox_Line.AutoSize = true;
            checkBox_Line.Location = new System.Drawing.Point(15, 23);
            checkBox_Line.Name = "checkBox_Line";
            checkBox_Line.Size = new System.Drawing.Size(15, 14);
            checkBox_Line.TabIndex = 36;
            checkBox_Line.UseVisualStyleBackColor = true;
            checkBox_Line.CheckedChanged += new System.EventHandler(CheckBox7_CheckedChanged);
            // 
            // button_Line
            // 
            button_Line.Location = new System.Drawing.Point(40, 17);
            button_Line.Name = "button_Line";
            button_Line.Size = new System.Drawing.Size(87, 27);
            button_Line.TabIndex = 37;
            button_Line.Text = "边线";
            button_Line.UseVisualStyleBackColor = true;
            button_Line.Click += new System.EventHandler(button8_Click);
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(checkBox_Fill_H);
            groupBox4.Controls.Add(button_Fill_H);
            groupBox4.Controls.Add(checkBox_Fill_V);
            groupBox4.Controls.Add(button_Fill_V);
            groupBox4.Location = new System.Drawing.Point(169, 178);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(142, 94);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            groupBox4.Text = "百分比填充";
            // 
            // checkBox_Fill_H
            // 
            checkBox_Fill_H.AutoSize = true;
            checkBox_Fill_H.Location = new System.Drawing.Point(15, 61);
            checkBox_Fill_H.Name = "checkBox_Fill_H";
            checkBox_Fill_H.Size = new System.Drawing.Size(15, 14);
            checkBox_Fill_H.TabIndex = 22;
            checkBox_Fill_H.UseVisualStyleBackColor = true;
            checkBox_Fill_H.CheckedChanged += new System.EventHandler(CheckBox9_CheckedChanged);
            // 
            // button_Fill_H
            // 
            button_Fill_H.Location = new System.Drawing.Point(40, 56);
            button_Fill_H.Name = "button_Fill_H";
            button_Fill_H.Size = new System.Drawing.Size(87, 27);
            button_Fill_H.TabIndex = 23;
            button_Fill_H.Text = "水平";
            button_Fill_H.UseVisualStyleBackColor = true;
            button_Fill_H.Click += new System.EventHandler(button10_Click);
            // 
            // checkBox_Fill_V
            // 
            checkBox_Fill_V.AutoSize = true;
            checkBox_Fill_V.Location = new System.Drawing.Point(15, 23);
            checkBox_Fill_V.Name = "checkBox_Fill_V";
            checkBox_Fill_V.Size = new System.Drawing.Size(15, 14);
            checkBox_Fill_V.TabIndex = 20;
            checkBox_Fill_V.UseVisualStyleBackColor = true;
            checkBox_Fill_V.CheckedChanged += new System.EventHandler(CheckBox10_CheckedChanged);
            // 
            // button_Fill_V
            // 
            button_Fill_V.Location = new System.Drawing.Point(40, 17);
            button_Fill_V.Name = "button_Fill_V";
            button_Fill_V.Size = new System.Drawing.Size(87, 27);
            button_Fill_V.TabIndex = 21;
            button_Fill_V.Text = "垂直";
            button_Fill_V.UseVisualStyleBackColor = true;
            button_Fill_V.Click += new System.EventHandler(button11_Click);
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(checkBox_Out_S);
            groupBox5.Controls.Add(button_Out_S);
            groupBox5.Controls.Add(checkBox_Out_D);
            groupBox5.Controls.Add(button_Out_D);
            groupBox5.Controls.Add(checkBox_Out_A);
            groupBox5.Controls.Add(button_Out_A);
            groupBox5.Controls.Add(checkBox_In_S);
            groupBox5.Controls.Add(button_In_S);
            groupBox5.Controls.Add(checkBox_In_D);
            groupBox5.Controls.Add(button_In_D);
            groupBox5.Controls.Add(checkBox_In_A);
            groupBox5.Controls.Add(button_In_A);
            groupBox5.Location = new System.Drawing.Point(20, 12);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(142, 260);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "输入输出";
            // 
            // checkBox_Out_S
            // 
            checkBox_Out_S.AutoSize = true;
            checkBox_Out_S.Location = new System.Drawing.Point(15, 228);
            checkBox_Out_S.Name = "checkBox_Out_S";
            checkBox_Out_S.Size = new System.Drawing.Size(15, 14);
            checkBox_Out_S.TabIndex = 10;
            checkBox_Out_S.UseVisualStyleBackColor = true;
            checkBox_Out_S.CheckedChanged += new System.EventHandler(CheckBox17_CheckedChanged);
            // 
            // button_Out_S
            // 
            button_Out_S.Location = new System.Drawing.Point(40, 222);
            button_Out_S.Name = "button_Out_S";
            button_Out_S.Size = new System.Drawing.Size(87, 27);
            button_Out_S.TabIndex = 11;
            button_Out_S.Text = "字符串输出";
            button_Out_S.UseVisualStyleBackColor = true;
            button_Out_S.Click += new System.EventHandler(Button18_Click);
            // 
            // checkBox_Out_D
            // 
            checkBox_Out_D.AutoSize = true;
            checkBox_Out_D.Location = new System.Drawing.Point(15, 190);
            checkBox_Out_D.Name = "checkBox_Out_D";
            checkBox_Out_D.Size = new System.Drawing.Size(15, 14);
            checkBox_Out_D.TabIndex = 8;
            checkBox_Out_D.UseVisualStyleBackColor = true;
            checkBox_Out_D.CheckedChanged += new System.EventHandler(CheckBox14_CheckedChanged);
            // 
            // button_Out_D
            // 
            button_Out_D.Location = new System.Drawing.Point(40, 184);
            button_Out_D.Name = "button_Out_D";
            button_Out_D.Size = new System.Drawing.Size(87, 27);
            button_Out_D.TabIndex = 9;
            button_Out_D.Text = "数字量输出";
            button_Out_D.UseVisualStyleBackColor = true;
            button_Out_D.Click += new System.EventHandler(Button15_Click);
            // 
            // checkBox_Out_A
            // 
            checkBox_Out_A.AutoSize = true;
            checkBox_Out_A.Location = new System.Drawing.Point(15, 152);
            checkBox_Out_A.Name = "checkBox_Out_A";
            checkBox_Out_A.Size = new System.Drawing.Size(15, 14);
            checkBox_Out_A.TabIndex = 6;
            checkBox_Out_A.UseVisualStyleBackColor = true;
            checkBox_Out_A.CheckedChanged += new System.EventHandler(CheckBox15_CheckedChanged);
            // 
            // button_Out_A
            // 
            button_Out_A.Location = new System.Drawing.Point(40, 146);
            button_Out_A.Name = "button_Out_A";
            button_Out_A.Size = new System.Drawing.Size(87, 27);
            button_Out_A.TabIndex = 7;
            button_Out_A.Text = "模拟量输出";
            button_Out_A.UseVisualStyleBackColor = true;
            button_Out_A.Click += new System.EventHandler(Button16_Click);
            // 
            // checkBox_In_S
            // 
            checkBox_In_S.AutoSize = true;
            checkBox_In_S.Location = new System.Drawing.Point(15, 99);
            checkBox_In_S.Name = "checkBox_In_S";
            checkBox_In_S.Size = new System.Drawing.Size(15, 14);
            checkBox_In_S.TabIndex = 4;
            checkBox_In_S.UseVisualStyleBackColor = true;
            checkBox_In_S.CheckedChanged += new System.EventHandler(CheckBox_In_S_CheckedChanged);
            // 
            // button_In_S
            // 
            button_In_S.Location = new System.Drawing.Point(40, 93);
            button_In_S.Name = "button_In_S";
            button_In_S.Size = new System.Drawing.Size(87, 27);
            button_In_S.TabIndex = 5;
            button_In_S.Text = "字符串输入";
            button_In_S.UseVisualStyleBackColor = true;
            button_In_S.Click += new System.EventHandler(Button17_Click);
            // 
            // checkBox_In_D
            // 
            checkBox_In_D.AutoSize = true;
            checkBox_In_D.Location = new System.Drawing.Point(15, 61);
            checkBox_In_D.Name = "checkBox_In_D";
            checkBox_In_D.Size = new System.Drawing.Size(15, 14);
            checkBox_In_D.TabIndex = 2;
            checkBox_In_D.UseVisualStyleBackColor = true;
            checkBox_In_D.CheckedChanged += new System.EventHandler(CheckBox_In_D_CheckedChanged);
            // 
            // button_In_D
            // 
            button_In_D.Location = new System.Drawing.Point(40, 55);
            button_In_D.Name = "button_In_D";
            button_In_D.Size = new System.Drawing.Size(87, 27);
            button_In_D.TabIndex = 3;
            button_In_D.Text = "数字量输入";
            button_In_D.UseVisualStyleBackColor = true;
            button_In_D.Click += new System.EventHandler(Button12_Click);
            // 
            // checkBox_In_A
            // 
            checkBox_In_A.AutoSize = true;
            checkBox_In_A.Location = new System.Drawing.Point(15, 23);
            checkBox_In_A.Name = "checkBox_In_A";
            checkBox_In_A.Size = new System.Drawing.Size(15, 14);
            checkBox_In_A.TabIndex = 0;
            checkBox_In_A.UseVisualStyleBackColor = true;
            checkBox_In_A.CheckedChanged += new System.EventHandler(CheckBox_In_A_CheckedChanged);
            // 
            // button_In_A
            // 
            button_In_A.Location = new System.Drawing.Point(40, 17);
            button_In_A.Name = "button_In_A";
            button_In_A.Size = new System.Drawing.Size(87, 27);
            button_In_A.TabIndex = 1;
            button_In_A.Text = "模拟量输入";
            button_In_A.UseVisualStyleBackColor = true;
            button_In_A.Click += new System.EventHandler(Button13_Click);
            // 
            // checkBox_Change_Page
            // 
            checkBox_Change_Page.AutoSize = true;
            checkBox_Change_Page.Location = new System.Drawing.Point(15, 23);
            checkBox_Change_Page.Name = "checkBox_Change_Page";
            checkBox_Change_Page.Size = new System.Drawing.Size(15, 14);
            checkBox_Change_Page.TabIndex = 12;
            checkBox_Change_Page.UseVisualStyleBackColor = true;
            checkBox_Change_Page.CheckedChanged += new System.EventHandler(CheckBox13_CheckedChanged);
            // 
            // button_Change_Page
            // 
            button_Change_Page.Location = new System.Drawing.Point(40, 17);
            button_Change_Page.Name = "button_Change_Page";
            button_Change_Page.Size = new System.Drawing.Size(87, 27);
            button_Change_Page.TabIndex = 13;
            button_Change_Page.Text = "页面切换";
            button_Change_Page.UseVisualStyleBackColor = true;
            button_Change_Page.Click += new System.EventHandler(Button14_Click);
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(710, 582);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(476, 290);
            textBox1.TabIndex = 19;
            textBox1.Visible = false;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(button_Enent_Mouse);
            groupBox6.Controls.Add(checkBox_Enent_Mouse);
            groupBox6.Controls.Add(button_Drag_V);
            groupBox6.Controls.Add(checkBox_Drag_V);
            groupBox6.Controls.Add(button_Drag_H);
            groupBox6.Controls.Add(checkBox_Drag_H);
            groupBox6.Controls.Add(button_Change_Page);
            groupBox6.Controls.Add(checkBox_Change_Page);
            groupBox6.Location = new System.Drawing.Point(169, 12);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new System.Drawing.Size(142, 159);
            groupBox6.TabIndex = 1;
            groupBox6.TabStop = false;
            groupBox6.Text = "鼠标动作";
            // 
            // button_Enent_Mouse
            // 
            button_Enent_Mouse.Location = new System.Drawing.Point(40, 122);
            button_Enent_Mouse.Name = "button_Enent_Mouse";
            button_Enent_Mouse.Size = new System.Drawing.Size(87, 27);
            button_Enent_Mouse.TabIndex = 19;
            button_Enent_Mouse.Text = "鼠标事件";
            button_Enent_Mouse.UseVisualStyleBackColor = true;
            button_Enent_Mouse.Click += new System.EventHandler(button21_Click);
            // 
            // checkBox_Enent_Mouse
            // 
            checkBox_Enent_Mouse.AutoSize = true;
            checkBox_Enent_Mouse.Location = new System.Drawing.Point(15, 128);
            checkBox_Enent_Mouse.Name = "checkBox_Enent_Mouse";
            checkBox_Enent_Mouse.Size = new System.Drawing.Size(15, 14);
            checkBox_Enent_Mouse.TabIndex = 18;
            checkBox_Enent_Mouse.UseVisualStyleBackColor = true;
            checkBox_Enent_Mouse.CheckedChanged += new System.EventHandler(checkBox20_CheckedChanged);
            // 
            // button_Drag_V
            // 
            button_Drag_V.Location = new System.Drawing.Point(40, 87);
            button_Drag_V.Name = "button_Drag_V";
            button_Drag_V.Size = new System.Drawing.Size(87, 27);
            button_Drag_V.TabIndex = 17;
            button_Drag_V.Text = "垂直拖拽";
            button_Drag_V.UseVisualStyleBackColor = true;
            button_Drag_V.Click += new System.EventHandler(button20_Click);
            // 
            // checkBox_Drag_V
            // 
            checkBox_Drag_V.AutoSize = true;
            checkBox_Drag_V.Location = new System.Drawing.Point(15, 93);
            checkBox_Drag_V.Name = "checkBox_Drag_V";
            checkBox_Drag_V.Size = new System.Drawing.Size(15, 14);
            checkBox_Drag_V.TabIndex = 16;
            checkBox_Drag_V.UseVisualStyleBackColor = true;
            checkBox_Drag_V.CheckedChanged += new System.EventHandler(checkBox19_CheckedChanged);
            // 
            // button_Drag_H
            // 
            button_Drag_H.Location = new System.Drawing.Point(40, 52);
            button_Drag_H.Name = "button_Drag_H";
            button_Drag_H.Size = new System.Drawing.Size(87, 27);
            button_Drag_H.TabIndex = 15;
            button_Drag_H.Text = "水平拖拽";
            button_Drag_H.UseVisualStyleBackColor = true;
            button_Drag_H.Click += new System.EventHandler(Button19_Click);
            // 
            // checkBox_Drag_H
            // 
            checkBox_Drag_H.AutoSize = true;
            checkBox_Drag_H.Location = new System.Drawing.Point(15, 58);
            checkBox_Drag_H.Name = "checkBox_Drag_H";
            checkBox_Drag_H.Size = new System.Drawing.Size(15, 14);
            checkBox_Drag_H.TabIndex = 14;
            checkBox_Drag_H.UseVisualStyleBackColor = true;
            checkBox_Drag_H.CheckedChanged += new System.EventHandler(CheckBox18_CheckedChanged);
            // 
            // buttonJinglingPand
            // 
            buttonJinglingPand.Location = new System.Drawing.Point(15, 87);
            buttonJinglingPand.Name = "buttonJinglingPand";
            buttonJinglingPand.Size = new System.Drawing.Size(112, 27);
            buttonJinglingPand.TabIndex = 44;
            buttonJinglingPand.Text = "精灵面板";
            buttonJinglingPand.UseVisualStyleBackColor = true;
            buttonJinglingPand.Click += new System.EventHandler(Button24_Click);
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(checkBox_DB_Creat);
            groupBox7.Controls.Add(button_DB_Creat);
            groupBox7.Controls.Add(checkBox_DB_MultiOperate);
            groupBox7.Controls.Add(button_DB_MultiOperate);
            groupBox7.Controls.Add(checkBox_DB_Delete);
            groupBox7.Controls.Add(checkBox_DB_Update);
            groupBox7.Controls.Add(checkBox_DB_Insert);
            groupBox7.Controls.Add(button_DB_Delete);
            groupBox7.Controls.Add(checkBox_DB_Select);
            groupBox7.Controls.Add(button_DB_Update);
            groupBox7.Controls.Add(button_DB_Insert);
            groupBox7.Controls.Add(button_DB_Select);
            groupBox7.Location = new System.Drawing.Point(647, 12);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new System.Drawing.Size(142, 235);
            groupBox7.TabIndex = 10;
            groupBox7.TabStop = false;
            groupBox7.Text = "数据库操作";
            // 
            // checkBox_DB_Creat
            // 
            checkBox_DB_Creat.AutoSize = true;
            checkBox_DB_Creat.Location = new System.Drawing.Point(15, 23);
            checkBox_DB_Creat.Name = "checkBox_DB_Creat";
            checkBox_DB_Creat.Size = new System.Drawing.Size(15, 14);
            checkBox_DB_Creat.TabIndex = 58;
            checkBox_DB_Creat.UseVisualStyleBackColor = true;
            checkBox_DB_Creat.CheckedChanged += new System.EventHandler(checkBox28_CheckedChanged);
            // 
            // button_DB_Creat
            // 
            button_DB_Creat.Location = new System.Drawing.Point(40, 17);
            button_DB_Creat.Name = "button_DB_Creat";
            button_DB_Creat.Size = new System.Drawing.Size(87, 27);
            button_DB_Creat.TabIndex = 57;
            button_DB_Creat.Text = "新建表";
            button_DB_Creat.UseVisualStyleBackColor = true;
            button_DB_Creat.Click += new System.EventHandler(button32_Click);
            // 
            // checkBox_DB_MultiOperate
            // 
            checkBox_DB_MultiOperate.AutoSize = true;
            checkBox_DB_MultiOperate.Location = new System.Drawing.Point(15, 203);
            checkBox_DB_MultiOperate.Name = "checkBox_DB_MultiOperate";
            checkBox_DB_MultiOperate.Size = new System.Drawing.Size(15, 14);
            checkBox_DB_MultiOperate.TabIndex = 53;
            checkBox_DB_MultiOperate.UseVisualStyleBackColor = true;
            checkBox_DB_MultiOperate.CheckedChanged += new System.EventHandler(CheckBox27_CheckedChanged);
            // 
            // button_DB_MultiOperate
            // 
            button_DB_MultiOperate.Location = new System.Drawing.Point(40, 197);
            button_DB_MultiOperate.Name = "button_DB_MultiOperate";
            button_DB_MultiOperate.Size = new System.Drawing.Size(87, 27);
            button_DB_MultiOperate.TabIndex = 54;
            button_DB_MultiOperate.Text = "复合操作";
            button_DB_MultiOperate.UseVisualStyleBackColor = true;
            button_DB_MultiOperate.Click += new System.EventHandler(Button29_Click);
            // 
            // checkBox_DB_Delete
            // 
            checkBox_DB_Delete.AutoSize = true;
            checkBox_DB_Delete.Location = new System.Drawing.Point(15, 167);
            checkBox_DB_Delete.Name = "checkBox_DB_Delete";
            checkBox_DB_Delete.Size = new System.Drawing.Size(15, 14);
            checkBox_DB_Delete.TabIndex = 51;
            checkBox_DB_Delete.UseVisualStyleBackColor = true;
            checkBox_DB_Delete.CheckedChanged += new System.EventHandler(checkBox26_CheckedChanged);
            // 
            // checkBox_DB_Update
            // 
            checkBox_DB_Update.AutoSize = true;
            checkBox_DB_Update.Location = new System.Drawing.Point(15, 131);
            checkBox_DB_Update.Name = "checkBox_DB_Update";
            checkBox_DB_Update.Size = new System.Drawing.Size(15, 14);
            checkBox_DB_Update.TabIndex = 49;
            checkBox_DB_Update.UseVisualStyleBackColor = true;
            checkBox_DB_Update.CheckedChanged += new System.EventHandler(CheckBox23_CheckedChanged);
            // 
            // checkBox_DB_Insert
            // 
            checkBox_DB_Insert.AutoSize = true;
            checkBox_DB_Insert.Location = new System.Drawing.Point(15, 95);
            checkBox_DB_Insert.Name = "checkBox_DB_Insert";
            checkBox_DB_Insert.Size = new System.Drawing.Size(15, 14);
            checkBox_DB_Insert.TabIndex = 47;
            checkBox_DB_Insert.UseVisualStyleBackColor = true;
            checkBox_DB_Insert.CheckedChanged += new System.EventHandler(checkBox24_CheckedChanged);
            // 
            // button_DB_Delete
            // 
            button_DB_Delete.Location = new System.Drawing.Point(40, 161);
            button_DB_Delete.Name = "button_DB_Delete";
            button_DB_Delete.Size = new System.Drawing.Size(87, 27);
            button_DB_Delete.TabIndex = 52;
            button_DB_Delete.Text = "删除数据";
            button_DB_Delete.UseVisualStyleBackColor = true;
            button_DB_Delete.Click += new System.EventHandler(Button28_Click);
            // 
            // checkBox_DB_Select
            // 
            checkBox_DB_Select.AutoSize = true;
            checkBox_DB_Select.Location = new System.Drawing.Point(15, 59);
            checkBox_DB_Select.Name = "checkBox_DB_Select";
            checkBox_DB_Select.Size = new System.Drawing.Size(15, 14);
            checkBox_DB_Select.TabIndex = 45;
            checkBox_DB_Select.UseVisualStyleBackColor = true;
            checkBox_DB_Select.CheckedChanged += new System.EventHandler(CheckBox25_CheckedChanged);
            // 
            // button_DB_Update
            // 
            button_DB_Update.Location = new System.Drawing.Point(40, 125);
            button_DB_Update.Name = "button_DB_Update";
            button_DB_Update.Size = new System.Drawing.Size(87, 27);
            button_DB_Update.TabIndex = 50;
            button_DB_Update.Text = "更新数据";
            button_DB_Update.UseVisualStyleBackColor = true;
            button_DB_Update.Click += new System.EventHandler(button25_Click);
            // 
            // button_DB_Insert
            // 
            button_DB_Insert.Location = new System.Drawing.Point(40, 89);
            button_DB_Insert.Name = "button_DB_Insert";
            button_DB_Insert.Size = new System.Drawing.Size(87, 27);
            button_DB_Insert.TabIndex = 48;
            button_DB_Insert.Text = "添加数据";
            button_DB_Insert.UseVisualStyleBackColor = true;
            button_DB_Insert.Click += new System.EventHandler(Button26_Click);
            // 
            // button_DB_Select
            // 
            button_DB_Select.Location = new System.Drawing.Point(40, 53);
            button_DB_Select.Name = "button_DB_Select";
            button_DB_Select.Size = new System.Drawing.Size(87, 27);
            button_DB_Select.TabIndex = 46;
            button_DB_Select.Text = "查询数据";
            button_DB_Select.UseVisualStyleBackColor = true;
            button_DB_Select.Click += new System.EventHandler(button27_Click);
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(buttonEventBind);
            groupBox8.Controls.Add(buttonJinglingPand);
            groupBox8.Controls.Add(button_PropertyBind);
            groupBox8.Location = new System.Drawing.Point(489, 147);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new System.Drawing.Size(142, 125);
            groupBox8.TabIndex = 9;
            groupBox8.TabStop = false;
            groupBox8.Text = "控件绑定";
            // 
            // buttonEventBind
            // 
            buttonEventBind.Location = new System.Drawing.Point(15, 52);
            buttonEventBind.Name = "buttonEventBind";
            buttonEventBind.Size = new System.Drawing.Size(112, 27);
            buttonEventBind.TabIndex = 43;
            buttonEventBind.Text = "事件绑定";
            buttonEventBind.UseVisualStyleBackColor = true;
            buttonEventBind.Click += new System.EventHandler(button31_Click);
            // 
            // button_PropertyBind
            // 
            button_PropertyBind.Location = new System.Drawing.Point(15, 17);
            button_PropertyBind.Name = "button_PropertyBind";
            button_PropertyBind.Size = new System.Drawing.Size(112, 27);
            button_PropertyBind.TabIndex = 42;
            button_PropertyBind.Text = "属性绑定";
            button_PropertyBind.UseVisualStyleBackColor = true;
            button_PropertyBind.Click += new System.EventHandler(button30_Click);
            // 
            // dhljForm
            // 
            AcceptButton = button_Close;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(811, 293);
            Controls.Add(groupBox6);
            Controls.Add(textBox1);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox8);
            Controls.Add(groupBox3);
            Controls.Add(groupBox7);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button_Close);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "dhljForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "动画连接";
            FormClosed += new System.Windows.Forms.FormClosedEventHandler(DhljForm_FormClosed);
            Load += new System.EventHandler(Form2_Load);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox8.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

    }
}
