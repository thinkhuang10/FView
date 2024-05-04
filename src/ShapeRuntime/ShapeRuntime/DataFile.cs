using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace ShapeRuntime;

[Serializable]
public class DataFile : IEventBind
{
    public string PageAuthority;

    public bool enableEventBind = true;

    public Dictionary<string, List<EventSetItem>> eventBindDict = new();

    [OptionalField]
    public bool IsWindow;

    [OptionalField]
    public bool IsCenter;

    [OptionalField]
    public bool CloseOnBestrow;

    [OptionalField]
    public bool CloseOnPart;

    [OptionalField]
    public string AlignType;

    [OptionalField]
    public ImageLayout pageImageLayout;

    [OptionalField]
    public Image pageimage;

    [OptionalField]
    public string pageImageNamef;

    [NonSerialized]
    public SizeF sizef;

    [NonSerialized]
    public PointF locationf;

    [NonSerialized]
    public List<CShape> ListAllShowCShape;

    public CShape[] tls;

    [OptionalField]
    public string pageName;

    public string name;

    public Color color;

    public Point location;

    public bool IsAutoScroll;

    public Size size;

    public Size PageOldSize;

    public bool visable;

    [OptionalField]
    public ProjectIO[] PIO;

    public string _pagedzgbLogic = "";

    public string _pagedzyxLogic = "";

    public string _pagedzqdLogic = "";

    public string[] pagedzshijiaoben = new string[3] { "", "", "" };

    public string[] cxdzLogic = new string[3] { "", "", "" };

    public int LogicTime = 1000;

    public string[] pagedzqdioios = new string[0];

    public string[] pagedzqdiotjs = new string[0];

    public string[] pagedzqdiozhis = new string[0];

    public string[] pagedzqditemprogs = new string[0];

    public string[] pagedzqditemtjs = new string[0];

    public string[] pagedzqditemzhis = new string[0];

    public string[] pagedzqditemfftjs = new string[0];

    public string[] pagedzqditemffbdss = new string[0];

    public string[] pagedzyxioios = new string[0];

    public string[] pagedzyxiotjs = new string[0];

    public string[] pagedzyxiozhis = new string[0];

    public string[] pagedzyxitemprogs = new string[0];

    public string[] pagedzyxitemtjs = new string[0];

    public string[] pagedzyxitemzhis = new string[0];

    public string[] pagedzyxitemfftjs = new string[0];

    public string[] pagedzyxitemffbdss = new string[0];

    public string[] pagedzgbioios = new string[0];

    public string[] pagedzgbiotjs = new string[0];

    public string[] pagedzgbiozhis = new string[0];

    public string[] pagedzgbitemprogs = new string[0];

    public string[] pagedzgbitemtjs = new string[0];

    public string[] pagedzgbitemzhis = new string[0];

    public string[] pagedzgbitemfftjs = new string[0];

    public string[] pagedzgbitemffbdss = new string[0];

    public bool EnableEventBind
    {
        get
        {
            return enableEventBind;
        }
        set
        {
            enableEventBind = value;
        }
    }

    public Dictionary<string, List<EventSetItem>> EventBindDict
    {
        get
        {
            return eventBindDict;
        }
        set
        {
            eventBindDict = value;
        }
    }

    public string pagedzgbLogic
    {
        get
        {
            StringBuilder stringBuilder = new();
            if (pagedzgbioios.Length != 0)
            {
                for (int i = 0; i < pagedzgbioios.Length; i++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(pagedzgbiotjs[i]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(pagedzgbioios[i]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(pagedzgbiozhis[i]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
                for (int j = 0; j < pagedzgbitemprogs.Length; j++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(pagedzgbitemtjs[j]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(pagedzgbitemprogs[j]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(pagedzgbitemzhis[j]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
                for (int k = 0; k < pagedzgbitemffbdss.Length; k++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(pagedzgbitemfftjs[k]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(pagedzgbitemffbdss[k]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (stringBuilder.ToString() + "\n" + pagedzshijiaoben[2] != null)
            {
                return pagedzshijiaoben[2];
            }
            return "";
        }
    }

    public string pagedzyxLogic
    {
        get
        {
            StringBuilder stringBuilder = new();
            if (pagedzyxioios.Length != 0)
            {
                for (int i = 0; i < pagedzyxioios.Length; i++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(pagedzyxiotjs[i]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(pagedzyxioios[i]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(pagedzyxiozhis[i]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
                for (int j = 0; j < pagedzyxitemprogs.Length; j++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(pagedzyxitemtjs[j]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(pagedzyxitemprogs[j]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(pagedzyxitemzhis[j]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
                for (int k = 0; k < pagedzyxitemffbdss.Length; k++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(pagedzyxitemfftjs[k]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(pagedzyxitemffbdss[k]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (stringBuilder.ToString() + "\n" + pagedzshijiaoben[1] != null)
            {
                return pagedzshijiaoben[1];
            }
            return "";
        }
    }

    public string pagedzqdLogic
    {
        get
        {
            StringBuilder stringBuilder = new();
            if (pagedzqdioios.Length != 0)
            {
                for (int i = 0; i < pagedzqdioios.Length; i++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(pagedzqdiotjs[i]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(pagedzqdioios[i]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(pagedzqdiozhis[i]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
                for (int j = 0; j < pagedzqditemprogs.Length; j++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(pagedzqditemtjs[j]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(pagedzqditemprogs[j]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(pagedzqditemzhis[j]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
                for (int k = 0; k < pagedzqditemffbdss.Length; k++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(pagedzqditemfftjs[k]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(pagedzqditemffbdss[k]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (stringBuilder.ToString() + "\n" + pagedzshijiaoben[0] != null)
            {
                return pagedzshijiaoben[0];
            }
            return "";
        }
    }

    public event EventHandler OnPageShow;

    public event EventHandler OnPageHide;

    public event EventHandler OnPageRun;

    public override string ToString()
    {
        return "DataFile: { Name=" + name + ", PageName=" + pageName + " }";
    }
}
