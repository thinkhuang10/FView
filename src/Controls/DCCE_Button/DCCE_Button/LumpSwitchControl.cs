using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using CommonSnappableTypes;
using ShapeRuntime;

namespace DCCE_Button;

[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("A730857A-86FC-476D-939E-68DE78998D81")]
[ComVisible(true)]
public class LumpSwitchControl : UserControl, IDCCEControl, IControlShape
{
    private bool isCheck;

    private SAVE_LumpSwitch saveData = new();

    private string id = "";

    private IContainer components;

    private Timer timer;

    [Browsable(false)]
    public bool isRuning { get; set; }

    [ReadOnly(false)]
    [DisplayName("名称")]
    [Category("设计")]
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
    [DisplayName("位置")]
    [Category("布局")]
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

    [Category("布局")]
    [DisplayName("大小")]
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
        SAVE_LumpSwitch graph = saveData;
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
        saveData = (SAVE_LumpSwitch)binaryFormatter.Deserialize(memoryStream);
        memoryStream.Dispose();
    }

    public static Image GetLogoStatic()
    {
        ResourceManager resourceManager = new(typeof(ResourcePic));
        return (Bitmap)resourceManager.GetObject("LumpSwitch_ON");
    }

    public Bitmap GetLogo()
    {
        ResourceManager resourceManager = new(typeof(ResourcePic));
        return (Bitmap)resourceManager.GetObject("LumpSwitch_ON");
    }

    public void Stop()
    {
    }

    public LumpSwitchControl()
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
        Size = new Size(87, 87);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        ResourceManager resourceManager = new(typeof(ResourcePic));
        Bitmap bitmap = (Bitmap)resourceManager.GetObject("LumpSwitch_ON");
        Bitmap bitmap2 = (Bitmap)resourceManager.GetObject("LumpSwitch_OFF");
        Graphics graphics = e.Graphics;
        Rectangle rect = new(0, 0, Size.Width, Size.Height);
        if (isCheck)
        {
            graphics.DrawImage(bitmap, rect);
        }
        else
        {
            graphics.DrawImage(bitmap2, rect);
        }
    }

    private void LumpSwitchControl_Click(object sender, EventArgs e)
    {
        if (isRuning)
        {
            isCheck = !isCheck;
            Invalidate();
            if (saveData.strVar == "")
            {
                return;
            }
            if (isCheck)
            {
                if (saveData.bBoolValue)
                {
                    try
                    {
                        SetValueEvent("[" + saveData.strVar + "]", 1);
                        return;
                    }
                    catch
                    {
                        MessageBox.Show("请检查DXP是否运行正常！", "提示");
                        return;
                    }
                }
                try
                {
                    SetValueEvent("[" + saveData.strVar + "]", saveData.iMaxValue);
                    return;
                }
                catch
                {
                    MessageBox.Show("请检查DXP是否运行正常！", "提示");
                    return;
                }
            }
            if (saveData.bBoolValue)
            {
                try
                {
                    SetValueEvent("[" + saveData.strVar + "]", 0);
                    return;
                }
                catch
                {
                    MessageBox.Show("请检查DXP是否运行正常！", "提示");
                    return;
                }
            }
            try
            {
                SetValueEvent("[" + saveData.strVar + "]", saveData.iMinValue);
                return;
            }
            catch
            {
                MessageBox.Show("请检查DXP是否运行正常！", "提示");
                return;
            }
        }
        LumpSwitchSetForm lumpSwitchSetForm = new(saveData);
        lumpSwitchSetForm.GetVarTableEvent += GetVarTableEvent;
        lumpSwitchSetForm.ShowDialog();
    }

    private void LumpSwitchControl_Load(object sender, EventArgs e)
    {
        if (!isRuning || saveData.strVar == "")
        {
            return;
        }
        string text = "";
        try
        {
            text = GetValueEvent("[" + saveData.strVar + "]").ToString();
        }
        catch
        {
            MessageBox.Show("请检查DXP是否运行正常！", "提示");
            return;
        }
        if (saveData.bBoolValue)
        {
            if (text == "False")
            {
                isCheck = false;
            }
            else
            {
                isCheck = true;
            }
        }
        else if (saveData.iMaxValue == text)
        {
            isCheck = true;
        }
        else if (saveData.iMinValue == text)
        {
            isCheck = false;
        }
        timer.Enabled = true;
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (saveData.bBoolValue)
            {
                try
                {
                    if (!Convert.ToBoolean(GetValueEvent("[" + saveData.strVar + "]")))
                    {
                        isCheck = false;
                        Invalidate();
                    }
                    else
                    {
                        isCheck = true;
                        Invalidate();
                    }
                    return;
                }
                catch
                {
                    MessageBox.Show("请检查DXP是否运行正常！", "提示");
                    timer.Enabled = false;
                    return;
                }
            }
            try
            {
                if (saveData.iMaxValue == GetValueEvent("[" + saveData.strVar + "]").ToString())
                {
                    isCheck = true;
                    Invalidate();
                }
                else if (saveData.iMinValue == GetValueEvent("[" + saveData.strVar + "]").ToString())
                {
                    isCheck = false;
                    Invalidate();
                }
            }
            catch
            {
                MessageBox.Show("请检查DXP是否运行正常！", "提示");
                timer.Enabled = false;
            }
        }
        catch
        {
            MessageBox.Show("开关定时刷新出现异常！", "提示");
            timer.Enabled = false;
        }
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
        timer = new System.Windows.Forms.Timer(components);
        base.SuspendLayout();
        timer.Interval = 1000;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.Name = "LumpSwitchControl";
        base.Load += new System.EventHandler(LumpSwitchControl_Load);
        base.Click += new System.EventHandler(LumpSwitchControl_Click);
        base.ResumeLayout(false);
    }
}
