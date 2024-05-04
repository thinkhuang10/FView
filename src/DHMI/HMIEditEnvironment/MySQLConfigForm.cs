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
        this.label9 = new System.Windows.Forms.Label();
        this.tbPassword = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.tbUser = new System.Windows.Forms.TextBox();
        this.label10 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.tbDataSource = new System.Windows.Forms.TextBox();
        this.tbDataBase = new System.Windows.Forms.TextBox();
        base.SuspendLayout();
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(12, 17);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(41, 12);
        this.label9.TabIndex = 43;
        this.label9.Text = "数据源";
        this.tbPassword.Location = new System.Drawing.Point(256, 46);
        this.tbPassword.Name = "tbPassword";
        this.tbPassword.PasswordChar = '*';
        this.tbPassword.Size = new System.Drawing.Size(120, 21);
        this.tbPassword.TabIndex = 42;
        this.tbPassword.Leave += new System.EventHandler(tbPassword_Leave);
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(209, 50);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(41, 12);
        this.label6.TabIndex = 41;
        this.label6.Text = "密  码";
        this.tbUser.Location = new System.Drawing.Point(66, 46);
        this.tbUser.Name = "tbUser";
        this.tbUser.Size = new System.Drawing.Size(120, 21);
        this.tbUser.TabIndex = 40;
        this.tbUser.Text = "root";
        this.tbUser.Leave += new System.EventHandler(tbUser_Leave);
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(12, 49);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(41, 12);
        this.label10.TabIndex = 39;
        this.label10.Text = "用户名";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(209, 17);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(41, 12);
        this.label1.TabIndex = 44;
        this.label1.Text = "数据库";
        this.tbDataSource.Location = new System.Drawing.Point(66, 14);
        this.tbDataSource.Name = "tbDataSource";
        this.tbDataSource.Size = new System.Drawing.Size(120, 21);
        this.tbDataSource.TabIndex = 45;
        this.tbDataSource.Text = "localhost";
        this.tbDataSource.Leave += new System.EventHandler(tbDataSource_Leave);
        this.tbDataBase.Location = new System.Drawing.Point(256, 14);
        this.tbDataBase.Name = "tbDataBase";
        this.tbDataBase.Size = new System.Drawing.Size(120, 21);
        this.tbDataBase.TabIndex = 46;
        this.tbDataBase.Leave += new System.EventHandler(tbDataBase_Leave);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(390, 84);
        base.Controls.Add(this.tbDataBase);
        base.Controls.Add(this.tbDataSource);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.label9);
        base.Controls.Add(this.tbPassword);
        base.Controls.Add(this.label6);
        base.Controls.Add(this.tbUser);
        base.Controls.Add(this.label10);
        base.Name = "MySQLConfigForm";
        this.Text = "MySQLConfigForm";
        base.Load += new System.EventHandler(MySQLConfigForm_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
