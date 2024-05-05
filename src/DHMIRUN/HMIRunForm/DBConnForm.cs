using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace HMIRunForm;

public class DBConnForm : Form
{
    private string dbType = "";

    private string dbConnStr = "";

    private ComboBox comboBox1;

    private Label label1;

    private PropertyGrid propertyGrid1;

    private Button button1;

    private Button button2;

    public string DbType
    {
        get
        {
            return dbType;
        }
        set
        {
            comboBox1.Text = value;
        }
    }

    public string DbConnStr
    {
        get
        {
            return dbConnStr;
        }
        set
        {
            try
            {
                ((ConnBuilder)comboBox1.SelectedItem).Builder.ConnectionString = value;
            }
            catch
            {
            }
        }
    }

    public DBConnForm()
    {
        InitializeComponent();
        DataTable factoryClasses = DbProviderFactories.GetFactoryClasses();
        foreach (DataRow row in factoryClasses.Rows)
        {
            comboBox1.Items.Add(new ConnBuilder(row));
        }
    }

    private void DBConnFrom_Load(object sender, EventArgs e)
    {
        if (comboBox1.SelectedIndex == -1)
        {
            comboBox1.SelectedIndex = 0;
        }
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        propertyGrid1.SelectedObject = ((ConnBuilder)comboBox1.SelectedItem).Builder;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        DbConnection conn = ((ConnBuilder)comboBox1.SelectedItem).Conn;
        conn.ConnectionString = ((ConnBuilder)comboBox1.SelectedItem).Builder.ConnectionString;
        try
        {
            conn.Open();
            MessageBox.Show("测试结果:数据源连接成功.");
        }
        catch (Exception ex)
        {
            MessageBox.Show("测试结果:数据源连接失败." + Environment.NewLine + ex.Message);
        }
        finally
        {
            try
            {
                conn.Close();
            }
            catch (Exception)
            {
            }
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        dbType = comboBox1.SelectedItem.ToString();
        dbConnStr = ((ConnBuilder)comboBox1.SelectedItem).Builder.ConnectionString;
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void InitializeComponent()
    {
        comboBox1 = new System.Windows.Forms.ComboBox();
        label1 = new System.Windows.Forms.Label();
        propertyGrid1 = new System.Windows.Forms.PropertyGrid();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new System.Drawing.Point(83, 11);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(271, 20);
        comboBox1.TabIndex = 0;
        comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(12, 14);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 1;
        label1.Text = "数据源类型";
        propertyGrid1.Location = new System.Drawing.Point(12, 45);
        propertyGrid1.Name = "propertyGrid1";
        propertyGrid1.Size = new System.Drawing.Size(504, 428);
        propertyGrid1.TabIndex = 2;
        button1.Location = new System.Drawing.Point(360, 9);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 3;
        button1.Text = "测试";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.Location = new System.Drawing.Point(441, 9);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 3;
        button2.Text = "保存";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(528, 485);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.Controls.Add(propertyGrid1);
        base.Controls.Add(label1);
        base.Controls.Add(comboBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "DBConnFrom";
        Text = "数据库管理";
        base.Load += new System.EventHandler(DBConnFrom_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
