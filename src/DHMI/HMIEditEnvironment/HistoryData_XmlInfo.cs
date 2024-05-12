using System.Windows.Forms;
using System.Xml;

namespace HMIEditEnvironment;

internal class HistoryData_XmlInfo
{
    private readonly XmlDocument xmlDoc = new();

    public bool CreatXML(string strPath)
    {
        try
        {
            XmlDeclaration newChild = xmlDoc.CreateXmlDeclaration("1.0", "GB2312", null);
            xmlDoc.AppendChild(newChild);
            XmlElement xmlElement = xmlDoc.CreateElement("DocumentRoot");
            xmlDoc.AppendChild(xmlElement);
            XmlNode xmlNode = xmlDoc.CreateElement("bCrossHistory");
            xmlNode.InnerText = "false";
            xmlElement.AppendChild(xmlNode);
            xmlDoc.Save(strPath);
            return true;
        }
        catch
        {
            MessageBox.Show("创建历史数据库服务Xml出现异常！", "提示");
            return false;
        }
    }

    public string ReadHistoryDataXml()
    {
        string filename = CEditEnvironmentGlobal.HMIPath + "\\DBCrossInfo.cfg";
        xmlDoc.Load(filename);
        XmlNode xmlNode = xmlDoc.SelectSingleNode("DocumentRoot");
        XmlNode xmlNode2 = xmlNode.SelectSingleNode("ServerStatus");
        return xmlNode2.InnerText;
    }

    public void WriteHistoryDataXml(string strResult)
    {
        string filename = CEditEnvironmentGlobal.HMIPath + "\\DBCrossInfo.cfg";
        xmlDoc.Load(filename);
        XmlNode xmlNode = xmlDoc.SelectSingleNode("DocumentRoot");
        XmlNode xmlNode2 = xmlNode.SelectSingleNode("DBCrossInfo");
        xmlNode2.InnerText = strResult;
        xmlDoc.Save(filename);
    }

    public void InsertHistoryDataXml(CDBVarDefine dbVar)
    {
        string filename = CEditEnvironmentGlobal.HMIPath + "\\HistoryDataXML.cfg";
        xmlDoc.Load(filename);
        XmlNode xmlNode = xmlDoc.SelectSingleNode("DocumentRoot");
        XmlElement xmlElement = xmlDoc.CreateElement("Table");
        xmlElement.SetAttribute("tableName", dbVar.strTableName);
        xmlNode.AppendChild(xmlElement);
        XmlElement xmlElement2 = xmlDoc.CreateElement("SaveTime");
        xmlElement2.InnerText = dbVar.strSaveTime;
        xmlElement.AppendChild(xmlElement2);
        XmlElement xmlElement3 = xmlDoc.CreateElement("SQLConnection");
        xmlElement3.InnerText = dbVar.strSQLConnection;
        xmlElement.AppendChild(xmlElement3);
        foreach (structTableInfo item in dbVar.ltTableInfo)
        {
            XmlElement xmlElement4 = xmlDoc.CreateElement("Row");
            xmlElement4.SetAttribute("ColumnName", item.strColumnName);
            xmlElement4.SetAttribute("DataType", item.strDataType);
            bool bMainKey = item.bMainKey;
            xmlElement4.SetAttribute("MainKey", bMainKey.ToString());
            bool bNullValue = item.bNullValue;
            xmlElement4.SetAttribute("NullValue", bNullValue.ToString());
            bool bAutoAdd = item.bAutoAdd;
            xmlElement4.SetAttribute("AutoAdd", bAutoAdd.ToString());
            xmlElement.AppendChild(xmlElement4);
        }
        xmlDoc.Save(filename);
    }
}
