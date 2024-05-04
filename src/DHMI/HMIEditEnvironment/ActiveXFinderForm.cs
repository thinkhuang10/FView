using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ACTIVEXFINDERLib;
using DevExpress.XtraEditors;
using HMIEditEnvironment.Properties;

namespace HMIEditEnvironment;

public class ActiveXFinderForm : XtraForm, ICallBack
{
    public struct ComInfo
    {
        public string strControlName;

        public string strCLSID;

        public string strPath;

        public string strBitmapPath;

        public string strVersion;
    }

    public SearchClass _search;

    private readonly List<ComInfo> ComInfoList = new();

    private IContainer components;

    private Button LoadButton;

    private ListView listViewCom;

    private ImageList imageList;

    private ContextMenuStrip contextMenuStrip1;

    private ToolStripMenuItem 导入ToolStripMenuItem;

    [DllImport("shell32.dll")]
    public static extern uint ExtractIconEx(string lpszFile, int nIconIndex, int[] phiconLarge, int[] phiconSmall, uint nIcons);

    public ActiveXFinderForm()
    {
        InitializeComponent();

        _search = new SearchClass();
        listViewCom.Columns.Add("Icon", 50);
        listViewCom.Columns.Add("CLSID", 150);
        listViewCom.Columns.Add("Name", 150);
        listViewCom.Columns.Add("Address", 150);
        listViewCom.Columns.Add("version", 150);
        Load_Click(this, null);
    }

    private void LoadActiveX()
    {
        _search.Advise(this);
        _search.GetComponentInfo();
        _search.Unadvise();
    }

    public void Result(string s, string s1, string s2, string s3, string s4)
    {
        ComInfo item = default;
        item.strControlName = s;
        item.strCLSID = s1;
        item.strPath = s2;
        item.strBitmapPath = s3;
        item.strVersion = s4;
        ComInfoList.Add(item);
    }

    private void Load_Click(object sender, EventArgs e)
    {
        LoadButton.Enabled = false;
        LoadActiveX();
        ComInfoList.Sort(ComInfoCompare);
        FillImagelist();
        listViewCom.SmallImageList = imageList;
        listViewCom.LargeImageList = imageList;
        for (int i = 0; i < ComInfoList.Count; i++)
        {
            ListViewItem listViewItem = new("");
            listViewItem.SubItems.Add(ComInfoList[i].strCLSID);
            listViewItem.SubItems.Add(ComInfoList[i].strControlName);
            listViewItem.SubItems.Add(ComInfoList[i].strPath);
            listViewItem.SubItems.Add(ComInfoList[i].strVersion);
            listViewItem.ImageIndex = i;
            listViewCom.Items.Add(listViewItem);
        }
    }

    private void FillImagelist()
    {
        foreach (ComInfo comInfo in ComInfoList)
        {
            string text = comInfo.strBitmapPath.Trim();
            if (text != null)
            {
                string[] array = text.Split(',');
                if (array.Length == 2)
                {
                    string lpszFile = array[0].Trim();
                    int.Parse(array[1].Trim());
                    int[] phiconLarge = new int[1];
                    int[] array2 = new int[1];
                    ExtractIconEx(lpszFile, 0, phiconLarge, array2, 1u);
                    int num = array2[0];
                    IntPtr intPtr = new(num);
                    if (intPtr != IntPtr.Zero)
                    {
                        Icon icon = Icon.FromHandle(intPtr);
                        imageList.Images.Add(icon.ToBitmap());
                    }
                    else
                    {
                        imageList.Images.Add(Resources.Generic_Document);
                    }
                }
                else
                {
                    imageList.Images.Add(Resources.Generic_Document);
                }
            }
            else
            {
                imageList.Images.Add(Resources.Generic_Document);
            }
        }
    }

