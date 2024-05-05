using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace HMIEditEnvironment;

public class MySQLConfigForm : Form
{
    private string strPath;

    private XmlNode pXmlNode;

    private readonly XmlDocument xmlDoc = new();

    private Label label9;

    private TextBox tbPassword;

    private Label label6;

    private TextBox tbUser;

    private Label label10;

    private Label label1;

    private TextBox tbDataSource;

    private TextBox tbDataBase;

    public MySQLConfigForm()
    {
        InitializeComponent();
    }

    private void MySQLConfigForm_Load(object sender, EventArgs e)
    {
        strPath = CEditEnvironmentGlobal.path + "\\DBConnectionInfo.cfg";
        xmlDoc.Load(strPath);
        pXmlNode = xmlDoc.SelectSingleNode("DocumentRoot");
        XmlNode xmlNode = pXmlNode.SelectSingleNode("MySQL_Server");
        tbDataSource.Text = xmlNode.InnerText;
        xmlNode = pXmlNode.SelectSingleNode("MySQL_DataBase");
        tbDataBase.Text = xmlNode.InnerText;
        xmlNode = pXmlNode.SelectSingleNode("MySQL_User");
        tbUser.Text = xmlNode.InnerText;
        xmlNode = pXmlNode.SelectSingleNode("MySQL_Password");
        tbPassword.Text = xmlNode.InnerText;
    }

    private void tbDataSource_Leave(object sender, EventArgs e)
    {
        try
        {
            XmlNode xmlNode = pXmlNode.SelectSingleNode("MySQL_Server");
            xmlNode.InnerText = tbDataSource.Text;
            xmlDoc.Save(strPath);
        }
        catch
        {
            MessageBox.Show("数据源输入出现异常！", "提示");
            tbDataSource.Text = "localhost";
        }
    }

    private void tbDataBase_Leave(object sender, EventArgs e)
    {
        try
        {
            if (!Regex.IsMatch(tbDataBase.Text.Trim(), "^(?([0-9\\- :!?])(?!)|([^ :]*))$"))
            {
                MessageBox.Show("数据库输入错误，请重新输入！", "提示");
                tbDataBase.Text = "";
            }
            else
            {
                XmlNode xmlNode = pXmlNode.SelectSingleNode("MySQL_DataBase");
                xmlNode.InnerText = tbDataBase.Text;
                xmlDoc.Save(strPath);
            }
        }
        catch
        {
            MessageBox.Show("数据库输入出现异常！", "提示");
            tbDataBase.Text = "";
        }
    }

    private void tbUser_Leave(object sender, EventArgs e)
    {
        try
        {
            if (!Regex.IsMatch(tbUser.Text.Trim(), "^(?([0-9\\- :!?])(?!)|([^ :]*))$"))
            {
                MessageBox.Show("用户名输入错误，请重新输入！", "提示");
                tbUser.Text = "root";
            }
            else
            {
                XmlNode xmlNode = pXmlNode.SelectSingleNode("MySQL_User");
                xmlNode.InnerText = tbUser.Text;
                xmlDoc.Save(strPath);
            }
        }
        catch
        {
            MessageBox.Show("用户名输入出现异常！", "提示");
            tbUser.Text = "root";
        }
    }

    private void tbPassword_Leave(object sender, EventArgs e)
    {
        try
        {
            XmlNode xmlNode = pXmlNode.SelectSingleNode("MySQL_Password");
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
        label9 = new System.Windows.Forms.Label();
        tbPassword = new System.Windows.Forms.TextBox();
        label6 = new System.Windows.Forms.Label();
        tbUser = new System.Windows.Forms.TextBox();
        label10 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        tbDataSource = new System.Windows.Forms.TextBox();
        tbDataBase = new System.Windows.Forms.TextBox();
        base.SuspendLayout();
        label9.AutoSize = true;
        label9.Location = new System.Drawing.Point(12, 17);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(41, 12);
        label9.TabIndex = 43;
        label9.Text = "数据源";
        tbPassword.Location = new System.Drawing.Point(256, 46);
        tbPassword.Name = "tbPassword";
        tbPassword.PasswordChar = '*';
        tbPassword.Size = new System.Drawing.Size(120, 21);
        tbPassword.TabIndex = 42;
        tbPassword.Leave += new System.EventHandler(tbPassword_Leave);
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(209, 50);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(41, 12);
        label6.TabIndex = 41;
        label6.Text = "密  码";
        tbUser.Location = new System.Drawing.Point(66, 46);
        tbUser.Name = "tbUser";
        tbUser.Size = new System.Drawing.Size(120, 21);
        tbUser.TabIndex = 40;
        tbUser.Text = "root";
        tbUser.Leave += new System.EventHandler(tbUser_Leave);
        label10.AutoSize = true;
        label10.Location = new System.Drawing.Point(12, 49);
        label10.Name = "label10";
        label10.Size = new System.Drawing.Size(41, 12);
        label10.TabIndex = 39;
        label10.Text = "用户名";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(209, 17);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(41, 12);
        label1.TabIndex = 44;
        label1.Text = "数据库";
        tbDataSource.Location = new System.Drawing.Point(66, 14);
        tbDataSource.Name = "tbDataSource";
        tbDataSource.Size = new System.Drawing.Size(120, 21);
        tbDataSource.TabIndex = 45;
        tbDataSource.Text = "localhost";
        tbDataSource.Leave += new System.EventHandler(tbDataSource_Leave);
        tbDataBase.Location = new System.Drawing.Point(256, 14);
        tbDataBase.Name = "tbDataBase";
        tbDataBase.Size = new System.Drawing.Size(120, 21);
        tbDataBase.TabIndex = 46;
        tbDataBase.Leave += new System.EventHandler(tbDataBase_Leave);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(390, 84);
        base.Controls.Add(tbDataBase);
        base.Controls.Add(tbDataSource);
        base.Controls.Add(label1);
        base.Controls.Add(label9);
        base.Controls.Add(tbPassword);
        base.Controls.Add(label6);
        base.Controls.Add(tbUser);
        base.Controls.Add(label10);
        base.Name = "MySQLConfigForm";
        Text = "MySQLConfigForm";
        base.Load += new System.EventHandler(MySQLConfigForm_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
