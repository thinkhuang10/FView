using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace HMIEditEnvironment;

public class SQLServerConfigForm : Form
{
    private string strPath;

    private XmlNode pXmlNode;

    private readonly XmlDocument xmlDoc = new();

    private TextBox tbPassword;

    private Label label6;

    private TextBox tbUser;

    private Label label10;

    private ComboBox cbLoginType;

    private Label label4;

    private ComboBox cbDataBase;

    private ComboBox cbDataSource;

    private Label label8;

    private Label label9;

    public SQLServerConfigForm()
    {
        InitializeComponent();
    }

    private void SQLServerConfigForm_Load(object sender, EventArgs e)
    {
        strPath = CEditEnvironmentGlobal.path + "\\DBConnectionInfo.cfg";
        if (!File.Exists(strPath))
        {
            XmlElement xmlElement = xmlDoc.CreateElement("DocumentRoot");
            xmlDoc.AppendChild(xmlElement);
            XmlElement xmlElement2 = xmlDoc.CreateElement("SQL_LoginType");
            xmlElement2.InnerText = "Windows登录";
            xmlElement.AppendChild(xmlElement2);
            XmlElement xmlElement3 = xmlDoc.CreateElement("SQL_Server");
            xmlElement3.InnerText = "localhost";
            xmlElement.AppendChild(xmlElement3);
            XmlElement xmlElement4 = xmlDoc.CreateElement("SQL_DataBase");
            xmlElement4.InnerText = "DCCE";
            xmlElement.AppendChild(xmlElement4);
            XmlElement xmlElement5 = xmlDoc.CreateElement("SQL_User");
            xmlElement5.InnerText = "sa";
            xmlElement.AppendChild(xmlElement5);
            XmlElement xmlElement6 = xmlDoc.CreateElement("SQL_Password");
            xmlElement6.InnerText = "123";
            xmlElement.AppendChild(xmlElement6);
            XmlElement xmlElement7 = xmlDoc.CreateElement("MySQL_Server");
            xmlElement7.InnerText = "localhost";
            xmlElement.AppendChild(xmlElement7);
            XmlElement xmlElement8 = xmlDoc.CreateElement("MySQL_DataBase");
            xmlElement8.InnerText = "DCCE";
            xmlElement.AppendChild(xmlElement8);
            XmlElement xmlElement9 = xmlDoc.CreateElement("MySQL_User");
            xmlElement9.InnerText = "root";
            xmlElement.AppendChild(xmlElement9);
            XmlElement xmlElement10 = xmlDoc.CreateElement("MySQL_Password");
            xmlElement10.InnerText = "123";
            xmlElement.AppendChild(xmlElement10);
            xmlDoc.Save(strPath);
        }
        xmlDoc.Load(strPath);
        pXmlNode = xmlDoc.SelectSingleNode("DocumentRoot");
        XmlNode xmlNode = pXmlNode.SelectSingleNode("SQL_LoginType");
        if (xmlNode.InnerText == "Windows登录")
        {
            cbLoginType.SelectedIndex = 0;
        }
        else if (xmlNode.InnerText == "用户名登录")
        {
            cbLoginType.SelectedIndex = 1;
        }
        else
        {
            cbLoginType.SelectedIndex = 0;
        }
        xmlNode = pXmlNode.SelectSingleNode("SQL_Server");
        cbDataSource.Text = xmlNode.InnerText;
        xmlNode = pXmlNode.SelectSingleNode("SQL_DataBase");
        cbDataBase.Text = xmlNode.InnerText;
        xmlNode = pXmlNode.SelectSingleNode("SQL_User");
        tbUser.Text = xmlNode.InnerText;
        xmlNode = pXmlNode.SelectSingleNode("SQL_Password");
        tbPassword.Text = xmlNode.InnerText;
    }