    public void StartProcess(string filename, string controlname, int imageindex, string clsid, string ver)
    {
        try
        {
            filename = '"' + filename + '"';
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "UserControl\\" + controlname + "\\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "UserControl\\" + controlname + "\\");
            }
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory + "UserControl\\" + controlname + "\\";
            ProcessStartInfo processStartInfo = new(AppDomain.CurrentDomain.BaseDirectory + "UserControl\\AxImp.exe", filename)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            Process process = new()
            {
                StartInfo = processStartInfo
            };
            process.OutputDataReceived += aximp_OutputDataReceived;
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.CancelOutputRead();
            process.Dispose();
            FileInfo fileInfo = new(filename.Replace("\"", ""));
            File.Copy(fileInfo.FullName, (AppDomain.CurrentDomain.BaseDirectory + "UserControl\\" + controlname + "\\ActiveX." + clsid + "." + ver + "." + fileInfo.Name).Replace("file:///", ""), overwrite: true);
            imageList.Images[imageindex].Save(AppDomain.CurrentDomain.BaseDirectory + "UserControl\\" + controlname + "\\Image.img");
            CEditEnvironmentGlobal.msgbox.Say("成功转换控件" + controlname + ".");
        }
        catch (Exception ex)
        {
            CEditEnvironmentGlobal.msgbox.Say("转换控件" + controlname + "失败.");
            MessageBox.Show(ex.Message);
        }
    }

    private void aximp_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        try
        {
            if (e.Data != null && e.Data.Trim() != "")
            {
                CEditEnvironmentGlobal.msgbox.Say(e.Data);
            }
        }
        catch (Exception)
        {
        }
    }

    private static int ComInfoCompare(ComInfo x, ComInfo y)
    {
        return string.Compare(x.strControlName, y.strControlName);
    }

    private void listViewCom_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        foreach (ListViewItem selectedItem in listViewCom.SelectedItems)
        {
            string text = selectedItem.SubItems[3].Text.Trim();
            StartProcess(text, selectedItem.SubItems[2].Text.Trim(), selectedItem.ImageIndex, selectedItem.SubItems[1].Text, selectedItem.SubItems[4].Text.Replace(".", "_"));
        }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }

    private void listViewCom_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        listViewCom.ListViewItemSorter = new ListViewItemComparer(e.Column);
    }

    private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (listViewCom.SelectedItems.Count == 0)
        {
            return;
        }

        foreach (ListViewItem selectedItem in listViewCom.SelectedItems)
        {
            string text = selectedItem.SubItems[3].Text.Trim();
            StartProcess(text, selectedItem.SubItems[2].Text.Trim(), selectedItem.ImageIndex, selectedItem.SubItems[1].Text, selectedItem.SubItems[4].Text.Replace(".", "_"));
        }
    }

    private void listViewCom_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            contextMenuStrip1.Show(listViewCom, e.Location);
        }
    }

    private void ActiveXFinderForm_Load(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.LoadButton = new System.Windows.Forms.Button();
        this.listViewCom = new System.Windows.Forms.ListView();
        this.imageList = new System.Windows.Forms.ImageList(this.components);
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.contextMenuStrip1.SuspendLayout();
        base.SuspendLayout();
        this.LoadButton.Dock = System.Windows.Forms.DockStyle.Top;
        this.LoadButton.Location = new System.Drawing.Point(0, 0);
        this.LoadButton.Name = "LoadButton";
        this.LoadButton.Size = new System.Drawing.Size(698, 27);
        this.LoadButton.TabIndex = 0;
        this.LoadButton.Text = "Load ActiveX";
        this.LoadButton.UseVisualStyleBackColor = true;
        this.LoadButton.Visible = false;
        this.LoadButton.Click += new System.EventHandler(Load_Click);
        this.listViewCom.Dock = System.Windows.Forms.DockStyle.Fill;
        this.listViewCom.FullRowSelect = true;
        this.listViewCom.HideSelection = false;
        this.listViewCom.Location = new System.Drawing.Point(0, 27);
        this.listViewCom.MultiSelect = false;
        this.listViewCom.Name = "listViewCom";
        this.listViewCom.Size = new System.Drawing.Size(698, 317);
        this.listViewCom.TabIndex = 1;
        this.listViewCom.UseCompatibleStateImageBehavior = false;
        this.listViewCom.View = System.Windows.Forms.View.Details;
        this.listViewCom.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(listViewCom_ColumnClick);
        this.listViewCom.MouseClick += new System.Windows.Forms.MouseEventHandler(listViewCom_MouseClick);
        this.listViewCom.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(listViewCom_MouseDoubleClick);
        this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
        this.imageList.ImageSize = new System.Drawing.Size(16, 16);
        this.imageList.TransparentColor = System.Drawing.Color.Transparent;
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.导入ToolStripMenuItem });
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
        this.导入ToolStripMenuItem.Name = "导入ToolStripMenuItem";
        this.导入ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
        this.导入ToolStripMenuItem.Text = "导入";
        this.导入ToolStripMenuItem.Click += new System.EventHandler(导入ToolStripMenuItem_Click);
        base.AcceptButton = this.LoadButton;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(698, 344);
        base.Controls.Add(this.listViewCom);
        base.Controls.Add(this.LoadButton);
        base.Name = "ActiveXFinderForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "ActiveX控件加载";
        base.Load += new System.EventHandler(ActiveXFinderForm_Load);
        this.contextMenuStrip1.ResumeLayout(false);
        base.ResumeLayout(false);
    }
}
