using CommonSnappableTypes;
using ShapeRuntime;
using System;
using System.Drawing;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class ChildForm : Form
{
    public CGlobal theglobal = new();

    public bool m_IsClosing;

    private UserShapeEditControl userControl21;

    [DllImport("user32")]
    public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        if (width < Screen.PrimaryScreen.Bounds.Size.Width && height < Screen.PrimaryScreen.Bounds.Size.Height)
        {
            base.SetBoundsCore(x, y, width, height, specified);
        }
        else
        {
            SetWindowPos((int)base.Handle, 0, x, y, width, height, 20);
        }
    }

    public ChildForm()
    {
        InitializeComponent();
        theglobal.uc2 = userControl21;
        theglobal.xmldoc = CEditEnvironmentGlobal.xmldoc;
        theglobal.ioitemroot = CEditEnvironmentGlobal.ioitemroot;
        theglobal.dhp = CEditEnvironmentGlobal.dhp;
        theglobal.dfs = CEditEnvironmentGlobal.dfs;
        theglobal.theform = this;
        theglobal.uc1.theglobal = theglobal;
        theglobal.uc2.dfs = CEditEnvironmentGlobal.dfs;
        theglobal.uc2.theglobal = theglobal;
        theglobal.df.ListAllShowCShape = theglobal.g_ListAllShowCShape;
        BitMapForIM pageImage = new()
        {
            img = theglobal.df.pageimage,
            ImgGUID = theglobal.df.pageImageNamef
        };
        theglobal.pageProp.theglobal = theglobal;
        theglobal.df.color = theglobal.pageProp.PageColor;
        theglobal.pageProp.PageImage = pageImage;
        theglobal.pageProp.PageImageLayout = theglobal.df.pageImageLayout;
        theglobal.df.location = theglobal.pageProp.PageLocation;
        theglobal.df.name = theglobal.pageProp.PageName;
        theglobal.df.size = theglobal.pageProp.PageSize;
        theglobal.df.visable = theglobal.pageProp.PageVisible;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        theglobal.pageProp.PageColor = theglobal.pageProp.PageColor;
        theglobal.pageProp.PageLocation = theglobal.pageProp.PageLocation;
        theglobal.pageProp.PageSize = theglobal.pageProp.PageSize;

        base.FormBorderStyle = FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
    }

    private void ChildForm_SizeChanged(object sender, EventArgs e)
    {
        try
        {
            theglobal.uc2.Size = new Size(Convert.ToInt32(base.Size.Width), Convert.ToInt32(base.Size.Height));
            theglobal.df.size = new Size(Convert.ToInt32(base.Size.Width), Convert.ToInt32(base.Size.Height));
            theglobal.uc2.RefreshGraphics();
        }
        catch (Exception)
        {
        }
    }

    private void ChildForm_LocationChanged(object sender, EventArgs e)
    {
        theglobal.uc2.RefreshGraphics();
    }

    private void ChildForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        m_IsClosing = true;
        while (true)
        {
            CShape[] array = userControl21.theglobal.g_ListAllShowCShape.ToArray();
            int num = 0;
            while (true)
            {
                if (num < array.Length)
                {
                    CShape cShape = array[num];
                    try
                    {
                        cShape.BeforeSaveMe();
                        cShape.AfterSaveMe();
                    }
                    catch
                    {
                        CEditEnvironmentGlobal.msgbox.Say("页面" + userControl21.theglobal.df.pageName + "图形" + cShape.Name + "序列化时出现问题.");
                        userControl21.theglobal.g_ListAllShowCShape.Remove(cShape);
                        break;
                    }
                    num++;
                    continue;
                }
                return;
            }
        }
    }

    private void InitializeComponent()
    {
        userControl21 = new HMIEditEnvironment.UserShapeEditControl();
        base.SuspendLayout();
        userControl21.AllowDrop = true;
        userControl21.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
        userControl21.BackColor = System.Drawing.Color.Transparent;
        userControl21.Dock = System.Windows.Forms.DockStyle.Fill;
        userControl21.ForeColor = System.Drawing.SystemColors.Control;
        userControl21.Location = new System.Drawing.Point(0, 0);
        userControl21.Name = "userControl21";
        userControl21.Size = new System.Drawing.Size(800, 600);
        userControl21.TabIndex = 0;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
        base.ClientSize = new System.Drawing.Size(800, 600);
        base.Controls.Add(userControl21);
        DoubleBuffered = true;
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        base.KeyPreview = true;
        base.Name = "ChildForm";
        Text = "图形编辑器";
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ChildForm_FormClosing);
        base.Load += new System.EventHandler(Form1_Load);
        base.LocationChanged += new System.EventHandler(ChildForm_LocationChanged);
        base.SizeChanged += new System.EventHandler(ChildForm_SizeChanged);
        base.ResumeLayout(false);
    }
}
