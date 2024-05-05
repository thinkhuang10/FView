using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using CommonSnappableTypes;

namespace ShapeRuntime;

public class CTimer : UserControl, IControlShape, IDCCEControl
{
    private bool _isRuning;

    private string id = "";

    private IContainer components;

    private Timer timer1;

    private Label label1;

    [ReadOnly(false)]
    [Browsable(false)]
    [DisplayName("运行环境")]
    [Category("设计")]
    [Description("DHMI保留属性，请勿修改！。")]
    public bool isRuning
    {
        get
        {
            return _isRuning;
        }
        set
        {
            _isRuning = value;
            label1.Visible = !_isRuning;
            if (_isRuning)
            {
                if (AutoStart)
                {
                    timer1.Start();
                }
            }
            else
            {
                timer1.Stop();
            }
        }
    }

    [Category("设计")]
    [ReadOnly(false)]
    [DisplayName("名称")]
    [Description("设定控件名称。")]
    public string ID
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
            IDChanged?.Invoke(this, null);
        }
    }

    [Category("布局")]
    [ReadOnly(false)]
    [Description("设定控件的位置(控件在运行状态下将自动隐藏)。")]
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

    [Browsable(false)]
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

    [ReadOnly(true)]
    [Browsable(false)]
    [DisplayName("定时器状态")]
    [Category("数据")]
    [Description("获得当前定时器运行状态，可通过Start或者Stop方法来进行控制。")]
    public bool TimerState
    {
        get
        {
            if (timer1 != null)
            {
                return timer1.Enabled;
            }
            return false;
        }
    }

    [ReadOnly(false)]
    [Category("数据")]
    [DisplayName("定时时间")]
    [Description("设定定时时间。")]
    public int Interval
    {
        get
        {
            return timer1.Interval;
        }
        set
        {
            if (value > 0)
            {
                timer1.Interval = value;
            }
            else
            {
                timer1.Interval = 1;
            }
        }
    }

    [Description("设定是否在页面加载后自动启动定时器。")]
    [ReadOnly(false)]
    [Category("数据")]
    [DisplayName("自动启动")]
    public bool AutoStart { get; set; }

    [Category("数据")]
    [DisplayName("自动重置")]
    [Description("设定是否在定时器触发后自动停止。")]
    [ReadOnly(false)]
    public bool AutoReset { get; set; }

    [Browsable(false)]
    public new bool AutoScroll
    {
        get
        {
            return base.AutoScroll;
        }
        set
        {
            base.AutoScroll = value;
        }
    }

    [Browsable(false)]
    public new Size AutoScrollMargin
    {
        get
        {
            return base.AutoScrollMargin;
        }
        set
        {
            base.AutoScrollMargin = value;
        }
    }

    [Browsable(false)]
    public new Size AutoScrollMinSize
    {
        get
        {
            return base.AutoScrollMinSize;
        }
        set
        {
            base.AutoScrollMinSize = value;
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

    [Browsable(false)]
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

    [Browsable(false)]
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

    [Browsable(false)]
    public new AutoValidate AutoValidate
    {
        get
        {
            return base.AutoValidate;
        }
        set
        {
            base.AutoValidate = value;
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

    [Browsable(false)]
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

    [Browsable(false)]
    public new BorderStyle BorderStyle
    {
        get
        {
            return base.BorderStyle;
        }
        set
        {
            base.BorderStyle = value;
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
    public new string AccessibleDefaultActionDescription
    {
        get
        {
            return base.AccessibleDefaultActionDescription;
        }
        set
        {
            base.AccessibleDefaultActionDescription = value;
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
    public new Point AutoScrollOffset
    {
        get
        {
            return base.AutoScrollOffset;
        }
        set
        {
            base.AutoScrollOffset = value;
        }
    }

    [Browsable(false)]
    public new LayoutEngine LayoutEngine => base.LayoutEngine;

    [Browsable(false)]
    public new BindingContext BindingContext
    {
        get
        {
            return base.BindingContext;
        }
        set
        {
            base.BindingContext = value;
        }
    }

    [Browsable(false)]
    public new int Bottom => base.Bottom;

    [Browsable(false)]
    public new Rectangle Bounds
    {
        get
        {
            return base.Bounds;
        }
        set
        {
            base.Bounds = value;
        }
    }

    [Browsable(false)]
    public new bool CanFocus => base.CanFocus;

    [Browsable(false)]
    public new bool CanSelect => base.CanSelect;

    [Browsable(false)]
    public new bool Capture
    {
        get
        {
            return base.Capture;
        }
        set
        {
            base.Capture = value;
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
    public new Rectangle ClientRectangle => base.ClientRectangle;

    [Browsable(false)]
    public new Size ClientSize
    {
        get
        {
            return base.ClientSize;
        }
        set
        {
            base.ClientSize = value;
        }
    }

    [Browsable(false)]
    public new string CompanyName => base.CompanyName;

    [Browsable(false)]
    public new bool ContainsFocus => base.ContainsFocus;

    [Browsable(false)]
    public new ContextMenu ContextMenu
    {
        get
        {
            return base.ContextMenu;
        }
        set
        {
            base.ContextMenu = value;
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
    public new bool Created => base.Created;

    [Browsable(false)]
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

    [Browsable(false)]
    public new ControlBindingsCollection DataBindings => base.DataBindings;

    [Browsable(false)]
    public new Rectangle DisplayRectangle => base.DisplayRectangle;

    [Browsable(false)]
    public new bool IsDisposed => base.IsDisposed;

    [Browsable(false)]
    public new bool Disposing => base.Disposing;

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
    public new bool Focused => base.Focused;

    [Browsable(false)]
    public new IntPtr Handle => base.Handle;

    [Browsable(false)]
    public new bool HasChildren => base.HasChildren;

    [Browsable(false)]
    public new int Height
    {
        get
        {
            return base.Height;
        }
        set
        {
            base.Height = value;
        }
    }

    [Browsable(false)]
    public new bool IsHandleCreated => base.IsHandleCreated;

    [Browsable(false)]
    public new bool InvokeRequired => base.InvokeRequired;

    [Browsable(false)]
    public new bool IsAccessible
    {
        get
        {
            return base.IsAccessible;
        }
        set
        {
            base.IsAccessible = value;
        }
    }

    [Browsable(false)]
    public new bool IsMirrored => base.IsMirrored;

    [Browsable(false)]
    public new int Left
    {
        get
        {
            return base.Left;
        }
        set
        {
            base.Left = value;
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
    public new string Name
    {
        get
        {
            return base.Name;
        }
        set
        {
            base.Name = value;
        }
    }

    [Browsable(false)]
    public new Control Parent
    {
        get
        {
            return base.Parent;
        }
        set
        {
            base.Parent = value;
        }
    }

    [Browsable(false)]
    public new string ProductName => base.ProductName;

    [Browsable(false)]
    public new string ProductVersion => base.ProductVersion;

    [Browsable(false)]
    public new bool RecreatingHandle => base.RecreatingHandle;

    [Browsable(false)]
    public new Region Region
    {
        get
        {
            return base.Region;
        }
        set
        {
            base.Region = value;
        }
    }

    [Browsable(false)]
    public new int Right => base.Right;

    [Browsable(false)]
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
    public new ISite Site
    {
        get
        {
            return base.Site;
        }
        set
        {
            base.Site = value;
        }
    }

    [Browsable(false)]
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
    public new int Top
    {
        get
        {
            return base.Top;
        }
        set
        {
            base.Top = value;
        }
    }

    [Browsable(false)]
    public new Control TopLevelControl => base.TopLevelControl;

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

    [Browsable(false)]
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

    [Browsable(false)]
    public new int Width
    {
        get
        {
            return base.Width;
        }
        set
        {
            base.Width = value;
        }
    }

    [Browsable(false)]
    public new IWindowTarget WindowTarget
    {
        get
        {
            return base.WindowTarget;
        }
        set
        {
            base.WindowTarget = value;
        }
    }

    [Browsable(false)]
    public new Size PreferredSize => base.PreferredSize;

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
    public new IContainer Container => base.Container;

    public event EventHandler Tick;

    public event EventHandler IDChanged;

    public event GetValue GetValueEvent;

    public event SetValue SetValueEvent;

    public event GetDataBase GetDataBaseEvent;

    public event GetVarTable GetVarTableEvent;

    public event GetValue GetSystemItemEvent;

    public void TimerON()
    {
        timer1.Start();
    }

    public void TimerOFF()
    {
        timer1.Stop();
    }

    public CTimer()
    {
        InitializeComponent();
    }

    public Bitmap GetLogo()
    {
        return null;
    }

    public byte[] Serialize()
    {
        return null;
    }

    public void DeSerialize(byte[] bytes)
    {
    }

    public void Stop()
    {
        timer1.Stop();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        if (AutoReset)
        {
            ((Timer)sender).Stop();
        }
        Tick?.Invoke(this, e);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        timer1 = new System.Windows.Forms.Timer(components);
        label1 = new System.Windows.Forms.Label();
        base.SuspendLayout();
        timer1.Interval = 1000;
        timer1.Tick += new System.EventHandler(timer1_Tick);
        label1.Dock = System.Windows.Forms.DockStyle.Fill;
        label1.Location = new System.Drawing.Point(0, 0);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(59, 27);
        label1.TabIndex = 0;
        label1.Text = "定时器";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.Transparent;
        base.Controls.Add(label1);
        Name = "CTimer";
        Size = new System.Drawing.Size(59, 27);
        base.ResumeLayout(false);
    }
}
