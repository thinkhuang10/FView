using System;
using System.ComponentModel;
using System.Drawing;

namespace ShapeRuntime;

[Serializable]
public class HTML5_CDateTimePickerProperty
{
    [NonSerialized]
    public CDateTimePicker theDateTimePicker;

    [Description("设定控件名称。")]
    [ReadOnly(false)]
    [DisplayName("名称")]
    [Category("设计")]
    public string ID
    {
        get
        {
            return theDateTimePicker.ID;
        }
        set
        {
            theDateTimePicker.ID = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("字体设置")]
    [Category("外观")]
    [Description("设置控件中字体的样式")]
    public Font Font
    {
        get
        {
            return theDateTimePicker.Font;
        }
        set
        {
            theDateTimePicker.Font = value;
        }
    }

    [Category("行为")]
    [DisplayName("Tab顺序索引")]
    [Description("设置Tab键顺序索引值")]
    [ReadOnly(false)]
    public int TabIndex
    {
        get
        {
            return theDateTimePicker.TabIndex;
        }
        set
        {
            theDateTimePicker.TabIndex = value;
        }
    }

    [Category("布局")]
    [DisplayName("控件位置")]
    [ReadOnly(false)]
    [Description("设置控件的位置（横纵坐标）")]
    public Point Location
    {
        get
        {
            return theDateTimePicker.Location;
        }
        set
        {
            theDateTimePicker.Location = value;
        }
    }

    [DisplayName("控件大小")]
    [ReadOnly(false)]
    [Category("布局")]
    [Description("设置控件的大小（横纵坐标）")]
    public Size Size
    {
        get
        {
            return theDateTimePicker.Size;
        }
        set
        {
            theDateTimePicker.Size = value;
        }
    }

    public HTML5_CDateTimePickerProperty()
    {
    }

    public HTML5_CDateTimePickerProperty(CDateTimePicker _theDateTimePicker)
    {
        theDateTimePicker = _theDateTimePicker;
    }
}
