using CommonSnappableTypes;
using ShapeRuntime;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace DynamicAlarm;

[Guid("08E1C200-564B-48CC-A63F-6A5B7A610F99")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class LedAlarmControl : UserControl, IDCCEControl, IControlShape
{
    public bool bValue = true;

    public bool bCurrentValue = true;

    private readonly Bitmap bmGreen;

    private readonly Bitmap bmBlack;

    private readonly Bitmap bmRed;

    public int iflash = 1;

    private SAVE_LedAlarm saveData = new();

    private string id = "";

    private Timer timer;

    [Browsable(false)]
    public bool isRuning { get; set; }

    [Description("设定控件名称。")]
    [Category("设计")]
    [ReadOnly(false)]
    [DisplayName("名称")]
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

    [Description("控件左上角相对于其容器左上角的坐标")]
    [ReadOnly(false)]
    [Category("布局")]
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

    [Description("控件的大小（以像素为单位）")]
    [ReadOnly(false)]
    [DisplayName("大小")]
    [Category("布局")]
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

    public event EventHandler TreeNodeClicked;

    public event EventHandler TreeNodeDoubleClicked;

    public event EventHandler TreeNodeSelectedChanged;

    public event EventHandler IDChanged;

    public byte[] Serialize()
    {
        SAVE_LedAlarm graph = saveData;
        BinaryFormatter binaryFormatter = new();
        MemoryStream memoryStream = new();
        binaryFormatter.Serialize(memoryStream, graph);
        byte[] result = memoryStream.ToArray();
        memoryStream.Dispose();
        return result;
    }

    public void DeSerialize(byte[] bytes)
    {
        BinaryFormatter binaryFormatter = new();
        MemoryStream memoryStream = new(bytes);
        saveData = (SAVE_LedAlarm)binaryFormatter.Deserialize(memoryStream);
        memoryStream.Dispose();
    }

    public static Image GetLogoStatic()
    {
        ResourceManager resourceManager = new(typeof(ResourcePic));
        return (Bitmap)resourceManager.GetObject("LedAlarm_Red");
    }

    public Bitmap GetLogo()
    {
        ResourceManager resourceManager = new(typeof(ResourcePic));
        return (Bitmap)resourceManager.GetObject("LedAlarm_Red");
    }

    public void Stop()
    {
    }

    private void LedAlarmControl_Load(object sender, EventArgs e)
    {
        if (!isRuning)
        {
            BackgroundImage = bmRed;
            timer.Enabled = false;
            return;
        }
        if (saveData.strVar == "")
        {
            BackgroundImage = bmGreen;
            return;
        }
        if (saveData.bBoolValue)
        {
            string text = "";
            try
            {
                text = GetValueEvent("[" + saveData.strVar + "]").ToString();
            }
            catch
            {
                MessageBox.Show("请检查DXP是否运行正常！", "提示");
                timer.Enabled = false;
                return;
            }
            if (text == "False")
            {
                bCurrentValue = false;
            }
            else
            {
                bCurrentValue = true;
            }
        }
        else
        {
            double num = 0.0;
            try
            {
                num = Convert.ToDouble(GetValueEvent("[" + saveData.strVar + "]"));
            }
            catch
            {
                MessageBox.Show("请检查DXP是否运行正常！", "提示");
                timer.Enabled = false;
                return;
            }
            double num2 = Convert.ToDouble(saveData.iMaxValue);
            double num3 = Convert.ToDouble(saveData.iMinValue);
            if (num >= num3 && num <= num2)
            {
                bCurrentValue = true;
            }
            else
            {
                bCurrentValue = false;
            }
        }
        if (!bCurrentValue)
        {
            BackgroundImage = bmGreen;
        }
        else
        {
            BackgroundImage = bmRed;
        }
        timer.Enabled = true;
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        if (saveData.strVar == "")
        {
            timer.Enabled = false;
            return;
        }
        if (saveData.bBoolValue)
        {
            string text = "";
            try
            {
                text = GetValueEvent("[" + saveData.strVar + "]").ToString();
            }
            catch
            {
                MessageBox.Show("请检查DXP是否运行正常！", "提示");
                timer.Enabled = false;
                return;
            }
            if (text == "False")
            {
                bCurrentValue = false;
            }
            else
            {
                bCurrentValue = true;
            }
        }
        else
        {
            double num = 0.0;
            try
            {
                num = Convert.ToDouble(GetValueEvent("[" + saveData.strVar + "]"));
            }
            catch
            {
                MessageBox.Show("请检查DXP是否运行正常！", "提示");
                timer.Enabled = false;
                return;
            }
            double num2 = Convert.ToDouble(saveData.iMaxValue);
            double num3 = Convert.ToDouble(saveData.iMinValue);
            if (num >= num3 && num <= num2)
            {
                bCurrentValue = true;
            }
            else
            {
                bCurrentValue = false;
            }
        }
        if (!bCurrentValue)
        {
            BackgroundImage = bmGreen;
        }
        else if (!saveData.bTwinkle)
        {
            BackgroundImage = bmRed;
        }
        else if (iflash % 2 == 0)
        {
            BackgroundImage = bmBlack;
            iflash = 1;
        }
        else
        {
            BackgroundImage = bmRed;
            iflash = 2;
        }
    }

    private void LedAlarmControl_Click(object sender, EventArgs e)
    {
        if (!isRuning)
        {
            LedAlarmSetForm ledAlarmSetForm = new(saveData);
            ledAlarmSetForm.GetVarTableEvent += GetVarTableEvent;
            ledAlarmSetForm.ShowDialog();
        }
    }

    public LedAlarmControl()
    {
        InitializeComponent();
        SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
        SetStyle(ControlStyles.DoubleBuffer, value: true);
        SetStyle(ControlStyles.ResizeRedraw, value: true);
        SetStyle(ControlStyles.Selectable, value: true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
        SetStyle(ControlStyles.UserPaint, value: true);
        BackColor = Color.Transparent;
        Cursor = Cursors.Hand;
        Size = new Size(149, 70);
        BackgroundImageLayout = ImageLayout.Stretch;
        ResourceManager resourceManager = new(typeof(ResourcePic));
        bmGreen = (Bitmap)resourceManager.GetObject("LedAlarm_Green");
        bmBlack = (Bitmap)resourceManager.GetObject("LedAlarm_Black");
        bmRed = (Bitmap)resourceManager.GetObject("LedAlarm_Red");
    }

    private void InitializeComponent()
    {
        timer = new System.Windows.Forms.Timer();
        base.SuspendLayout();
        timer.Interval = 1000;
        timer.Tick += new System.EventHandler(timer_Tick);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.Name = "LedAlarmControl";
        base.Load += new System.EventHandler(LedAlarmControl_Load);
        base.Click += new System.EventHandler(LedAlarmControl_Click);
        base.ResumeLayout(false);
    }
}
