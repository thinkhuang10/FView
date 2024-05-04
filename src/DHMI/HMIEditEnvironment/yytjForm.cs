using DevExpress.XtraEditors;
using HMIEditEnvironment.Properties;
using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class yytjForm : XtraForm
{
    private readonly List<CIOItem> IOS = new();

    private readonly List<ProjectIO> PIOS;

    private readonly HMIProjectFile dhp;

    private readonly List<DataFile> dfs;

    private readonly DataTable dt = new();

    private readonly List<CRow> CRS = new();

    private DataGridView dataGridView1;

    public yytjForm(HMIProjectFile _hpf, List<DataFile> _dfs, CIOItem _ioroot, List<ProjectIO> _PIOS)
    {
        InitializeComponent();

        dhp = _hpf;
        dfs = _dfs;
        ScanIO(IOS, _ioroot);
        PIOS = _PIOS;
    }

    public DataRow FindInUse(string ItemName)
    {
        yytjForm_Load(null, null);
        DataRow[] array = dt.Select("名称='" + ItemName + "'");
        if (array.Length == 0)
        {
            return null;
        }
        return array[0];
    }

    public void FindInUseInitial()
    {
        yytjForm_Load(null, null);
    }

    public DataRow FindInUseNew(string ItemName)
    {
        DataRow[] array = dt.Select("名称='" + ItemName + "'");
        if (array.Length == 0)
        {
            return null;
        }
        return array[0];
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    public void ScanIO(List<CIOItem> LIO, CIOItem root)
    {
        foreach (CIOItem item in root.subitem)
        {
            if (item.IsLeaf)
            {
                LIO.Add(item);
            }
            ScanIO(LIO, item);
        }
    }

    private void yytjForm_Load(object sender, EventArgs e)
    {
        dt.Columns.Add(" ", typeof(Bitmap));
        dt.Columns.Add("名称");
        dt.Columns.Add("标签");
        dt.Columns.Add("类型");
        dt.Columns.Add("画面");
        dt.Columns.Add("元素");
        dt.Columns.Add("引用类型");
        dt.Columns.Add("引用情况");
        foreach (CIOItem iO in IOS)
        {
            CRow cRow = new();
            if (dhp.cxdzqdLogic.Contains("[" + iO.name + "]"))
            {
                if (cRow.name == "")
                {
                    cRow.name = iO.name;
                    cRow.tag = iO.tag;
                    cRow.type = iO.type;
                    cRow.page = "";
                    cRow.ytpye = "程序启动脚本";
                    cRow.text = "程序启动脚本内容";
                }
                else
                {
                    CRow cRow2 = new()
                    {
                        name = iO.name,
                        tag = iO.tag,
                        type = iO.type,
                        page = "",
                        ytpye = "程序启动脚本",
                        text = "程序启动脚本内容"
                    };
                    cRow.LR.Add(cRow2);
                }
            }
            if (dhp.cxdzyxLogic.Contains("[" + iO.name + "]"))
            {
                if (cRow.name == "")
                {
                    cRow.name = iO.name;
                    cRow.tag = iO.tag;
                    cRow.type = iO.type;
                    cRow.page = "";
                    cRow.ytpye = "程序运行脚本";
                    cRow.text = "程序运行脚本内容";
                }
                else
                {
                    CRow cRow3 = new()
                    {
                        name = iO.name,
                        tag = iO.tag,
                        type = iO.type,
                        page = "",
                        ytpye = "程序运行脚本",
                        text = "程序运行脚本内容"
                    };
                    cRow.LR.Add(cRow3);
                }
            }
            if (dhp.cxdzgbLogic.Contains("[" + iO.name + "]"))
            {
                if (cRow.name == "")
                {
                    cRow.name = iO.name;
                    cRow.tag = iO.tag;
                    cRow.type = iO.type;
                    cRow.page = "";
                    cRow.ytpye = "程序关闭脚本";
                    cRow.text = "程序关闭脚本内容";
                }
                else
                {
                    CRow cRow4 = new()
                    {
                        name = iO.name,
                        tag = iO.tag,
                        type = iO.type,
                        page = "",
                        ytpye = "程序关闭脚本",
                        text = "程序关闭脚本内容"
                    };
                    cRow.LR.Add(cRow4);
                }
            }
            string[] cxdzqdioios = dhp.cxdzqdioios;
            foreach (string text in cxdzqdioios)
            {
                if (text != null && text.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序启动变量绑定";
                        cRow.text = text;
                    }
                    else
                    {
                        CRow cRow5 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序启动变量绑定",
                            text = text
                        };
                        cRow.LR.Add(cRow5);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzqdiotjs;
            foreach (string text2 in cxdzqdioios)
            {
                if (text2 != null && text2.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序启动变量条件";
                        cRow.text = text2;
                    }
                    else
                    {
                        CRow cRow6 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序启动变量条件",
                            text = text2
                        };
                        cRow.LR.Add(cRow6);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzqdiozhis;
            foreach (string text3 in cxdzqdioios)
            {
                if (text3 != null && text3.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序启动变量赋值";
                        cRow.text = text3;
                    }
                    else
                    {
                        CRow cRow7 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序启动变量赋值",
                            text = text3
                        };
                        cRow.LR.Add(cRow7);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzqditemfftjs;
            foreach (string text4 in cxdzqdioios)
            {
                if (text4 != null && text4.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序启动元素方法条件";
                        cRow.text = text4;
                    }
                    else
                    {
                        CRow cRow8 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序启动元素方法条件",
                            text = text4
                        };
                        cRow.LR.Add(cRow8);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzqditemffbdss;
            foreach (string text5 in cxdzqdioios)
            {
                if (text5 != null && text5.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序启动元素方法表达式";
                        cRow.text = text5;
                    }
                    else
                    {
                        CRow cRow9 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序启动元素方法表达式",
                            text = text5
                        };
                        cRow.LR.Add(cRow9);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzqditemtjs;
            foreach (string text6 in cxdzqdioios)
            {
                if (text6 != null && text6.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序启动元素属性条件";
                        cRow.text = text6;
                    }
                    else
                    {
                        CRow cRow10 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "条件",
                            text = text6
                        };
                        cRow.LR.Add(cRow10);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzqditemzhis;
            foreach (string text7 in cxdzqdioios)
            {
                if (text7 != null && text7.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序启动元素属性赋值";
                        cRow.text = text7;
                    }
                    else
                    {
                        CRow cRow11 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序启动元素属性赋值",
                            text = text7
                        };
                        cRow.LR.Add(cRow11);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxioios;
            foreach (string text8 in cxdzqdioios)
            {
                if (text8 != null && text8.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序运行变量绑定";
                        cRow.text = text8;
                    }
                    else
                    {
                        CRow cRow12 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序运行变量绑定",
                            text = text8
                        };
                        cRow.LR.Add(cRow12);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxiotjs;
            foreach (string text9 in cxdzqdioios)
            {
                if (text9 != null && text9.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序运行变量绑定条件";
                        cRow.text = text9;
                    }
                    else
                    {
                        CRow cRow13 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序运行变量绑定条件",
                            text = text9
                        };
                        cRow.LR.Add(cRow13);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxiozhis;
            foreach (string text10 in cxdzqdioios)
            {
                if (text10 != null && text10.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序运行变量赋值";
                        cRow.text = text10;
                    }
                    else
                    {
                        CRow cRow14 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序运行变量赋值",
                            text = text10
                        };
                        cRow.LR.Add(cRow14);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxitemfftjs;
            foreach (string text11 in cxdzqdioios)
            {
                if (text11 != null && text11.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序运行元素方法条件";
                        cRow.text = text11;
                    }
                    else
                    {
                        CRow cRow15 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序运行元素方法条件",
                            text = text11
                        };
                        cRow.LR.Add(cRow15);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxitemffbdss;
            foreach (string text12 in cxdzqdioios)
            {
                if (text12 != null && text12.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序运行元素方法表达式";
                        cRow.text = text12;
                    }
                    else
                    {
                        CRow cRow16 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序运行元素方法表达式",
                            text = text12
                        };
                        cRow.LR.Add(cRow16);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxitemtjs;
            foreach (string text13 in cxdzqdioios)
            {
                if (text13 != null && text13.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序运行元素属性条件";
                        cRow.text = text13;
                    }
                    else
                    {
                        CRow cRow17 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序运行元素属性条件",
                            text = text13
                        };
                        cRow.LR.Add(cRow17);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxitemzhis;
            foreach (string text14 in cxdzqdioios)
            {
                if (text14 != null && text14.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序运行元素属性赋值";
                        cRow.text = text14;
                    }
                    else
                    {
                        CRow cRow18 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序运行元素属性赋值",
                            text = text14
                        };
                        cRow.LR.Add(cRow18);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbioios;
            foreach (string text15 in cxdzqdioios)
            {
                if (text15 != null && text15.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序关闭变量绑定";
                        cRow.text = text15;
                    }
                    else
                    {
                        CRow cRow19 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序关闭变量绑定",
                            text = text15
                        };
                        cRow.LR.Add(cRow19);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbiotjs;
            foreach (string text16 in cxdzqdioios)
            {
                if (text16 != null && text16.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序关闭变量条件";
                        cRow.text = text16;
                    }
                    else
                    {
                        CRow cRow20 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序关闭变量条件",
                            text = text16
                        };
                        cRow.LR.Add(cRow20);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbiozhis;
            foreach (string text17 in cxdzqdioios)
            {
                if (text17 != null && text17.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序关闭变量赋值";
                        cRow.text = text17;
                    }
                    else
                    {
                        CRow cRow21 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序关闭变量赋值",
                            text = text17
                        };
                        cRow.LR.Add(cRow21);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbitemfftjs;
            foreach (string text18 in cxdzqdioios)
            {
                if (text18 != null && text18.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序关闭元素方法条件";
                        cRow.text = text18;
                    }
                    else
                    {
                        CRow cRow22 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序关闭元素方法条件",
                            text = text18
                        };
                        cRow.LR.Add(cRow22);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbitemffbdss;
            foreach (string text19 in cxdzqdioios)
            {
                if (text19 != null && text19.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序关闭元素方法表达式";
                        cRow.text = text19;
                    }
                    else
                    {
                        CRow cRow23 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序关闭元素方法表达式",
                            text = text19
                        };
                        cRow.LR.Add(cRow23);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbitemtjs;
            foreach (string text20 in cxdzqdioios)
            {
                if (text20 != null && text20.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序关闭元素属性条件";
                        cRow.text = text20;
                    }
                    else
                    {
                        CRow cRow24 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序关闭元素属性条件",
                            text = text20
                        };
                        cRow.LR.Add(cRow24);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbitemzhis;
            foreach (string text21 in cxdzqdioios)
            {
                if (text21 != null && text21.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = "";
                        cRow.ytpye = "程序关闭元素属性赋值";
                        cRow.text = text21;
                    }
                    else
                    {
                        CRow cRow25 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = "",
                            ytpye = "程序关闭元素属性赋值",
                            text = text21
                        };
                        cRow.LR.Add(cRow25);
                    }
                }
            }
            foreach (DataFile df in dfs)
            {
                if (df.pagedzqdLogic.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = df.name;
                        cRow.ytpye = "绑定变量";
                        cRow.text = "页面显示脚本部分";
                    }
                    else
                    {
                        CRow cRow26 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = df.name,
                            ytpye = "绑定变量",
                            text = "页面显示脚本部分"
                        };
                        cRow.LR.Add(cRow26);
                    }
                }
                if (df.pagedzyxLogic.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = df.name;
                        cRow.ytpye = "绑定变量";
                        cRow.text = "页面运行脚本部分";
                    }
                    else
                    {
                        CRow cRow27 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = df.name,
                            ytpye = "绑定变量",
                            text = "页面运行脚本部分"
                        };
                        cRow.LR.Add(cRow27);
                    }
                }
                if (df.pagedzgbLogic.Contains("[" + iO.name + "]"))
                {
                    if (cRow.name == "")
                    {
                        cRow.name = iO.name;
                        cRow.tag = iO.tag;
                        cRow.type = iO.type;
                        cRow.page = df.name;
                        cRow.ytpye = "绑定变量";
                        cRow.text = "页面隐藏脚本部分";
                    }
                    else
                    {
                        CRow cRow28 = new()
                        {
                            name = iO.name,
                            tag = iO.tag,
                            type = iO.type,
                            page = df.name,
                            ytpye = "绑定变量",
                            text = "页面隐藏脚本部分"
                        };
                        cRow.LR.Add(cRow28);
                    }
                }
                cxdzqdioios = df.pagedzqdioios;
                foreach (string text22 in cxdzqdioios)
                {
                    if (text22 != null && text22.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面绑定变量";
                            cRow.text = text22;
                        }
                        else
                        {
                            CRow cRow29 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面绑定变量",
                                text = text22
                            };
                            cRow.LR.Add(cRow29);
                        }
                    }
                }
                cxdzqdioios = df.pagedzqdiotjs;
                foreach (string text23 in cxdzqdioios)
                {
                    if (text23 != null && text23.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面条件";
                            cRow.text = text23;
                        }
                        else
                        {
                            CRow cRow30 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面条件",
                                text = text23
                            };
                            cRow.LR.Add(cRow30);
                        }
                    }
                }
                cxdzqdioios = df.pagedzqdiozhis;
                foreach (string text24 in cxdzqdioios)
                {
                    if (text24 != null && text24.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面赋值";
                            cRow.text = text24;
                        }
                        else
                        {
                            CRow cRow31 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面赋值",
                                text = text24
                            };
                            cRow.LR.Add(cRow31);
                        }
                    }
                }
                cxdzqdioios = df.pagedzqditemfftjs;
                foreach (string text25 in cxdzqdioios)
                {
                    if (text25 != null && text25.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面条件";
                            cRow.text = text25;
                        }
                        else
                        {
                            CRow cRow32 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面条件",
                                text = text25
                            };
                            cRow.LR.Add(cRow32);
                        }
                    }
                }
                cxdzqdioios = df.pagedzqditemffbdss;
                foreach (string text26 in cxdzqdioios)
                {
                    if (text26 != null && text26.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面方法";
                            cRow.text = text26;
                        }
                        else
                        {
                            CRow cRow33 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面方法",
                                text = text26
                            };
                            cRow.LR.Add(cRow33);
                        }
                    }
                }
                cxdzqdioios = df.pagedzqditemtjs;
                foreach (string text27 in cxdzqdioios)
                {
                    if (text27 != null && text27.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面条件";
                            cRow.text = text27;
                        }
                        else
                        {
                            CRow cRow34 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面条件",
                                text = text27
                            };
                            cRow.LR.Add(cRow34);
                        }
                    }
                }
                cxdzqdioios = df.pagedzqditemzhis;
                foreach (string text28 in cxdzqdioios)
                {
                    if (text28 != null && text28.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面赋值";
                            cRow.text = text28;
                        }
                        else
                        {
                            CRow cRow35 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面赋值",
                                text = text28
                            };
                            cRow.LR.Add(cRow35);
                        }
                    }
                }
                cxdzqdioios = df.pagedzyxioios;
                foreach (string text29 in cxdzqdioios)
                {
                    if (text29 != null && text29.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面绑定变量";
                            cRow.text = text29;
                        }
                        else
                        {
                            CRow cRow36 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面绑定变量",
                                text = text29
                            };
                            cRow.LR.Add(cRow36);
                        }
                    }
                }
                cxdzqdioios = df.pagedzyxiotjs;
                foreach (string text30 in cxdzqdioios)
                {
                    if (text30 != null && text30.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面条件";
                            cRow.text = text30;
                        }
                        else
                        {
                            CRow cRow37 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面条件",
                                text = text30
                            };
                            cRow.LR.Add(cRow37);
                        }
                    }
                }
                cxdzqdioios = df.pagedzyxiozhis;
                foreach (string text31 in cxdzqdioios)
                {
                    if (text31 != null && text31.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面赋值";
                            cRow.text = text31;
                        }
                        else
                        {
                            CRow cRow38 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面赋值",
                                text = text31
                            };
                            cRow.LR.Add(cRow38);
                        }
                    }
                }
                cxdzqdioios = df.pagedzyxitemfftjs;
                foreach (string text32 in cxdzqdioios)
                {
                    if (text32 != null && text32.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面条件";
                            cRow.text = text32;
                        }
                        else
                        {
                            CRow cRow39 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面条件",
                                text = text32
                            };
                            cRow.LR.Add(cRow39);
                        }
                    }
                }
                cxdzqdioios = df.pagedzyxitemffbdss;
                foreach (string text33 in cxdzqdioios)
                {
                    if (text33 != null && text33.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面方法";
                            cRow.text = text33;
                        }
                        else
                        {
                            CRow cRow40 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面方法",
                                text = text33
                            };
                            cRow.LR.Add(cRow40);
                        }
                    }
                }
                cxdzqdioios = df.pagedzyxitemtjs;
                foreach (string text34 in cxdzqdioios)
                {
                    if (text34 != null && text34.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面条件";
                            cRow.text = text34;
                        }
                        else
                        {
                            CRow cRow41 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面条件",
                                text = text34
                            };
                            cRow.LR.Add(cRow41);
                        }
                    }
                }
                cxdzqdioios = df.pagedzyxitemzhis;
                foreach (string text35 in cxdzqdioios)
                {
                    if (text35 != null && text35.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面赋值";
                            cRow.text = text35;
                        }
                        else
                        {
                            CRow cRow42 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面赋值",
                                text = text35
                            };
                            cRow.LR.Add(cRow42);
                        }
                    }
                }
                cxdzqdioios = df.pagedzgbioios;
                foreach (string text36 in cxdzqdioios)
                {
                    if (text36 != null && text36.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面绑定变量";
                            cRow.text = text36;
                        }
                        else
                        {
                            CRow cRow43 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面绑定变量",
                                text = text36
                            };
                            cRow.LR.Add(cRow43);
                        }
                    }
                }
                cxdzqdioios = df.pagedzgbiotjs;
                foreach (string text37 in cxdzqdioios)
                {
                    if (text37 != null && text37.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面条件";
                            cRow.text = text37;
                        }
                        else
                        {
                            CRow cRow44 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面条件",
                                text = text37
                            };
                            cRow.LR.Add(cRow44);
                        }
                    }
                }
                cxdzqdioios = df.pagedzgbiozhis;
                foreach (string text38 in cxdzqdioios)
                {
                    if (text38 != null && text38.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面赋值";
                            cRow.text = text38;
                        }
                        else
                        {
                            CRow cRow45 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面赋值",
                                text = text38
                            };
                            cRow.LR.Add(cRow45);
                        }
                    }
                }
                cxdzqdioios = df.pagedzgbitemfftjs;
                foreach (string text39 in cxdzqdioios)
                {
                    if (text39 != null && text39.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面条件";
                            cRow.text = text39;
                        }
                        else
                        {
                            CRow cRow46 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面条件",
                                text = text39
                            };
                            cRow.LR.Add(cRow46);
                        }
                    }
                }
                cxdzqdioios = df.pagedzgbitemffbdss;
                foreach (string text40 in cxdzqdioios)
                {
                    if (text40 != null && text40.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面方法";
                            cRow.text = text40;
                        }
                        else
                        {
                            CRow cRow47 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面方法",
                                text = text40
                            };
                            cRow.LR.Add(cRow47);
                        }
                    }
                }
                cxdzqdioios = df.pagedzgbitemtjs;
                foreach (string text41 in cxdzqdioios)
                {
                    if (text41 != null && text41.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面条件";
                            cRow.text = text41;
                        }
                        else
                        {
                            CRow cRow48 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面条件",
                                text = text41
                            };
                            cRow.LR.Add(cRow48);
                        }
                    }
                }
                cxdzqdioios = df.pagedzgbitemzhis;
                foreach (string text42 in cxdzqdioios)
                {
                    if (text42 != null && text42.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.ytpye = "页面赋值";
                            cRow.text = text42;
                        }
                        else
                        {
                            CRow cRow49 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                ytpye = "页面赋值",
                                text = text42
                            };
                            cRow.LR.Add(cRow49);
                        }
                    }
                }
                foreach (CShape item in df.ListAllShowCShape)
                {
                    if (item.bxysbhbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.bxysbhbianliang;
                        }
                        else
                        {
                            CRow cRow50 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.bxysbhbianliang
                            };
                            cRow.LR.Add(cRow50);
                        }
                    }
                    if (item.czbfbbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.czbfbbianliang;
                        }
                        else
                        {
                            CRow cRow51 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.czbfbbianliang
                            };
                            cRow.LR.Add(cRow51);
                        }
                    }
                    if (item.cztzbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.cztzbianliang;
                        }
                        else
                        {
                            CRow cRow52 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.cztzbianliang
                            };
                            cRow.LR.Add(cRow52);
                        }
                    }
                    if (item.czydbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.czydbianliang;
                        }
                        else
                        {
                            CRow cRow53 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.czydbianliang
                            };
                            cRow.LR.Add(cRow53);
                        }
                    }
                    if (item.gdbhbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.gdbhbianliang;
                        }
                        else
                        {
                            CRow cRow54 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.gdbhbianliang
                            };
                            cRow.LR.Add(cRow54);
                        }
                    }
                    if (item.kdbhbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.kdbhbianliang;
                        }
                        else
                        {
                            CRow cRow55 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.kdbhbianliang
                            };
                            cRow.LR.Add(cRow55);
                        }
                    }
                    if (item.mbxzbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.mbxzbianliang;
                        }
                        else
                        {
                            CRow cRow56 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.mbxzbianliang
                            };
                            cRow.LR.Add(cRow56);
                        }
                    }
                    if (item.spbfbbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.spbfbbianliang;
                        }
                        else
                        {
                            CRow cRow57 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.spbfbbianliang
                            };
                            cRow.LR.Add(cRow57);
                        }
                    }
                    if (item.sptzbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.sptzbianliang;
                        }
                        else
                        {
                            CRow cRow58 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.sptzbianliang
                            };
                            cRow.LR.Add(cRow58);
                        }
                    }
                    if (item.spydbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.spydbianliang;
                        }
                        else
                        {
                            CRow cRow59 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.spydbianliang
                            };
                            cRow.LR.Add(cRow59);
                        }
                    }
                    if (item.tcs1ysbhbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.tcs1ysbhbianliang;
                        }
                        else
                        {
                            CRow cRow60 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.tcs1ysbhbianliang
                            };
                            cRow.LR.Add(cRow60);
                        }
                    }
                    if (item.tcs2ysbhbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.tcs2ysbhbianliang;
                        }
                        else
                        {
                            CRow cRow61 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.tcs2ysbhbianliang
                            };
                            cRow.LR.Add(cRow61);
                        }
                    }
                    if (item.txycbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.txycbianliang;
                        }
                        else
                        {
                            CRow cRow62 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.txycbianliang
                            };
                            cRow.LR.Add(cRow62);
                        }
                    }
                    if (item.zfcscbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.zfcscbianliang;
                        }
                        else
                        {
                            CRow cRow63 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.zfcscbianliang
                            };
                            cRow.LR.Add(cRow63);
                        }
                    }
                    if (item.aobianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.aobianliang;
                        }
                        else
                        {
                            CRow cRow64 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.aobianliang
                            };
                            cRow.LR.Add(cRow64);
                        }
                    }
                    if (item.dobianlaing.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.dobianlaing;
                        }
                        else
                        {
                            CRow cRow65 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.dobianlaing
                            };
                            cRow.LR.Add(cRow65);
                        }
                    }
                    if (item.zfcsrbianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.zfcsrbianliang;
                        }
                        else
                        {
                            CRow cRow66 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.zfcsrbianliang
                            };
                            cRow.LR.Add(cRow66);
                        }
                    }
                    if (item.aibianliang.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.aibianliang;
                        }
                        else
                        {
                            CRow cRow67 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.aibianliang
                            };
                            cRow.LR.Add(cRow67);
                        }
                    }
                    if (item.dibianlaing.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "变量绑定";
                            cRow.text = item.dibianlaing;
                        }
                        else
                        {
                            CRow cRow68 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "变量绑定",
                                text = item.dibianlaing
                            };
                            cRow.LR.Add(cRow68);
                        }
                    }
                    cxdzqdioios = item.UserLogic;
                    foreach (string text43 in cxdzqdioios)
                    {
                        if (text43 != null && text43.Contains("[" + iO.name + "]"))
                        {
                            if (cRow.name == "")
                            {
                                cRow.name = iO.name;
                                cRow.tag = iO.tag;
                                cRow.type = iO.type;
                                cRow.page = df.name;
                                cRow.shapename = item.ShapeName;
                                cRow.ytpye = "脚本";
                                cRow.text = "";
                            }
                            else
                            {
                                CRow cRow69 = new()
                                {
                                    name = iO.name,
                                    tag = iO.tag,
                                    type = iO.type,
                                    page = df.name,
                                    shapename = item.ShapeName,
                                    ytpye = "脚本",
                                    text = ""
                                };
                                cRow.LR.Add(cRow69);
                            }
                        }
                    }
                    if (item.DBOKScriptUser != null && item.DBOKScriptUser.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "脚本";
                            cRow.text = "";
                        }
                        else
                        {
                            CRow cRow70 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "脚本",
                                text = ""
                            };
                            cRow.LR.Add(cRow70);
                        }
                    }
                    if (item.DBErrScriptUser != null && item.DBOKScriptUser.Contains("[" + iO.name + "]"))
                    {
                        if (cRow.name == "")
                        {
                            cRow.name = iO.name;
                            cRow.tag = iO.tag;
                            cRow.type = iO.type;
                            cRow.page = df.name;
                            cRow.shapename = item.ShapeName;
                            cRow.ytpye = "脚本";
                            cRow.text = "";
                        }
                        else
                        {
                            CRow cRow71 = new()
                            {
                                name = iO.name,
                                tag = iO.tag,
                                type = iO.type,
                                page = df.name,
                                shapename = item.ShapeName,
                                ytpye = "脚本",
                                text = ""
                            };
                            cRow.LR.Add(cRow71);
                        }
                    }
                    cxdzqdioios = item.ysdzshijianbiaodashi;
                    foreach (string text44 in cxdzqdioios)
                    {
                        if (text44 != null && text44.Contains("[" + iO.name + "]"))
                        {
                            if (cRow.name == "")
                            {
                                cRow.name = iO.name;
                                cRow.tag = iO.tag;
                                cRow.type = iO.type;
                                cRow.page = df.name;
                                cRow.shapename = item.ShapeName;
                                cRow.ytpye = "元素动作脚本";
                                cRow.text = "";
                            }
                            else
                            {
                                CRow cRow72 = new()
                                {
                                    name = iO.name,
                                    tag = iO.tag,
                                    type = iO.type,
                                    page = df.name,
                                    shapename = item.ShapeName,
                                    ytpye = "元素动作脚本",
                                    text = ""
                                };
                                cRow.LR.Add(cRow72);
                            }
                        }
                    }
                }
            }
            if (cRow.name != "")
            {
                CRS.Add(cRow);
            }
        }
        foreach (ProjectIO pIO in PIOS)
        {
            CRow cRow73 = new();
            if (dhp.cxdzqdLogic.Contains("[" + pIO.name + "]"))
            {
                if (cRow73.name == "")
                {
                    cRow73.name = pIO.name;
                    cRow73.tag = pIO.tag;
                    cRow73.type = pIO.type;
                    cRow73.page = "";
                    cRow73.ytpye = "程序启动脚本";
                    cRow73.text = "程序启动脚本内容";
                }
                else
                {
                    CRow cRow74 = new()
                    {
                        name = pIO.name,
                        tag = pIO.tag,
                        type = pIO.type,
                        page = "",
                        ytpye = "程序启动脚本",
                        text = "程序启动脚本内容"
                    };
                    cRow73.LR.Add(cRow74);
                }
            }
            if (dhp.cxdzyxLogic.Contains("[" + pIO.name + "]"))
            {
                if (cRow73.name == "")
                {
                    cRow73.name = pIO.name;
                    cRow73.tag = pIO.tag;
                    cRow73.type = pIO.type;
                    cRow73.page = "";
                    cRow73.ytpye = "程序运行脚本";
                    cRow73.text = "程序运行脚本内容";
                }
                else
                {
                    CRow cRow75 = new()
                    {
                        name = pIO.name,
                        tag = pIO.tag,
                        type = pIO.type,
                        page = "",
                        ytpye = "程序运行脚本",
                        text = "程序运行脚本内容"
                    };
                    cRow73.LR.Add(cRow75);
                }
            }
            if (dhp.cxdzgbLogic.Contains("[" + pIO.name + "]"))
            {
                if (cRow73.name == "")
                {
                    cRow73.name = pIO.name;
                    cRow73.tag = pIO.tag;
                    cRow73.type = pIO.type;
                    cRow73.page = "";
                    cRow73.ytpye = "程序关闭脚本";
                    cRow73.text = "程序关闭脚本内容";
                }
                else
                {
                    CRow cRow76 = new()
                    {
                        name = pIO.name,
                        tag = pIO.tag,
                        type = pIO.type,
                        page = "",
                        ytpye = "程序关闭脚本",
                        text = "程序关闭脚本内容"
                    };
                    cRow73.LR.Add(cRow76);
                }
            }
            string[] cxdzqdioios = dhp.cxdzqdioios;
            foreach (string text45 in cxdzqdioios)
            {
                if (text45 != null && text45.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "绑定变量";
                        cRow73.text = text45;
                    }
                    else
                    {
                        CRow cRow77 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "绑定变量",
                            text = text45
                        };
                        cRow73.LR.Add(cRow77);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzqdiotjs;
            foreach (string text46 in cxdzqdioios)
            {
                if (text46 != null && text46.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "条件";
                        cRow73.text = text46;
                    }
                    else
                    {
                        CRow cRow78 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "条件",
                            text = text46
                        };
                        cRow73.LR.Add(cRow78);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzqdiozhis;
            foreach (string text47 in cxdzqdioios)
            {
                if (text47 != null && text47.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "赋值";
                        cRow73.text = text47;
                    }
                    else
                    {
                        CRow cRow79 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "赋值",
                            text = text47
                        };
                        cRow73.LR.Add(cRow79);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzqditemfftjs;
            foreach (string text48 in cxdzqdioios)
            {
                if (text48 != null && text48.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "条件";
                        cRow73.text = text48;
                    }
                    else
                    {
                        CRow cRow80 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "条件",
                            text = text48
                        };
                        cRow73.LR.Add(cRow80);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzqditemffbdss;
            foreach (string text49 in cxdzqdioios)
            {
                if (text49 != null && text49.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "方法";
                        cRow73.text = text49;
                    }
                    else
                    {
                        CRow cRow81 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "方法",
                            text = text49
                        };
                        cRow73.LR.Add(cRow81);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzqditemtjs;
            foreach (string text50 in cxdzqdioios)
            {
                if (text50 != null && text50.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "条件";
                        cRow73.text = text50;
                    }
                    else
                    {
                        CRow cRow82 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "条件",
                            text = text50
                        };
                        cRow73.LR.Add(cRow82);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzqditemzhis;
            foreach (string text51 in cxdzqdioios)
            {
                if (text51 != null && text51.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "赋值";
                        cRow73.text = text51;
                    }
                    else
                    {
                        CRow cRow83 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "赋值",
                            text = text51
                        };
                        cRow73.LR.Add(cRow83);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxioios;
            foreach (string text52 in cxdzqdioios)
            {
                if (text52 != null && text52.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "绑定变量";
                        cRow73.text = text52;
                    }
                    else
                    {
                        CRow cRow84 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "绑定变量",
                            text = text52
                        };
                        cRow73.LR.Add(cRow84);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxiotjs;
            foreach (string text53 in cxdzqdioios)
            {
                if (text53 != null && text53.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "条件";
                        cRow73.text = text53;
                    }
                    else
                    {
                        CRow cRow85 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "条件",
                            text = text53
                        };
                        cRow73.LR.Add(cRow85);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxiozhis;
            foreach (string text54 in cxdzqdioios)
            {
                if (text54 != null && text54.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "赋值";
                        cRow73.text = text54;
                    }
                    else
                    {
                        CRow cRow86 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "赋值",
                            text = text54
                        };
                        cRow73.LR.Add(cRow86);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxitemfftjs;
            foreach (string text55 in cxdzqdioios)
            {
                if (text55 != null && text55.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "条件";
                        cRow73.text = text55;
                    }
                    else
                    {
                        CRow cRow87 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "条件",
                            text = text55
                        };
                        cRow73.LR.Add(cRow87);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxitemffbdss;
            foreach (string text56 in cxdzqdioios)
            {
                if (text56 != null && text56.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "方法";
                        cRow73.text = text56;
                    }
                    else
                    {
                        CRow cRow88 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "方法",
                            text = text56
                        };
                        cRow73.LR.Add(cRow88);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxitemtjs;
            foreach (string text57 in cxdzqdioios)
            {
                if (text57 != null && text57.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "条件";
                        cRow73.text = text57;
                    }
                    else
                    {
                        CRow cRow89 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "条件",
                            text = text57
                        };
                        cRow73.LR.Add(cRow89);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzyxitemzhis;
            foreach (string text58 in cxdzqdioios)
            {
                if (text58 != null && text58.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "赋值";
                        cRow73.text = text58;
                    }
                    else
                    {
                        CRow cRow90 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "赋值",
                            text = text58
                        };
                        cRow73.LR.Add(cRow90);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbioios;
            foreach (string text59 in cxdzqdioios)
            {
                if (text59 != null && text59.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "绑定变量";
                        cRow73.text = text59;
                    }
                    else
                    {
                        CRow cRow91 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "绑定变量",
                            text = text59
                        };
                        cRow73.LR.Add(cRow91);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbiotjs;
            foreach (string text60 in cxdzqdioios)
            {
                if (text60 != null && text60.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "条件";
                        cRow73.text = text60;
                    }
                    else
                    {
                        CRow cRow92 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "条件",
                            text = text60
                        };
                        cRow73.LR.Add(cRow92);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbiozhis;
            foreach (string text61 in cxdzqdioios)
            {
                if (text61 != null && text61.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "赋值";
                        cRow73.text = text61;
                    }
                    else
                    {
                        CRow cRow93 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "赋值",
                            text = text61
                        };
                        cRow73.LR.Add(cRow93);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbitemfftjs;
            foreach (string text62 in cxdzqdioios)
            {
                if (text62 != null && text62.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "条件";
                        cRow73.text = text62;
                    }
                    else
                    {
                        CRow cRow94 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "条件",
                            text = text62
                        };
                        cRow73.LR.Add(cRow94);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbitemffbdss;
            foreach (string text63 in cxdzqdioios)
            {
                if (text63 != null && text63.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "方法";
                        cRow73.text = text63;
                    }
                    else
                    {
                        CRow cRow95 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "方法",
                            text = text63
                        };
                        cRow73.LR.Add(cRow95);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbitemtjs;
            foreach (string text64 in cxdzqdioios)
            {
                if (text64 != null && text64.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "条件";
                        cRow73.text = text64;
                    }
                    else
                    {
                        CRow cRow96 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "条件",
                            text = text64
                        };
                        cRow73.LR.Add(cRow96);
                    }
                }
            }
            cxdzqdioios = dhp.cxdzgbitemzhis;
            foreach (string text65 in cxdzqdioios)
            {
                if (text65 != null && text65.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = "";
                        cRow73.ytpye = "赋值";
                        cRow73.text = text65;
                    }
                    else
                    {
                        CRow cRow97 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = "",
                            ytpye = "赋值",
                            text = text65
                        };
                        cRow73.LR.Add(cRow97);
                    }
                }
            }
            foreach (DataFile df2 in dfs)
            {
                if (df2.pagedzqdLogic.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = df2.name;
                        cRow73.ytpye = "绑定变量";
                        cRow73.text = "页面启动脚本部分";
                    }
                    else
                    {
                        CRow cRow98 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = df2.name,
                            ytpye = "绑定变量",
                            text = "页面启动脚本部分"
                        };
                        cRow73.LR.Add(cRow98);
                    }
                }
                if (df2.pagedzyxLogic.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = df2.name;
                        cRow73.ytpye = "绑定变量";
                        cRow73.text = "页面启动脚本部分";
                    }
                    else
                    {
                        CRow cRow99 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = df2.name,
                            ytpye = "绑定变量",
                            text = "页面启动脚本部分"
                        };
                        cRow73.LR.Add(cRow99);
                    }
                }
                if (df2.pagedzgbLogic.Contains("[" + pIO.name + "]"))
                {
                    if (cRow73.name == "")
                    {
                        cRow73.name = pIO.name;
                        cRow73.tag = pIO.tag;
                        cRow73.type = pIO.type;
                        cRow73.page = df2.name;
                        cRow73.ytpye = "绑定变量";
                        cRow73.text = "页面启动脚本部分";
                    }
                    else
                    {
                        CRow cRow100 = new()
                        {
                            name = pIO.name,
                            tag = pIO.tag,
                            type = pIO.type,
                            page = df2.name,
                            ytpye = "绑定变量",
                            text = "页面启动脚本部分"
                        };
                        cRow73.LR.Add(cRow100);
                    }
                }
                cxdzqdioios = df2.pagedzqdioios;
                foreach (string text66 in cxdzqdioios)
                {
                    if (text66 != null && text66.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "绑定变量";
                            cRow73.text = text66;
                        }
                        else
                        {
                            CRow cRow101 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "绑定变量",
                                text = text66
                            };
                            cRow73.LR.Add(cRow101);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzqdiotjs;
                foreach (string text67 in cxdzqdioios)
                {
                    if (text67 != null && text67.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "条件";
                            cRow73.text = text67;
                        }
                        else
                        {
                            CRow cRow102 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "条件",
                                text = text67
                            };
                            cRow73.LR.Add(cRow102);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzqdiozhis;
                foreach (string text68 in cxdzqdioios)
                {
                    if (text68 != null && text68.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "赋值";
                            cRow73.text = text68;
                        }
                        else
                        {
                            CRow cRow103 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "赋值",
                                text = text68
                            };
                            cRow73.LR.Add(cRow103);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzqditemfftjs;
                foreach (string text69 in cxdzqdioios)
                {
                    if (text69 != null && text69.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "条件";
                            cRow73.text = text69;
                        }
                        else
                        {
                            CRow cRow104 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "条件",
                                text = text69
                            };
                            cRow73.LR.Add(cRow104);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzqditemffbdss;
                foreach (string text70 in cxdzqdioios)
                {
                    if (text70 != null && text70.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "方法";
                            cRow73.text = text70;
                        }
                        else
                        {
                            CRow cRow105 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "方法",
                                text = text70
                            };
                            cRow73.LR.Add(cRow105);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzqditemtjs;
                foreach (string text71 in cxdzqdioios)
                {
                    if (text71 != null && text71.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "条件";
                            cRow73.text = text71;
                        }
                        else
                        {
                            CRow cRow106 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "条件",
                                text = text71
                            };
                            cRow73.LR.Add(cRow106);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzqditemzhis;
                foreach (string text72 in cxdzqdioios)
                {
                    if (text72 != null && text72.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "赋值";
                            cRow73.text = text72;
                        }
                        else
                        {
                            CRow cRow107 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "赋值",
                                text = text72
                            };
                            cRow73.LR.Add(cRow107);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzyxioios;
                foreach (string text73 in cxdzqdioios)
                {
                    if (text73 != null && text73.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "绑定变量";
                            cRow73.text = text73;
                        }
                        else
                        {
                            CRow cRow108 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "绑定变量",
                                text = text73
                            };
                            cRow73.LR.Add(cRow108);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzyxiotjs;
                foreach (string text74 in cxdzqdioios)
                {
                    if (text74 != null && text74.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "条件";
                            cRow73.text = text74;
                        }
                        else
                        {
                            CRow cRow109 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "条件",
                                text = text74
                            };
                            cRow73.LR.Add(cRow109);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzyxiozhis;
                foreach (string text75 in cxdzqdioios)
                {
                    if (text75 != null && text75.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "赋值";
                            cRow73.text = text75;
                        }
                        else
                        {
                            CRow cRow110 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "赋值",
                                text = text75
                            };
                            cRow73.LR.Add(cRow110);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzyxitemfftjs;
                foreach (string text76 in cxdzqdioios)
                {
                    if (text76 != null && text76.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "条件";
                            cRow73.text = text76;
                        }
                        else
                        {
                            CRow cRow111 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "条件",
                                text = text76
                            };
                            cRow73.LR.Add(cRow111);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzyxitemffbdss;
                foreach (string text77 in cxdzqdioios)
                {
                    if (text77 != null && text77.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "方法";
                            cRow73.text = text77;
                        }
                        else
                        {
                            CRow cRow112 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "方法",
                                text = text77
                            };
                            cRow73.LR.Add(cRow112);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzyxitemtjs;
                foreach (string text78 in cxdzqdioios)
                {
                    if (text78 != null && text78.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "条件";
                            cRow73.text = text78;
                        }
                        else
                        {
                            CRow cRow113 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "条件",
                                text = text78
                            };
                            cRow73.LR.Add(cRow113);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzyxitemzhis;
                foreach (string text79 in cxdzqdioios)
                {
                    if (text79 != null && text79.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "赋值";
                            cRow73.text = text79;
                        }
                        else
                        {
                            CRow cRow114 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "赋值",
                                text = text79
                            };
                            cRow73.LR.Add(cRow114);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzgbioios;
                foreach (string text80 in cxdzqdioios)
                {
                    if (text80 != null && text80.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "绑定变量";
                            cRow73.text = text80;
                        }
                        else
                        {
                            CRow cRow115 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "绑定变量",
                                text = text80
                            };
                            cRow73.LR.Add(cRow115);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzgbiotjs;
                foreach (string text81 in cxdzqdioios)
                {
                    if (text81 != null && text81.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "条件";
                            cRow73.text = text81;
                        }
                        else
                        {
                            CRow cRow116 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "条件",
                                text = text81
                            };
                            cRow73.LR.Add(cRow116);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzgbiozhis;
                foreach (string text82 in cxdzqdioios)
                {
                    if (text82 != null && text82.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "赋值";
                            cRow73.text = text82;
                        }
                        else
                        {
                            CRow cRow117 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "赋值",
                                text = text82
                            };
                            cRow73.LR.Add(cRow117);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzgbitemfftjs;
                foreach (string text83 in cxdzqdioios)
                {
                    if (text83 != null && text83.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "条件";
                            cRow73.text = text83;
                        }
                        else
                        {
                            CRow cRow118 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "条件",
                                text = text83
                            };
                            cRow73.LR.Add(cRow118);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzgbitemffbdss;
                foreach (string text84 in cxdzqdioios)
                {
                    if (text84 != null && text84.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "方法";
                            cRow73.text = text84;
                        }
                        else
                        {
                            CRow cRow119 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "方法",
                                text = text84
                            };
                            cRow73.LR.Add(cRow119);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzgbitemtjs;
                foreach (string text85 in cxdzqdioios)
                {
                    if (text85 != null && text85.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "条件";
                            cRow73.text = text85;
                        }
                        else
                        {
                            CRow cRow120 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "条件",
                                text = text85
                            };
                            cRow73.LR.Add(cRow120);
                        }
                    }
                }
                cxdzqdioios = df2.pagedzgbitemzhis;
                foreach (string text86 in cxdzqdioios)
                {
                    if (text86 != null && text86.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.ytpye = "赋值";
                            cRow73.text = text86;
                        }
                        else
                        {
                            CRow cRow121 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                ytpye = "赋值",
                                text = text86
                            };
                            cRow73.LR.Add(cRow121);
                        }
                    }
                }
                foreach (CShape item2 in df2.ListAllShowCShape)
                {
                    if (item2.bxysbhbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.bxysbhbianliang;
                        }
                        else
                        {
                            CRow cRow122 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.bxysbhbianliang
                            };
                            cRow73.LR.Add(cRow122);
                        }
                    }
                    if (item2.czbfbbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.czbfbbianliang;
                        }
                        else
                        {
                            CRow cRow123 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.czbfbbianliang
                            };
                            cRow73.LR.Add(cRow123);
                        }
                    }
                    if (item2.cztzbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.cztzbianliang;
                        }
                        else
                        {
                            CRow cRow124 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.cztzbianliang
                            };
                            cRow73.LR.Add(cRow124);
                        }
                    }
                    if (item2.czydbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.czydbianliang;
                        }
                        else
                        {
                            CRow cRow125 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.czydbianliang
                            };
                            cRow73.LR.Add(cRow125);
                        }
                    }
                    if (item2.gdbhbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.gdbhbianliang;
                        }
                        else
                        {
                            CRow cRow126 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.gdbhbianliang
                            };
                            cRow73.LR.Add(cRow126);
                        }
                    }
                    if (item2.kdbhbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.kdbhbianliang;
                        }
                        else
                        {
                            CRow cRow127 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.kdbhbianliang
                            };
                            cRow73.LR.Add(cRow127);
                        }
                    }
                    if (item2.mbxzbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.mbxzbianliang;
                        }
                        else
                        {
                            CRow cRow128 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.mbxzbianliang
                            };
                            cRow73.LR.Add(cRow128);
                        }
                    }
                    if (item2.spbfbbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.spbfbbianliang;
                        }
                        else
                        {
                            CRow cRow129 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.spbfbbianliang
                            };
                            cRow73.LR.Add(cRow129);
                        }
                    }
                    if (item2.sptzbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.sptzbianliang;
                        }
                        else
                        {
                            CRow cRow130 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.sptzbianliang
                            };
                            cRow73.LR.Add(cRow130);
                        }
                    }
                    if (item2.spydbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.spydbianliang;
                        }
                        else
                        {
                            CRow cRow131 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.spydbianliang
                            };
                            cRow73.LR.Add(cRow131);
                        }
                    }
                    if (item2.tcs1ysbhbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.tcs1ysbhbianliang;
                        }
                        else
                        {
                            CRow cRow132 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.tcs1ysbhbianliang
                            };
                            cRow73.LR.Add(cRow132);
                        }
                    }
                    if (item2.tcs2ysbhbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.tcs2ysbhbianliang;
                        }
                        else
                        {
                            CRow cRow133 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.tcs2ysbhbianliang
                            };
                            cRow73.LR.Add(cRow133);
                        }
                    }
                    if (item2.txycbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.txycbianliang;
                        }
                        else
                        {
                            CRow cRow134 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.txycbianliang
                            };
                            cRow73.LR.Add(cRow134);
                        }
                    }
                    if (item2.zfcscbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.zfcscbianliang;
                        }
                        else
                        {
                            CRow cRow135 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.zfcscbianliang
                            };
                            cRow73.LR.Add(cRow135);
                        }
                    }
                    if (item2.aobianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.aobianliang;
                        }
                        else
                        {
                            CRow cRow136 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.aobianliang
                            };
                            cRow73.LR.Add(cRow136);
                        }
                    }
                    if (item2.dobianlaing.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.dobianlaing;
                        }
                        else
                        {
                            CRow cRow137 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.dobianlaing
                            };
                            cRow73.LR.Add(cRow137);
                        }
                    }
                    if (item2.zfcsrbianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.zfcsrbianliang;
                        }
                        else
                        {
                            CRow cRow138 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.zfcsrbianliang
                            };
                            cRow73.LR.Add(cRow138);
                        }
                    }
                    if (item2.aibianliang.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.aibianliang;
                        }
                        else
                        {
                            CRow cRow139 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.aibianliang
                            };
                            cRow73.LR.Add(cRow139);
                        }
                    }
                    if (item2.dibianlaing.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "变量绑定";
                            cRow73.text = item2.dibianlaing;
                        }
                        else
                        {
                            CRow cRow140 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "变量绑定",
                                text = item2.dibianlaing
                            };
                            cRow73.LR.Add(cRow140);
                        }
                    }
                    cxdzqdioios = item2.sbsjglbianliangs;
                    foreach (string text87 in cxdzqdioios)
                    {
                        if (text87 != null && text87.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "变量绑定";
                                cRow73.text = text87;
                            }
                            else
                            {
                                CRow cRow141 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "变量绑定",
                                    text = text87
                                };
                                cRow73.LR.Add(cRow141);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjlcioios;
                    foreach (string text88 in cxdzqdioios)
                    {
                        if (text88 != null && text88.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "绑定变量";
                                cRow73.text = text88;
                            }
                            else
                            {
                                CRow cRow142 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "绑定变量",
                                    text = text88
                                };
                                cRow73.LR.Add(cRow142);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjlciotjs;
                    foreach (string text89 in cxdzqdioios)
                    {
                        if (text89 != null && text89.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "条件";
                                cRow73.text = text89;
                            }
                            else
                            {
                                CRow cRow143 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "条件",
                                    text = text89
                                };
                                cRow73.LR.Add(cRow143);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjlciozhis;
                    foreach (string text90 in cxdzqdioios)
                    {
                        if (text90 != null && text90.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "赋值";
                                cRow73.text = text90;
                            }
                            else
                            {
                                CRow cRow144 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "赋值",
                                    text = text90
                                };
                                cRow73.LR.Add(cRow144);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjlcitemfftjs;
                    foreach (string text91 in cxdzqdioios)
                    {
                        if (text91 != null && text91.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "条件";
                                cRow73.text = text91;
                            }
                            else
                            {
                                CRow cRow145 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "条件",
                                    text = text91
                                };
                                cRow73.LR.Add(cRow145);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjlcitemffbdss;
                    foreach (string text92 in cxdzqdioios)
                    {
                        if (text92 != null && text92.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "方法";
                                cRow73.text = text92;
                            }
                            else
                            {
                                CRow cRow146 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "方法",
                                    text = text92
                                };
                                cRow73.LR.Add(cRow146);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjlcitemtjs;
                    foreach (string text93 in cxdzqdioios)
                    {
                        if (text93 != null && text93.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "条件";
                                cRow73.text = text93;
                            }
                            else
                            {
                                CRow cRow147 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "条件",
                                    text = text93
                                };
                                cRow73.LR.Add(cRow147);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjlcitemzhis;
                    foreach (string text94 in cxdzqdioios)
                    {
                        if (text94 != null && text94.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "赋值";
                                cRow73.text = text94;
                            }
                            else
                            {
                                CRow cRow148 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "赋值",
                                    text = text94
                                };
                                cRow73.LR.Add(cRow148);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrcioios;
                    foreach (string text95 in cxdzqdioios)
                    {
                        if (text95 != null && text95.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "绑定变量";
                                cRow73.text = text95;
                            }
                            else
                            {
                                CRow cRow149 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "绑定变量",
                                    text = text95
                                };
                                cRow73.LR.Add(cRow149);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrciotjs;
                    foreach (string text96 in cxdzqdioios)
                    {
                        if (text96 != null && text96.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "条件";
                                cRow73.text = text96;
                            }
                            else
                            {
                                CRow cRow150 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "条件",
                                    text = text96
                                };
                                cRow73.LR.Add(cRow150);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrciozhis;
                    foreach (string text97 in cxdzqdioios)
                    {
                        if (text97 != null && text97.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "赋值";
                                cRow73.text = text97;
                            }
                            else
                            {
                                CRow cRow151 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "赋值",
                                    text = text97
                                };
                                cRow73.LR.Add(cRow151);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrcitemfftjs;
                    foreach (string text98 in cxdzqdioios)
                    {
                        if (text98 != null && text98.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "条件";
                                cRow73.text = text98;
                            }
                            else
                            {
                                CRow cRow152 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "条件",
                                    text = text98
                                };
                                cRow73.LR.Add(cRow152);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrcitemffbdss;
                    foreach (string text99 in cxdzqdioios)
                    {
                        if (text99 != null && text99.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "方法";
                                cRow73.text = text99;
                            }
                            else
                            {
                                CRow cRow153 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "方法",
                                    text = text99
                                };
                                cRow73.LR.Add(cRow153);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrcitemtjs;
                    foreach (string text100 in cxdzqdioios)
                    {
                        if (text100 != null && text100.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "条件";
                                cRow73.text = text100;
                            }
                            else
                            {
                                CRow cRow154 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "条件",
                                    text = text100
                                };
                                cRow73.LR.Add(cRow154);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrcitemzhis;
                    foreach (string text101 in cxdzqdioios)
                    {
                        if (text101 != null && text101.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "赋值";
                                cRow73.text = text101;
                            }
                            else
                            {
                                CRow cRow155 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "赋值",
                                    text = text101
                                };
                                cRow73.LR.Add(cRow155);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjldcioios;
                    foreach (string text102 in cxdzqdioios)
                    {
                        if (text102 != null && text102.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "绑定变量";
                                cRow73.text = text102;
                            }
                            else
                            {
                                CRow cRow156 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "绑定变量",
                                    text = text102
                                };
                                cRow73.LR.Add(cRow156);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjldciotjs;
                    foreach (string text103 in cxdzqdioios)
                    {
                        if (text103 != null && text103.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "条件";
                                cRow73.text = text103;
                            }
                            else
                            {
                                CRow cRow157 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "条件",
                                    text = text103
                                };
                                cRow73.LR.Add(cRow157);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjldciozhis;
                    foreach (string text104 in cxdzqdioios)
                    {
                        if (text104 != null && text104.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "赋值";
                                cRow73.text = text104;
                            }
                            else
                            {
                                CRow cRow158 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "赋值",
                                    text = text104
                                };
                                cRow73.LR.Add(cRow158);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjldcitemfftjs;
                    foreach (string text105 in cxdzqdioios)
                    {
                        if (text105 != null && text105.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "条件";
                                cRow73.text = text105;
                            }
                            else
                            {
                                CRow cRow159 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "条件",
                                    text = text105
                                };
                                cRow73.LR.Add(cRow159);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjldcitemffbdss;
                    foreach (string text106 in cxdzqdioios)
                    {
                        if (text106 != null && text106.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "方法";
                                cRow73.text = text106;
                            }
                            else
                            {
                                CRow cRow160 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "方法",
                                    text = text106
                                };
                                cRow73.LR.Add(cRow160);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjldcitemtjs;
                    foreach (string text107 in cxdzqdioios)
                    {
                        if (text107 != null && text107.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "条件";
                                cRow73.text = text107;
                            }
                            else
                            {
                                CRow cRow161 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "条件",
                                    text = text107
                                };
                                cRow73.LR.Add(cRow161);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjldcitemzhis;
                    foreach (string text108 in cxdzqdioios)
                    {
                        if (text108 != null && text108.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "赋值";
                                cRow73.text = text108;
                            }
                            else
                            {
                                CRow cRow162 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "赋值",
                                    text = text108
                                };
                                cRow73.LR.Add(cRow162);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrdcioios;
                    foreach (string text109 in cxdzqdioios)
                    {
                        if (text109 != null && text109.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "绑定变量";
                                cRow73.text = text109;
                            }
                            else
                            {
                                CRow cRow163 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "绑定变量",
                                    text = text109
                                };
                                cRow73.LR.Add(cRow163);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrdciotjs;
                    foreach (string text110 in cxdzqdioios)
                    {
                        if (text110 != null && text110.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "条件";
                                cRow73.text = text110;
                            }
                            else
                            {
                                CRow cRow164 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "条件",
                                    text = text110
                                };
                                cRow73.LR.Add(cRow164);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrdciozhis;
                    foreach (string text111 in cxdzqdioios)
                    {
                        if (text111 != null && text111.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "赋值";
                                cRow73.text = text111;
                            }
                            else
                            {
                                CRow cRow165 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "赋值",
                                    text = text111
                                };
                                cRow73.LR.Add(cRow165);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrdcitemfftjs;
                    foreach (string text112 in cxdzqdioios)
                    {
                        if (text112 != null && text112.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "条件";
                                cRow73.text = text112;
                            }
                            else
                            {
                                CRow cRow166 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "条件",
                                    text = text112
                                };
                                cRow73.LR.Add(cRow166);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrdcitemffbdss;
                    foreach (string text113 in cxdzqdioios)
                    {
                        if (text113 != null && text113.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "方法";
                                cRow73.text = text113;
                            }
                            else
                            {
                                CRow cRow167 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "方法",
                                    text = text113
                                };
                                cRow73.LR.Add(cRow167);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrdcitemtjs;
                    foreach (string text114 in cxdzqdioios)
                    {
                        if (text114 != null && text114.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "条件";
                                cRow73.text = text114;
                            }
                            else
                            {
                                CRow cRow168 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "条件",
                                    text = text114
                                };
                                cRow73.LR.Add(cRow168);
                            }
                        }
                    }
                    cxdzqdioios = item2.sbsjrdcitemzhis;
                    foreach (string text115 in cxdzqdioios)
                    {
                        if (text115 != null && text115.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "赋值";
                                cRow73.text = text115;
                            }
                            else
                            {
                                CRow cRow169 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "赋值",
                                    text = text115
                                };
                                cRow73.LR.Add(cRow169);
                            }
                        }
                    }
                    if (item2.DBOKScriptUser != null && item2.DBOKScriptUser.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "赋值";
                            cRow73.text = item2.DBOKScriptUser;
                        }
                        else
                        {
                            CRow cRow170 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "赋值",
                                text = item2.DBOKScriptUser
                            };
                            cRow73.LR.Add(cRow170);
                        }
                    }
                    if (item2.DBErrScriptUser != null && item2.DBOKScriptUser.Contains("[" + pIO.name + "]"))
                    {
                        if (cRow73.name == "")
                        {
                            cRow73.name = pIO.name;
                            cRow73.tag = pIO.tag;
                            cRow73.type = pIO.type;
                            cRow73.page = df2.name;
                            cRow73.shapename = item2.ShapeName;
                            cRow73.ytpye = "赋值";
                            cRow73.text = item2.DBErrScriptUser;
                        }
                        else
                        {
                            CRow cRow171 = new()
                            {
                                name = pIO.name,
                                tag = pIO.tag,
                                type = pIO.type,
                                page = df2.name,
                                shapename = item2.ShapeName,
                                ytpye = "赋值",
                                text = item2.DBErrScriptUser
                            };
                            cRow73.LR.Add(cRow171);
                        }
                    }
                    cxdzqdioios = item2.UserLogic;
                    foreach (string text116 in cxdzqdioios)
                    {
                        if (text116 != null && text116.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "脚本";
                                cRow73.text = "";
                            }
                            else
                            {
                                CRow cRow172 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "脚本",
                                    text = ""
                                };
                                cRow73.LR.Add(cRow172);
                            }
                        }
                    }
                    cxdzqdioios = item2.ysdzshijianbiaodashi;
                    foreach (string text117 in cxdzqdioios)
                    {
                        if (text117 != null && text117.Contains("[" + pIO.name + "]"))
                        {
                            if (cRow73.name == "")
                            {
                                cRow73.name = pIO.name;
                                cRow73.tag = pIO.tag;
                                cRow73.type = pIO.type;
                                cRow73.page = df2.name;
                                cRow73.shapename = item2.ShapeName;
                                cRow73.ytpye = "元素动作脚本";
                                cRow73.text = "";
                            }
                            else
                            {
                                CRow cRow173 = new()
                                {
                                    name = pIO.name,
                                    tag = pIO.tag,
                                    type = pIO.type,
                                    page = df2.name,
                                    shapename = item2.ShapeName,
                                    ytpye = "元素动作脚本",
                                    text = ""
                                };
                                cRow73.LR.Add(cRow173);
                            }
                        }
                    }
                }
            }
            if (cRow73.name != "")
            {
                CRS.Add(cRow73);
            }
        }
        foreach (CRow cR in CRS)
        {
            DataRow dataRow = dt.NewRow();
            if (cR.LR.Count != 0)
            {
                dataRow[0] = Resources.unpuker;
            }
            else
            {
                dataRow[0] = new Bitmap(23, 23);
            }
            dataRow[1] = cR.name;
            dataRow[2] = cR.tag;
            dataRow[3] = cR.type;
            dataRow[4] = cR.page;
            dataRow[5] = cR.shapename;
            dataRow[6] = cR.ytpye;
            dataRow[7] = cR.text;
            dt.Rows.Add(dataRow);
        }
        dataGridView1.DataSource = dt;
        dataGridView1.Columns[0].Width = 23;
    }

    private void dataGridView1_DoubleClick(object sender, EventArgs e)
    {
    }

    private bool CheckEquals(Bitmap b1, Bitmap b2)
    {
        if (b1.Size != b2.Size)
        {
            return false;
        }
        for (int i = 0; i < b1.Size.Width; i++)
        {
            for (int j = 0; j < b1.Size.Height; j++)
            {
                if (b1.GetPixel(i, j) != b2.GetPixel(i, j))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1 || e.ColumnIndex != 0 || dataGridView1.SelectedCells.Count == 0)
        {
            return;
        }
        string text = (string)dataGridView1.Rows[e.RowIndex].Cells[1].Value;
        int firstDisplayedScrollingRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
        if (CheckEquals((Bitmap)dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value, Resources.unpuker))
        {
            dt.Rows.Clear();
            foreach (CRow cR in CRS)
            {
                bool flag = false;
                if (cR.name == text)
                {
                    flag = true;
                }
                DataRow dataRow = dt.NewRow();
                if (cR.LR.Count != 0)
                {
                    if (flag)
                    {
                        dataRow[0] = Resources.puker;
                    }
                    else
                    {
                        dataRow[0] = Resources.unpuker;
                    }
                }
                else
                {
                    dataRow[0] = new Bitmap(23, 23);
                }
                dataRow[1] = cR.name;
                dataRow[2] = cR.tag;
                dataRow[3] = cR.type;
                dataRow[4] = cR.page;
                dataRow[5] = cR.shapename;
                dataRow[6] = cR.ytpye;
                dataRow[7] = cR.text;
                dt.Rows.Add(dataRow);
                if (!flag || cR.LR.Count == 0)
                {
                    continue;
                }
                for (int i = 0; i < cR.LR.Count; i++)
                {
                    CRow cRow = cR.LR[i];
                    DataRow dataRow2 = dt.NewRow();
                    if (i == cR.LR.Count - 1)
                    {
                        dataRow2[0] = Resources.tr;
                    }
                    else
                    {
                        dataRow2[0] = Resources.r;
                    }
                    dataRow2[1] = cR.name;
                    dataRow2[2] = cR.tag;
                    dataRow2[3] = cR.type;
                    dataRow2[4] = cRow.page;
                    dataRow2[5] = cRow.shapename;
                    dataRow2[6] = cRow.ytpye;
                    dataRow2[7] = cRow.text;
                    dt.Rows.Add(dataRow2);
                }
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 23;
        }
        else if (CheckEquals((Bitmap)dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value, Resources.puker))
        {
            dt.Clear();
            foreach (CRow cR2 in CRS)
            {
                DataRow dataRow3 = dt.NewRow();
                if (cR2.LR.Count != 0)
                {
                    dataRow3[0] = Resources.unpuker;
                }
                else
                {
                    dataRow3[0] = new Bitmap(23, 23);
                }
                dataRow3[1] = cR2.name;
                dataRow3[2] = cR2.tag;
                dataRow3[3] = cR2.type;
                dataRow3[4] = cR2.page;
                dataRow3[5] = cR2.shapename;
                dataRow3[6] = cR2.ytpye;
                dataRow3[7] = cR2.text;
                dt.Rows.Add(dataRow3);
            }
            dataGridView1.DataSource = dt;
        }
        try
        {
            dataGridView1.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
        }
        catch
        {
        }
    }

    private void InitializeComponent()
    {
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        ((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
        base.SuspendLayout();
        this.dataGridView1.AllowUserToAddRows = false;
        this.dataGridView1.AllowUserToDeleteRows = false;
        this.dataGridView1.AllowUserToResizeRows = false;
        this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dataGridView1.Location = new System.Drawing.Point(0, 0);
        this.dataGridView1.MultiSelect = false;
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.ReadOnly = true;
        this.dataGridView1.RowHeadersVisible = false;
        this.dataGridView1.RowTemplate.Height = 23;
        this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dataGridView1.ShowCellErrors = false;
        this.dataGridView1.ShowCellToolTips = false;
        this.dataGridView1.ShowEditingIcon = false;
        this.dataGridView1.ShowRowErrors = false;
        this.dataGridView1.Size = new System.Drawing.Size(729, 537);
        this.dataGridView1.TabIndex = 0;
        this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick);
        this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
        this.dataGridView1.DoubleClick += new System.EventHandler(dataGridView1_DoubleClick);
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(729, 537);
        base.Controls.Add(this.dataGridView1);
        base.Name = "yytjForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "引用统计";
        base.Load += new System.EventHandler(yytjForm_Load);
        ((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
        base.ResumeLayout(false);
    }
}
