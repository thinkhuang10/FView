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
        if (!Directory.Exists(CEditEnvironmentGlobal.HMIPath + "\\Resources"))
        {
            Directory.CreateDirectory(CEditEnvironmentGlobal.HMIPath + "\\Resources");
        }
        List<string> list = new(Directory.GetFiles(CEditEnvironmentGlobal.HMIPath + "\\Resources"));
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
                fileInfo.CopyTo(CEditEnvironmentGlobal.HMIPath + "\\Resources\\" + text);
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
                File.Delete(CEditEnvironmentGlobal.HMIPath + "\\Resources\\" + selectLvi.Tag.ToString());
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
        components = new System.ComponentModel.Container();
        groupBox1 = new System.Windows.Forms.GroupBox();
        pictureBox1 = new System.Windows.Forms.PictureBox();
        groupBox2 = new System.Windows.Forms.GroupBox();
        button2 = new System.Windows.Forms.Button();
        button1 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        button4 = new System.Windows.Forms.Button();
        listView1 = new System.Windows.Forms.ListView();
        imageList1 = new System.Windows.Forms.ImageList(components);
        groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        groupBox2.SuspendLayout();
        base.SuspendLayout();
        groupBox1.Controls.Add(pictureBox1);
        groupBox1.Location = new System.Drawing.Point(12, 14);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(308, 279);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "预览图片";
        pictureBox1.BackColor = System.Drawing.Color.White;
        pictureBox1.Location = new System.Drawing.Point(6, 20);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new System.Drawing.Size(296, 253);
        pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        groupBox2.Controls.Add(button2);
        groupBox2.Controls.Add(button1);
        groupBox2.Location = new System.Drawing.Point(16, 303);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(304, 72);
        groupBox2.TabIndex = 1;
        groupBox2.TabStop = false;
        groupBox2.Text = "资源管理";
        button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        button2.Location = new System.Drawing.Point(163, 31);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 1;
        button2.Text = "  删除(&D)";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        button1.Location = new System.Drawing.Point(55, 31);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 0;
        button1.Text = "   导入(&I)";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button3.Location = new System.Drawing.Point(622, 388);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(75, 23);
        button3.TabIndex = 3;
        button3.Text = "确定";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        button4.Location = new System.Drawing.Point(723, 388);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(75, 23);
        button4.TabIndex = 4;
        button4.Text = "取消";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(button4_Click);
        listView1.LargeImageList = imageList1;
        listView1.Location = new System.Drawing.Point(326, 14);
        listView1.MultiSelect = false;
        listView1.Name = "listView1";
        listView1.Size = new System.Drawing.Size(515, 361);
        listView1.TabIndex = 6;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.VirtualMode = true;
        listView1.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(listView1_CacheVirtualItems);
        listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(listView1_ItemSelectionChanged);
        listView1.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);
        listView1.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(listView1_SearchForVirtualItem);
        listView1.SelectedIndexChanged += new System.EventHandler(listView1_SelectedIndexChanged);
        listView1.VirtualItemsSelectionRangeChanged += new System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventHandler(listView1_VirtualItemsSelectionRangeChanged);
        listView1.DoubleClick += new System.EventHandler(listView1_DoubleClick);
        listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(listView1_KeyDown);
        imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
        imageList1.ImageSize = new System.Drawing.Size(48, 48);
        imageList1.TransparentColor = System.Drawing.Color.Transparent;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        AutoScroll = true;
        base.ClientSize = new System.Drawing.Size(853, 419);
        base.Controls.Add(listView1);
        base.Controls.Add(button4);
        base.Controls.Add(button3);
        base.Controls.Add(groupBox2);
        base.Controls.Add(groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "ImageManage";
        Text = "图片资源管理器";
        base.Load += new System.EventHandler(ImageManage_Load);
        groupBox1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        groupBox2.ResumeLayout(false);
        base.ResumeLayout(false);
    }
}
