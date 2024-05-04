using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using CommonSnappableTypes;

namespace ShapeRuntime;

[Serializable]
public class HTML5_CPictureBoxProperty
{
    [NonSerialized]
    public CPictureBox thePictureBox;

    [DisplayName("控件图片")]
    [Editor(typeof(VarTableImageManage), typeof(UITypeEditor))]
    [Category("外观")]
    [Description("在控件中显示的图像。")]
    [ReadOnly(false)]
    [DHMINeedSerProperty]
    public BitMapForIM Image
    {
        get
        {
            return thePictureBox.Image;
        }
        set
        {
            thePictureBox.Image = value;
        }
    }

    [ReadOnly(false)]
    [Category("设计")]
    [Description("设定控件名称。")]
    [DisplayName("名称")]
    public string ID
    {
        get
        {
            return thePictureBox.ID;
        }
        set
        {
            thePictureBox.ID = value;
        }
    }

    [DisplayName("图像大小")]
    [Category("布局")]
    [Description("设置控件的长宽")]
    [ReadOnly(false)]
    public Size Size
    {
        get
        {
            return thePictureBox.Size;
        }
        set
        {
            thePictureBox.Size = value;
        }
    }

    [DisplayName("图像的可见性")]
    [Category("外观")]
    [Description("设置控件的长宽")]
    [ReadOnly(false)]
    public bool Visible
    {
        get
        {
            return thePictureBox.Visible;
        }
        set
        {
            thePictureBox.Visible = value;
        }
    }

    [DisplayName("图像位置")]
    [Category("布局")]
    [Description("设置控件的位置")]
    [ReadOnly(false)]
    public Point Location
    {
        get
        {
            return thePictureBox.Location;
        }
        set
        {
            thePictureBox.Location = value;
        }
    }

    [Description("设置控件的名称")]
    [DisplayName("图像名称")]
    [Category("行为")]
    [ReadOnly(false)]
    public string Name
    {
        get
        {
            return thePictureBox.Name;
        }
        set
        {
            thePictureBox.Name = value;
        }
    }

    [Description("设置或获取控件的背景色")]
    [Category("外观")]
    [DisplayName("控件背景色")]
    [ReadOnly(false)]
    public Color BackColor
    {
        get
        {
            return thePictureBox.BackColor;
        }
        set
        {
            thePictureBox.BackColor = value;
        }
    }

    public HTML5_CPictureBoxProperty()
    {
    }

    public HTML5_CPictureBoxProperty(CPictureBox _thePictureBox)
    {
        thePictureBox = _thePictureBox;
    }
}
