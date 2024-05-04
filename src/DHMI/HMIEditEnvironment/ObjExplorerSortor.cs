using System.Collections;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class ObjExplorerSortor : IComparer
{
    public bool m_bSortType;

    public ObjExplorerSortor()
    {
        m_bSortType = true;
    }

    public int Compare(object x, object y)
    {
        string text = ((ListViewItem)x).SubItems[0].Text;
        string text2 = ((ListViewItem)y).SubItems[0].Text;
        string strS;
        string strN;
        bool flag = SplitStr(text, out strS, out strN);
        string strS2;
        string strN2;
        bool flag2 = SplitStr(text2, out strS2, out strN2);
        if (flag && flag2 && strS == strS2)
        {
            while (strN.Length < strN2.Length)
            {
                strN = "0" + strN;
            }
            while (strN2.Length < strN.Length)
            {
                strN2 = "0" + strN2;
            }
            text = strN;
            text2 = strN2;
        }
        if (m_bSortType)
        {
            return string.Compare(text, text2);
        }
        return string.Compare(text2, text);
    }

    private bool SplitStr(string str, out string strS, out string strN)
    {
        strS = (strN = "");
        if (str == null || str.Length == 0)
        {
            return false;
        }
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            if (char.IsDigit(c))
            {
                strN += c;
                continue;
            }
            strS += strN;
            strS += c;
            strN = "";
        }
        if (strN.Length <= 0)
        {
            return false;
        }
        return true;
    }
}
