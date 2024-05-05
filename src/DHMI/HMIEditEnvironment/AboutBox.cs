using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HMIEditEnvironment;

internal class AboutBox : XtraForm
{
    private readonly List<string> ansStrs = new() { "console" };

    private readonly List<string> nowStrs = new() { "" };

    private TableLayoutPanel tableLayoutPanel;

    private Label labelProductName;

    private Label labelVersion;

    private Label labelCopyright;

    private Label labelCompanyName;

    private TextBox textBoxDescription;

    private Button okButton;

    public string AssemblyTitle
    {
        get
        {
            object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), inherit: false);
            if (customAttributes.Length > 0)
            {
                AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)customAttributes[0];
                if (assemblyTitleAttribute.Title != "")
                {
                    return assemblyTitleAttribute.Title;
                }
            }
            return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
        }
    }

    public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

    public string AssemblyDescription
    {
        get
        {
            object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), inherit: false);
            if (customAttributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
        }
    }

    public string AssemblyProduct
    {
        get
        {
            object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), inherit: false);
            if (customAttributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyProductAttribute)customAttributes[0]).Product;
        }
    }

    public string AssemblyCopyright
    {
        get
        {
            object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), inherit: false);
            if (customAttributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
        }
    }

    public string AssemblyCompany
    {
        get
        {
            object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), inherit: false);
            if (customAttributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyCompanyAttribute)customAttributes[0]).Company;
        }
    }

    [DllImport("kernel32.dll")]
    public static extern bool AllocConsole();

    public AboutBox()
    {
        InitializeComponent();
        Text = $"关于 {AssemblyTitle} ";
        labelProductName.Text = AssemblyProduct;
        labelVersion.Text = $"版本 {AssemblyVersion} ";
        labelCopyright.Text = AssemblyCopyright;
        labelCompanyName.Text = AssemblyCompany;
        textBoxDescription.Text = AssemblyDescription;
    }

    private void AboutBox_Load(object sender, EventArgs e)
    {
    }

    private void AboutBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        for (int i = 0; i < ansStrs.Count; i++)
        {
            if (e.KeyChar == ansStrs[i][nowStrs[i].Length])
            {
                nowStrs[i] += e.KeyChar;
            }
            else
            {
                nowStrs[i] = e.KeyChar.ToString();
            }
            if (nowStrs[i] == ansStrs[i] && ansStrs[i] == "console")
            {
                AllocConsole();
                Console.WriteLine("开启控制台。");
                nowStrs[i] = "";
            }
        }
    }

    private void InitializeComponent()
    {
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            labelProductName = new System.Windows.Forms.Label();
            labelVersion = new System.Windows.Forms.Label();
            labelCopyright = new System.Windows.Forms.Label();
            labelCompanyName = new System.Windows.Forms.Label();
            textBoxDescription = new System.Windows.Forms.TextBox();
            okButton = new System.Windows.Forms.Button();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel.Controls.Add(labelProductName, 0, 0);
            tableLayoutPanel.Controls.Add(labelVersion, 0, 1);
            tableLayoutPanel.Controls.Add(labelCopyright, 0, 2);
            tableLayoutPanel.Controls.Add(labelCompanyName, 0, 3);
            tableLayoutPanel.Controls.Add(textBoxDescription, 0, 4);
            tableLayoutPanel.Controls.Add(okButton, 0, 5);
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location = new System.Drawing.Point(10, 9);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.Size = new System.Drawing.Size(487, 286);
            tableLayoutPanel.TabIndex = 0;
            // 
            // labelProductName
            // 
            labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            labelProductName.Location = new System.Drawing.Point(7, 0);
            labelProductName.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
            labelProductName.MaximumSize = new System.Drawing.Size(0, 19);
            labelProductName.Name = "labelProductName";
            labelProductName.Size = new System.Drawing.Size(477, 19);
            labelProductName.TabIndex = 19;
            labelProductName.Text = "产品名称";
            labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            labelVersion.Location = new System.Drawing.Point(7, 28);
            labelVersion.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
            labelVersion.MaximumSize = new System.Drawing.Size(0, 19);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new System.Drawing.Size(477, 19);
            labelVersion.TabIndex = 0;
            labelVersion.Text = "版本";
            labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            labelCopyright.Location = new System.Drawing.Point(7, 56);
            labelCopyright.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
            labelCopyright.MaximumSize = new System.Drawing.Size(0, 19);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new System.Drawing.Size(477, 19);
            labelCopyright.TabIndex = 21;
            labelCopyright.Text = "版权";
            labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCompanyName
            // 
            labelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            labelCompanyName.Location = new System.Drawing.Point(7, 84);
            labelCompanyName.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
            labelCompanyName.MaximumSize = new System.Drawing.Size(0, 19);
            labelCompanyName.Name = "labelCompanyName";
            labelCompanyName.Size = new System.Drawing.Size(477, 19);
            labelCompanyName.TabIndex = 22;
            labelCompanyName.Text = "公司名称";
            labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDescription
            // 
            textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxDescription.Location = new System.Drawing.Point(7, 115);
            textBoxDescription.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.ReadOnly = true;
            textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBoxDescription.Size = new System.Drawing.Size(477, 137);
            textBoxDescription.TabIndex = 23;
            textBoxDescription.TabStop = false;
            textBoxDescription.Text = "说明";
            // 
            // okButton
            // 
            okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            okButton.Location = new System.Drawing.Point(397, 259);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(87, 24);
            okButton.TabIndex = 24;
            okButton.Text = "确定";
            // 
            // AboutBox
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(507, 304);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            IconOptions.ShowIcon = false;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutBox";
            Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "AboutBox";
            Load += new System.EventHandler(AboutBox_Load);
            KeyPress += new System.Windows.Forms.KeyPressEventHandler(AboutBox_KeyPress);
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);

    }
}
