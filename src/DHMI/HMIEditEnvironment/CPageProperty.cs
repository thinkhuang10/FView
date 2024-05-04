using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using CommonSnappableTypes;

namespace HMIEditEnvironment;

[Serializable]
public class CPageProperty
{
    [NonSerialized]
    public CGlobal theglobal;

    private readonly string pageAuthority = "";

    [Description("设置脚本中所使用的名称(目前只支持英文下划线数字且数字不可开头)。")]
    [DisplayName("脚本名称")]
    [Category("设计")]
    [ReadOnly(false)]
    public string PageName
    {
        get
        {
            return theglobal.df.name;
        }
        set
        {
            theglobal.theform.Name = value;
            theglobal.df.name = value;
        }
    }

    [DisplayName("页面权限")]
    [Category("权限设置")]
    [Description("设置页面曲线，范围A~J。")]
    [ReadOnly(false)]
    public string PageAuthority
    {
        get
        {
            return theglobal.df.PageAuthority;
        }
        set
        {
            theglobal.df.PageAuthority = value;
        }
    }

    [Category("设计")]
    [DisplayName("页面名称")]
    [Description("设置页面名称。")]
    [ReadOnly(false)]
    public string ShowName
    {
        get
        {
            return theglobal.df.pageName;
        }
        set
        {
            theglobal.df.pageName = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("页面尺寸")]
    [Description("设置页面尺寸。最大尺寸支持7000*7000")]
    [Category("设计")]
    public Size PageSize
    {
        get
        {
            return theglobal.df.size;
        }
        set
        {
            if (value.Height <= 7000 && value.Width <= 7000)
            {
                theglobal.uc2.backgroundReDraw = true;
                theglobal.uc2.Size = value;
                theglobal.theform.Size = new Size(Convert.ToInt32(value.Width), Convert.ToInt32(value.Height));
                theglobal.df.size = value;
                theglobal.df.PageOldSize = value;
            }
        }
    }

    [Description("设置页面位置。")]
    [ReadOnly(false)]
    [Category("设计")]
    [DisplayName("页面位置")]
    public Point PageLocation
    {
        get
        {
            return theglobal.df.location;
        }
        set
        {
            theglobal.theform.Location = new Point(0, 0);
            theglobal.df.location = value;
        }
    }

    [DisplayName("页面颜色")]
    [Description("设置页面背景颜色。")]
    [ReadOnly(false)]
    [Category("设计")]
    public Color PageColor
    {
        get
        {
            return theglobal.uc2.BackColor;
        }
        set
        {
            theglobal.uc2.backgroundReDraw = true;
            theglobal.uc2.BackColor = value;
            theglobal.df.color = value;
        }
    }

    [Category("设计")]
    [ReadOnly(false)]
    [DisplayName("背景图片")]
    [Description("设置页面背景图片。")]
    [Editor(typeof(VarTableImageManage), typeof(UITypeEditor))]
    public BitMapForIM PageImage
    {
        get
        {
            BitMapForIM bitMapForIM = new()
            {
                img = theglobal.uc2.BackgroundImage,
                ImgGUID = theglobal.df.pageImageNamef
            };
            return bitMapForIM;
        }
        set
        {
            theglobal.uc2.backgroundReDraw = true;
            theglobal.uc2.BackgroundImage = value.img;
            theglobal.df.pageimage = value.img;
            theglobal.df.pageImageNamef = value.ImgGUID;
        }
    }

    [Browsable(false)]
    public ImageLayout PageImageLayout
    {
        get
        {
            return theglobal.uc2.BackgroundImageLayout;
        }
        set
        {
            theglobal.uc2.backgroundReDraw = true;
            theglobal.uc2.BackgroundImageLayout = value;
            theglobal.df.pageImageLayout = value;
        }
    }

    [ReadOnly(false)]
    [Category("设计")]
    [DisplayName("页面可见性")]
    [Description("设置页面是否可见。")]
    public bool PageVisible
    {
        get
        {
            return theglobal.df.visable;
        }
        set
        {
            theglobal.df.visable = value;
        }
    }

    [Browsable(false)]
    public bool CloseOnBestrow
    {
        get
        {
            return theglobal.df.CloseOnBestrow;
        }
        set
        {
            theglobal.df.CloseOnBestrow = value;
        }
    }

    [Browsable(false)]
    public bool CloseOnPart
    {
        get
        {
            return theglobal.df.CloseOnPart;
        }
        set
        {
            theglobal.df.CloseOnPart = value;
        }
    }

    [DisplayName("窗体模式")]
    [Category("设计")]
    [Description("设置是否开启窗体模式。")]
    [ReadOnly(false)]
    public bool IsWindow
    {
        get
        {
            return theglobal.df.IsWindow;
        }
        set
        {
            theglobal.df.IsWindow = value;
        }
    }

    public CPageProperty()
    {
    }

    public CPageProperty(CGlobal _theglobal)
    {
        theglobal = _theglobal;
    }
}
