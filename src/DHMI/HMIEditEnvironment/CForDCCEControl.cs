using System.Windows.Forms;
using CommonSnappableTypes;
using ShapeRuntime;

namespace HMIEditEnvironment;

internal class CForDCCEControl
{
    public static string GetVarTableEvent(string controlname)
    {
        if (controlname == "ProjectPath")
        {
            return CEditEnvironmentGlobal.HMIPath;
        }
        if (controlname == "PageNames")
        {
            string text = "";
            foreach (DataFile df in CEditEnvironmentGlobal.dfs)
            {
                string text2 = text;
                text = text2 + df.name + ":" + df.pageName + "|";
            }
            if (text.EndsWith("|"))
            {
                text = text.Substring(0, text.Length - 1);
            }
            return text;
        }
        VarTable varTable = new(CEditEnvironmentGlobal.dhp, CEditEnvironmentGlobal.xmldoc, controlname);
        varTable.ShowDialog();
        if (varTable.DialogResult == DialogResult.OK)
        {
            switch (controlname)
            {
                case "history":
                case "realtime":
                    {
                        string[] array2 = varTable.value.Split('|');
                        string[] array3 = varTable.tagvalue.Split('|');
                        string text4 = "";
                        for (int j = 0; j < array2.Length; j++)
                        {
                            string text5 = text4;
                            text4 = text5 + "|" + array2[j] + "|" + array3[j];
                        }
                        return text4.Substring(1);
                    }
                case "para":
                    {
                        string[] array = varTable.value.Split('|');
                        string text3 = "";
                        for (int i = 0; i < array.Length; i++)
                        {
                            text3 = text3 + "|" + array[i];
                        }
                        return text3.Substring(1);
                    }
                default:
                    return varTable.value;
            }
        }
        if (controlname == "history" || controlname == "realtime")
        {
            return "|";
        }
        return "";
    }

    public static bool ValidateVarEvent(string varName)
    {
        return CheckIOExists.IOItemList.Contains(varName);
    }

    public static BitMapForIM GetImage(object InitialImage)
    {
        ImageManage imageManage = new();
        if (imageManage.ShowDialog() == DialogResult.OK)
        {
            BitMapForIM bitMapForIM = new()
            {
                img = imageManage.OutImage
            };
            if (imageManage.OutImage != null)
            {
                bitMapForIM.ImgGUID = imageManage.OutImage.Tag.ToString();
            }
            return bitMapForIM;
        }
        BitMapForIM bitMapForIM2 = new();
        return (BitMapForIM)InitialImage;
    }
}
