using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using CommonSnappableTypes;

namespace ShapeRuntime;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("0FBAA0D6-F428-4dbe-A41C-2D28097C4366")]
[ComVisible(true)]
public class CGroupBox : GroupBox, IDCCEControl, IControlShape
{
    private bool m_bIsRunning;

    private string id = "";

    private string _text = "";

    [Browsable(false)]
    public bool isRuning
    {
        get
        {
            return m_bIsRunning;
        }
        set
        {
            m_bIsRunning = value;
        }
    }

    [DisplayName("名称")]
    [Description("设定控件名称。")]
    [ReadOnly(false)]
    [Category("设计")]
    public string ID
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
            if (this.IDChanged != null)
            {
                this.IDChanged(this, null);
            }
        }
    }

    [Description("与控件关联的文本")]
    [Category("外观")]
    [DisplayName("文本")]
    [ReadOnly(false)]
    public override string Text
    {
        get
        {
            if (_text != null)
            {
                return _text;
            }
            return base.Text;
        }
        set
        {
            _text = value;
            if (base.Text != value)
            {
                base.Text = value;
                Invalidate();
            }
        }
    }

    [ReadOnly(false)]
    [Category("外观")]
    [DisplayName("背景图片")]
    [Description("用于该控件的背景图像")]
    public new Image BackgroundImage
    {
        get
        {
            return base.BackgroundImage;
        }
        set
        {
            base.BackgroundImage = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("背景图片布局")]
    [Category("外观")]
    [Description("用于组件的背景图像布局")]
    public new ImageLayout BackgroundImageLayout
    {
        get
        {
            return base.BackgroundImageLayout;
        }
        set
        {
            base.BackgroundImageLayout = value;
        }
    }

    [Category("外观")]
    [DisplayName("外观类型")]
    [ReadOnly(false)]
    [Description("确定当用户将鼠标移动到控件上并单击时该控件的外观")]
    public new FlatStyle FlatStyle
    {
        get
        {
            return base.FlatStyle;
        }
        set
        {
            base.FlatStyle = value;
        }
    }

    [DisplayName("背景色")]
    [ReadOnly(false)]
    [Category("外观")]
    [Description("设置或获取控件的背景色")]
    public new Color BackColor
    {
        get
        {
            return base.BackColor;
        }
        set
        {
            base.BackColor = value;
        }
    }

    [DisplayName("光标")]
    [ReadOnly(false)]
    [Category("外观")]
    [Description("指针移动过该控件时显示的光标")]
    public new Cursor Cursor
    {
        get
        {
            return base.Cursor;
        }
        set
        {
            base.Cursor = value;
        }
    }

    [Category("外观")]
    [DisplayName("字体设置")]
    [ReadOnly(false)]
    [Description("设置控件中字体的样式")]
    public new Font Font
    {
        get
        {
            return base.Font;
        }
        set
        {
            base.Font = value;
        }
    }

    [DisplayName("文本色")]
    [ReadOnly(false)]
    [Category("外观")]
    [Description("设置或获取控件的文本色")]
    public new Color ForeColor
    {
        get
        {
            return base.ForeColor;
        }
        set
        {
            base.ForeColor = value;
        }
    }

    [DisplayName("显示方式")]
    [Category("外观")]
    [Description("组件是否应该从右向左进行绘制")]
    [ReadOnly(false)]
    public new RightToLeft RightToLeft
    {
        get
        {
            return base.RightToLeft;
        }
        set
        {
            base.RightToLeft = value;
        }
    }

    [Browsable(false)]
    public new bool UseWaitCursor
    {
        get
        {
            return base.UseWaitCursor;
        }
        set
        {
            base.UseWaitCursor = value;
        }
    }

    [Category("布局")]
    [ReadOnly(false)]
    [Description("控件左上角相对于其容器左上角的坐标")]
    [DisplayName("位置")]
    public new Point Location
    {
        get
        {
            return base.Location;
        }
        set
        {
            base.Location = value;
        }
    }

    [DisplayName("大小")]
    [Category("布局")]
    [Description("控件的大小（以像素为单位）")]
    [ReadOnly(false)]
    public new Size Size
    {
        get
        {
            return base.Size;
        }
        set
        {
            base.Size = value;
        }
    }

    [Browsable(false)]
    public new AnchorStyles Anchor
    {
        get
        {
            return base.Anchor;
        }
        set
        {
            base.Anchor = value;
        }
    }

    [Browsable(false)]
    public new DockStyle Dock
    {
        get
        {
            return base.Dock;
        }
        set
        {
            base.Dock = value;
        }
    }

    [Browsable(false)]
    public new Padding Margin
    {
        get
        {
            return base.Margin;
        }
        set
        {
            base.Margin = value;
        }
    }

    [Browsable(false)]
    public new Size MaximumSize
    {
        get
        {
            return base.MaximumSize;
        }
        set
        {
            base.MaximumSize = value;
        }
    }

    [Browsable(false)]
    public new Size MinimumSize
    {
        get
        {
            return base.MinimumSize;
        }
        set
        {
            base.MinimumSize = value;
        }
    }

    [Browsable(false)]
    public new bool AutoSize
    {
        get
        {
            return base.AutoSize;
        }
        set
        {
            base.AutoSize = value;
        }
    }

    [Browsable(false)]
    public new AutoSizeMode AutoSizeMode
    {
        get
        {
            return base.AutoSizeMode;
        }
        set
        {
            base.AutoSizeMode = value;
        }
    }

    [Browsable(false)]
    public new Padding Padding
    {
        get
        {
            return base.Padding;
        }
        set
        {
            base.Padding = value;
        }
    }

    [Browsable(false)]
    public new bool CausesValidation
    {
        get
        {
            return base.CausesValidation;
        }
        set
        {
            base.CausesValidation = value;
        }
    }

    [Browsable(false)]
    public new string AccessibleName
    {
        get
        {
            return base.AccessibleName;
        }
        set
        {
            base.AccessibleName = value;
        }
    }

    [Browsable(false)]
    public new AccessibleRole AccessibleRole
    {
        get
        {
            return base.AccessibleRole;
        }
        set
        {
            base.AccessibleRole = value;
        }
    }

    [Browsable(false)]
    public new string AccessibleDescription
    {
        get
        {
            return base.AccessibleDescription;
        }
        set
        {
            base.AccessibleDescription = value;
        }
    }

    [Browsable(false)]
    public new object Tag
    {
        get
        {
            return base.Tag;
        }
        set
        {
            base.Tag = value;
        }
    }

    [Browsable(false)]
    public new ControlBindingsCollection DataBindings => base.DataBindings;

    [ReadOnly(false)]
    [Description("确定该控件是可见还是隐藏")]
    [DisplayName("可见性")]
    [Category("行为")]
    public new bool Visible
    {
        get
        {
            return base.Visible;
        }
        set
        {
            base.Visible = value;
        }
    }

    [DisplayName("启用控件")]
    [ReadOnly(false)]
    [Category("行为")]
    [Description("指示是否已启用该控件")]
    public new bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
        }
    }

    [Browsable(false)]
    public new bool AllowDrop
    {
        get
        {
            return base.AllowDrop;
        }
        set
        {
            base.AllowDrop = value;
        }
    }

    [Browsable(false)]
    public new ContextMenuStrip ContextMenuStrip
    {
        get
        {
            return base.ContextMenuStrip;
        }
        set
        {
            base.ContextMenuStrip = value;
        }
    }

    [Browsable(false)]
    public new int TabIndex
    {
        get
        {
            return base.TabIndex;
        }
        set
        {
            base.TabIndex = value;
        }
    }

    [Browsable(false)]
    public new bool TabStop
    {
        get
        {
            return base.TabStop;
        }
        set
        {
            base.TabStop = value;
        }
    }

    [Browsable(false)]
    public new bool UseCompatibleTextRendering
    {
        get
        {
            return base.UseCompatibleTextRendering;
        }
        set
        {
            base.UseCompatibleTextRendering = value;
        }
    }

    [Browsable(false)]
    public new ImeMode ImeMode
    {
        get
        {
            return base.ImeMode;
        }
        set
        {
            base.ImeMode = value;
        }
    }

    public event GetValue GetValueEvent;

    public event SetValue SetValueEvent;

    public event GetDataBase GetDataBaseEvent;

    public event GetVarTable GetVarTableEvent;

    public event GetValue GetSystemItemEvent;

    public event EventHandler IDChanged;

    public Bitmap GetLogo()
    {
        return null;
    }

    private void t_Tick(object sender, EventArgs e)
    {
        if (Enabled)
        {
            Enabled = false;
        }
    }

    public CGroupBox()
    {
        Text = "分组框";
        ForeColor = Color.Black;
    }

    public byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        CGroupBoxSaveItems cGroupBoxSaveItems = new()
        {
            Text = Text
        };
        formatter.Serialize(memoryStream, cGroupBoxSaveItems);
        byte[] result = memoryStream.ToArray();
        memoryStream.Close();
        return result;
    }

    public void DeSerialize(byte[] bytes)
    {
        try
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream(bytes);
            CGroupBoxSaveItems cGroupBoxSaveItems = (CGroupBoxSaveItems)formatter.Deserialize(stream);
            stream.Close();
            Text = cGroupBoxSaveItems.Text;
        }
        catch (Exception)
        {
        }
    }

    public void Stop()
    {
    }
}
