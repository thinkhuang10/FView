using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace CommonSnappableTypes;

public class VarTableImageManage : UITypeEditor
{
    public static GetImageName GetImage;

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
    {
        IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
        if (windowsFormsEditorService != null && GetImage != null)
        {
            return GetImage(value);
        }
        return value;
    }
}
