using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
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
[Guid("DC8C74FC-902F-4360-B190-CF0F1A6782F7")]
[ComVisible(true)]
public class CPictureBox : PictureBox, IDCCEControl, IControlShape
{
    private string id = "";

    [NonSerialized]
    private Image _img;

    private string ImageName = "";

    [Browsable(false)]
    public bool isRuning { get; set; }

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

    [Description("设定控件名称。")]
    [ReadOnly(false)]
    [Category("设计")]
    [DisplayName("ID")]
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

    [Editor(typeof(VarTableImageManage), typeof(UITypeEditor))]
    [DisplayName("Image")]
    [Category("外观")]
    [Description("在控件中显示的图像。")]
    [ReadOnly(false)]
    [DHMINeedSerProperty]
    public new BitMapForIM Image
    {
        get
        {
            BitMapForIM bitMapForIM = new()
            {
                img = _img,
                ImgGUID = ImageName
            };
            return bitMapForIM;
        }
        set
        {
            if (value != null)
            {
                Image img2 = (base.Image = value.img);
                _img = img2;
                ImageName = value.ImgGUID;
            }
            else
            {
                Image img3 = (base.Image = null);
                _img = img3;
            }
        }
    }

    [ReadOnly(false)]
    [Category("外观")]
    [Description("返回图片在控件中的布局")]
    [DisplayName("图片布局")]
    public PictureBoxSizeMode FillSizeMode
    {
        get
        {
            return SizeMode;
        }
        set
        {
            SizeMode = value;
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
    public new PictureBoxSizeMode SizeMode
    {
        get
        {
            return base.SizeMode;
        }
        set
        {
            base.SizeMode = value;
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
    public new Image ErrorImage
    {
        get
        {
            return base.ErrorImage;
        }
        set
        {
            base.ErrorImage = value;
        }
    }

    [Browsable(false)]
    public new string ImageLocation
    {
        get
        {
            return base.ImageLocation;
        }
        set
        {
            base.ImageLocation = value;
        }
    }

    [Browsable(false)]
    public new Image InitialImage
    {
        get
        {
            return base.InitialImage;
        }
        set
        {
            base.InitialImage = value;
        }
    }

    [Browsable(false)]
    public new bool WaitOnLoad
    {
        get
        {
            return base.WaitOnLoad;
        }
        set
        {
            base.WaitOnLoad = value;
        }
    }

    public event GetValue GetValueEvent;

    public event SetValue SetValueEvent;

    public event GetDataBase GetDataBaseEvent;

    public event GetVarTable GetVarTableEvent;

    public event GetValue GetSystemItemEvent;

    public new event EventHandler Click;

    public event requestEventBindDictDele RequestEventBindDict;

    public event requestPropertyBindDataDele RequestPropertyBindData;

    public event EventHandler IDChanged;

    public Bitmap GetLogo()
    {
        return null;
    }

    public CPictureBox()
    {
        base.Click += CPictureBox_Click;
        BackColor = SystemColors.GrayText;
        ForeColor = Color.Black;
    }

    private void CPictureBox_Click(object sender, EventArgs e)
    {
        Click?.Invoke(sender, e);
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
        try
        {
            base.OnPaint(pe);
        }
        catch (Exception)
        {
            Image = null;
        }
    }

    public byte[] Serialize()
    {
        if (Image != null && Image.img != null && string.IsNullOrEmpty(ImageName))
        {
            ImageName = DHMIImageManage.SaveImage(Image.img);
        }
        using MemoryStream memoryStream = new();
        IFormatter formatter = new BinaryFormatter();
        formatter.Serialize(memoryStream, new CPictureBoxSaveItems
        {
            FillSizeMode = Convert.ToInt32(SizeMode),
            hide = !base.Visible,
            disable = !base.Enabled,
            ImageTag = ImageName
        });
        return memoryStream.ToArray();
    }

    public void DeSerialize(byte[] bytes)
    {
        try
        {
            CPictureBoxSaveItems cPictureBoxSaveItems = null;
            using (Stream serializationStream = new MemoryStream(bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                cPictureBoxSaveItems = (CPictureBoxSaveItems)formatter.Deserialize(serializationStream);
            }
            try
            {
                FillSizeMode = (PictureBoxSizeMode)Enum.Parse(typeof(PictureBoxSizeMode), cPictureBoxSaveItems.FillSizeMode.ToString());
            }
            catch
            {
            }
            base.Visible = !cPictureBoxSaveItems.hide;
            base.Enabled = !cPictureBoxSaveItems.disable;
            ImageName = cPictureBoxSaveItems.ImageTag;
            if (!string.IsNullOrEmpty(ImageName))
            {
                Image = new BitMapForIM
                {
                    img = DHMIImageManage.LoadImage(ImageName),
                    ImgGUID = ImageName
                };
            }
        }
        catch
        {
        }
    }

    public void Stop()
    {
    }
}
