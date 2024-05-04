using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace CommonSnappableTypes;

public class VarTableUITypeEditor : UITypeEditor
{
    public static GetVarTable GetVar;

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
    {
        IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
        if (windowsFormsEditorService != null && GetVar != null)
        {
            return GetVar("");
        }
        return value;
    }
}
