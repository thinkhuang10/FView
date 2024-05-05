using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CommonSnappableTypes;

namespace ShapeRuntime;

[Serializable]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("619A6A42-AA9F-4be9-A3B6-93EBA8B440AB")]
public class CDateTimePicker : DateTimePicker, IDCCEControl, IControlShape
{
    private bool enablestate;

    private string id = "";

    [Browsable(false)]
    public bool isRuning { get; set; }

    [Browsable(false)]
    public bool Enablestate
    {
        get
        {
            return !enablestate;
        }
        set
        {
            enablestate = !value;
        }
    }

    [Description("设定控件名称。")]
    [ReadOnly(false)]
    [DisplayName("ID")]
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
            IDChanged?.Invoke(this, null);
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
    public new Color CalendarForeColor
    {
        get
        {
            return base.CalendarForeColor;
        }
        set
        {
            base.CalendarForeColor = value;
        }
    }

    [Browsable(false)]
    public int Year
    {
        get
        {
            return base.Value.Year;
        }
        set
        {
            base.Value = new DateTime(value, base.Value.Month, base.Value.Day, base.Value.Hour, base.Value.Minute, base.Value.Second);
        }
    }

    [Browsable(false)]
    public int Month
    {
        get
        {
            return base.Value.Month;
        }
        set
        {
            base.Value = new DateTime(base.Value.Year, value, base.Value.Day, base.Value.Hour, base.Value.Minute, base.Value.Second);
        }
    }

    [Browsable(false)]
    public int Day
    {
        get
        {
            return base.Value.Day;
        }
        set
        {
            base.Value = new DateTime(base.Value.Year, base.Value.Month, value, base.Value.Hour, base.Value.Minute, base.Value.Second);
        }
    }

    [Browsable(false)]
    public int Hour
    {
        get
        {
            return base.Value.Hour;
        }
        set
        {
            base.Value = new DateTime(base.Value.Year, base.Value.Month, base.Value.Day, value, base.Value.Minute, base.Value.Second);
        }
    }

    [Browsable(false)]
    public int Minute
    {
        get
        {
            return base.Value.Minute;
        }
        set
        {
            base.Value = new DateTime(base.Value.Year, base.Value.Month, base.Value.Day, base.Value.Hour, value, base.Value.Second);
        }
    }

    [Browsable(false)]
    public int Second
    {
        get
        {
            return base.Value.Second;
        }
        set
        {
            base.Value = new DateTime(base.Value.Year, base.Value.Month, base.Value.Day, base.Value.Hour, base.Value.Minute, value);
        }
    }

    [Browsable(false)]
    public new Font CalendarFont
    {
        get
        {
            return base.CalendarFont;
        }
        set
        {
            base.CalendarFont = value;
        }
    }

    [Browsable(false)]
    public new Color CalendarMonthBackground
    {
        get
        {
            return base.CalendarMonthBackground;
        }
        set
        {
            base.CalendarMonthBackground = value;
        }
    }

    [Browsable(false)]
    public new Color CalendarTitleBackColor
    {
        get
        {
            return base.CalendarTitleBackColor;
        }
        set
        {
            base.CalendarTitleBackColor = value;
        }
    }

    [Browsable(false)]
    public new Color CalendarTitleForeColor
    {
        get
        {
            return base.CalendarTitleForeColor;
        }
        set
        {
            base.CalendarTitleForeColor = value;
        }
    }

    [Browsable(false)]
    public new Color CalendarTrailingForeColor
    {
        get
        {
            return base.CalendarTrailingForeColor;
        }
        set
        {
            base.CalendarTrailingForeColor = value;
        }
    }

    [Browsable(false)]
    public new LeftRightAlignment DropDownAlign
    {
        get
        {
            return base.DropDownAlign;
        }
        set
        {
            base.DropDownAlign = value;
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
    public new bool RightToLeftLayout
    {
        get
        {
            return base.RightToLeftLayout;
        }
        set
        {
            base.RightToLeftLayout = value;
        }
    }

    [Browsable(false)]
    public new bool ShowCheckBox
    {
        get
        {
            return base.ShowCheckBox;
        }
        set
        {
            base.ShowCheckBox = value;
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
    public string ReadOnly { get; set; }

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
    public new bool Checked
    {
        get
        {
            return base.Checked;
        }
        set
        {
            base.Checked = value;
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

    public new event EventHandler ValueChanged;

    public event requestEventBindDictDele RequestEventBindDict;

    public event requestPropertyBindDataDele RequestPropertyBindData;

    public event EventHandler IDChanged;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

    public Bitmap GetLogo()
    {
        return null;
    }

    public CDateTimePicker()
    {
        base.ValueChanged += CDateTimePicker_ValueChanged;
        base.MouseDown += CDateTimePicker_MouseDown;
        base.Format = DateTimePickerFormat.Custom;
        base.CustomFormat = "yyyy/MM/dd HH:mm:ss";
        ForeColor = Color.Black;
    }

    private void CDateTimePicker_MouseDown(object sender, MouseEventArgs e)
    {
        if (base.Format == DateTimePickerFormat.Short || base.Format == DateTimePickerFormat.Long)
        {
            SendMessage(new HandleRef(this, base.Handle), 260, 40, 0);
        }
    }

    private void CDateTimePicker_ValueChanged(object sender, EventArgs e)
    {
        ValueChanged?.Invoke(sender, e);
    }

    public byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        CDateTimePickerSaveItems cDateTimePickerSaveItems = new()
        {
            Text = Text,
            BackColor = base.BackColor,
            ForeColor = base.ForeColor,
            Font = base.Font,
            TabIndex = TabIndex,
            hide = !base.Visible,
            disable = !base.Enabled,
            enablestate = enablestate
        };
        formatter.Serialize(memoryStream, cDateTimePickerSaveItems);
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
            CDateTimePickerSaveItems cDateTimePickerSaveItems = (CDateTimePickerSaveItems)formatter.Deserialize(stream);
            stream.Close();
            Text = cDateTimePickerSaveItems.Text;
            base.BackColor = cDateTimePickerSaveItems.BackColor;
            base.ForeColor = cDateTimePickerSaveItems.ForeColor;
            base.Font = cDateTimePickerSaveItems.Font;
            TabIndex = cDateTimePickerSaveItems.TabIndex;
            base.Visible = !cDateTimePickerSaveItems.hide;
            base.Enabled = !cDateTimePickerSaveItems.disable;
            enablestate = cDateTimePickerSaveItems.enablestate;
        }
        catch (Exception)
        {
        }
    }

    public void Stop()
    {
    }
}
