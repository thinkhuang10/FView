using DevExpress.XtraBars;
using ShapeRuntime;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class ColorEditControl : PopupControlContainer
{
    private Color c = Color.DarkBlue;

    private Color c2 = Color.White;

    public CGlobal theglobal;

    public CShape st;

    private readonly Dictionary<RectangleF, Brush> brushDict = new();

    protected override void OnPopup()
    {
        theglobal = CEditEnvironmentGlobal.mdiparent.userCommandControl21.theglobal;
        if (theglobal.SelectedShapeList.Count != 0)
        {
            st = theglobal.SelectedShapeList[0];
        }
        if (st != null)
        {
            c = st.Color1;
            if (st.Color1 != st.Color2)
            {
                c2 = st.Color2;
            }
        }
        base.OnPopup();
        base.Size = new Size(150, 230);
    }

    public ColorEditControl()
    {
        InitializeComponent();
        base.Size = new Size(150, 230);
        base.Paint += ColorEditControl_Paint;
        base.MouseClick += ColorEditControl_MouseClick;
    }

    private void ColorEditControl_MouseClick(object sender, MouseEventArgs e)
    {
        if (st == null)
        {
            HidePopup();
            return;
        }
        List<CShape> list = new();
        List<CShape> list2 = new();
        foreach (CShape selectedShape in theglobal.SelectedShapeList)
        {
            list.Add(selectedShape.Copy());
            Region region = new();
            for (int i = 0; i < 8; i++)
            {
                region.MakeEmpty();
                RectangleF rect = new(10f + 35f * (float)(i % 4), 10f + 35f * (float)(i / 4), 25f, 25f);
                region.Union(rect);
                if (region.IsVisible(e.Location))
                {
                    selectedShape.BrushStyle = CShape._BrushStyle.线性渐变填充;
                    selectedShape.FillAngel = i * 45;
                    if (selectedShape.Color1 == selectedShape.Color2)
                    {
                        selectedShape.Color2 = c2;
                    }
                    theglobal.uc2.RefreshGraphics();
                    HidePopup();
                    break;
                }
            }
            for (int j = 0; j < 16; j++)
            {
                region.MakeEmpty();
                RectangleF rect2 = new(10f + 35f * (float)(j % 4), 80f + 35f * (float)(j / 4), 25f, 25f);
                region.Union(rect2);
                if (region.IsVisible(e.Location))
                {
                    selectedShape.BrushStyle = CShape._BrushStyle.图案填充;
                    selectedShape.HatchStyle = (HatchStyle)j;
                    if (selectedShape.Color1 == selectedShape.Color2)
                    {
                        selectedShape.Color2 = c2;
                    }
                    theglobal.uc2.RefreshGraphics();
                    HidePopup();
                    break;
                }
            }
            list2.Add(selectedShape);
        }
        theglobal.ForUndo(list2, list);
    }

    private void ColorEditControl_Paint(object sender, PaintEventArgs e)
    {
        brushDict.Clear();
        Graphics graphics = e.Graphics;
        for (int i = 0; i < 8; i++)
        {
            RectangleF rectangleF = new(10f + 35f * (float)(i % 4), 10f + 35f * (float)(i / 4), 25f, 25f);
            LinearGradientBrush linearGradientBrush = new(rectangleF, c, c2, i * 45);
            graphics.FillRectangle(linearGradientBrush, rectangleF);
            brushDict.Add(rectangleF, linearGradientBrush);
        }
        for (int j = 0; j < 16; j++)
        {
            RectangleF rectangleF2 = new(10f + 35f * (float)(j % 4), 80f + 35f * (float)(j / 4), 25f, 25f);
            HatchBrush hatchBrush = new((HatchStyle)j, c, c2);
            graphics.FillRectangle(hatchBrush, rectangleF2);
            brushDict.Add(rectangleF2, hatchBrush);
        }
        for (int k = 0; k < 6; k++)
        {
            for (int l = 0; l < 4; l++)
            {
                graphics.DrawRectangle(Pens.Black, 10f + 35f * (float)l, 10f + 35f * (float)k, 25f, 25f);
            }
        }
    }

    private void InitializeComponent()
    {
        ((System.ComponentModel.ISupportInitialize)this).BeginInit();
        base.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this).EndInit();
        base.ResumeLayout(false);
    }
}