    private void cbLoginType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ("Windows登录" == cbLoginType.Text)
        {
            tbUser.Enabled = false;
            tbPassword.Enabled = false;
        }
        else if ("用户名登录" == cbLoginType.Text)
        {
            tbUser.Enabled = true;
            tbPassword.Enabled = true;
        }
        XmlNode xmlNode = pXmlNode.SelectSingleNode("SQL_LoginType");
        xmlNode.InnerText = cbLoginType.Text;
        xmlDoc.Save(strPath);
    }

    private void cbDataSource_DropDown(object sender, EventArgs e)
    {
        try
        {
            cbDataSource.Items.Clear();
            DataTable dataSources = SqlClientFactory.Instance.CreateDataSourceEnumerator().GetDataSources();
            DataColumn column = dataSources.Columns["InstanceName"];
            DataColumn column2 = dataSources.Columns["ServerName"];
            DataRowCollection rows = dataSources.Rows;
            string empty = string.Empty;
            for (int i = 0; i < rows.Count; i++)
            {
                string text = rows[i][column2] as string;
                empty = ((rows[i][column] is string { Length: not 0 } text2 && !("MSSQLSERVER" == text2)) ? (text + "\\" + text2) : text);
                cbDataSource.Items.Add(empty);
            }
        }
        catch
        {
            MessageBox.Show("自动捕获局域网中安装的SQL数据库出现异常！", "提示");
        }
    }

    private void cbDataSource_Leave(object sender, EventArgs e)
    {
        try
        {
            XmlNode xmlNode = pXmlNode.SelectSingleNode("SQL_Server");
            xmlNode.InnerText = cbDataSource.Text;
            xmlDoc.Save(strPath);
        }
        catch
        {
            MessageBox.Show("数据源输入出现异常！", "提示");
            cbDataSource.Text = "localhost";
        }
    }

    private void cbDataBase_DropDown(object sender, EventArgs e)
    {
        string cmdText = "select name from sys.databases where database_id > 6";
        SqlConnection sqlConnection = ((!(cbLoginType.SelectedItem.ToString() == "Windows登录")) ? new SqlConnection("Server=" + cbDataSource.Text.Trim() + ";user id=" + tbUser.Text.Trim() + ";password=" + tbPassword.Text.Trim()) : new SqlConnection("Server=" + cbDataSource.Text.Trim() + ";Integrated Security=True"));
        try
        {
            SqlCommand sqlCommand = new(cmdText, sqlConnection);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
                IDataReader dataReader = sqlCommand.ExecuteReader();
                cbDataBase.Items.Clear();
                while (dataReader.Read())
                {
                    cbDataBase.Items.Add(dataReader["name"].ToString());
                }
                dataReader.Close();
            }
        }
        catch
        {
            MessageBox.Show("自动捕获数据服务器中的数据库出现异常！", "提示");
        }
        finally
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Dispose();
            }
        }
    }

    private void cbDataBase_Leave(object sender, EventArgs e)
    {
        try
        {
            if (!Regex.IsMatch(cbDataBase.Text.Trim(), "^(?([0-9\\- :!?])(?!)|([^ :]*))$"))
            {
                MessageBox.Show("数据库输入错误，请重新输入！", "提示");
                cbDataBase.Text = "";
            }
            else
            {
                XmlNode xmlNode = pXmlNode.SelectSingleNode("SQL_DataBase");
                xmlNode.InnerText = cbDataBase.Text;
                xmlDoc.Save(strPath);
            }
        }
        catch
        {
            MessageBox.Show("数据库输入出现异常！", "提示");
            cbDataBase.Text = "";
        }
    }

    private void tbUser_Leave(object sender, EventArgs e)
    {
        try
        {
            if (!Regex.IsMatch(tbUser.Text.Trim(), "^(?([0-9\\- :!?])(?!)|([^ :]*))$"))
            {
                MessageBox.Show("用户名输入错误，请重新输入！", "提示");
                tbUser.Text = "sa";
            }
            else
            {
                XmlNode xmlNode = pXmlNode.SelectSingleNode("SQL_User");
                xmlNode.InnerText = tbUser.Text;
                xmlDoc.Save(strPath);
            }
        }
        catch
        {
            MessageBox.Show("用户名输入出现异常！", "提示");
            tbUser.Text = "sa";
        }
    }

    private void tbPassword_Leave(object sender, EventArgs e)
    {
        try
        {
            XmlNode xmlNode = pXmlNode.SelectSingleNode("SQL_Password");
            xmlNode.InnerText = tbPassword.Text;
            xmlDoc.Save(strPath);
        }
        catch
        {
            MessageBox.Show("密码输入出现异常！", "提示");
            tbPassword.Text = "";
        }
    }

    private void InitializeComponent()
    {
        this.tbPassword = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.tbUser = new System.Windows.Forms.TextBox();
        this.label10 = new System.Windows.Forms.Label();
        this.cbLoginType = new System.Windows.Forms.ComboBox();
        this.label4 = new System.Windows.Forms.Label();
        this.cbDataBase = new System.Windows.Forms.ComboBox();
        this.cbDataSource = new System.Windows.Forms.ComboBox();
        this.label8 = new System.Windows.Forms.Label();
        this.label9 = new System.Windows.Forms.Label();
        base.SuspendLayout();
        this.tbPassword.Location = new System.Drawing.Point(268, 47);
        this.tbPassword.Name = "tbPassword";
        this.tbPassword.PasswordChar = '*';
        this.tbPassword.Size = new System.Drawing.Size(120, 21);
        this.tbPassword.TabIndex = 33;
        this.tbPassword.Text = "123456";
        this.tbPassword.Leave += new System.EventHandler(tbPassword_Leave);
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(221, 51);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(41, 12);
        this.label6.TabIndex = 32;
        this.label6.Text = "密  码";
        this.tbUser.Location = new System.Drawing.Point(268, 15);
        this.tbUser.Name = "tbUser";
        this.tbUser.Size = new System.Drawing.Size(120, 21);
        this.tbUser.TabIndex = 31;
        this.tbUser.Text = "sa";
        this.tbUser.Leave += new System.EventHandler(tbUser_Leave);
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(221, 19);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(41, 12);
        this.label10.TabIndex = 30;
        this.label10.Text = "用户名";
        this.cbLoginType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cbLoginType.FormattingEnabled = true;
        this.cbLoginType.Items.AddRange(new object[2] { "Windows登录", "用户名登录" });
        this.cbLoginType.Location = new System.Drawing.Point(77, 15);
        this.cbLoginType.Name = "cbLoginType";
        this.cbLoginType.Size = new System.Drawing.Size(120, 20);
        this.cbLoginType.TabIndex = 29;
        this.cbLoginType.SelectedIndexChanged += new System.EventHandler(cbLoginType_SelectedIndexChanged);
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(18, 19);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(53, 12);
        this.label4.TabIndex = 28;
        this.label4.Text = "登录方式";
        this.cbDataBase.FormattingEnabled = true;
        this.cbDataBase.Location = new System.Drawing.Point(77, 79);
        this.cbDataBase.Name = "cbDataBase";
        this.cbDataBase.Size = new System.Drawing.Size(120, 20);
        this.cbDataBase.TabIndex = 27;
        this.cbDataBase.DropDown += new System.EventHandler(cbDataBase_DropDown);
        this.cbDataBase.Leave += new System.EventHandler(cbDataBase_Leave);
        this.cbDataSource.FormattingEnabled = true;
        this.cbDataSource.Location = new System.Drawing.Point(77, 47);
        this.cbDataSource.Name = "cbDataSource";
        this.cbDataSource.Size = new System.Drawing.Size(120, 20);
        this.cbDataSource.TabIndex = 26;
        this.cbDataSource.Text = "localhost";
        this.cbDataSource.DropDown += new System.EventHandler(cbDataSource_DropDown);
        this.cbDataSource.Leave += new System.EventHandler(cbDataSource_Leave);
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(23, 82);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(41, 12);
        this.label8.TabIndex = 25;
        this.label8.Text = "数据库";
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(23, 50);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(41, 12);
        this.label9.TabIndex = 34;
        this.label9.Text = "数据源";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(406, 117);
        base.Controls.Add(this.label9);
        base.Controls.Add(this.tbPassword);
        base.Controls.Add(this.label6);
        base.Controls.Add(this.tbUser);
        base.Controls.Add(this.label10);
        base.Controls.Add(this.cbLoginType);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.cbDataBase);
        base.Controls.Add(this.cbDataSource);
        base.Controls.Add(this.label8);
        base.Name = "SQLServerConfigForm";
        this.Text = "SQLServerConfigForm";
        base.Load += new System.EventHandler(SQLServerConfigForm_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
