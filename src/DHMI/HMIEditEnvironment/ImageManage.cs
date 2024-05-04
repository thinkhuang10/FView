using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CommonSnappableTypes;
using HMIEditEnvironment.Properties;

namespace HMIEditEnvironment;

public class ImageManage : Form
{
    public Image OutImage;

    private List<ListViewItem> myCache;

    private ListViewItem selectLvi;

    private IContainer components;

    private GroupBox groupBox1;

    private GroupBox groupBox2;

    private Button button2;

    private Button button1;

    private Button button3;

    private Button button4;

    private ListView listView1;

    private ImageList imageList1;

    private PictureBox pictureBox1;

    public ImageManage()
    {
        InitializeComponent();
    }

    private void ImageManage_Load(object sender, EventArgs e)
    {
        if (!Directory.Exists(CEditEnvironmentGlobal.path + "\\Resources"))
        {
            Directory.CreateDirectory(CEditEnvironmentGlobal.path + "\\Resources");
        }
        List<string> list = new(Directory.GetFiles(CEditEnvironmentGlobal.path + "\\Resources"));
        List<string> list2 = new()
        {
            ".GIF",
            ".JPG",
            ".BMP",
            ".PNG"
        };
        List<string> list3 = list2;
        string[] array = list.ToArray();
        foreach (string text in array)
        {
            FileInfo fileInfo = new(text);
            if (!list3.Contains(fileInfo.Extension.ToUpper()))
            {
                list.Remove(text);
            }
        }
        myCache = new List<ListViewItem>();
        ListViewItem listViewItem = new()
        {
            Tag = "null"
        };
        myCache.Add(listViewItem);
        for (int j = 0; j < list.Count; j++)
        {
            string tag = list[j].Substring(list[j].LastIndexOf("\\") + 1);
            ListViewItem listViewItem2 = new()
            {
                Tag = tag
            };
            myCache.Add(listViewItem2);
        }
        listView1.VirtualListSize = list.Count + 1;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        try
        {
            imageImport();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void imageImport()
    {
        OpenFileDialog openFileDialog = new()
        {
            Multiselect = true,
            Filter = "Image Files (*.jpg)(*.gif)(*.bmp)(*.png)|*.jpg;*.gif;*.bmp;*.png",
            FileName = null
        };
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            string[] fileNames = openFileDialog.FileNames;
            foreach (string fileName in fileNames)
            {
                FileInfo fileInfo = new(fileName);
                string text = Guid.NewGuid().ToString() + fileInfo.Extension;
                fileInfo.CopyTo(CEditEnvironmentGlobal.path + "\\Resources\\" + text);
                ListViewItem listViewItem = new()
                {
                    Tag = text
                };
                myCache.Add(listViewItem);
                selectLvi = listViewItem;
                pictureBox1.Image = DHMIImageManage.LoadImage(text);
                listView1.VirtualListSize++;
            }
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (selectLvi != null && selectLvi.Tag != null && MessageBox.Show("确定删除吗？", "确认删除？", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                File.Delete(CEditEnvironmentGlobal.path + "\\Resources\\" + selectLvi.Tag.ToString());
                myCache.Remove(selectLvi);
                listView1.VirtualListSize--;
                selectLvi = null;
                pictureBox1.Image = null;
                listView1.Items[0].Selected = true;
            }
        }
        catch
        {
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        if (selectLvi != null)
        {
            OutImage = DHMIImageManage.LoadImage(selectLvi.Tag.ToString());
            base.DialogResult = DialogResult.OK;
            Close();
        }
        else
        {
            MessageBox.Show("请选择图片资源");
        }
    }

    private void pictureBox1_DoubleClick(object sender, EventArgs e)
    {
    }

    private void listView1_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
    {
    }

    private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
    {
        try
        {
            if (myCache != null)
            {
                e.Item = myCache[e.ItemIndex];
            }
        }
        catch
        {
        }
    }

    private void listView1_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
    {
    }

    public bool ThumbnailCallback()
    {
        return false;
    }

    private void listView1_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
    {
        try
        {
            Image.GetThumbnailImageAbort callback = ThumbnailCallback;
            for (int i = e.StartIndex; i <= e.EndIndex; i++)
            {
                string text = (string)myCache[i].Tag;
                if (text == "null" && !imageList1.Images.ContainsKey("null"))
                {
                    imageList1.Images.Add("null", Resources.nullImage);
                }
                if (!imageList1.Images.ContainsKey(text))
                {
                    imageList1.Images.Add(text, DHMIImageManage.LoadImage(text).GetThumbnailImage(48, 48, callback, IntPtr.Zero));
                }
                myCache[i].ImageIndex = imageList1.Images.IndexOfKey(text);
            }
        }
        catch
        {
        }
    }

    private void listView1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
        selectLvi = e.Item;
        pictureBox1.Image = DHMIImageManage.LoadImage(selectLvi.Tag.ToString());
    }

    private void listView1_DoubleClick(object sender, EventArgs e)
    {
        if (selectLvi != null)
        {
            OutImage = DHMIImageManage.LoadImage(selectLvi.Tag.ToString());
            base.DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void listView1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Delete)
        {
            button2_Click(null, null);
        }
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
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.button2 = new System.Windows.Forms.Button();
        this.button1 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.button4 = new System.Windows.Forms.Button();
        this.listView1 = new System.Windows.Forms.ListView();
        this.imageList1 = new System.Windows.Forms.ImageList(this.components);
        this.groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
        this.groupBox2.SuspendLayout();
        base.SuspendLayout();
        this.groupBox1.Controls.Add(this.pictureBox1);
        this.groupBox1.Location = new System.Drawing.Point(12, 14);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(308, 279);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "预览图片";
        this.pictureBox1.BackColor = System.Drawing.Color.White;
        this.pictureBox1.Location = new System.Drawing.Point(6, 20);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(296, 253);
        this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.pictureBox1.TabIndex = 0;
        this.pictureBox1.TabStop = false;
        this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.groupBox2.Controls.Add(this.button2);
        this.groupBox2.Controls.Add(this.button1);
        this.groupBox2.Location = new System.Drawing.Point(16, 303);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(304, 72);
        this.groupBox2.TabIndex = 1;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "资源管理";
        this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.button2.Location = new System.Drawing.Point(163, 31);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 1;
        this.button2.Text = "  删除(&D)";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.button1.Location = new System.Drawing.Point(55, 31);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 0;
        this.button1.Text = "   导入(&I)";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button3.Location = new System.Drawing.Point(622, 388);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(75, 23);
        this.button3.TabIndex = 3;
        this.button3.Text = "确定";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.button4.Location = new System.Drawing.Point(723, 388);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(75, 23);
        this.button4.TabIndex = 4;
        this.button4.Text = "取消";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(button4_Click);
        this.listView1.LargeImageList = this.imageList1;
        this.listView1.Location = new System.Drawing.Point(326, 14);
        this.listView1.MultiSelect = false;
        this.listView1.Name = "listView1";
        this.listView1.Size = new System.Drawing.Size(515, 361);
        this.listView1.TabIndex = 6;
        this.listView1.UseCompatibleStateImageBehavior = false;
        this.listView1.VirtualMode = true;
        this.listView1.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(listView1_CacheVirtualItems);
        this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(listView1_ItemSelectionChanged);
        this.listView1.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);
        this.listView1.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(listView1_SearchForVirtualItem);
        this.listView1.SelectedIndexChanged += new System.EventHandler(listView1_SelectedIndexChanged);
        this.listView1.VirtualItemsSelectionRangeChanged += new System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventHandler(listView1_VirtualItemsSelectionRangeChanged);
        this.listView1.DoubleClick += new System.EventHandler(listView1_DoubleClick);
        this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(listView1_KeyDown);
        this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
        this.imageList1.ImageSize = new System.Drawing.Size(48, 48);
        this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.AutoScroll = true;
        base.ClientSize = new System.Drawing.Size(853, 419);
        base.Controls.Add(this.listView1);
        base.Controls.Add(this.button4);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "ImageManage";
        this.Text = "图片资源管理器";
        base.Load += new System.EventHandler(ImageManage_Load);
        this.groupBox1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
        this.groupBox2.ResumeLayout(false);
        base.ResumeLayout(false);
    }
}
