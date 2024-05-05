using CommonSnappableTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace ShapeRuntime;

[Serializable]
[Guid("FD36F835-8BE3-4219-A143-7F6CEEEFA3E3")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class CCheckBox : CheckBox, IDCCEControl, IControlShape
{
    private bool Runing;

    private bool userclick;

    private string id = "";

    private string textVarBind = "";

    private string varBind = "";

    private bool enablestate;

    [Browsable(false)]
    public bool isRuning
    {
        get
        {
            return Runing;
        }
        set
        {
            Runing = value;
        }
    }

    [Browsable(false)]
    public int BColor
    {
        get
        {
            return base.BackColor.ToArgb();
        }
        set
        {
            base.BackColor = Color.FromArgb(value);
        }
    }

    [Browsable(false)]
    public int FColor
    {
        get
        {
            return base.ForeColor.ToArgb();
        }
        set
        {
            base.ForeColor = Color.FromArgb(value);
        }
    }

    [DisplayName("ID")]
    [Category("设计")]
    [Description("设定控件名称。")]
    [ReadOnly(false)]
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
    public new int ImageIndex
    {
        get
        {
            return base.ImageIndex;
        }
        set
        {
            base.ImageIndex = value;
        }
    }

    [Browsable(false)]
    public new string ImageKey
    {
        get
        {
            return base.ImageKey;
        }
        set
        {
            base.ImageKey = value;
        }
    }

    [Browsable(false)]
    public new ImageList ImageList
    {
        get
        {
            return base.ImageList;
        }
        set
        {
            base.ImageList = value;
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
    public new TextImageRelation TextImageRelation
    {
        get
        {
            return base.TextImageRelation;
        }
        set
        {
            base.TextImageRelation = value;
        }
    }

    [Browsable(false)]
    public new bool UseMnemonic
    {
        get
        {
            return base.UseMnemonic;
        }
        set
        {
            base.UseMnemonic = value;
        }
    }

    [Browsable(false)]
    public new bool UseVisualStyleBackColor
    {
        get
        {
            return base.UseVisualStyleBackColor;
        }
        set
        {
            base.UseVisualStyleBackColor = value;
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
    public new CheckState CheckState
    {
        get
        {
            return base.CheckState;
        }
        set
        {
            base.CheckState = value;
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
    [DisplayName("外形设置")]
    [Category("外观")]
    [Description("设置控件中字体的样式")]
    [ReadOnly(false)]
    public new FlatButtonAppearance FlatAppearance
    {
        get
        {
            return base.FlatAppearance;
        }
        set
        {
            base.FlatAppearance.BorderColor = value.BorderColor;
            base.FlatAppearance.BorderSize = value.BorderSize;
            base.FlatAppearance.MouseDownBackColor = value.MouseDownBackColor;
            base.FlatAppearance.MouseOverBackColor = value.MouseOverBackColor;
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
    public string TextVarBind
    {
        get
        {
            return textVarBind;
        }
        set
        {
            textVarBind = value;
            Text = "#Bind:" + textVarBind;
        }
    }

    [Browsable(false)]
    public string VarBind
    {
        get
        {
            return varBind;
        }
        set
        {
            varBind = value;
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
    public new bool AutoEllipsis
    {
        get
        {
            return base.AutoEllipsis;
        }
        set
        {
            base.AutoEllipsis = value;
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

    public event GetValue GetValueEvent;

    public event SetValue SetValueEvent;

    public event GetDataBase GetDataBaseEvent;

    public event GetVarTable GetVarTableEvent;

    public event GetValue GetSystemItemEvent;

    public event EventHandler CheckedChange;

    public event requestEventBindDictDele RequestEventBindDict;

    public event requestPropertyBindDataDele RequestPropertyBindData;

    public event EventHandler IDChanged;

    public Bitmap GetLogo()
    {
        return null;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (varBind != null && varBind != "")
            {
                base.Checked = Convert.ToBoolean(GetValueEvent("[" + varBind + "]"));
            }
        }
        catch (Exception)
        {
        }
        try
        {
            if (textVarBind != null && textVarBind != "")
            {
                base.Text = Convert.ToString(GetValueEvent("[" + textVarBind + "]"));
            }
        }
        catch (Exception)
        {
        }
    }

    public CCheckBox()
    {
        base.Click += CCheckBox_Click;
        base.CheckedChanged += CCheckBox_CheckedChanged;
        Text = "多选框";
        ForeColor = Color.Black;
    }

    private void CCheckBox_Click(object sender, EventArgs e)
    {
        if (Runing && varBind != null && varBind != "")
        {
            userclick = true;
            SetValueEvent("[" + varBind + "]", base.Checked);
            base.Checked = !base.Checked;
            userclick = false;
        }
    }

    private void CCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckedChange != null && !userclick)
        {
            CheckedChange(sender, e);
        }
    }

    public byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        CCheckBoxSaveItems cCheckBoxSaveItems = new()
        {
            Text = Text,
            BackColor = base.BackColor,
            ForeColor = base.ForeColor,
            Font = base.Font,
            Checked = base.Checked,
            TabIndex = TabIndex,
            VarBind = varBind,
            hide = !base.Visible,
            disable = !base.Enabled,
            Enabled = base.Enabled,
            textVarBind = textVarBind
        };
        formatter.Serialize(memoryStream, cCheckBoxSaveItems);
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
            CCheckBoxSaveItems cCheckBoxSaveItems = (CCheckBoxSaveItems)formatter.Deserialize(stream);
            stream.Close();
            Text = cCheckBoxSaveItems.Text;
            base.BackColor = cCheckBoxSaveItems.BackColor;
            base.ForeColor = cCheckBoxSaveItems.ForeColor;
            base.Font = cCheckBoxSaveItems.Font;
            base.Checked = cCheckBoxSaveItems.Checked;
            TabIndex = cCheckBoxSaveItems.TabIndex;
            VarBind = cCheckBoxSaveItems.VarBind;
            base.Visible = !cCheckBoxSaveItems.hide;
            enablestate = cCheckBoxSaveItems.Enabled;
            textVarBind = cCheckBoxSaveItems.textVarBind;
        }
        catch (Exception)
        {
        }
    }

    public void Stop()
    {
    }
}
