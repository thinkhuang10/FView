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
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("4D729D4E-E1B0-4d55-9768-95CFEB82DCEA")]
[ComVisible(true)]
public class CLabel : Label, IDCCEControl, IControlShape
{
    private bool Runing;

    private string id = "";

    private string currentText = "";

    private string textVarBind = "";

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
    public string DisplayStr
    {
        get
        {
            return Text;
        }
        set
        {
            Text = value;
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

    [Category("设计")]
    [DisplayName("ID")]
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
            if (this.IDChanged != null)
            {
                this.IDChanged(this, null);
            }
        }
    }

    public override string Text
    {
        get
        {
            if (base.Text == "" && currentText != "")
            {
                return currentText;
            }
            return base.Text;
        }
        set
        {
            base.Text = value;
            currentText = base.Text;
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
    public string TextVarBind
    {
        get
        {
            return textVarBind;
        }
        set
        {
            textVarBind = value;
            if (value != "")
            {
                Text = "#Bind:" + value;
            }
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

    public event GetValue GetValueEvent;

    public event SetValue SetValueEvent;

    public event GetDataBase GetDataBaseEvent;

    public event GetVarTable GetVarTableEvent;

    public event GetValue GetSystemItemEvent;

    public new event EventHandler Click;

    public event requestEventBindDictDele requestEventBindDict;

    public event requestPropertyBindDataDele requestPropertyBindData;

    public event EventHandler IDChanged;

    public Bitmap GetLogo()
    {
        return null;
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (textVarBind != null && textVarBind != "")
            {
                base.Text = Convert.ToString(this.GetValueEvent("[" + textVarBind + "]"));
            }
        }
        catch (Exception)
        {
        }
    }

    public CLabel()
    {
        base.Click += CLabel_Click;
        Text = "标签";
        ForeColor = Color.Black;
    }

    private void CLabel_Click(object sender, EventArgs e)
    {
        if (this.Click != null)
        {
            this.Click(sender, e);
        }
    }

    public byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        CLabelSaveItems cLabelSaveItems = new()
        {
            Text = currentText,
            BackColor = base.BackColor,
            ForeColor = base.ForeColor,
            Font = base.Font,
            hide = !base.Visible,
            disable = !base.Enabled,
            align = TextAlign,
            textVarBind = textVarBind
        };
        formatter.Serialize(memoryStream, cLabelSaveItems);
        byte[] result = memoryStream.ToArray();
        memoryStream.Close();
        return result;
    }

    public void DeSerialize(byte[] bytes)
    {
        try
        {
            IFormatter formatter = new BinaryFormatter();
            using Stream stream = new MemoryStream(bytes);
            CLabelSaveItems cLabelSaveItems = (CLabelSaveItems)formatter.Deserialize(stream);
            stream.Close();
            Text = cLabelSaveItems.Text;
            base.BackColor = cLabelSaveItems.BackColor;
            base.ForeColor = cLabelSaveItems.ForeColor;
            base.Font = cLabelSaveItems.Font;
            base.Visible = !cLabelSaveItems.hide;
            base.Enabled = !cLabelSaveItems.disable;
            TextAlign = cLabelSaveItems.align;
            textVarBind = cLabelSaveItems.textVarBind;
        }
        catch (Exception)
        {
        }
    }

    public void Stop()
    {
    }
}
