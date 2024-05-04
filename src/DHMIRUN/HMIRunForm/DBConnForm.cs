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
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.label1 = new System.Windows.Forms.Label();
        this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Location = new System.Drawing.Point(83, 11);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new System.Drawing.Size(271, 20);
        this.comboBox1.TabIndex = 0;
        this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(12, 14);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 1;
        this.label1.Text = "数据源类型";
        this.propertyGrid1.Location = new System.Drawing.Point(12, 45);
        this.propertyGrid1.Name = "propertyGrid1";
        this.propertyGrid1.Size = new System.Drawing.Size(504, 428);
        this.propertyGrid1.TabIndex = 2;
        this.button1.Location = new System.Drawing.Point(360, 9);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 3;
        this.button1.Text = "测试";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.Location = new System.Drawing.Point(441, 9);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 3;
        this.button2.Text = "保存";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(528, 485);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.propertyGrid1);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.comboBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "DBConnFrom";
        this.Text = "数据库管理";
        base.Load += new System.EventHandler(DBConnFrom_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
