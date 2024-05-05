using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;

namespace HMIEditEnvironment;

public class DBConnConfigForm : Form
{
    private string strPath;

    public string DbType = "";

    public string DbConnStr = "";

    private readonly MySQLConfigForm mySQLCF = new();

    private readonly SQLServerConfigForm sqlServerCF = new();

    private Label label1;

    private ComboBox cbDBType;

    private GroupBox groupBox;

    private Panel panel;

    private Button btSure;

    private Button btCancel;

    private Button btTest;

    private Label label2;

    public DBConnConfigForm()
    {
        InitializeComponent();
    }

    private void DBConnConfigForm_Load(object sender, EventArgs e)
    {
        cbDBType.SelectedIndex = 0;
        sqlServerCF.FormBorderStyle = FormBorderStyle.None;
        sqlServerCF.Dock = DockStyle.Fill;
        sqlServerCF.TopLevel = false;
        panel.Controls.Clear();
        panel.Controls.Add(sqlServerCF);
        sqlServerCF.Show();
        strPath = CEditEnvironmentGlobal.path + "\\DBConnectionInfo.cfg";
    }

    private void cbDBType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbDBType.SelectedItem.ToString() == "SQL Server")
        {
            sqlServerCF.FormBorderStyle = FormBorderStyle.None;
            sqlServerCF.Dock = DockStyle.Fill;
            sqlServerCF.TopLevel = false;
            panel.Controls.Clear();
            panel.Controls.Add(sqlServerCF);
            sqlServerCF.Show();
        }
        else if (cbDBType.SelectedItem.ToString() == "MySQL")
        {
            mySQLCF.FormBorderStyle = FormBorderStyle.None;
            mySQLCF.Dock = DockStyle.Fill;
            mySQLCF.TopLevel = false;
            panel.Controls.Clear();
            panel.Controls.Add(mySQLCF);
            mySQLCF.Show();
        }
    }

    private bool SQLServerConnStr()
    {
        XmlDocument xmlDocument = new();
        xmlDocument.Load(strPath);
        XmlNode xmlNode = xmlDocument.SelectSingleNode("DocumentRoot");
        XmlNode xmlNode2 = xmlNode.SelectSingleNode("SQL_Server");
        string text = xmlNode2.InnerText;
        xmlNode2 = xmlNode.SelectSingleNode("SQL_DataBase");
        string text2 = xmlNode2.InnerText;
        xmlNode2 = xmlNode.SelectSingleNode("SQL_LoginType");
        string text3 = xmlNode2.InnerText;
        xmlNode2 = xmlNode.SelectSingleNode("SQL_User");
        string text4 = xmlNode2.InnerText;
        xmlNode2 = xmlNode.SelectSingleNode("SQL_Password");
        string text5 = xmlNode2.InnerText;
        if ("" == text)
        {
            MessageBox.Show("数据源输入为空，请重新输入！", "提示");
            return false;
        }
        if ("" == text2)
        {
            MessageBox.Show("数据库输入为空，请重新输入！", "提示");
            return false;
        }
        if (text3 == "Windows登录")
        {
            DbType = "SqlClient Data Provider";
            DbConnStr = "Server=" + text + ";Initial Catalog=" + text2 + ";Integrated Security=True";
        }
        else if (text3 == "用户名登录")
        {
            if ("" == text4)
            {
                MessageBox.Show("用户名输入为空，请重新输入！", "提示");
                return false;
            }
            if ("" == text5)
            {
                MessageBox.Show("密码输入为空，请重新输入！", "提示");
                return false;
            }
            DbType = "SqlClient Data Provider";
            DbConnStr = "Server=" + text + ";user id=" + text4 + ";password=" + text5 + ";initial catalog=" + text2;
        }
        return true;
    }

    private bool MySQLConnStr()
    {
        XmlDocument xmlDocument = new();
        xmlDocument.Load(strPath);
        XmlNode xmlNode = xmlDocument.SelectSingleNode("DocumentRoot");
        XmlNode xmlNode2 = xmlNode.SelectSingleNode("MySQL_Server");
        string text = xmlNode2.InnerText;
        xmlNode2 = xmlNode.SelectSingleNode("MySQL_User");
        string text2 = xmlNode2.InnerText;
        xmlNode2 = xmlNode.SelectSingleNode("MySQL_DataBase");
        string text3 = xmlNode2.InnerText;
        xmlNode2 = xmlNode.SelectSingleNode("MySQL_Password");
        string text4 = xmlNode2.InnerText;
        if ("" == text)
        {
            MessageBox.Show("数据源输入为空，请重新输入！", "提示");
            return false;
        }
        if ("" == text2)
        {
            MessageBox.Show("用户名输入为空，请重新输入！", "提示");
            return false;
        }
        if ("" == text3)
        {
            MessageBox.Show("数据库输入为空，请重新输入！", "提示");
            return false;
        }
        if ("" == text4)
        {
            MessageBox.Show("密码输入为空，请重新输入！", "提示");
            return false;
        }
        DbType = "MySQL Data Provider";
        DbConnStr = "Server=" + text + ";user id=" + text2 + ";password=" + text4 + ";database=" + text3;
        return true;
    }

    private void btSure_Click(object sender, EventArgs e)
    {
        if ((!(cbDBType.SelectedItem.ToString() == "SQL Server") || SQLServerConnStr()) && (!(cbDBType.SelectedItem.ToString() == "MySQL") || MySQLConnStr()))
        {
            base.DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void JudgeSQLConnection()
    {
        try
        {
            if (!SQLServerConnStr())
            {
                return;
            }
            using SqlConnection sqlConnection = new();
            sqlConnection.ConnectionString = DbConnStr;
            sqlConnection.Open();
            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("测试结果:数据源连接成功.");
            }
            else if (sqlConnection.State == ConnectionState.Closed)
            {
                MessageBox.Show("测试结果:数据源连接失败.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("测试结果:数据源连接失败." + Environment.NewLine + ex.Message);
        }
    }

    private void JudgeMySQLConnection()
    {
        try
        {
            if (!MySQLConnStr())
            {
                return;
            }
            using MySqlConnection mySqlConnection = new();
            mySqlConnection.ConnectionString = DbConnStr;
            mySqlConnection.Open();
            if (mySqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("测试结果:数据源连接成功.");
            }
            else if (mySqlConnection.State == ConnectionState.Closed)
            {
                MessageBox.Show("测试结果:数据源连接失败.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("测试结果:数据源连接失败." + Environment.NewLine + ex.Message);
        }
    }

    private void btTest_Click(object sender, EventArgs e)
    {
        if (cbDBType.Text == "MySQL")
        {
            JudgeMySQLConnection();
        }
        else if (cbDBType.Text == "SQL Server")
        {
            JudgeSQLConnection();
        }
    }

    private void btCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void InitializeComponent()
    {
        label1 = new System.Windows.Forms.Label();
        cbDBType = new System.Windows.Forms.ComboBox();
        groupBox = new System.Windows.Forms.GroupBox();
        panel = new System.Windows.Forms.Panel();
        btSure = new System.Windows.Forms.Button();
        btCancel = new System.Windows.Forms.Button();
        btTest = new System.Windows.Forms.Button();
        label2 = new System.Windows.Forms.Label();
        groupBox.SuspendLayout();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(15, 16);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 0;
        label1.Text = "数据库类型";
        cbDBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cbDBType.Items.AddRange(new object[2] { "SQL Server", "MySQL" });
        cbDBType.Location = new System.Drawing.Point(86, 12);
        cbDBType.Name = "cbDBType";
        cbDBType.Size = new System.Drawing.Size(120, 20);
        cbDBType.TabIndex = 1;
        cbDBType.SelectedIndexChanged += new System.EventHandler(cbDBType_SelectedIndexChanged);
        groupBox.Controls.Add(panel);
        groupBox.Location = new System.Drawing.Point(13, 42);
        groupBox.Name = "groupBox";
        groupBox.Size = new System.Drawing.Size(398, 125);
        groupBox.TabIndex = 2;
        groupBox.TabStop = false;
        groupBox.Text = "配置";
        panel.Dock = System.Windows.Forms.DockStyle.Fill;
        panel.Location = new System.Drawing.Point(3, 17);
        panel.Name = "panel";
        panel.Size = new System.Drawing.Size(392, 105);
        panel.TabIndex = 0;
        btSure.Location = new System.Drawing.Point(244, 198);
        btSure.Name = "btSure";
        btSure.Size = new System.Drawing.Size(75, 23);
        btSure.TabIndex = 3;
        btSure.Text = "确 定";
        btSure.UseVisualStyleBackColor = true;
        btSure.Click += new System.EventHandler(btSure_Click);
        btCancel.Location = new System.Drawing.Point(325, 198);
        btCancel.Name = "btCancel";
        btCancel.Size = new System.Drawing.Size(75, 23);
        btCancel.TabIndex = 4;
        btCancel.Text = "取 消";
        btCancel.UseVisualStyleBackColor = true;
        btCancel.Click += new System.EventHandler(btCancel_Click);
        btTest.Location = new System.Drawing.Point(212, 11);
        btTest.Name = "btTest";
        btTest.Size = new System.Drawing.Size(75, 23);
        btTest.TabIndex = 5;
        btTest.Text = "测 试";
        btTest.UseVisualStyleBackColor = true;
        btTest.Click += new System.EventHandler(btTest_Click);
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(22, 174);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(263, 12);
        label2.TabIndex = 6;
        label2.Text = "注:跨平台数据库连接只能使用用户名登陆方式！";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(425, 233);
        base.Controls.Add(label2);
        base.Controls.Add(btTest);
        base.Controls.Add(btCancel);
        base.Controls.Add(btSure);
        base.Controls.Add(groupBox);
        base.Controls.Add(cbDBType);
        base.Controls.Add(label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "DBConnConfigForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "数据库管理";
        base.Load += new System.EventHandler(DBConnConfigForm_Load);
        groupBox.ResumeLayout(false);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
